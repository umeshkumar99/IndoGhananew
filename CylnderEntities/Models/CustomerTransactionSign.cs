using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace CylnderEntities
{
    public class CustomerTransactionSign
    {

     public string TransactionNumber { get; set; }
 
   
    public int CustomerID { get; set; }
    
   
    public int CurrentCustomerBranchID { get; set; }
   
   
    public int IsSatisfied { get; set; }
    
   
    public string CustomerSignature { get; set; }
   
   
    public int logid { get; set; }
    
   
    public string ForDate { get; set; }
   
   
    public string CurrentDateTime { get; set; }
   
   
    public int CompanyID { get; set; }
   
   
    public int BranchID { get; set; }
   
   
    public int UserID { get; set; }
    
   
    public string Sstat { get; set; }

    public string Remarks { get; set; }

    }
}