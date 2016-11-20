namespace MassDefect.Data
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class MassDefectContext : DbContext
    {
        // Your context has been configured to use a 'MassDefectContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'MassDefect.Data.MassDefectContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'MassDefectContext' 
        // connection string in the application configuration file.
        public MassDefectContext()
            : base("name=MassDefectContext")
        {
        }

        public virtual IDbSet<SolarSystem> SolarSystems { get; set; }

        public virtual IDbSet<Star> Stars { get; set; }

        public virtual IDbSet<Planet> Planets { get; set; }

        public virtual IDbSet<Person> Persons { get; set; }

        public virtual IDbSet<Anomaly> Anomalies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anomaly>()
             .HasMany<Person>(s => s.AnomalyVictims)
             .WithMany(c => c.Anomalies)
             .Map(cs =>
             {
                 cs.MapLeftKey("AnomalyId");
                 cs.MapRightKey("PersonId");
                 cs.ToTable("AnomalyVictims");
             });
        }
    }
}