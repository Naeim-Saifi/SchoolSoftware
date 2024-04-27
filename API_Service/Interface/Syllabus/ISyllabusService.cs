using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.Syllabus;
using AIS.Data.RequestResponseModel.SyllabusVideo;
//using AIS.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminDashboard.Server.API_Service.Interface.Syllabus
{
    public interface ISyllabusService
    {
        Task<APIReturnModel> CRUD_MasterUnit(UnitMasterModel unitMasterModel);
        Task<APIReturnModel> CRUD_MasterTopic(TopicMasterModel topicMasterModel);
        Task<APIReturnModel> CRUD_MasterChapter(ChapterMasterModel chapterMasterModel);
        Task<APIReturnModel> CRUD_SyllabusSetup(SyllabusSetupModel syllabusSetupModel);

        Task<APIReturnModel> CRUD_SyllabusVideoDetails(SyllabusVideoAPIModel syllabusVideoAPIModel);


        Task<IEnumerable<Unit_Output_Model>> Get_Unit_List(Unit_Input_Para_Model unit_Input_Para_Model);
        Task<IEnumerable<Chapter_Output_Model>> Get_Chapter_List(Chapter_Input_Para_Model chapter_Input_Para_Model);
        Task<IEnumerable<Topic_Output_Model>> Get_Topic_List(Topic_Input_Para_Model topic_Input_Para_Model);
        Task<IEnumerable<Syllabus_Output_Model>> Get_Syllabus_List(Syllabus_Input_Para_Model syllabus_Input_Para_Model);
        Task<IEnumerable<SyllabusStatus_Output_Model>> Get_SyllabusStatus_List(SyllabusStatus_Input_Para_Model syllabus_Input_Para_Model);

        Task<IEnumerable<SyllabusVideo_Output_Model>> Get_SyllabusVideo_List(SyllabusVideo_Input_Para_Model syllabusVideo_Input_Para_Model);
        Task<List<MonthNameListModel>> GetMonthNameList();


    }
}
