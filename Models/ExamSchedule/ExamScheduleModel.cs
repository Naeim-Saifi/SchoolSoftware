using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdminDashboard.Server.Models.ExamSchedule
{
    public class ExamScheduleModel
    {
        public int ExamScheduleId { get; set; }
        public string SchoolCode { get; set; }
        public string FinancialYear { get; set; }
        public int ExamTypeId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int MonthId { get; set; }
        public int SubjectId { get; set; }
        public int UnitId { get; set; }
        public int ChapterId { get; set; }
        public int TopicId { get; set; }
        public string FromDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string RoomName { get; set; }
        public int CreatedByUserId { get; set; }
        public string OperationType { get; set; }

    }
    public class ExamScheduleListModel
    {
        public int examScheduleId { get; set; }

        public int examTypeId { get; set; }
        public string examTypeName { get; set; }
        public int monthId { get; set; }
        public string monthNameDetails { get; set; }
        public int classId { get; set; }
        public string className { get; set; }
        //public int SectionId { get; set; }
        //public string SectionName { get; set; }

        public int subjectId { get; set; }
        public string subjectName { get; set; }
        public int unitId { get; set; }
        public string unitTitle { get; set; }
        //public int ChapterId { get; set; }
        //public string ChapterName { get; set; }
        //public int TopicId { get; set; }
        //public string TopicName { get; set; }
        public string examScheduleDate { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        //public string RoomName { get; set; }
        public string createdByUserName { get; set; }
        public string createdDate { get; set; }
        public string updatedDate { get; set; }
        //public string UpdatedByUserName{ get; set; }

    }

    public class ExamType
    {
        public int eamTypeId { get; set; }
        public string examTypeName { get; set; }
    }

    public class ClassDetails
    {
        public string? ClassId { get; set; }
        public string? ClassName { get; set; }
    }
    public class ExamScheduleViewModel
    {
        public int examScheduleId { get; set; }

        [Required(ErrorMessage = "The Exam type required.")]
        public string ExamType { get; set; }

        [Required(ErrorMessage = "The Month is required.")]
        public string MonthName { get; set; }

        [Required(ErrorMessage = "The Class name is required.")]
        public string ClassDetails { get; set; }

        [Required(ErrorMessage = "The Subject Name is required.")]
        public string SubjectName { get; set; }

        [Required(ErrorMessage = "The Unit Name is required.")]
        public string UnitName { get; set; }
        //[Required(ErrorMessage = "The Chapter Name is required.")]
        //public string ChapterName { get; set; }
        //[Required(ErrorMessage = "The Topic Name is required.")]
        //public string TopicName { get; set; }

        [Required(ErrorMessage = "The Exam Date is required.")]
        public DateTime ExamDate { get; set; }

        [Required(ErrorMessage = "The StartTime is required.")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "The EndTime is required.")]
        public DateTime EndTime { get; set; }
    }


}
