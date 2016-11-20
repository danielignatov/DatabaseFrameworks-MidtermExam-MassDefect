namespace MassDefect.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Planet
    {
        #region Fields
        private ICollection<Person> persons;
        #endregion

        #region Constructor
        public Planet()
        {
            this.persons = new HashSet<Person>();
        }
        #endregion

        #region Properties
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Star Sun { get; set; }

        public SolarSystem SolarSystem { get; set; }

        public virtual ICollection<Person> Persons
        {
            get
            {
                return this.persons;
            }
            set
            {
                this.persons = value;
            }
        }
        #endregion
    }
}