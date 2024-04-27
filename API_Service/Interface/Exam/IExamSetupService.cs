using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.ExamMasterSetup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminDashboard.Server.API_Service.Interface.Exam
{
    public interface IExamMasterSetupService
    {
        Task<APIReturnModel> CRUD_ExamTypeMaster(Exam_Type_Master_Model exam_Type_Master_Model);
        Task<APIReturnModel> CRUD_ExamScheduleDetails(Exam_schedule_Details_Model exam_schedule_Details_Model);
        Task<APIReturnModel> CRUD_UnitWiseMarksEntry(List<UnitWiseMarksEntryAPIModel> unitWiseMarksEntryAPIModel);

        Task<APIReturnModel> CRUD_TermWiseExamSchedule(List<Exam_schedule_Details_Model> exam_schedule_Details_Model);
        Task<IEnumerable<Exam_Type_List_Output_Model>> GET_Exam_Type_MasterLIST(Exam_Type_List_Input_Model exam_Type_List_Input_Model);
        Task<UnitWiseMarksDetails> GET_UnitWiseMarksEntry(UnitWiseMasrks_Input_Model  unitWiseMasrks_Input_Model);
        Task<IEnumerable<Exam_Schedule_Details_List_Output_Model>> GET_Exam_Schedule_Details_LIST(Exam_Schedule_Details_List_Input_Model exam_Schedule_Details_List_Input_Model);
        Task<IEnumerable<TermWiseExamSchedule_List_Output_Model>> GET_TermWiseExamSchedule_Details_LIST(Exam_Schedule_Details_List_Input_Model exam_Schedule_Details_List_Input_Model);
        Task<string> GetGradeDetails(decimal obtainMarks);
    }
}
