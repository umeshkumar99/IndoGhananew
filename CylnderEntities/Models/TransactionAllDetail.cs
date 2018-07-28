using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CylnderEntities
{

    public class TransactionAllDetail
    {





        public string TransactionNumber { get; set; }


        public int TransactionMode{ get; set; }


        public int SourceCylinderID{ get; set; }


        public int flgSourceBarCodeExists{ get; set; }


        public string SourceBarCodeNumber{ get; set; }


        public string SourceCylinderNumber{ get; set; }


        public int SourceCylinderSize{ get; set; }


        public int TargetCylinderID{ get; set; }



        public int flgTargetBarCodeExists{ get; set; }


        public string TargetBarCodeNumber{ get; set; }



        public string TargetCylinderNumber{ get; set; }


        public int TargetCylinderSize{ get; set; }


        public int Sstat{ get; set; }





        public int CustomerID{ get; set; }


        public int CurrentCustomerBranchID{ get; set; }


        public string CustomerName{ get; set; }


        public string VendorName{ get; set; }


        public string SizeUOM{ get; set; }



        public string PresentState{ get; set; }



        public int PresentStateID{ get; set; }



        public int LocationID;





        public string VanBatchNumber;



        public string TransactionDateTime;




        public int CompanyID;


        public int BranchID;

        public int UserID;
        public string GasInUse;

    }
}