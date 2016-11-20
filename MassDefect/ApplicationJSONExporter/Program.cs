namespace ApplicationJSONExporter
{
    using MassDefect.Data;
    using System;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            var context = new MassDefectContext();

            ExportPlanetsWhichAreNotAnomalyOrigins(context);

            ExportPeopleWhichHaveNotBeenVictims(context);

            ExportTopAnomaly(context);
        }

        private static void ExportTopAnomaly(MassDefectContext context)
        {
            throw new NotImplementedException();
        }

        private static void ExportPeopleWhichHaveNotBeenVictims(MassDefectContext context)
        {
            throw new NotImplementedException();
        }

        private static void ExportPlanetsWhichAreNotAnomalyOrigins(MassDefectContext context)
        {
            //var exportedPlanets = context.Planets
            //    .Where(planet => !planet.OriginPlanet.Any())
            //    .Select(planet => new
            //    {
            //        name = planet.Name
            //    });
        }
    }
}