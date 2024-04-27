using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.Expense;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminDashboard.Server.API_Service.Interface.Expense
{
    public interface IExpenseService
    {

        Task<APIReturnModel> CRUD_AccountType(AccountTypeMasterAPIModel accountTypeMasterAPIModel);
        Task<APIReturnModel> CRUD_VendorDetails(VenderMasterAPIModel venderMasterAPIModel);
        Task<APIReturnModel> CRUD_ExpenseDetails(ExpenseDetailAPIModel expenseDetailAPIModel);
        Task<IEnumerable<AccountTypeListModel>> Get_AccountType_List(AccountType_Input_Para_Model accountType_Input_Para_Model);
        Task<IEnumerable<VenderMasterListModel>> Get_VendorDetails_List(Vendor_Input_Para_Model vendor_Input_Para_Model);
        Task<IEnumerable<ExpenseDetailListModel>> Get_ExpenseDetails_List(Expense_Input_Para_Model Expense_Input_Para_Model);
        Task<APIReturnModel> CRUD_AccountBillDetails(CobmineBillDetailsInputModel cobmineBillDetailsInputModel);
        Task<BillDetailsOutPutModel> Get_AccountBillList(AccountBill_Input_Para_Model  accountBill_Input_Para_Model);
        Task<IEnumerable<AccountBillDetailsList>> Get_AccountWiseBillList(AccountBill_Input_Para_Model accountBill_Input_Para_Model);

        Task<APIReturnModel> CRUD_VendorPaymentDetails(AccountPaymentAPIModel accountPaymentAPIModel);
        Task<APIReturnModel> CRUD_AccountWisePaymentDetails(AccountWisePaymentAPIModel accountPaymentAPIModel);

    }
}
