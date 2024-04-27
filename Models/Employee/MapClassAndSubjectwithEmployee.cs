using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Models.Employee
{
    public class MapClassAndSubjectwithEmployeeViewModel
    {
        [Required(ErrorMessage = "The teacher is required.")]
        public int TeacherId { get; set; }
        [Required(ErrorMessage = "The teacher is required.")]
        public int ClassId { get; set; }
        [Required(ErrorMessage = "The teacher is required.")]
        public int SectionID { get; set; }
        [Required(ErrorMessage = "The teacher is required.")]
        public int SubjectID { get; set; }
    }
}
