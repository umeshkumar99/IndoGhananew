using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CylnderEntities
{
    public class AlertMessageTrackModel
    {

         
    public int TransactionMode { get; set; }


        public string CylinderNumber { get; set; }

   
    public string DateTime { get; set; }



        public int CompanyID { get; set; }

        public int BranchID { get; set; }

        public int UserID;

  
    public int CurrentCustomerBranchID { get; set; }

        public string CustomerName { get; set; }


        public string MessageDescription { get; set; }


        public string MessageLocation { get; set; }





    }
}