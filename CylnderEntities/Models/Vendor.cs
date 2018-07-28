using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace CylnderEntities
{
    [MetadataType(typeof(Vendor))]
    public partial class usp_VendorMasterGetbyID_Result
    {

    }
    [MetadataType(typeof(VendorBranch))]
    public partial class usp_VendorBranchMasterGetbyID_Result
    {
    }
    [MetadataType(typeof(Vendor))]
    public partial class usp_VendorMasterGet_Result
    {
    }
        public class Vendor
    {
        public int VendorID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Owner Name")]
        [Display(Name = "Owner Name")]
        [MinLength(5, ErrorMessage = "Owner Name cannot be less than 5 characters")]
        [MaxLength(200, ErrorMessage = "Owner Name cannot be greater than 200 characters")]
        public string VendorName { get; set; }
        [MaxLength(500, ErrorMessage = "Owner address cannot be greater than 500 characters")]
        [Display(Name = "Owner Address")]
        public string VendorAddress { get; set; }
        [MaxLength(100, ErrorMessage = "Name cannot be greater than 100 characters")]
        public string ContactPersonName { get; set; }
        [MaxLength(20, ErrorMessage = " Contact number cannot be greater than 20 characters")]
        public string ContactNumber { get; set; }
        [DataType(DataType.EmailAddress)]
        [MaxLength(100, ErrorMessage = "Email Address cannot be greater than 100 characters")]
        public string EmailID { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<int> CreatedByID { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<int> UpdatedbyID { get; set; }
        public string Updatedby { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select the Status")]
        public Nullable<bool> status { get; set; }
    }
    public class VendorBranch
    {

        public int VendorID { get; set; }
        [Display(Name = "Owner Name")]
        public string VendorName { get; set; }
        public int VendorBranchID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the branch Name")]
        public string VendorBranchName { get; set; }

        [Display(Name ="Owner Branch Address")]
        [MaxLength(500, ErrorMessage = "Branch address cannot be greater than 500 characters")]
        public string VendorBranchAddress { get; set; }

        [MaxLength(100, ErrorMessage = "Name cannot be greater than 100 characters")]
        public string ContactPersonName { get; set; }
        [MaxLength(20, ErrorMessage = " Contact number cannot be greater than 20 characters")]

        public string ContactNumber { get; set; }
        [DataType(DataType.EmailAddress)]
        [MaxLength(100, ErrorMessage = "EmailAddress cannot be greater than 100 characters")]
        public string EmailID { get; set; }
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