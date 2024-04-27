using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Models.MasterConfiguration.SubjectMaster
{
    public class MapsubjectwithClassModel
    {
        public int mapsubjectwithclassId { get; set; }
        public int masterClassId { get; set; }
        public int masterSubjectId { get; set; }
        public string className { get; set; }
        public string subjectName { get; set; }
        public string subjectCode { get; set; }
        public string marksType { get; set; }
        public string marksTypeDescription { get; set; }
        public string subjectType { get; set; }
        public string subjectTypeDescription { get; set; }
        public int displayOrder { get; set; }
        public int activeStatus { get; set; }
        public string activeStatusDetails { get; set; }
        public int createdByUserId { get; set; }
        public int updatedByUserId { get; set; }
        public string createdDate { get; set; }
        public string updatedDate { get; set; }
    }
}
