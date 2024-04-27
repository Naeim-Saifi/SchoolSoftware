using AdminDashboard.Server.API_Service.Interface.TransactionSetUp;
using AdminDashboard.Server.Service.Interface.LocalStorage;
using AIS.Data.APIReturnModel;
using AIS.Data.Model;
using AIS.Data.RequestResponseModel.TransactionSetUp;

using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdminDashboard.Server.API_Service.Service.TransactionSetUp
{
    public class TransactionSetupService : ITransactionSetUpService
    {
        private readonly HttpClient httpClient;
        private ILocalStorageService _localStorageService;
        public TransactionSetupService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public async Task<IEnumerable<Transaction_List_Output_Model>> GET_Transaction_List(Transaction_List_Input_Para_Model transaction_List_Input_Para_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/TransactionSetUp/GET_Transaction_List?SchoolCode={transaction_List_Input_Para_Model.schoolCode}" +
                    $"&FromDate={transaction_List_Input_Para_Model.fromDate}" +
                    $"&ToDate={transaction_List_Input_Para_Model.toDate}" +
                     $"&userId={transaction_List_Input_Para_Model.userId}" +
                    $"&FinancialYear={transaction_List_Input_Para_Model.financialYear}" +
                    $"&ReportType={transaction_List_Input_Para_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<Transaction_List_Output_Model[]>(el.GetRawText());
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


        public async Task<IEnumerable<UserWiseFeeCollection>> Get_UserWiseFeeCollection(Transaction_List_Input_Para_Model transaction_List_Input_Para_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
              
                var r = await httpClient.GetJsonAsync<Response>($"/api/TransactionSetUp/Get_UserWiseFeeCollection?SchoolCode={transaction_List_Input_Para_Model.schoolCode}" +
                    $"&FromDate={transaction_List_Input_Para_Model.fromDate}" +
                    $"&ToDate={transaction_List_Input_Para_Model.toDate}" +
                     $"&userId={transaction_List_Input_Para_Model.userId}" +                     
                    $"&FinancialYear={transaction_List_Input_Para_Model.financialYear}" +
                    $"&ReportType={transaction_List_Input_Para_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<UserWiseFeeCollection[]>(el.GetRawText());
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
        public async Task<IEnumerable<PaymentModeFeeCollection>> Get_PaymentModeFeeCollection(Transaction_List_Input_Para_Model transaction_List_Input_Para_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/TransactionSetUp/Get_PaymentModeFeeCollection?SchoolCode={transaction_List_Input_Para_Model.schoolCode}" +
                    $"&fromDate={transaction_List_Input_Para_Model.fromDate}" +
                    $"&toDate={transaction_List_Input_Para_Model.toDate}" +
                     $"&userId={transaction_List_Input_Para_Model.userId}" +
                    $"&FinancialYear={transaction_List_Input_Para_Model.financialYear}" +
                    $"&reportType={transaction_List_Input_Para_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<PaymentModeFeeCollection[]>(el.GetRawText());
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

        public async Task<IEnumerable<Transaction_Header_List_Output_Model>> GET_TransactionHeader_List(Transaction_Header_List_Input_Para_Model transaction_Header_List_Input_Para_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/TransactionSetUp/GET_TransactionHeader_List?SchoolCode={transaction_Header_List_Input_Para_Model.schoolCode}" +
                    $"&fromDate={transaction_Header_List_Input_Para_Model.fromDate}" +
                    $"&toDate={transaction_Header_List_Input_Para_Model.toDate}" +
                    $"&userId={transaction_Header_List_Input_Para_Model.userId}" +
                    $"&FinancialYear={transaction_Header_List_Input_Para_Model.financialYear}" +
                    $"&reportType={transaction_Header_List_Input_Para_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<Transaction_Header_List_Output_Model[]>(el.GetRawText());
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

        public async Task<IEnumerable<Pending_Fee_List_Output_Model>> GET_PendingFee_List(Pending_Fee_List_Input_Para_Model pending_Fee_List_Input_Para_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/TransactionSetUp/GET_PendingFee_List?SchoolCode={pending_Fee_List_Input_Para_Model.schoolCode}" +
                     $"&ClassId={pending_Fee_List_Input_Para_Model.classId}" +
                      $"&SectionId={pending_Fee_List_Input_Para_Model.sectionId}" +
                       $"&userId={pending_Fee_List_Input_Para_Model.userId}" +
                    $"&FinancialYear={pending_Fee_List_Input_Para_Model.financialYear}" +
                    $"&reportType={pending_Fee_List_Input_Para_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<Pending_Fee_List_Output_Model[]>(el.GetRawText());
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
        public async Task<IEnumerable<MonthWise_TransactionDetails_LIST_Output_Model>> GET_Transaction_Details_MonthWise_List(TransactionDetails_Input_Para_Model transactionDetails_LIST_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/TransactionSetUp/GET_TransactionDetails_LIST?SchoolCode={transactionDetails_LIST_Model.schoolCode}" +
                    $"&FinancialYear={transactionDetails_LIST_Model.financialYear}" +
                    $"&reportType={transactionDetails_LIST_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<MonthWise_TransactionDetails_LIST_Output_Model[]>(el.GetRawText());
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

        public async Task<IEnumerable<ClassWise_TransactionDetails_LIST_Output_Model>> GET_Transaction_Details_ClassWise_List(TransactionDetails_Input_Para_Model transactionDetails_LIST_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/TransactionSetUp/GET_TransactionDetails_LIST?SchoolCode={transactionDetails_LIST_Model.schoolCode}" +
                    $"&FinancialYear={transactionDetails_LIST_Model.financialYear}" +
                    $"&reportType={transactionDetails_LIST_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<ClassWise_TransactionDetails_LIST_Output_Model[]>(el.GetRawText());
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

        public async Task<IEnumerable<PaymentMode_TransactionDetails_LIST_Output_Model>> GET_Transaction_Details_PaymentMode_List(TransactionDetails_Input_Para_Model transactionDetails_LIST_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/TransactionSetUp/GET_TransactionDetails_LIST?SchoolCode={transactionDetails_LIST_Model.schoolCode}" +
                    $"&FinancialYear={transactionDetails_LIST_Model.financialYear}" +
                    $"&reportType={transactionDetails_LIST_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<PaymentMode_TransactionDetails_LIST_Output_Model[]>(el.GetRawText());
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
        public async Task<IEnumerable<MonthWiseMode_TransactionDetails_LIST_Output_Model>> GET_Transaction_Details_MonthWiseMode_List(TransactionDetails_Input_Para_Model transactionDetails_LIST_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/TransactionSetUp/GET_TransactionDetails_LIST?SchoolCode={transactionDetails_LIST_Model.schoolCode}" +
                    $"&FinancialYear={transactionDetails_LIST_Model.financialYear}" +
                    $"&reportType={transactionDetails_LIST_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<MonthWiseMode_TransactionDetails_LIST_Output_Model[]>(el.GetRawText());
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


        public async Task<IEnumerable<MonthWise_PendingFee_LIST_Output_Model>> GET_PendingFee_MonthWise_List(Generic_PendingFee_Input_Para_Model generic_PendingFee_Input_Para_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/TransactionSetUp/Generic_PendingFee_List?SchoolCode={generic_PendingFee_Input_Para_Model.schoolCode}" +
                    $"&FinancialYear={generic_PendingFee_Input_Para_Model.financialYear}" +
                    $"&reportType={generic_PendingFee_Input_Para_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<MonthWise_PendingFee_LIST_Output_Model[]>(el.GetRawText());
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

        public async Task<IEnumerable<ClassWise_PendingFee_LIST_Output_Model>> GET_PendingFee_ClassWise_List(Generic_PendingFee_Input_Para_Model generic_PendingFee_Input_Para_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/TransactionSetUp/Generic_PendingFee_List?SchoolCode={generic_PendingFee_Input_Para_Model.schoolCode}" +
                    $"&FinancialYear={generic_PendingFee_Input_Para_Model.financialYear}" +
                    $"&reportType={generic_PendingFee_Input_Para_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<ClassWise_PendingFee_LIST_Output_Model[]>(el.GetRawText());
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

        public async Task<IEnumerable<BusStopWise_PendingFee_LIST_Output_Model>> GET_PendingFee_BustStopWise_List(Generic_PendingFee_Input_Para_Model generic_PendingFee_Input_Para_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/TransactionSetUp/Generic_PendingFee_List?SchoolCode={generic_PendingFee_Input_Para_Model.schoolCode}" +
                    $"&FinancialYear={generic_PendingFee_Input_Para_Model.financialYear}" +
                    $"&reportType={generic_PendingFee_Input_Para_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<BusStopWise_PendingFee_LIST_Output_Model[]>(el.GetRawText());
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


        public async Task<TodayTransactionStatus> GET_TodayTransactionStatus(Transaction_List_Input_Para_Model transaction_List_Input_Para_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/TransactionSetUp/GET_TodayTransactionStatus?SchoolCode={transaction_List_Input_Para_Model.schoolCode}" +
                    $"&fromDate={transaction_List_Input_Para_Model.fromDate}" +
                    $"&toDate={transaction_List_Input_Para_Model.toDate}" +
                     $"&userId={transaction_List_Input_Para_Model.userId}" +
                    $"&FinancialYear={transaction_List_Input_Para_Model.financialYear}" +
                    $"&reportType={transaction_List_Input_Para_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    //return System.Text.Json.JsonSerializer.Deserialize<TodayTransactionStatus[]>(el.GetRawText());
                    return System.Text.Json.JsonSerializer.Deserialize(el.GetRawText(), typeof(TodayTransactionStatus)) as TodayTransactionStatus;
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

       
        public async Task<IEnumerable<PendingFeeFollowUpDetailsListModel>> GET_PendingFeeFollowUpDetails_List(PendingFeeFollowUpDetails_Input_Para_Model pendingFeeFollowUpDetails_Input_Para_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/TransactionSetUp/GET_PendingFeeFollowUpDetails_List?SchoolCode={pendingFeeFollowUpDetails_Input_Para_Model.schoolCode}" +
                     $"&studentID={pendingFeeFollowUpDetails_Input_Para_Model.studentID}" +
                     $"&userRoleId={pendingFeeFollowUpDetails_Input_Para_Model.userRoleId}" +
                      $"&FinancialYear={pendingFeeFollowUpDetails_Input_Para_Model.financialYear}" +
                    
                    $"&reportType={pendingFeeFollowUpDetails_Input_Para_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<PendingFeeFollowUpDetailsListModel[]>(el.GetRawText());
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
        public async Task<APIReturnModel> CRUD_PendingFeeFollowUpDetails(PendingFeeFollowUpDetailsAPIModel pendingFeeFollowUpDetailsAPIModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/TransactionSetUp/CRUD_PendingFeeFollowUpDetails", pendingFeeFollowUpDetailsAPIModel);
            return response;
        }
 
    }
}
