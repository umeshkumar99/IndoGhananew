using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CylnderEntities
{
    [MetadataType(typeof(Customer))]
    public partial class usp_CustomerMasterGetbyID_Result
    {
     
    }
    public class Customer
    {
        public int CustomerID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Customer Name")]
        [MaxLength(50, ErrorMessage = "Customer Name cannot be greater than 50 characters")]
        [MinLength(5, ErrorMessage = "Customer Name cannot be less than 5 characters")]
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string ContactPersonName { get; set; }
        public string ContactNumber { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select the wether Customer is Owner also")]
        public Nullable<bool> IsOwner { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<int> CreatedByID { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<int> UpdatedbyID { get; set; }
        public string Updatedby { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select the Status")]
        public Nullable<bool> status { get; set; }
    }
    }