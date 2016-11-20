namespace MassDefect.ApplicationXML
{
    using Data;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using System.Xml.XPath;

    class Program
    {
        private const string NewAnomaliesPath = "../../../datasets/new-anomalies.xml";

        static void Main(string[] args)
        {
            var xml = XDocument.Load(NewAnomaliesPath);
            var anomalies = xml.XPathSelectElements("anomalies/anomaly");

            var context = new MassDefectContext();

            foreach (var anomaly in anomalies)
            {
                try
                {
                    ImportAnomalyAndVictims(anomaly, context);
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        private static void ImportAnomalyAndVictims(XElement anomalyNode, MassDefectContext context)
        {
            var originPlanetName = anomalyNode.Attribute("origin-planet");
            var teleportPlanetName = anomalyNode.Attribute("teleport-planet");

            if ((context.Planets.Where(n => n.Name == originPlanetName.Value).Count() > 0) &&
                (context.Planets.Where(n => n.Name == teleportPlanetName.Value).Count() > 0) &&
                (originPlanetName.Value != null) &&
                (teleportPlanetName.Value != null))
            {
                var anomalyEntity = new Anomaly()
                {
                    OriginPlanet = GetPlanetByName(originPlanetName.Value, context),
                    TeleportPlanet = GetPlanetByName(teleportPlanetName.Value, context)
                };

                context.Anomalies.Add(anomalyEntity);

                var victims = anomalyNode.XPathSelectElements("victims/victim");

                foreach (var victim in victims)
                {
                    ImportVictim(victim, context, anomalyEntity);
                }

                context.SaveChanges();

                Console.WriteLine("Successfully imported anomaly and victims.");
            }
            else
            {
                Console.WriteLine("Error: Invalid data.");
            }
        }

        private static void ImportVictim(XElement victimNode, MassDefectContext context, Anomaly anomaly)
        {
            var name = victimNode.Attribute("name");

            if ((context.Persons.Where(n => n.Name == name.Value).Count() > 0) &&
                    (name.Value != null))
            {
                var personEntity = GetPersonByName(name.Value, context);

                anomaly.AnomalyVictims.Add(personEntity);
            }
            else
            {
                Console.WriteLine("Error: Invalid data.");
            }
        }

        private static Person GetPersonByName(string value, MassDefectContext context)
        {
            var person = context.Persons.Where(n => n.Name == value).SingleOrDefault();

            return person;
        }

        private static Planet GetPlanetByName(string value, MassDefectContext context)
        {
            var planet = context.Planets.Where(n => n.Name == value).SingleOrDefault();

            return planet;
        }
    }
}