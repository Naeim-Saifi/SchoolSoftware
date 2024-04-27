using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Models.Enquiry
{
    public class EnquiryModel
    {
        public int enquiryID { get; set; }
        public string schoolCode { get; set; }
        public string financialYear { get; set; }
        public string studentName { get; set; }
        public string fatherName { get; set; }
        public string motherName { get; set; }
        public string fMobileNo { get; set; }
        public string mMobileNo { get; set; }
        public string previousSchoolName { get; set; }
        public string emailID { get; set; }
        public int currentClassID { get; set; }
        public int promotedClassID { get; set; }
        public int genderID { get; set; }
        public string dob { get; set; }
        public int visitingStatus { get; set; }
        public int visitingType { get; set; }
        public int locationID { get; set; }
        public string locationDetails { get; set; }
        public string visitingRemrks { get; set; }
        public string address { get; set; }
        public int createdByUserId { get; set; }
        public string operationType { get; set; }
    }
    public class EnquiryListModel
    {
        public int enquiryID { get; set; }
        [Required]
        public string studentName { get; set; }
        [Required]
        public string fatherName { get; set; }
        public string motherName { get; set; }
        public string fMobileNo { get; set; }
        public string mMobileNo { get; set; }
        public int currentClassID { get; set; }
        [Required]
        public string previousSchoolName { get; set; }
        public string emailID { get; set; }
        public string className { get; set; }
        public int promotedClassID { get; set; }
        public string promotedClass { get; set; }
        public int genderID { get; set; }
        [Required]
        public string gender { get; set; }
        public string dob { get; set; }       
        public int visitingStatus { get; set; }
        [Required]
        public string visitingStatusDetaills { get; set; }        
        public int visitingType { get; set; }
        [Required]
        public string visitingTypeDetails { get; set; }
        //public int locationID { get; set; }
        //public string locationIDDetails { get; set; }
        //public string locationDetails { get; set; }
        public string visitingRemrks { get; set; }
        public string address { get; set; }
        public string activeStatus { get; set; }
        public string CreatedOn { get; set; }
        public string CreatedID { get; set; }
        public string UpdateOn { get; set; }
        public int status { get; set; }

    }
   
    public class EnquiryViewModel
    {
        public int? enquiryID { get; set; }
        [Required]
        public string studentName { get; set; }
        [Required]
        public string fatherName { get; set; }
        public string? motherName { get; set; }
        public string? fMobileNo { get; set; }
        public string? mMobileNo { get; set; }
    }
    public class InputEnquiryModel
    {
        public string schoolCode { get; set; }
        public string financiyalYear { get; set; }
        public int userId { get; set; }
        public string fromDate { get; set; }
        public string todate { get; set; }
        public string reportType { get; set; }
    }

    public class enquiryViewModel
    {
        public int? enquiryID { get; set; } = 0;
        [Required(ErrorMessage = "Student Name is required")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Use letters only please")]
        public string studentName { get; set; }
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Use letters only please")]
        [Required(ErrorMessage = "Father Name is required")]

        public string fatherName { get; set; }
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Use letters only please")]
        [Required(ErrorMessage = "Mother Name is required")]
        public string? motherName { get; set; }
        [Required(ErrorMessage = "Father Mobile No is required")]
        public string? fMobileNo { get; set; }
        public string? mMobileNo { get; set; }

        [Required(ErrorMessage = "Previous School Details is required")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Use letters only please")]
        public string? PreviousSchoolDetails { get; set; }

        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string? EmailId { get; set; }
        [Required(ErrorMessage = "Current Class is required")]
        public string? CurrentClass { get; set; }
        public string? NextClass { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public string? Gender { get; set; }
        public DateTime? DOB { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Visiting Status is required")]
        public string? VisitingStatus { get; set; }
        [Required(ErrorMessage = "Visiting Type is required")]
        public string? VisitingType { get; set; }
        public string? VisitingRemarks { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string? Address { get; set; }


    }
}
