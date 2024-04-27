using AIS.Model;
using AIS.Model.Academic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIS.FrontEnd_07062021.Service.Interface
{
    public interface IMasterChapterService
    {
        Task<IEnumerable<MasterChapterListModel>> GetMasterChapterList(int SchoolId, int MasterChapterId, int Status, string OperationType);
        Task<APIReturnModel> AddUpdateMasterChapter(MasterChapterModel MasterChapterModel);

    }
}
