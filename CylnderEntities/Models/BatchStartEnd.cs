using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CylnderEntities
{
    public class BatchStartEnd
    {
        public string VanBatchNumber { get; set; }
        public string BatchStartDateTime { get; set; }
        public string BatchEndDatetime { get; set; }
        public string ForDate { get; set; }
        public Nullable<int> Sstat { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> BranchID { get; set; }
        public Nullable<int> UserID { get; set; }



    }
}