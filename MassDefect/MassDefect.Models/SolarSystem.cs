namespace MassDefect.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SolarSystem
    {
        #region Constructor
        public SolarSystem()
        {

        }
        #endregion

        #region Properties
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        #endregion
    }
}