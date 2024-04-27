using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AIS.Model.Employee
{

    public class DropdwonGender
    {
        public int ID { get; set; }
        public string Text { get; set; }
    }
    public class DropdwonUserRole
    {
        public int ID { get; set; }
        public string Text { get; set; }
    }
    public class EmployeeViewModel
    {
        public int userId { get; set; } = 0;

        [Required(ErrorMessage = "The Employee Code is required.")]
        public string? EmployeeCode { get; set; }
        [Required(ErrorMessage = "First Name is required.")]
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Gender is required.")]        
        public string? DropdwonGender { get; set; }
        //public int GenderID { get; set; }

        [Required(ErrorMessage = "The Employee Role is required.")]         
        public int? DropdwonUserRole { get; set; }
        //public int EmployeeRoleID { get; set; }

        [Required(ErrorMessage = "The E-Mail is required.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string? Email { get; set; }
       

        [Required(ErrorMessage = "The mobile no is required.")]
        [StringLength(10, ErrorMessage = "Phone Number length can't be more than 10.")]
        public string? PhoneNumber { get; set; }
    }

    public class EmployeeModel
    {
        public int EmployeeId { get; set; }
        //public int UserId { get; set; }
        //public int SchoolId { get; set; }
        //[Required(ErrorMessage = "The School Code is required.")]
        public string Schoolcode { get; set; }
        public string FinancialYear { get; set; }
        //public int? MasterEmployeeId { get; set; }
        //public int? MasterDepartmentId { get; set; }
        //public int? PersonalDetailId { get; set; }
        //public int? BankDetailId { get; set; }
        public string? BioMatricId { get; set; }

        [Required(ErrorMessage = "The Employee Code is required.")]
        public string EmployeeCode { get; set; }
        public string? Title { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public int GenderID { get; set; }

        [Required(ErrorMessage = "The Employee Role is required.")]
        public int EmployeeRole { get; set; }
        public DateTime? JoiningDate { get; set; }
        public DateTime? AppointmentDate { get; set; }

        [Required(ErrorMessage = "The E-Mail is required.")]
        public string Email { get; set; }
        public DateTime? DOB { get; set; }

        [Required(ErrorMessage = "The mobile no is required.")]
        [StringLength(10, ErrorMessage = "Phone Number length can't be more than 10.")]
        public string PhoneNumber { get; set; }
        public string? Board { get; set; }
        public string? JobTitle { get; set; }
        public string? OfferSalary { get; set; }
        public string? Qualification { get; set; }
        public string? EmployeeReferral { get; set; }
        public int? Experience { get; set; }
        public int? ActiveStatus { get; set; }
        public int? CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        //public DateTime? CreatedDate { get; set; }
        //public DateTime? UpdatedDate { get; set; }
        //public string MasterEmployeeName { get; set; }
        //public string MasterDepartmentName { get; set; }
        public int? MaritalStatus { get; set; }
        public DateTime? AnniversaryDate { get; set; }
        public int? Religion { get; set; }
        public int? CastCategory { get; set; }
        public int? Nationality { get; set; }
        public int? BloodGroup { get; set; }
        public int? HandicapStatus { get; set; }
        public string MedicalCondition { get; set; }

        public int? UIDType { get; set; }
        public string? UIDNumber { get; set; }

        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public string? City { get; set; }
        public string? PinCode { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }

       
        public string? BankName { get; set; }
        public string? AccountNo { get; set; }
        public string? IfscCode { get; set; }
        public string? PanCard { get; set; }

        public string? UserName { get; set; }

        public string OperationType { get; set; }
    }

    public class EmployeeApiModel
    {
        public int EmployeeId { get; set; }
        //public int UserId { get; set; }
        //public int SchoolId { get; set; }
        //[Required(ErrorMessage = "The School Code is required.")]
        public string Schoolcode { get; set; }
        public string FinancialYear { get; set; }
        //public int? MasterEmployeeId { get; set; }
        //public int? MasterDepartmentId { get; set; }
        //public int? PersonalDetailId { get; set; }
        //public int? BankDetailId { get; set; }
        public string? BioMatricId { get; set; }
        public string EmployeeCode { get; set; }
        public string? Title { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public int GenderID { get; set; }
        public int EmployeeRole { get; set; }
        public DateTime? JoiningDate { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string Email { get; set; }
        public DateTime? DOB { get; set; }
        public string PhoneNumber { get; set; }
        public string? Board { get; set; }
        public string? JobTitle { get; set; }
        public string? OfferSalary { get; set; }
        public string? Qualification { get; set; }
        public string? EmployeeReferral { get; set; }
        public int? Experience { get; set; }
        public int? ActiveStatus { get; set; }
        public int? CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        //public DateTime? CreatedDate { get; set; }
        //public DateTime? UpdatedDate { get; set; }
        //public string MasterEmployeeName { get; set; }
        //public string MasterDepartmentName { get; set; }
        public int? MaritalStatus { get; set; }
        public DateTime? AnniversaryDate { get; set; }
        public int? Religion { get; set; }
        public int? CastCategory { get; set; }
        public int? Nationality { get; set; }
        public int? BloodGroup { get; set; }
        public int? HandicapStatus { get; set; }
        public string MedicalCondition { get; set; }

        public int? UIDType { get; set; }
        public string? UIDNumber { get; set; }

        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public string? City { get; set; }
        public string? PinCode { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }


        public string? BankName { get; set; }
        public string? AccountNo { get; set; }
        public string? IfscCode { get; set; }
        public string? PanCard { get; set; }

        public string? UserName { get; set; }

        public string OperationType { get; set; }
    }

    public class EmployeeDetailsModel
    {
        public string employeeCode { get; set; }
        public int userId { get; set; }

        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        
        public string employeeName { get; set; }
        public string bioMatricId { get; set; }

        public string jobTitle { get; set; }
        public string joiningDate { get; set; }
        public int gender { get; set; }
        public string genderStatusDetails { get; set; }
        public string maritalStatus { get; set; }
        public DateTime anniversaryDate { get; set; }
        public int religion { get; set; }
        public string religionDetails { get; set; }
        public int castCategory { get; set; }
        public string castCategoryDetails { get; set; }
        public int bloodGroup { get; set; }
        public string bloodGroupDetails { get; set; }
        public string uIDType { get; set; }
        public string uIDTypeDetails { get; set; }
        public string uIDNumber { get; set; }
        public int activeStatus { get; set; }
        public string activeStatusDetails { get; set; }
        public int userRole { get; set; }
        public string roleStatusDetails { get; set; }
        public string accountNo { get; set; }
        public string iFSCCode { get; set; }
        public string bankName { get; set; }
        public string panCard { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string pinCode { get; set; }
        public string stateStatusDetails { get; set; }

    }
}
