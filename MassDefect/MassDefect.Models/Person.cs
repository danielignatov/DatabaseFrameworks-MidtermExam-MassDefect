namespace MassDefect.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Person
    {
        #region Fields
        private ICollection<Anomaly> anomalies;
        #endregion

        #region Constructor
        public Person()
        {
            this.anomalies = new HashSet<Anomaly>();
        }
        #endregion

        #region Properties
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        public Planet HomePlanet { get; set; }

        public ICollection<Anomaly> Anomalies
        {
            get
            {
                return this.anomalies;
            }
            set
            {
                this.anomalies = value;
            }
        }
        #endregion
    }
}
