using AdminDashboard.Server.Models.SyllabusSetup;
using AIS.Data.BaseClass;
using AIS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Service.Interface.SyllabusSetup
{
       public interface ISyllabusSetupService
        {
            Task<APIReturnModel> AddUpdateSyllabusSetup(SyllabusSetupAPIModel  syllabusSetupModel);
            Task<IEnumerable<SyllabusSetupListModel>> GetSyllabusSetupDetails(DefaultInputParameterModel defaultInputParameterModel);
        }
}
