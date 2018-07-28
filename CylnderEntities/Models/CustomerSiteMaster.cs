using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CylnderEntities
{
    [MetadataType(typeof(CustomerSiteMaster))]
    public partial class usp_CustomerSiteMasterGetbyID_Result
    {

    }

    public class CustomerSiteMaster
    {
        public int CustomerIDSiteID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select the Customer")]
        public Nullable<int> CustomerID { get; set; }
        public string CustomerName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Customer SiteName")]
        
        [MinLength(5, ErrorMessage = "Customer SiteName cannot be less than 5 characters")]
        [MaxLength(200, ErrorMessage = "Site Name cannot be greater than 200 characters")]
        public string SiteName { get; set; }
        [MaxLength(500, ErrorMessage = "Customer Site address cannot be greater than 500 characters")]
        public string SiteAddress { get; set; }
        [MaxLength(100, ErrorMessage = "Site ContactPersonName cannot be greater than 100 characters")]

        public string ContactPersonName { get; set; }
        [MaxLength(20, ErrorMessage = "Site ContactNumber cannot be greater than 20 characters")]
        public string ContactNumber { get; set; }
        [DataType(DataType.EmailAddress)]
        [MaxLength(100, ErrorMessage = "Site EmailAddress cannot be greater than 100 characters")]
        public string Email { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<int> CreatedByID { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<int> UpdateByID { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select the Status")]
        public Nullable<bool> status { get; set; }
    }

    [MetadataType(typeof(CustomerSiteMasterGetbyIDDetails))]
    public partial class usp_CustomerSiteMasterGetbyIDDetails_Result
    {
        
    }
    public partial class CustomerSiteMasterGetbyIDDetails
    {
        public int CustomerIDSiteID { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public string CustomerName { get; set; }
        [MaxLength(200, ErrorMessage = "Site Name cannot be greater than 200 characters")]
        public string SiteName { get; set; }
        [MaxLength(500, ErrorMessage = "Customer Site address cannot be greater than 500 characters")]
        public string SiteAddress { get; set; }
        [MaxLength(100, ErrorMessage = "Site ContactPersonName cannot be greater than 100 characters")]

        public string ContactPersonName { get; set; }
        [MaxLength(20, ErrorMessage = "Site ContactNumber cannot be greater than 20 characters")]
        public string ContactNumber { get; set; }
        [DataType(DataType.EmailAddress)]
        [MaxLength(100, ErrorMessage = "Site EmailAddress cannot be greater than 100 characters")]
        public string Email { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<int> CreatedByID { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<int> UpdateByID { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<bool> status { get; set; }
    }
}