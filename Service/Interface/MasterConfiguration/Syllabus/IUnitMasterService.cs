using AdminDashboard.Server.Models.MasterConfiguration.UnitMaster;
using AdminDashboard.Server.Models.SyllabusDetails;
using AIS.Data.BaseClass;
using AIS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Service.Interface.MasterConfiguration.Syllabus
{
    public interface IUnitMasterService
    {
        
        Task<APIReturnModel> AddUpdateUnitMaster(MasterUnitAPIModel  unitMasterModel);
        Task<APIReturnModel> AddUpdateChapterMaster(MasterChapterAPIModel chapterMasterModel);
        Task<APIReturnModel> AddUpdateTopicMaster(MasterTopicAPIModel topicMasterModel);

        Task<IEnumerable<MasterUnitListModel>> GetUnitMasterDetails(DefaultInputParameterModel defaultInputParameterModel);
        Task<UnitMasterListDetailsModel> GetUnitMasterDetailsList(string SchoolCode, string FinancialYear, string OperationType);

        Task<IEnumerable<MasterChapterListModel>> GetChapterMasterDetails(DefaultInputParameterModel defaultInputParameterModel);

        Task<IEnumerable<MasterTopicListModel>> GetTopicMasterDetails(DefaultInputParameterModel defaultInputParameterModel);
    }
}
