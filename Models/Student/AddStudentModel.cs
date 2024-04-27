using System;
using System.ComponentModel.DataAnnotations;

namespace AdminDashboard.Server.Models.Student
{
    
    public class AddStudentViewModel
    {
        //public int? enquiryID { get; set; } = 0;
        //[Required(ErrorMessage = "Student Name is required")]
        //[RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Use letters only please")]
        //public string studentName { get; set; }
        //[RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Use letters only please")]
        //[Required(ErrorMessage = "Father Name is required")]

        //public string fatherName { get; set; }
        //[RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Use letters only please")]
        //[Required(ErrorMessage = "Mother Name is required")]
        //public string? motherName { get; set; }
        //[Required(ErrorMessage = "Father Mobile No is required")]
        //public string? fMobileNo { get; set; }
        //public string? mMobileNo { get; set; }

        //[Required(ErrorMessage = "Previous School Details is required")]
        //[RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Use letters only please")]
        //public string? PreviousSchoolDetails { get; set; }

        //[RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        //public string? EmailId { get; set; }
        //[Required(ErrorMessage = "Current Class is required")]
        //public string? className { get; set; }
        //[Required(ErrorMessage = "Current Class is required")]
        //public string? sectionName { get; set; }
        //[Required(ErrorMessage = "Gender is required")]
        //public string? Gender { get; set; }
        //public DateTime? DOB { get; set; } = DateTime.Now;

        //[Required(ErrorMessage = "Visiting Status is required")]
        //public string? VisitingStatus { get; set; }
        //[Required(ErrorMessage = "Visiting Type is required")]
        //public string? VisitingType { get; set; }
        //public string? VisitingRemarks { get; set; }

        //[Required(ErrorMessage = "Address is required")]
        //public string? Address { get; set; }

        public int? StudentID { get; set; }
       
        public string? SRNNumber { get; set; }
        public string? WithdrawalNumber { get; set; }
        [Required(ErrorMessage = "Class Name is required")]
        public string? ClassName { get; set; }
        [Required(ErrorMessage = "Section Name is required")]
        public string? SectionName { get; set; }
        [Required(ErrorMessage = "AdmissionNumber is required")]
        public string? AdmissionNumber { get; set; }
        [Required(ErrorMessage = "EmailId is required")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string? EmailId { get; set; }
        
        [Required(ErrorMessage = "StudentType is required")]
        public string? StudentType { get; set; }
        [Required(ErrorMessage = "AdmissionType is required")]
        public string? AdmissionType { get; set; }
        [Required(ErrorMessage = "RollNo Name is required")]
        public string? RollNo { get; set; }
        [Required(ErrorMessage = "Transport Mode is required")]
        public string? TransportMode { get; set; }
        [Required(ErrorMessage = "Bus Stop is required")]
        public string? BusStop { get; set; }
        [Required(ErrorMessage = "Student Name is required")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Use letters only please")]
        public string StudentName { get; set; }
        [Required(ErrorMessage = "Father Name is required")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Use letters only please")]
        public string? FatherName { get; set; }
        [Required(ErrorMessage = "Father Mobile No is required")]
        public string? FatherMobileNo { get; set; }
        [Required(ErrorMessage = "Mother Name is required")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Use letters only please")]
        public string? MotherName { get; set; }
        [Required(ErrorMessage = "Mother Mobile No is required")]
        public string? MothermobileNo { get; set; }
        [Required(ErrorMessage = "Roll Number is required")]
        public string? RollNumber { get; set; }
        [Required(ErrorMessage = "DOB is required")]
        public DateTime? DOB { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Gender is required")]
        public string? Gender { get; set; }
        [Required(ErrorMessage = "Religion is required")]
        public string? Religion { get; set; }
        [Required(ErrorMessage = "Admission Date is required")]
        public DateTime? AdmDate { get; set; } = DateTime.Now;       
        public string? House { get; set; }
        [Required(ErrorMessage = "Cast Category is required")]
        public string? CastCategory { get; set; }
        [Required(ErrorMessage = "Cast Category is required")]
        public string? BloodGroup { get; set; }
        [Required(ErrorMessage = "Aadhar No is required")]
        public string? UIDNumber { get; set; }
        public string? UIDTypeDetails { get; set; }
        [Required(ErrorMessage = "Fee Category is required")]
        public string? FeeCategory { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "State is required")]
        public string? State { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string? City { get; set; }
        [Required(ErrorMessage = "PinCode is required")]
        public string? PinCode { get; set; }

        [Required(ErrorMessage = "Class is required")]
        public string? Class { get; set; }
        
        
      
    };
}
