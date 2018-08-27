using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CylnderEntities
{
    public class VehicleUserMapping
    {
        [Required]
        public int VehicleID { get; set; }
        [Required]
        public int UserD { get; set; }
        public string VehicleName { get; set; }
        public string UserName { get; set; }
    }
}