using AIS.Data.BaseClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace AIS.Data.RequestResponseModel.MasterData
{
    public class MasterClassAPIInputModel : DefaultInputModel
    {
        public int masterClassId { get; set; }
        public string? className { get; set; }
        public string? classCode { get; set; }
        public int? displayOrder { get; set; }
    }
    public class MasterClassListModel : DefaultOutputModel
    {
        public int masterClassId { get; set; }
        public string className { get; set; }
        public string classCode { get; set; }
        public string displayOrder { get; set; }
        public string statusDetails { get; set; }

    }

     
    public class MasterBatchAPIInputModel : DefaultInputModel
    {
        public int masterBatchId { get; set; }
        public string? batchName { get; set; }
        public string? batchCode { get; set; }
        public int? displayOrder { get; set; }
    }
    public class MasterBatchListModel : DefaultOutputModel
    {
        public int masterBatchId { get; set; }
        public string batchName { get; set; }
        public string batchCode { get; set; }
        public string displayOrder { get; set; }
        public string statusDetails { get; set; }


    }
    public class MasterSectionAPIInputModel : DefaultInputModel
    {
        public int masterSectionId { get; set; }
        public string? sectionName { get; set; }
        public string? sectionCode { get; set; }
        public int? displayOrder { get; set; }
    }
    public class MasterSectionListModel : DefaultOutputModel
    {
        public int masterSectionId  { get; set; }
        public string sectionsName { get; set; }
        public string sectionCode { get; set; }
        public string displayOrder { get; set; }
        public string statusDetails { get; set; }


    }
    public class MasterDepartmentAPIInputModel : DefaultInputModel
    {
        public int masterDeparmentId { get; set; }
        public string? deparmentName { get; set; }
        public string? deparmentCode { get; set; }
        public int? displayOrder { get; set; }
    }
    public class MasterDepartmentListModel : DefaultOutputModel
    {
        public int masterDepartmentId { get; set; }
        public string departmentName { get; set; }
        public string departmentCode { get; set; }
        public string displayOrder { get; set; }
        public string statusDetails { get; set; }


    }

    public class MasterDocumentAPIInputModel : DefaultInputModel
    {
        public int masterDocumentId { get; set; }
        public string? documentName { get; set; }
        public string? documentCode { get; set; }
        public int? displayOrder { get; set; }
    }
    public class MasterDocumentListModel : DefaultOutputModel
    {
        public int masterDocumentId { get; set; }
        public string documentName { get; set; }
        public string documentCode { get; set; }
        public string displayOrder { get; set; }
        public string statusDetails { get; set; }


    }

    public class MasterEmployeeAPIInputModel : DefaultInputModel
    {
        public int masterEmployeeId { get; set; }
        public string? employeeName { get; set; }
        public string? employeeCode { get; set; }
        public int? displayOrder { get; set; }
    }
    public class MasterEmployeeListModel : DefaultOutputModel
    {
        public int masterEmployeeId { get; set; }
        public string employeeName { get; set; }
        public string employeeCode { get; set; }
        public string displayOrder { get; set; }
        public string statusDetails { get; set; }


    }

    public class MasterOccuptionAPIInputModel : DefaultInputModel
    {
        public int masterOccuptionId { get; set; }
        public string? occuptionName { get; set; }
        public string? occuptionCode { get; set; }
        public int? displayOrder { get; set; }
    }

    public class MasterOccuptionListModel : DefaultOutputModel
    {
        public int masterOccuptionId { get; set; }
        public string occuptionName { get; set; }
        public string occuptionCode { get; set; }
        public string displayOrder { get; set; }
        public string statusDetails { get; set; }


    }
    public class MasterStateAPIInputModel : DefaultInputModel
    {
        public int masterStateId { get; set; }
        public int masterCountryId { get; set; }
        public string? masteStateName { get; set; }
        public int? masteStateCode { get; set; }

    }
    public class MasterStateListModel : DefaultOutputModel
    {
        public int masterStateId { get; set; }
        public string stateName { get; set; }
        public string stateCode { get; set; }
        public string statusOrder { get; set; }
        //public string statusDetails { get; set; }


    }
    public class MasterSubjectAPIInputModel : DefaultInputModel
    {
        public int  masterSubjectId { get; set; }
        public string subjectName { get; set; }
        public string subjectCode { get; set; }
        public int? marksType { get; set; }
        public string? MarksTypeDescription { get; set; }
        public int? subjectType { get; set; }
        public string? SubjectTypeDescription { get; set; }
        public int? displayOrder { get; set; }
    }
    public class MastersSubjectListModel : DefaultOutputModel
    {
        public int masterSubjectId { get; set; }
        public string subjectName { get; set; }
        public string subjectCode { get; set; }
        public string displayOrder { get; set; }
        public int? marksType { get; set; }
        public string? MarksTypeDescription { get; set; }
        public int? subjectType { get; set; }
        public string? SubjectTypeDescription { get; set; }
        public string statusDetails { get; set; }



    }

    //11-04-23 farhat
    public class MasterBusStopWithRouteDetailsAPIInputModel : DefaultInputModel
    {
        public int masterBusStopWithRouteId { get; set; }
        public string busRouteName { get; set; }
        public string busStopDescription { get; set; }
        public int? displayOrder { get; set; }
    }

    public class MasterBusStopWithRouteDetailsListModel : DefaultOutputModel
    {
        public int masterRouteId { get; set; }
        public string busStopName { get; set; }
        public string busStopDescription { get; set; }
        public string displayOrder { get; set; }
        public string statusDetails { get; set; }
    }

    public class MasterConcessionDetailsAPIInputModel : DefaultInputModel
    {
        public int masterConcessionId { get; set; }
        public string concessionName { get; set; }
        public string concessionDescription { get; set; }
        public string? displayOrder { get; set; }
    }
    public class MasterConcessionDetailsListModel : DefaultOutputModel
    {
        public int masterConcessionId { get; set; }
        public string concessionName { get; set; }
        public string concessionDescription { get; set; }
        public string displayOrder { get; set; }
        public string statusDetails { get; set; }
    }

    public class MasterDiscountDetailsAPIInputModel : DefaultInputModel
    {
        public int masterDiscountId { get; set; }
        public int totalDiscount { get; set; }
        public string? displayOrder { get; set; }

    }
    public class MasterDiscountDetailsListModel : DefaultOutputModel
    {
        public int masterDiscountId { get; set; }
        public int totalDiscount { get; set; }
        public string displayOrder { get; set; }
        public string statusDetails { get; set; }
    }

    public class MasterFeeDetailsAPIInputModel : DefaultInputModel
    {
        public int masterFeeId { get; set; }
        public string feeName { get; set; }
        public int feeAmount { get; set; }
        public string feeDescription { get; set; }
        public string? displayOrder { get; set; }

    }
    public class MasterFeeDetailsListModel : DefaultOutputModel
    {
        public int masterFeeId { get; set; }
        public string feeName { get; set; }
        public string feeAmount { get; set; }
        public string feeDescription { get; set; }
        public string displayOrder { get; set; }
        public string statusDetails { get; set; }
    }

    public class MasterExamTypeDetailsAPIInputModel : DefaultInputModel
    {
        public int masterExamtypeId { get; set; }
        public string examName { get; set; }
        public string examDescription { get; set; }
        public string displayOrder { get; set; }

    }
    public class MasterExamTypeDetailsListModel : DefaultOutputModel
    {
        public int masterExamtypeId { get; set; }
        public string examName { get; set; }
        public string examDescription { get; set; }
        public string displayOrder { get; set; }
        public string statusDetails { get; set; }
    }

    public class MasterItemDetailsAPIInputModel : DefaultInputModel
    {
        public int masterItemId { get; set; }
        public string itemName { get; set; }
        public string itemType { get; set; }
        public string barCodeNo { get; set; }
        public string itemCode { get; set; }
        public string itemDescription { get; set; }
        public int? displayOrder { get; set; }

    }
    public class MasterItemDetailsListModel : DefaultOutputModel
    {
        public int masterItemId { get; set; }
        public string itemName { get; set; }
        public string itemType { get; set; }
        public string barCodeNo { get; set; }
        public string itemCode { get; set; }
        public string itemDescription { get; set; }
        public string displayOrder { get; set; }
        public string statusDetails { get; set; }
    }

    public class MasterStudentGatePassDetailsAPIInputModel : DefaultInputModel
    {
        public int masterGatePassId { get; set; }
        public int admissionNo { get; set; }
        public string fatherName { get; set; }
        public string nameOfStudent { get; set; }
        public string rollNo { get; set; }
        public string classSection { get; set; }
        public string address { get; set; }
        public string phoneNo { get; set; }
        public DateTime currentDateTime { get; set; }
        public string visitorName { get; set; }
        public string visitorMobileNo { get; set; }
        public string remarks { get; set; }
        public int? displayOrder { get; set; }

    }
    public class MasterStudentGatePassDetailsListModel : DefaultOutputModel
    {
        public int masterStudentGatePassId { get; set; }
        public int admissionNo { get; set; }
        public string fatherName { get; set; }
        public string nameOfStudent { get; set; }
        public string rollNo { get; set; }
        public string classSection { get; set; }
        public string address { get; set; }
        public string phoneNo { get; set; }
        public DateTime currentDateTime { get; set; }
        public string visitorName { get; set; }
        public string visitorMobileNo { get; set; }
        public string remarks { get; set; }
        public string displayOrder { get; set; }
        public string statusDetails { get; set; }
    }

    public class MasterVehicleDetailsAPIInputModel : DefaultInputModel
    {
        public int masterVehicleId { get; set; }
        public string vehicleName { get; set; }
        public string description { get; set; }
        public int? displayOrder { get; set; }

    }
    public class MasterVehicleDetailsListModel : DefaultOutputModel
    {
        public int masterVehicleId { get; set; }
        public string vehicleName { get; set; }
        public string description { get; set; }
        public string displayOrder { get; set; }
        public string statusDetails { get; set; }
    }

    public class MasterVenderDetailsAPIInputModel : DefaultInputModel
    {
        public int masterVenderId { get; set; }
        public string venderName { get; set; }
        public string venderCode { get; set; }
        public string gSTNO { get; set; }
        public string contactNo { get; set; }
        public string address { get; set; }
        public string venderType { get; set; }
        public int? displayOrder { get; set; }

    }
    public class MasterVenderDetailsListModel : DefaultOutputModel
    {
        public int masterVenderId { get; set; }
        public string venderName { get; set; }
        public string venderCode { get; set; }
        public string gSTNO { get; set; }
        public string contactNo { get; set; }
        public string address { get; set; }
        public string venderType { get; set; }
        public string displayOrder { get; set; }
        public string statusDetails { get; set; }
    }

    public class MasterFeeTermDetailsAPIInputModel : DefaultInputModel
    {
        public int feeTermId { get; set; }
        public string feeTermName { get; set; }
        public int? displayOrder { get; set; }

    }
    public class MasterFeeTermDetailsListModel : DefaultOutputModel
    {
        public int feeTermId { get; set; }
        public string feeTermName { get; set; }
        public string displayOrder { get; set; }
        public string statusDetails { get; set; }
    }
}