using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CylnderEntities
{

    [MetadataType(typeof(CylinderMasterMetaData))]
    public partial class usp_CylinderMasterGetByID_Result
    { }

    public class CylinderMasterMetaData
        {
        public int CylinderID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Cylinder Number")]
        [MaxLength(50, ErrorMessage = "Cylinder No. cannot be greater than 50 characters")]
        [MinLength(5, ErrorMessage = "Cylinder No. cannot be less than 5 characters")]
        public string CylindeNumber { get; set; }
        public string Barcode { get; set; }
        public Nullable<int> ManufacturerID { get; set; }
        public string ManufacturerName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:mm-dd-yyyy}", ApplyFormatInEditMode =false)]
        public string PurchaseDate { get; set; }
        public Nullable<int> InitialGasID { get; set; }
        public string InitialGas { get; set; }
        public Nullable<int> WLCapacity { get; set; }
        public Nullable<int> WLCapacityUOMID { get; set; }
        public string WLCapacityUOM { get; set; }
        public Nullable<int> WorkingPressure { get; set; }
        public Nullable<int> WorkingPressureUOMID { get; set; }
        public string WorkingPressureUOM { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:mm-dd-yyyy}", ApplyFormatInEditMode = false)]
        public Nullable<System.DateTime> TestDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:mm-dd-yyyy}", ApplyFormatInEditMode = false)]
        public Nullable<System.DateTime> NextTestDate { get; set; }
        public Nullable<int> ValveModelID { get; set; }
        public string ValveModel { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select the Present State")]
        public Nullable<int> PresentStateID { get; set; }
        public string PresentState { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select the Present State")]
        public Nullable<int> GasInUseID { get; set; }
        public string GasInUse { get; set; }    
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select the Vendor Name")]
        [Display(Name = "Owner Name")]
        public Nullable<int> VendorID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select the Vendor Branch")]
        public Nullable<int> VendorBranchID { get; set; }
        [Display(Name ="Owner Name")]
        public string VendorName { get; set; }
        public string VendorBranchName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the size")]
        public Nullable<double> Size { get; set; }
        public Nullable<double> ActualSize { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select the UOM")]
        public Nullable<int> SizeUOMID { get; set; }
        public string SizeUOM { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<int> CurrentCustomerBranchID { get; set; }
        public string CustomerSiteName { get; set; }
        public string CustomerName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:mm-dd-yyyy}", ApplyFormatInEditMode = false)]
        public Nullable<System.DateTime> CurrentDeliveryDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:mm-dd-yyyy}", ApplyFormatInEditMode = false)]
        public Nullable<System.DateTime> CurrentRecieveDate { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select the Current Location")]
        public Nullable<int> CurrentLocationID { get; set; }
        public string CurrentLocation { get; set; }
        public Nullable<int> Branchid { get; set; }
        public string BranchName { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<int> CreatedByID { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<int> UpdatedByID { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select the Status")]
        public Nullable<bool> status { get; set; }
        public string Path { get; set; }
    }
}