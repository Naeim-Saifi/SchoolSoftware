using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Models.TimeTable
{

    public class DropdownModel
    {
        [Required(ErrorMessage = "Please select your Class.")]
        public string ?ClassName { get; set; } 
        [Required(ErrorMessage = "Please select your Section.")]
        public string ?SectionName { get; set; } 
        [Required(ErrorMessage = "Please select your Subject.")]
        public string ?SubjectName { get; set; } 
        [Required(ErrorMessage = "Please select Teacher.")]
        public string ?TeacherName { get; set; } 
        [Required(ErrorMessage = "Please select Day.")]
        public string ?DayName { get; set; } 
        [Required(ErrorMessage = "Please select Period.")]
        public string ?PeriodName { get; set; } 

    }

    public class ClassName
    {
        public string className { get; set; }
        public int classid { get; set; }
    }
    public class SectionName
    {
        public string sectionname { get; set; }
    }
    public class SubjectName
    {
        public string subjectName { get; set; }
    }
    public class TeacherName
    {
        public string teacherName { get; set; }
    }
    public class DayName
    {
        public string dayName { get; set; }
    }
    public class PeriodName
    {
        public string periodName { get; set; }
    }
    public class TimeTableModel
    { 
        public int MasterClassId { get; set; }
        public int MasterSectionId { get; set; }
        public int MasterSubjectId { get; set; }
        public int MasterTeacherId { get; set; }
        public int PeriodId { get; set; }
        public int DayId { get; set; }
        
    }
    public class ViewTimeTableModel
    {
       
        [Required(ErrorMessage = "Please select your Class.")]
        public string? ClassName { get; set; }
        [Required(ErrorMessage = "Please select your Section.")]
        public string? SectionName { get; set; }
        [Required(ErrorMessage = "Please select your Subject.")]
        public string? SubjectName { get; set; }
        [Required(ErrorMessage = "Please select Teacher.")]
        public string? TeacherName { get; set; }
        [Required(ErrorMessage = "Please select Day.")]
        public string? DayName { get; set; }
        [Required(ErrorMessage = "Please select Period.")]
        public string? PeriodName { get; set; }

    }
    public class TimeTableListModel
    {
        public int timeTableID { get; set; }
        public string schoolCode { get; set; }
        public string financialYear { get; set; }
        public int classId { get; set; }
        public string className { get; set; }
        public int masterSectionId { get; set; }
        public string sectionName { get; set; }
        public int dayid { get; set; }
        public string dayDetails { get; set; }
        public int periodId { get; set; }
        public string periodName { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public int masterSubjectId { get; set; }
        public string subjectName { get; set; }
        public string employeeId { get; set; }
        public string teacherName { get; set; }
        public string createdDate { get; set; }
        public string updatedDate { get; set; }

    }

    public class PeriodModel
    {
        public string SchoolCode { get; set; }
        public string FinancialYear { get; set; }
        public int PeriodId { get; set; }
        public string PeriodName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int ActiveStatus { get; set; }
        public int CreatedByUserId { get; set; }
        public int UpdatedByUserId { get; set; }
        public string OperationType { get; set; }
    }

    public class PeriodViewModel
    {
        
        public int PeriodId { get; set; }
         
        [Required(ErrorMessage = "Please Enter Period Name.")]
        public string PeriodName { get; set; }
        [Required(ErrorMessage = "Please Enter StartTime.")]
        public string StartTime { get; set; }
        [Required(ErrorMessage = "Please Enter EndTime.")]
        public string EndTime { get; set; }
        //public int ActiveStatus { get; set; }
        //public int CreatedByUserId { get; set; }
        //public int UpdatedByUserId { get; set; }
        //public string OperationType { get; set; }
    }
    public class PeriodListModel
    {
        
        public int periodId { get; set; }
        public string periodName { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string createdOn { get; set; }
        public string updateOn { get; set; }

    }

    public class DayModelList
    {
        public int DayId { get; set; }
        public string DayName { get; set; }
    }
     

}
