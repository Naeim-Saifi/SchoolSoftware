using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Models.Dashboard.Management
{
    public class ClassWiseStudentCountModel
    {
        public string className { get; set; }
        public long studentCount { get; set; }
    }
    public class ClassandSectionWiseStudentCountModel
    {
        public string className { get; set; }
        public string sectionName { get; set; }
        public long studentCount { get; set; }
    }
    public class GenderwiseStudentCountModel
    {
        public string genderName { get; set; }
        public long studentCount { get; set; }
    }
}
