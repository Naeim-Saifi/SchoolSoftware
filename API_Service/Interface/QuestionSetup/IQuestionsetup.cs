using AIS.Data.APIReturnModel;
using AIS.Data.Model;
using AIS.Data.RequestResponseModel.QuestionSetUp;
using AIS.Data.RequestResponseModel.Syllabus;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminDashboard.Server.API_Service.Interface.QuestionSetup
{
    public interface IQuestionsetup
    { 
        Task<APIReturnModel> CRUD_QuestionBank(QuestionBankAPIModel questionBankAPIModel);
        Task<IEnumerable<QuestionBankListModel>> GET_QuestionList(QuestionBankList_ParaModel questionBankList_ParaModel);
        ///Task<ReturnModel> CRUD_QuestionTypeMaster(QuestionTypeMasterAPIModel questionTypeMasterAPIModel);
        Task<APIReturnModel> CRUD_SelectedQuestionPaper(List<SelectedQuestionListAPIModel>  selectedQuestionListModels);
        /// <summary>
        /// This class performs an important function.
        /// 
        ///Task<ReturnModel> CRUD_QuestionBank(QuestionBankAPIModel questionBankAPIModel);
        ///Task<ReturnModel> CRUD_QuestionTypeMaster(QuestionTypeMasterAPIModel questionTypeMasterAPIModel);
        ///Task<ReturnModel> GET_QuestionList(QuestionBankList_ParaModel questionBankList_ParaModel);
        ///Task<ReturnModel> CRUD_SelectedQuestionPaper(List<SelectedQuestionListAPIModel> selectedQuestionListModels);
        /// </summary>


        Task<IEnumerable<QuestionPaperListOutPutModel>> GET_QuestionPaperList(QuestionPaperList_ParaModel questionPaperList_ParaModel);
        Task<IEnumerable<QuestionPaperDetailsListOutPutModel>> GET_QuestionPaperDetailsList(QuestionPaperList_ParaModel questionPaperList_ParaModel);


    }
}
