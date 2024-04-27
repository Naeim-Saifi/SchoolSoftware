using AIS.Model;
using AIS.Model.Mapping;
using AIS.Model.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIS.FrontEnd_07062021.Service.Interface
{
    public interface IMapClassWithSubjectService
    {
        Task<IEnumerable<MapClassWithSubjectList>> GetMapClassWithSubjectList( int MapClassWithSubjectId, int Status, string OperationType);
        Task<APIReturnModel> AddUpdateMapClassWithSubject(MapClassWithSubjectModel masterBatchModel);

    }
}
