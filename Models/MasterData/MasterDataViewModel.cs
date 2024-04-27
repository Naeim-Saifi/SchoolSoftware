using System;
using System.ComponentModel.DataAnnotations;

namespace AdminDashboard.Server.Models.MasterData
{

    public class MasterClassViewModel
    {
        public int masterClassId { get; set; } = 0;
        [Required(ErrorMessage = "Class Name is requerid")]
        public string? className { get; set; }
        [Required(ErrorMessage = "Class Code is requerid")]
        public string? classCode { get; set; }
        [Range(0, 100, ErrorMessage = "Enter number between 0 to 100")]
        public int? displayOrder { get; set; }

    }
    public class SectionMasterViewModel
    {
        public int masterSectionId { get; set; } = 0;

        [Required(ErrorMessage = "Section Name is requerid")]
        public string? sectionName { get; set; }
        [Required(ErrorMessage = "Section Code is requerid")]
        public string? sectionCode { get; set; }
        [Range(0, 100, ErrorMessage = "Enter number between 0 to 100")]
        public int? displayOrder { get; set; }

    }
    public class MasterBatchViewModel
    {

        public int masterBatchId { get; set; } = 0;
        [Required(ErrorMessage = "Batch Name is requerid")]
        public string? batchName { get; set; }
        [Required(ErrorMessage = "Batch Code is requerid")]
        public string? batchCode { get; set; }
        [Range(0, 100, ErrorMessage = "Enter number between 0 to 100")]
        public int? displayOrder { get; set; }


    }

    public class DepartmentMasterViewModel
    {
        public int masterDeparmentId { get; set; } = 0;

        [Required(ErrorMessage = "Department Name is requird")]
        public string? deparmentName { get; set; }
        [Required(ErrorMessage = "Department Code is requird")]
        public string? deparmentCode { get; set; }
        [Range(0, 100, ErrorMessage = "Enter number between 0 to 100")]
        public int? displayOrder { get; set; }

    }
    public class EmployeeMasterViewModel
    {
        public int masterEmployeeId { get; set; } = 0;
        [Required(ErrorMessage = "Employee Name is requird")]
        public string? employeeName { get; set; }
        [Required(ErrorMessage = "Employee Code is requird")]
        public string? employeeCode { get; set; }
        [Range(0, 100, ErrorMessage = "Enter number between 0 to 100")]
        public int? displayOrder { get; set; }

    }
    public class DocumentMasterViewModel
    {
        public int masterDocumentId { get; set; }

        [Required(ErrorMessage = "Document Name is requird")]
        public string? documentName { get; set; }
        [Required(ErrorMessage = "Document Code is requird")]
        public string? documentCode { get; set; }
        [Range(0, 100, ErrorMessage = "Enter number between 0 to 100")]
        public int? displayOrder { get; set; }
    }
    public class OccupationMasterViewModel
    {
        public int masterOccuptionId { get; set; } = 0;

        [Required(ErrorMessage = "Occupation Name is requird")]
        public string? occuptionName { get; set; }
        [Required(ErrorMessage = "Occupation Code is requird")]
        public string? occuptionCode { get; set; }
        [Range(0, 100, ErrorMessage = "Enter number between 0 to 100")]
        public int? displayOrder { get; set; }
    }

    public class SubjectMasterViewModel
    {
        public int masterSubjectId { get; set; } = 0;

        [Required(ErrorMessage = "Subject name is requird")]
        public string? subjectName { get; set; }
        [Required(ErrorMessage = "Subject Code is requird")]
        public string? subjectCode { get; set; }
        [Range(0, 100, ErrorMessage = "Enter number between 0 to 100")]
        public int? displayOrder { get; set; }
    }


    public class MasterStateViewModel
    {
        public int masterStateId { get; set; } = 0;
        [Required(ErrorMessage = "Subject Code is requird")]
        public string? masteStateName { get; set; }
        [Required(ErrorMessage = "Subject Name is requird")]
        public string? masteStateCode { get; set; }
    }

