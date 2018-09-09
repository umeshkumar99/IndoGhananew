using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CylnderEntities
{

    [MetadataType(typeof(UserDetails))]
    public partial class usp_tblUserMasterGetByID_Result
    { }
    public class UserDetails
    {

        public int User_Id { get; set; }
        [Display(Name ="User Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the User Name")]
        [MaxLength(100, ErrorMessage = "User Name cannot be greater than 10 characters")]
        [MinLength(5, ErrorMessage = "User Name cannot be less than 5 characters")]
        public string User_Name { get; set; }

        [MaxLength(200, ErrorMessage = "Address cannot be greater than 200 characters")]
        public string Address { get; set; }

        [MaxLength(100, ErrorMessage = "Email Address cannot be greater than 100 characters")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the mobile No.")]
        [MaxLength(20, ErrorMessage = "MobileNo cannot be greater than 20 characters")]
        [MinLength(8, ErrorMessage = "MobileNo cannot be less than 8 characters")]
        [Display(Name ="MobileNo.")]
        public string Contact_Number { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the User ID")]
        [MaxLength(50, ErrorMessage = "LoginID cannot be greater than 50 characters")]
        [MinLength(5, ErrorMessage = "LoginID cannot be less than 5 characters")]
        [Display(Name = "LoginID.")]
        public string Login_Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the password")]
        [MaxLength(50, ErrorMessage = "Password cannot be greater than 50 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "IMIE1 cannot be greater than 20 characters")]
        public string IMIE1 { get; set; }

        [MaxLength(20, ErrorMessage = "IMIE2 cannot be greater than 20 characters")]
        
        public string IMIE2 { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select the Group")]
        public int Group_Id { get; set; }
        public System.DateTime CreationDate { get; set; }
        public bool Status { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public string Group_Name { get; set; }
        public Nullable<int> GroupType { get; set; }
        public string RedirectUrl { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select the Branch")]
        public Nullable<int> Branch_Id { get; set; }
        public string BranchName { get; set; }
        public Nullable<int> Company_Id { get; set; }
        public string CompanyName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select the User Status")]
        public bool UserStatus { get; set; }
    }


}