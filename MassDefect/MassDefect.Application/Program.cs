namespace MassDefect.Application
{
    using MassDefect.Data;
    using MassDefect.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    class Program
    {
        private const string SolarSystemsPath = "../../../datasets/solar-systems.json";

        private const string StarsPath = "../../../datasets/stars.json";

        private const string PlanetsPath = "../../../datasets/planets.json";

        private const string PersonsPath = "../../../datasets/persons.json";

        private const string AnomaliesPath = "../../../datasets/anomalies.json";

        private const string AnomalyVictimsPath = "../../../datasets/anomaly-victims.json";

        static void Main(string[] args)
        {
            ImportSolarSystems();
            ImportStars();
            ImportPlanets();
            ImportPersons();
            ImportAnomalies();
            ImportAnomalyVictims();
        }

        private static void ImportAnomalyVictims()
        {
            MassDefectContext context = new MassDefectContext();
            var json = File.ReadAllText(AnomalyVictimsPath);
            var anomalyVictims = JsonConvert.DeserializeObject<IEnumerable<AnomalyVictimsDTO>>(json);

            foreach (var anomalyVictim in anomalyVictims)
            {
                if ((anomalyVictim.Id.ToString() == null) || 
                    (anomalyVictim.Person == null))
                {
                    Console.WriteLine("Error: Invalid data.");
                    continue;
                }

                if ((context.Persons.Where(n => n.Name == anomalyVictim.Person).Count() == 0) ||
                    (context.Anomalies.Where(i => i.Id == anomalyVictim.Id).Count() == 0))
                {
                    Console.WriteLine("Error: Invalid data.");
                    continue;
                }

                var anomalyEntity = GetAnomalyById(anomalyVictim.Id, context);
                var personEntity = GetPersonByName(anomalyVictim.Person, context);

                anomalyEntity.AnomalyVictims.Add(personEntity);
                context.SaveChanges();
            }
        }

        private static void ImportAnomalies()
        {
            MassDefectContext context = new MassDefectContext();
            var json = File.ReadAllText(AnomaliesPath);
            var anomalies = JsonConvert.DeserializeObject<IEnumerable<AnomalyDTO>>(json);
            
            foreach (var anomaly in anomalies)
            {
                if ((context.Planets.Where(n => n.Name == anomaly.OriginPlanet).Count() == 0) ||
                    (context.Planets.Where(n => n.Name == anomaly.TeleportPlanet).Count() == 0))
                {
                    Console.WriteLine("Error: Invalid data.");
                    continue;
                }
            
                var anomalyEntity = new Anomaly()
                {
                    OriginPlanet = GetPlanetByName(anomaly.OriginPlanet, context),

                    TeleportPlanet = GetPlanetByName(anomaly.TeleportPlanet, context)
                };
            
                context.Anomalies.Add(anomalyEntity);
                context.SaveChanges();

                Console.WriteLine($"Successfully imported anomaly.");
            }
        }

        private static void ImportPersons()
        {
            MassDefectContext context = new MassDefectContext();
            var json = File.ReadAllText(PersonsPath);
            var persons = JsonConvert.DeserializeObject<IEnumerable<PersonDTO>>(json);
            
            foreach (var person in persons)
            {
                if ((person.Name == null) || (context.Planets.Where(n => n.Name == person.HomePlanet).Count() == 0))
                {
                    Console.WriteLine("Error: Invalid data.");
                    continue;
                }
            
                var PersonEntity = new Person()
                {
                    Name = person.Name,
            
                    HomePlanet = GetPlanetByName(person.HomePlanet, context)
                };
            
                context.Persons.Add(PersonEntity);
                context.SaveChanges();

                Console.WriteLine($"Successfully imported Person {PersonEntity.Name}.");
            }
        }

        private static void ImportPlanets()
        {
            MassDefectContext context = new MassDefectContext();
            var json = File.ReadAllText(PlanetsPath);
            var planets = JsonConvert.DeserializeObject<IEnumerable<PlanetDTO>>(json);

            foreach (var planet in planets)
            {
                if ((planet.Name == null) || (context.Stars.Where(n => n.Name == planet.Sun).Count() == 0) || (context.SolarSystems.Where(n => n.Name == planet.SolarSystem).Count() == 0))
                {
                    Console.WriteLine("Error: Invalid data.");
                    continue;
                }

                var PlanetEntity = new Planet()
                {
                    Name = planet.Name,

                    Sun = GetSunByName(planet.Sun, context),

                    SolarSystem = GetSolarSystemByName(planet.SolarSystem, context)
                };

                context.Planets.Add(PlanetEntity);
                context.SaveChanges();

                Console.WriteLine($"Successfully imported Planet {PlanetEntity.Name}.");
            }
        }

        private static void ImportStars()
        {
            MassDefectContext context = new MassDefectContext();
            var json = File.ReadAllText(StarsPath);
            var stars = JsonConvert.DeserializeObject<IEnumerable<StarDTO>>(json);

            foreach (var star in stars)
            {
                if ((star.Name == null) || (star.SolarSystem == null) || (context.SolarSystems.Where(n => n.Name == star.SolarSystem).Count() == 0))
                {
                    Console.WriteLine("Error: Invalid data.");
                    continue;
                }

                var StarEntity = new Star()
                {
                    Name = star.Name,

                    SolarSystem = GetSolarSystemByName(star.SolarSystem, context)
                };

                context.Stars.Add(StarEntity);
                context.SaveChanges();

                Console.WriteLine($"Successfully imported Star {StarEntity.Name}.");
            }
        }

        private static void ImportSolarSystems()
        {
            MassDefectContext context = new MassDefectContext();
            var json = File.ReadAllText(SolarSystemsPath);
            var solarSystems = JsonConvert.DeserializeObject<IEnumerable<SolarSystemDTO>>(json);

            foreach (var solarSystem in solarSystems)
            {
                if (solarSystem.Name == null)
                {
                    Console.WriteLine("Error: Invalid data.");
                    continue;
                }

                var solarSystemEntity = new SolarSystem()
                {
                    Name = solarSystem.Name
                };

                context.SolarSystems.Add(solarSystemEntity);
                context.SaveChanges();

                Console.WriteLine($"Successfully imported SolarSystem {solarSystemEntity.Name}.");
            }
        }

        public static SolarSystem GetSolarSystemByName(string solarSystemName, MassDefectContext context)
        {
            var solarSystem = context.SolarSystems.Where(n => n.Name == solarSystemName).SingleOrDefault();

            return solarSystem;
        }

        public static Star GetSunByName(string sunName, MassDefectContext context)
        {
            var star = context.Stars.Where(n => n.Name == sunName).SingleOrDefault();

            return star;
        }

        public static Planet GetPlanetByName(string planetName, MassDefectContext context)
        {
            var planet = context.Planets.Where(n => n.Name == planetName).SingleOrDefault();

            return planet;
        }

        public static Anomaly GetAnomalyById(int anomalyId, MassDefectContext context)
        {
            var anomaly = context.Anomalies.Where(i => i.Id == anomalyId).SingleOrDefault();

            return anomaly;
        }

        public static Person GetPersonByName(string personName, MassDefectContext context)
        {
            var person = context.Persons.Where(n => n.Name == personName).SingleOrDefault();

            return person;
        }
    }
}