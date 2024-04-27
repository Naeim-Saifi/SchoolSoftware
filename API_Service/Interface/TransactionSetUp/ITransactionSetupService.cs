using AIS.Data.APIReturnModel;
using AIS.Data.Model;
using AIS.Data.RequestResponseModel.TransactionSetUp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminDashboard.Server.API_Service.Interface.TransactionSetUp
{ 
    public interface ITransactionSetUpService
    {

        Task<IEnumerable<Transaction_List_Output_Model>> GET_Transaction_List(Transaction_List_Input_Para_Model transaction_List_Input_Para_Model);
        Task<IEnumerable<Transaction_Header_List_Output_Model>> GET_TransactionHeader_List(Transaction_Header_List_Input_Para_Model transaction_Header_List_Input_Para_Model);
        Task<IEnumerable<UserWiseFeeCollection>> Get_UserWiseFeeCollection(Transaction_List_Input_Para_Model transaction_List_Input_Para_Model);
        Task<IEnumerable<PaymentModeFeeCollection>> Get_PaymentModeFeeCollection(Transaction_List_Input_Para_Model transaction_List_Input_Para_Model);
        Task<IEnumerable<Pending_Fee_List_Output_Model>> GET_PendingFee_List(Pending_Fee_List_Input_Para_Model pending_Fee_List_Input_Para_Model);
        Task<IEnumerable<MonthWise_TransactionDetails_LIST_Output_Model>> GET_Transaction_Details_MonthWise_List(TransactionDetails_Input_Para_Model transactionDetails_LIST_Model);
        Task<IEnumerable<ClassWise_TransactionDetails_LIST_Output_Model>> GET_Transaction_Details_ClassWise_List(TransactionDetails_Input_Para_Model transactionDetails_LIST_Model);
        Task<IEnumerable<PaymentMode_TransactionDetails_LIST_Output_Model>> GET_Transaction_Details_PaymentMode_List(TransactionDetails_Input_Para_Model transactionDetails_LIST_Model);
        Task<IEnumerable<MonthWiseMode_TransactionDetails_LIST_Output_Model>> GET_Transaction_Details_MonthWiseMode_List(TransactionDetails_Input_Para_Model transactionDetails_LIST_Model);
        Task<IEnumerable<MonthWise_PendingFee_LIST_Output_Model>> GET_PendingFee_MonthWise_List(Generic_PendingFee_Input_Para_Model generic_PendingFee_Input_Para_Model);
        Task<IEnumerable<ClassWise_PendingFee_LIST_Output_Model>> GET_PendingFee_ClassWise_List(Generic_PendingFee_Input_Para_Model generic_PendingFee_Input_Para_Model);
        Task<IEnumerable<BusStopWise_PendingFee_LIST_Output_Model>> GET_PendingFee_BustStopWise_List(Generic_PendingFee_Input_Para_Model generic_PendingFee_Input_Para_Model);
        Task<TodayTransactionStatus> GET_TodayTransactionStatus(Transaction_List_Input_Para_Model transaction_List_Input_Para_Model);
        Task<APIReturnModel> CRUD_PendingFeeFollowUpDetails(PendingFeeFollowUpDetailsAPIModel pendingFeeFollowUpDetailsAPIModel);
        Task<IEnumerable<PendingFeeFollowUpDetailsListModel>> GET_PendingFeeFollowUpDetails_List(PendingFeeFollowUpDetails_Input_Para_Model pendingFeeFollowUpDetails_Input_Para_Model);
    }
}

