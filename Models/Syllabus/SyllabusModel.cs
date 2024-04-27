using System.ComponentModel.DataAnnotations;

namespace AdminDashboard.Server.Models.SyllabusDetails
{
    public class MasterUnitViewModel
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
    public class MasterUnitAPIModel
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
    public class MasterUnitListModel
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

    public class MasterChapterViewModel
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
    public class MasterChapterAPIModel
    {
        public int subjectChapterId { get; set; } = 0;
        public int subjectUnitId { get; set; }
        public string schoolCode { get; set; }
        public string financialYear { get; set; }
        public string chapterTitle { get; set; }
        public string chapterDescription { get; set; }
        public int activeStatus { get; set; }
        public int createdByUserId { get; set; }
        public int updatedByUserId { get; set; }
        public string operationType { get; set; }
    }
    public class MasterChapterListModel
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

    public class MasterTopicViewModel
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

    public class MasterTopicAPIModel
    {
        public int subjectTopicId { get; set; }
        public string schoolCode { get; set; }
        public string financialYear { get; set; }
        public int? subjectChapterId { get; set; }
        public string? topicTitle { get; set; }
        public string? topicDescription { get; set; }

        public int? activeStatus { get; set; }
        public int? createdByUserId { get; set; }
        public int? updatedByUserId { get; set; }
        public string operationType { get; set; }
    }
    public class MasterTopicListModel
    {
        public string schoolCode { get; set; }
        public int masterTopicId { get; set; } = 0;

        public int masterClassId { get; set; }
        
        public string className { get; set; }
        public int masterSectionId { get; set; }
        public string sectionName { get; set; }
        public int masterSubjectId { get; set; }
        
        public string subjectName { get; set; }
        public int subjectUnitId { get; set; }
        
        public string unitTitle { get; set; }
        public string unitDescription { get; set; }
        public int subjectChapterId { get; set; }
        
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
}