    public class MasterBusStopWithRouteDetailsViewModel
    {
        public int masterBusStopWithRouteId { get; set; }=0;
        [Required(ErrorMessage = "Route Name is requird")]
        public string busRouteName { get; set; }
        [Required(ErrorMessage = "Description is requird")]
        public string busStopDescription { get; set; }
        [Range(0, 100, ErrorMessage = "Enter number between 0 to 100")]
        public int? displayOrder { get; set; }
    }
    public class MasterConcessionDetailsViewModel
    {
        public int masterConcessionId { get; set; }
        [Required(ErrorMessage = "Concession Name is requird")]
        public string concessionName { get; set; }
        [Required(ErrorMessage = "Concession Description is requird")]
        public string concessionDescription { get; set; }
        [Range(0, 100, ErrorMessage = "Enter number between 0 to 100")]
        public int? displayOrder { get; set; }
    }
    public class MasterDiscountDetailsViewModel
    {
        public int masterDiscountId { get; set; }
        [Required(ErrorMessage = "Total Discount is requird")]
        public int totalDiscount { get; set; }
        [Range(0, 100, ErrorMessage = "Enter number between 0 to 100")]
        public int? displayOrder { get; set; }

    }
    public class MasterFeeDetailsViewModel
    {
        public int masterFeeId { get; set; }
        [Required(ErrorMessage = "Fee Name is requird")]
        public string feeName { get; set; }
        [Required(ErrorMessage = "Fee Description is requird")]
        public int feeAmount { get; set; }
        public string feeDescription { get; set; }
        [Range(0, 100, ErrorMessage = "Enter number between 0 to 100")]
        public int? displayOrder { get; set; }

    }
    public class MasterExamTypeDetailsViewModel
    {
        public int masterExamtypeId { get; set; }
        [Required(ErrorMessage = "Exam Name is requird")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Use letters only please")]
        public string examName { get; set; }
        [Required(ErrorMessage = "Exam Description is requird")]
        public string examDescription { get; set; }
        [Range(0, 100, ErrorMessage = "Enter number between 0 to 100")]
        public int? displayOrder { get; set; }

    }
    public class MasterItemDetailsViewModel
    {
        public int masterItemId { get; set; }
        [Required(ErrorMessage = "Item Name is requird")]
        public string itemName { get; set; }
        [Required(ErrorMessage = "Item Type is requird")]
        public string itemType { get; set; }
        [Required(ErrorMessage = "BarCode No is requird")]
        public string barCodeNo { get; set; }
        [Required(ErrorMessage = "ItemCode is requird")]
        public string itemCode { get; set; }
        [Required(ErrorMessage = "Item Description is requird")]
        public string itemDescription { get; set; }
        [Range(0, 100, ErrorMessage = "Enter number between 0 to 100")]
        public int? displayOrder { get; set; }

    }
    public class MasterStudentGatePassDetailsViewModel
    {
        public int masterGatePassId { get; set; }
        [Required(ErrorMessage = "Admission No is requird")]
        public int admissionNo { get; set; }
        [Required(ErrorMessage = "Father Name is requird")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Use letters only please")]
        public string fatherName { get; set; }
        [Required(ErrorMessage = "Name Of Student is requird")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Use letters only please")]
        public string nameOfStudent { get; set; }
        [Required(ErrorMessage = "Roll No is requird")]
        public string rollNo { get; set; }
        [Required(ErrorMessage = "Class Section is requird")]
        public string classSection { get; set; }
        [Required(ErrorMessage = "Address is requird")]
        public string address { get; set; }
        [Required(ErrorMessage = "Phone No is requird")]
        public string phoneNo { get; set; }
        public DateTime currentDateTime { get; set; }
        [Required(ErrorMessage = "Visitor Name is requird")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Use letters only please")]
        public string visitorName { get; set; }
        [Required(ErrorMessage = "Visitor Mobile No is requird")]
        public string visitorMobileNo { get; set; }
        public string remarks { get; set; }
        [Range(0, 100, ErrorMessage = "Enter number between 0 to 100")]
        public int? displayOrder { get; set; }

    }
    public class MasterVehicleDetailsViewModel
    {
        public int masterVehicleId { get; set; }
        [Required(ErrorMessage = "Vehicle Name is requird")]
        public string vehicleName { get; set; }
        [Required(ErrorMessage = "Description is requird")]
        public string description { get; set; }
        [Range(0, 100, ErrorMessage = "Enter number between 0 to 100")]
        public int? displayOrder { get; set; }

    }
    public class MasterVenderDetailsViewModel
    {
        public int masterVenderId { get; set; }
        [Required(ErrorMessage = "Vender Name is requird")]
        public string venderName { get; set; }
        [Required(ErrorMessage = "Vender Code is requird")]
        public string venderCode { get; set; }
        [Required(ErrorMessage = "GST NO is requird")]
        public string gSTNO { get; set; }
        [Required(ErrorMessage = "ContactNo is requird")]
        public string contactNo { get; set; }
        [Required(ErrorMessage = "Address is requird")]
        public string address { get; set; }
        [Required(ErrorMessage = "VenderType is requird")]
        public string venderType { get; set; }
        [Range(0, 100, ErrorMessage = "Enter number between 0 to 100")]
        public int? displayOrder { get; set; }

    }
    public class MasterFeeTermDetailsViewModel
    {
        public int feeTermId { get; set; }
        [Required(ErrorMessage = "Fee Term Name is requird")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Use letters only please")]
        public string feeTermName { get; set; }
        [Range(0, 100, ErrorMessage = "Enter number between 0 to 100")]
        public int? displayOrder { get; set; }

    }

}
