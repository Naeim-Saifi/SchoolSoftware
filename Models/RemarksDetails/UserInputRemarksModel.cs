using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Models.RemarksDetails
{
    public class UserInputViewRemarksModel
    {
        [Required(ErrorMessage = "Please select your Class.")]
        public string ClassName { get; set; }
        [Required(ErrorMessage = "Please select your Class.")]
        public string SectionName { get; set; }
        [Required(ErrorMessage = "Please select your Class.")]
        public string RemarksType { get; set; }
        [Required(ErrorMessage = "Please select your Class.")]
        public string SubjectName { get; set; }
    }
    public class UserInputRemarksModel
    {
        [Required(ErrorMessage = "Please select your Class.")]
        public int ClassName { get; set; }
        [Required(ErrorMessage = "Please select your Class.")]
        public int SectionName { get; set; }
        [Required(ErrorMessage = "Please select your Class.")]
        public int RemarksType { get; set; }
        [Required(ErrorMessage = "Please select your Class.")]
        public int SubjectName { get; set; }
    }
}
