using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Models.QuestionBank
{
    


    public class QuestionPaperGenerate
    {
        public int QuestionBankId { get; set; } = 0;


        [Required(ErrorMessage = "The Class Name is required.")]
        public string? masterClassId { get; set; }
        [Required(ErrorMessage = "The Subject Name is required.")]
        public string? masterSubjectId { get; set; }
        [Required(ErrorMessage = "The Unit Name is required.")]
        public string? SubjectUnitId { get; set; }

        [Required(ErrorMessage = "The Chapter Name is required.")]
        public string? subjectChapterId { get; set; }

        [Required(ErrorMessage = "The Topic Title is required.")]
        public string? subjectTopicId { get; set; }
        public string? QuestionLevelId { get; set; }
        public string? QuestionTypeId { get; set; }
        public string? ExamTypeId { get; set; }


    }
}
