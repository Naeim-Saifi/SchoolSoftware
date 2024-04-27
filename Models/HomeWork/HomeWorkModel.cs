using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Models.HomeWork
{
    public class HomeWorkModel
    {
        public int HomeWorkID { get; set; }
        public int ClassID { get; set; }
        public int SectionId { get; set; }
        public int SubjectID { get; set; }
        public int UnitId { get; set; }
        public int ChapterId { get; set; }
        public int TopicID { get; set; }
        public int HomeWorkTypeid { get; set; }
        public string HomeWorkTitle { get; set; }
        public string HomeworkDescription { get; set; }
        public int TeacherId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string SchoolCode { get; set; }
        public string FinancialYear { get; set; }      
        public int CreatedByUserId { get; set; }
        public int UpdatedByUserId { get; set; }
        public string OperationType { get; set; }
    }
    public class unitWiseMarksEntryViewModel
    {

        [Required(ErrorMessage = "Please select your Class.")]
        public string ClassName { get; set; }
        [Required(ErrorMessage = "Please select your Section.")]
        public string SectionName { get; set; }
        [Required(ErrorMessage = "Please select your Subject.")]
        public string SubjectName { get; set; }
        [Required(ErrorMessage = "Please select your Unit.")]
        public string UnitName { get; set; }
        [Required(ErrorMessage = "Please select your Chapter.")]
        public string ChapterName { get; set; }
        [Required(ErrorMessage = "Please select your Topic.")]
        public string TopicName { get; set; }
        [Required(ErrorMessage = "Please select your Title.")]
        public string HomeWorkType { get; set; }
        [Required(ErrorMessage = "Please select your Title.")]
        public string HomeWorkTitle { get; set; }
        [Required(ErrorMessage = "Please select your Description.")]
        public string HomeworkDescription { get; set; }
        //[Required(ErrorMessage = "Please select From Date.")]
        //public DateTime? StartDate { get; set; }
       

    }
    public class HomeworkListModel
    {
        public int homeWorkID { get; set; }
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
        public int homeWorkType { get; set; }
        public string homeWorkTypeDetails { get; set; }
        public string homeWorkTitle { get; set; }
        public string homeworkDescription { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }

    }
}
