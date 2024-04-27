using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AIS.Data.RequestResponseModel.AISSchoolDetails
{
    public  class SchoolDetailsModel
    {
        public int Id { get; set; }
        public string School_Name { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string Director_Name { get; set; }
        public string Directore_MobileNo { get; set; }
        public string Principal_Name { get; set; }
        public string Principal_MobileNo { get; set; }
        public int Visiting_Status { get; set; }
        public string visitingRemarks { get; set; }
        public string EmailID { get; set; }
        public int UserID { get; set; }
        public string SchoolCode { get; set; }
        public string FinancialYear { get; set; }
        public string OperationType { get; set; }

    }
    public class SchoolDetailsListModel
    {
        
        public int id { get; set; }
        [Required(ErrorMessage = "School Name is required")] 
        [StringLength(50, MinimumLength = 3, ErrorMessage = "School length should between 3 and 50")]
        public string school_Name { get; set; }
        [Required(ErrorMessage = "Address is required")]
        [StringLength(50, MinimumLength = 3)]
        public string address { get; set; }
        [Required(ErrorMessage = "Location is required")]
        [StringLength(50, MinimumLength = 3)]
        public string location { get; set; }
        [DisplayName("Director Name")]
        [Required(ErrorMessage = "Director Name is required")]
        [StringLength(50, MinimumLength = 3)]
        public string director_Name { get; set; }
        [Required]
        [StringLength(10)]
        public string directore_MobileNo { get; set; }
        //[DisplayName("Principal Name")]
        [Required(ErrorMessage = "Principal Name is required")]
        [StringLength(50, MinimumLength = 3)]
        public string principal_Name { get; set; }
       
        [Required(ErrorMessage = "Principal Mobile No required.")]
        [StringLength(10)]
        public string principal_MobileNo { get; set; }
        //[Required(ErrorMessage = "Please enter your email address")]
        //[DataType(DataType.EmailAddress)]
        //[Display(Name = "Email address")]
        //[MaxLength(50)]
        //[RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        //public string emailID { get; set; }
        
        public string emailID { get; set; }
        public int visiting_Status { get; set; } 
        public string visitingRemarks { get; set; }
        

    }
    public class InputModel
    {
        public string SchoolCode { get; set; }
        public string FinancialYear { get; set; }
        public string? fromDate { get; set; }
        public string? toDate { get; set; }
        public int UserId { get; set; }
        public string OperationType { get; set; }
         
    } 

}
