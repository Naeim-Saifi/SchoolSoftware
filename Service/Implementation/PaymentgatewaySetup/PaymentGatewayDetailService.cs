using AdminDashboard.Server.Service.Interface.PaymentDetail;
using AIS.Model;
using AIS.Model.PaymentGatewaySetup;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Service.Implementation.PaymentgatewaySetup
{
    public class PaymentGatewayDetailService : IPaymentGatewayDetailService
    {
        private readonly HttpClient httpClient;

        public PaymentGatewayDetailService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<PaymentGatewayListModel>> GetPaymentGatewayList(string SchoolCode, int SchoolId, int paymentGatewayDetailId, int Status, string OperationType)
        {
            //PaymentGatewayDetailList[] PaymentGatewayDetailList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var r = await httpClient.GetJsonAsync<Response>($"/api/RazorpaygetwayDetail/GetRazorpaygetwayDetailList?SchoolCode={SchoolCode}&SchoolId={SchoolId}&PId={1}&Status={123}&OperationType={OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                return System.Text.Json.JsonSerializer.Deserialize<PaymentGatewayListModel[]>(el.GetRawText());
            }
            else
            {
                return null;
            }

        }

        public async Task<APIReturnModel> AddUpdatePaymentGatewayDetail(PaymentGatewayModel paymentGatewayModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/RazorpaygetwayDetail/AddUpdateRazorpaygetwayDetail", paymentGatewayModel);
            return response;

        }

        
    }
}
