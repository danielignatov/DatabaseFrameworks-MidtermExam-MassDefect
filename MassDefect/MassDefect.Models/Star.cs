namespace MassDefect.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Star
    {
        #region Constructor
        public Star()
        {

        }
        #endregion

        #region Properties
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        public SolarSystem SolarSystem { get; set; }
        #endregion
    }
}