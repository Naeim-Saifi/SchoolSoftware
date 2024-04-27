using AdminDashboard.Server.Models.MasterConfiguration.SubjectMaster;
using AIS.Model.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Models.MasterConfiguration.SyllabusMaster
{
    public class UnitMasterListModel
    {
        public int subjectUnitId { get; set; }
        public MasterClassListModel masterClassListModel { get; set; }
        //public int masterClassId { get; set; }
        //public string className { get; set; }
        public MapsubjectwithClassModel mapsubjectwithClassModel { get; set; }
        //public int masterSubjectId { get; set; }
        //public string subjectName { get; set; }
       
        public string unitTitle { get; set; }
        public string unitDescription { get; set; }
        public string activeStatus { get; set; }
        public string activeStatusDetails { get; set; }
        public string createdDate { get; set; }
        public string updatedDate { get; set; }

    }
}
