using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CylnderEntities
{
    public class CylinderInVan
    {

     public Nullable<int> vehicleID { get; set;}
    
    public Nullable<double> initialSize { get; set; }

    public Nullable<double> remainingsizeforRefill { get; set; }

        public string vanBatchNumber { get; set; }

        public string VendorName { get; set; }

        public string cylinderLoadingDateTime { get; set; }

        public Nullable<int> cylinderID{ get; set;}
    
   
    public string cylinderNumber{ get; set;}
    
   
    public Nullable<int> cylinderVendorID{ get; set;}
    
   
    public Nullable<int> cylinderBranchID{ get; set;}
    
   
    public Nullable<int> sstat{ get; set;}
    
   
    public Nullable<int> transactionPoint{ get; set;}
    
   
    public Nullable<int> LocationID{ get; set;}
    
   
    public Nullable<int> TransType{ get; set;}
    
   
    public Nullable<int> CylinderStatus{ get; set;}
    
   
    public Nullable<int> OwnerId{ get; set;}
    
   
    public Nullable<int> vendorBranchID{ get; set;}



    
   
    public Nullable<int> CompanyID{ get; set;}
    
   
    public Nullable<int> BranchID{ get; set;}
    
   
    public Nullable<int> UserID{ get; set;}


    }
}