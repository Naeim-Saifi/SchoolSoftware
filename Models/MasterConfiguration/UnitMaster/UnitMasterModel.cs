using AdminDashboard.Server.Models.MasterConfiguration.SubjectMaster;
using AIS.Model.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Models.MasterConfiguration.UnitMaster
{
   
    public class UnitMasterViewModel
    {
        public int? subjectUnitId { get; set; } = 0;

        [Required(ErrorMessage = "The Class Name is required.")]
        public string masterClassId { get; set; }
        [Required(ErrorMessage = "The Subject Name is required.")]
        public string masterSubjectId { get; set; }
        [Required(ErrorMessage = "The Unit Name is required.")]
        public string? unitTitle { get; set; }
        public string? unitDescription { get; set; }
    }
    public class UnitMasterModel
    {
        public int? subjectUnitId { get; set; } = 0;
        public string schoolCode { get; set; }
        public string FinancialYear { get; set; }
        public int masterClassId { get; set; }
        public int masterSubjectId { get; set; }
        public string? unitTitle { get; set; }
        public string? unitDescription { get; set; }

        public int? activeStatus { get; set; }
        public int? createdByUserId { get; set; }
        public int updatedByUserId { get; set; }
        public string operationType { get; set; }
    }
    public class UnitMasterListModel
    {
       
        public int masterClassId { get; set; }      
        public string className { get; set; }
        public int masterSubjectId { get; set; }
        public string subjectName { get; set; }
        public int subjectUnitId { get; set; }
        public string unitTitle { get; set; }
        public string unitDescription { get; set; }
        public string activeStatus { get; set; }
        public string activeStatusDetails { get; set; }
        public string createdDate { get; set; }
        public string updatedDate { get; set; }

    }

    public class ChapterMasterViewModel
    {
        public int? SubjectChapterId { get; set; } = 0;

        [Required(ErrorMessage = "The Class Name is required.")]
        public string? masterClassId { get; set; }
        [Required(ErrorMessage = "The Subject Name is required.")]
        public string? masterSubjectId { get; set; }
        [Required(ErrorMessage = "The Unit Name is required.")]
        public string? SubjectUnitId { get; set; }
        
        [Required(ErrorMessage = "The Chapter Name is required.")]
        public string? ChapterTitle { get; set; }
        public string ChapterDescription { get; set; }
    }
    public class ChapterMasterModel
    {
        public int SubjectChapterId { get; set; } = 0;
        public int SubjectUnitId { get; set; }
        public string SchoolCode { get; set; }
        public string FinancialYear { get; set; }
        public string ChapterTitle { get; set; }
        public string ChapterDescription { get; set; }
        public int ActiveStatus { get; set; }
        public int CreatedByUserId { get; set; }
        public int UpdatedByUserId { get; set; }
        public string OperationType { get; set; }
    }
    public class ChapterMasterListModel
    {
        public string schoolCode { get; set; }
        public int masterClassId { get; set; }
        public string className { get; set; }
        public int masterSubjectId { get; set; }
        public string subjectName { get; set; }
        public int subjectUnitId { get; set; }
        public string unitTitle { get; set; }
        public string unitDescription { get; set; }
        public int subjectChapterId { get; set; }
        public string chapterTitle { get; set; }
        public string chapterDescription { get; set; }
        public int activeStatus { get; set; }
        public string activeStatusDetails { get; set; }
        public int createdByUserId { get; set; }
        public int updatedByUserId { get; set; }
        public string createdDate { get; set; }
        public string updatedDate { get; set; }

    }

    public class TopicMasterViewModel
    {
        public int subjectTopicId { get; set; } = 0;

        [Required(ErrorMessage = "The Class Name is required.")]
        public string? masterClassId { get; set; }
        [Required(ErrorMessage = "The Subject Name is required.")]
        public string? masterSubjectId { get; set; }
        [Required(ErrorMessage = "The Unit Name is required.")]
        public string? SubjectUnitId { get; set; }

        [Required(ErrorMessage = "The Chapter Name is required.")]
        public string? subjectChapterId { get; set; }

        [Required(ErrorMessage = "The Topic Title is required.")]
        public string? subjectTopicTitle { get; set; }
        public string? subjectTopicDescription { get; set; }

    }

    public class TopicMasterModel
    {
        public int SubjectTopicId { get; set; }
        public string SchoolCode { get; set; }
        public string FinancialYear { get; set; }
        public int? SubjectChapterId { get; set; }
        public string? TopicTitle { get; set; }
        public string? TopicDescription { get; set; }

        public int? ActiveStatus { get; set; }
        public int? CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public string OperationType { get; set; }
    }
    public class TopicMasterListModel
    {
        public string schoolCode { get; set; }
        public int masterTopicId { get; set; } = 0;
      
        public int masterClassId { get; set; }
        [Required(ErrorMessage = "The Class Name is required.")]
        public string className { get; set; }
        public int masterSectionId { get; set; }
        public string sectionName { get; set; }
        public int masterSubjectId { get; set; }
        [Required(ErrorMessage = "The Subject Name is required.")]
        public string subjectName { get; set; }
        public int subjectUnitId { get; set; }
        [Required(ErrorMessage = "The Unit Name is required.")]
        public string unitTitle { get; set; }
        public string unitDescription { get; set; }
        public int subjectChapterId { get; set; }
        [Required(ErrorMessage = "The chapter name is required.")]
        public string chapterTitle { get; set; }
        public string chapterDescription { get; set; }
        public int subjectTopicId { get; set; }
        public string topicTitle { get; set; }
        public string topicDescription { get; set; }
        public int activeStatus { get; set; }
        public string activeStatusDetails { get; set; }
        public int createdByUserId { get; set; }
        public int updatedByUserId { get; set; }
        public string createdDate { get; set; }
        public string updatedDate { get; set; }

    }

    public class UnitMasterListDetailsModel
    {
        //public List<UnitDetails> unitDetails { get; set; }
        public UnitDetails unitDetails { get; set; }
        public List<MasterClassListModel> masterClassListModel { get; set; }
        public List<MapsubjectwithClassModel> mapsubjectwithClassModel { get; set; }

    }
    public class UnitDetails
    {
        public int? masterClassId { get; set; }
        public string? className { get; set; }
        public int? masterSubjectId { get; set; }
        public string? subjectName { get; set; }
        public int? subjectUnitId { get; set; }
        public string? unitTitle { get; set; }
        public string? unitDescription { get; set; }
        public string? activeStatus { get; set; }
        public string? activeStatusDetails { get; set; }
        public string? createdDate { get; set; }
        public string? updatedDate { get; set; }
    }


}
