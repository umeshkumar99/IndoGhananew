using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CylnderEntities
{
    public class DayStartDayEndTable
    {

        public string DayStartDateTime { get; set; }

        public string DayEndDateTime { get; set; }

        public string ForDate { get; set; }

        public string Sstat { get; set; }

        public int CompanyID { get; set; }


        public int BranchID { get; set; }

        public int UserID { get; set; }
        
        public int logid { get; set; }
        public string IMEI { get; set; }
    }
}