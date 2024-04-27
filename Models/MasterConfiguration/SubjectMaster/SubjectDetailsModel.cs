using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Models.MasterConfiguration.SubjectMaster
{
    public class SubjectDetailsModel
    {
        public List<MasterSubjectListModel> masterSubjectListModels { get; set; }
        public List<MapsubjectwithClassModel> mapsubjectwithClassModels { get; set; }
    }
}
