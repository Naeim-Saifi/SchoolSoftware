using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Models.MasterConfiguration.SubjectMaster
{
    public class MasterSubjectListModel
    {
        public int masterSubjectId { get; set; }
        public string subjectName { get; set; }
        public string subjectCode { get; set; }
        public string marksType { get; set; }

        public string marksTypeDescription { get; set; }
        public string subjectType { get; set; }
        public string subjectTypeDescription { get; set; }
        public int DisplayOrder { get; set; }
        public int ActiveStatus { get; set; }
        public string activeStatusDetails { get; set; }
        public int createdByUserId { get; set; }
        public string createdByUser { get; set; }
        public string createdDate { get; set; }

        public string updatedByUserId { get; set; }
        public string updatedByUser { get; set; }
        public string updatedDate { get; set; }
    }
}
