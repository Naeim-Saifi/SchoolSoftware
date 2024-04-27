using AIS.Model;
using AIS.Model.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIS.FrontEnd_07062021.Service.Interface
{
    public interface IMasterBatchService
    {
        Task<IEnumerable<MasterBatchListModel>> GetMasterBatchList(string SchoolCode,int SchoolId,int MasterBatchId,int Status,string OperationType);
        Task<APIReturnModel>AddUpdateMasterBatch(MasterBatchModel masterBatchModel);
        
    }
}
