using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;



namespace CylnderEntities
{
    public class CylinderRecieveDeliver
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:mm-dd-yyyy}", ApplyFormatInEditMode = false)]
        [Required]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:mm-dd-yyyy}", ApplyFormatInEditMode = false)]
        [Required]
        public DateTime EndDate { get; set; }
    }
}