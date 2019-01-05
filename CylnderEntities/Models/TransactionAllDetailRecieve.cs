using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CylnderEntities
{

    public class TransactionAllDetailCylinderRecieve
    {





        public string TransactionNumber { get; set; }


        public int TransactionMode{ get; set; }


        public int SourceCylinderID{ get; set; }


        public int flgSourceBarCodeExists{ get; set; }


        public string SourceBarCodeNumber{ get; set; }


        public string SourceCylinderNumber{ get; set; }


        public double SourceCylinderSize{ get; set; }


        public int TargetCylinderID{ get; set; }



        public int flgTargetBarCodeExists{ get; set; }


        public string TargetBarCodeNumber{ get; set; }



        public string TargetCylinderNumber{ get; set; }


        public double TargetCylinderSize{ get; set; }


        public int Sstat{ get; set; }





        public int CustomerID{ get; set; }


        public int CurrentCustomerBranchID{ get; set; }


        public string CustomerName{ get; set; }


        public string VendorName{ get; set; }


        public string SizeUOM{ get; set; }



        public string PresentState{ get; set; }



        public int PresentStateID{ get; set; }



        public int LocationID { get; set; }





        public string VanBatchNumber { get; set; }



        public string TransactionDateTime { get; set; }




        public int CompanyID { get; set; }


        public int BranchID { get; set; }

        public int UserID { get; set; }
        public string GasInUse { get; set; }

    }
}