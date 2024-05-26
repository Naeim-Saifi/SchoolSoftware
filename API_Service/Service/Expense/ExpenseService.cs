using AdminDashboard.Server.API_Service.Interface.Expense;
using AdminDashboard.Server.Service.Interface.LocalStorage;
using AIS.Data.APIReturnModel;
using AIS.Data.Model;
using AIS.Data.RequestResponseModel.Dashboard;
using AIS.Data.RequestResponseModel.Expense;
using AIS.Data.RequestResponseModel.Syllabus;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdminDashboard.Server.API_Service.Service.Expense
{
    public class ExpenseService : IExpenseService
    {

        private readonly HttpClient httpClient;
        private readonly ILocalStorageService _localStorageService;
        public ExpenseService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            _localStorageService = localStorageService;
        }
        public async Task<APIReturnModel> CRUD_AccountType(AccountTypeMasterAPIModel accountTypeMasterAPIModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/Expense/CRUD_AccountType", accountTypeMasterAPIModel);
            return response;
        }
        public async Task<APIReturnModel> CRUD_VendorDetails(VenderMasterAPIModel venderMasterAPIModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/Expense/CRUD_VendorDetails", venderMasterAPIModel);
            return response;
        }
        public async Task<APIReturnModel> CRUD_ExpenseDetails(ExpenseDetailAPIModel expenseDetailAPIModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/Expense/CRUD_ExpenseDetails", expenseDetailAPIModel);
            return response;
        }
         
        public async Task<IEnumerable<AccountTypeListModel>> Get_AccountType_List(AccountType_Input_Para_Model accountType_Input_Para_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/Expense/Get_AccountType_List?SchoolCode={accountType_Input_Para_Model.schoolCode}" +
                    $"&classId={accountType_Input_Para_Model.accountId}" +                    
                    $"&FinancialYear={accountType_Input_Para_Model.financialYear}" +
                    $"&reportType={accountType_Input_Para_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<AccountTypeListModel[]>(el.GetRawText());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }

        public async Task<IEnumerable<VenderMasterListModel>> Get_VendorDetails_List(Vendor_Input_Para_Model vendor_Input_Para_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/Expense/Get_VendorDetails_List?SchoolCode={vendor_Input_Para_Model.schoolCode}" +
                    $"&vendorId={vendor_Input_Para_Model.vendorId}" +
                     
                    $"&FinancialYear={vendor_Input_Para_Model.financialYear}" +
                    $"&reportType={vendor_Input_Para_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<VenderMasterListModel[]>(el.GetRawText());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }

        public async Task<IEnumerable<ExpenseDetailListModel>> Get_ExpenseDetails_List(Expense_Input_Para_Model Expense_Input_Para_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/Expense/Get_ExpenseDetails_List?SchoolCode={Expense_Input_Para_Model.schoolCode}" +
                    $"&classId={Expense_Input_Para_Model.fromDate}" +
                    $"&toDate={Expense_Input_Para_Model.toDate}" +
                    $"&ExpenseId={Expense_Input_Para_Model.ExpenseId}" +
                    $"&PaymentAccountID={Expense_Input_Para_Model.paymentAccountID}" +
                    $"&FinancialYear={Expense_Input_Para_Model.financialYear}" +
                    $"&reportType={Expense_Input_Para_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<ExpenseDetailListModel[]>(el.GetRawText());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }

        public async Task<IEnumerable<AccountBillDetailsList>> Get_AccountWiseBillList(AccountBill_Input_Para_Model accountBill_Input_Para_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/Expense/Get_AccountWiseBillList?SchoolCode={accountBill_Input_Para_Model.schoolCode}" +
                    $"&classId={accountBill_Input_Para_Model.fromDate}" +
                    $"&toDate={accountBill_Input_Para_Model.toDate}" +
                    $"&ExpenseId={accountBill_Input_Para_Model.AccountID}" +
                    $"&PaymentAccountID={accountBill_Input_Para_Model.InvoiceID}" +
                    $"&FinancialYear={accountBill_Input_Para_Model.financialYear}" +
                    $"&reportType={accountBill_Input_Para_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<AccountBillDetailsList[]>(el.GetRawText());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }

        public async Task<APIReturnModel> CRUD_AccountBillDetails(CobmineBillDetailsInputModel cobmineBillDetailsInputModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/Expense/CRUD_AccountBillDetails", cobmineBillDetailsInputModel);
            return response;
        }
        public async Task<BillDetailsOutPutModel> Get_AccountBillList(AccountBill_Input_Para_Model accountBill_Input_Para_Model)
        {

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var r = await httpClient.GetJsonAsync<Response>($"/api/Expense/Get_AccountBillList?SchoolCode={accountBill_Input_Para_Model.schoolCode}" +
                $"&FinancialYear={accountBill_Input_Para_Model.financialYear}" +
                $"&fromdate={accountBill_Input_Para_Model.fromDate}" +
                $"&endate={accountBill_Input_Para_Model.toDate}" +
                $"&accountID={accountBill_Input_Para_Model.AccountID}" +
                $"&InvoiceID={accountBill_Input_Para_Model.InvoiceID}" +
                $"&ReportType={accountBill_Input_Para_Model.reportType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

            if (r.IsError == false)
            {
                return System.Text.Json.JsonSerializer.Deserialize(el.GetRawText(), typeof(BillDetailsOutPutModel)) as BillDetailsOutPutModel;

            }
            else
            {
                return null;
            }

        }

        public async Task<APIReturnModel> CRUD_VendorPaymentDetails(AccountPaymentAPIModel accountPaymentAPIModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/Expense/CRUD_VendorPaymentDetails", accountPaymentAPIModel);
            return response;
        }

        public async Task<APIReturnModel> CRUD_AccountWisePaymentDetails(AccountWisePaymentAPIModel accountPaymentAPIModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/Expense/CRUD_AccountWisePaymentDetails", accountPaymentAPIModel);
            return response;
        }


    }
}
