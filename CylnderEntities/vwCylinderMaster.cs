
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace CylnderEntities
{

using System;
    using System.Collections.Generic;
    
public partial class vwCylinderMaster
{

    public int CylinderID { get; set; }

    public string CylindeNumber { get; set; }

    public string Barcode { get; set; }

    public Nullable<int> ManufacturerID { get; set; }

    public string ManufacturerName { get; set; }

    public Nullable<System.DateTime> PurchaseDate { get; set; }

    public Nullable<int> InitialGasID { get; set; }

    public string InitialGas { get; set; }

    public Nullable<int> WLCapacity { get; set; }

    public Nullable<int> WLCapacityUOMID { get; set; }

    public string WLCapacityUOM { get; set; }

    public Nullable<int> WorkingPressure { get; set; }

    public Nullable<int> WorkingPressureUOMID { get; set; }

    public string WorkingPressureUOM { get; set; }

    public Nullable<System.DateTime> TestDate { get; set; }

    public Nullable<System.DateTime> NextTestDate { get; set; }

    public Nullable<int> ValveModelID { get; set; }

    public string ValveModel { get; set; }

    public Nullable<int> PresentStateID { get; set; }

    public string PresentState { get; set; }

    public Nullable<int> GasInUseID { get; set; }

    public string GasInUse { get; set; }

    public Nullable<int> VendorID { get; set; }

    public string VendorName { get; set; }

    public Nullable<int> VendorBranchID { get; set; }

    public string VendorBranchName { get; set; }

    public Nullable<double> Size { get; set; }

    public Nullable<double> ActualSize { get; set; }

    public Nullable<int> SizeUOMID { get; set; }

    public string SizeUOM { get; set; }

    public Nullable<int> CustomerID { get; set; }

    public Nullable<int> CurrentCustomerBranchID { get; set; }

    public string CustomerName { get; set; }

    public string CustomerSiteName { get; set; }

    public Nullable<System.DateTime> CurrentDeliveryDate { get; set; }

    public Nullable<System.DateTime> CurrentRecieveDate { get; set; }

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

    public Nullable<bool> status { get; set; }

    public Nullable<bool> IsPrintDone { get; set; }

}

}
