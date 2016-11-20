namespace MassDefect.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Anomaly
    {
        #region Fields
        private ICollection<Person> anomalyVictims;
        #endregion

        #region Constructor
        public Anomaly()
        {
            this.anomalyVictims = new HashSet<Person>();
        }
        #endregion

        #region Properties
        [Key]
        public int Id { get; set; }

        public Planet OriginPlanet { get; set; }
        
        public Planet TeleportPlanet { get; set; }

        public virtual ICollection<Person> AnomalyVictims
        {
            get
            {
                return this.anomalyVictims;
            }
            set
            {
                this.anomalyVictims = value;
            }
        }
        #endregion
    }
}
