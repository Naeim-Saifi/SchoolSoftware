using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdminDashboard.Server.Models.SyllabusSetup
{
  
    public class SyllabusSetupAPIModel
    {
            public int SyllabusId { get; set; }
            public string SchoolCode { get; set; }
            public string FinancialYear { get; set; }
            public int MonthId { get; set; }
            public int ClassId { get; set; }      
            public int SubjectId { get; set; }
            public int UnitId { get; set; }
            public int ChapterId { get; set; }
            public int TopicId { get; set; }           
            public int CreatedByUserId { get; set; }
            public string OperationType { get; set; }

    }

    public class SyllabusSetupListModel
    {
        public SyllabusSetupListModel()
        { }
        public SyllabusSetupListModel(string _ClassName,int _ClassId)
        {
            this.className = _ClassName;
            this.classId = _ClassId;

        }
        public int syllabusId { get; set; }      
        public int monthId { get; set; }
        public string monthNameDetails { get; set; }
        public int classId { get; set; }
        public string className { get; set; }
        public int subjectId { get; set; }
        public string subjectName { get; set; }
        public int unitId { get; set; }
        public string unitName { get; set; }
        public int chapterId { get; set; }
        public string chapterName { get; set; }
        public int topicId { get; set; }
        public string topicName { get; set; }
        public string activeStatus { get; set; }
        public int createdByUser { get; set; }
        public string createdDate { get; set; }
        public string updatedDate { get; set; }
        public string updatedByUser { get; set; }

    }
    public class SyllabusviewModel
    {


        public string SyllabusId { get; set; } = "0";

        [Required(ErrorMessage = "Please select month name")]
        public string? MonthId { get; set; }
        [Required(ErrorMessage = "Please select class name")]
        public string? ClassId { get; set; }
        [Required(ErrorMessage = "Please subject name")]
        public string? SubjectId { get; set; }
        [Required(ErrorMessage = "Please unit name")]
        public string? UnitId { get; set; }
        [Required(ErrorMessage = "Please chapter name")]
        public string? ChapterId { get; set; }
        [Required(ErrorMessage = "Please topic name")]
        public string? TopicId { get; set; }
    }
    public class ClassName
    {
        public string className { get; set; }
        public int classid { get; set; }
    }
}
