using AdminDashboard.Server.Service.Interface.Razorpay;
using AIS.Model;
using AIS.Model.PaymentGatewaySetup;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Service.Implementation.Razorpay
{
    public class RazorpaygetwayService : IRazorpaygetwayService
    {
        private readonly HttpClient httpClient;

        public RazorpaygetwayService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        //public async Task<IEnumerable<ClassMasterListModel>> GetClassMasterList(string SchoolCode, int SchoolId, int ClassMasterId, int Status, string OperationType)
        //{
        //    //ClassMasterList[] ClassMasterList;
        //    JsonConvert.DefaultSettings = () => new JsonSerializerSettings
        //    {
        //        ContractResolver = new CamelCasePropertyNamesContractResolver()
        //    };
        //    var r = await httpClient.GetJsonAsync<Response>($"/api/ClassMaster/GetClassMasterList?SchoolCode={SchoolCode}&SchoolId={SchoolId}&ClassMasterId={ClassMasterId}&Status={Status}&OperationType={OperationType}");
        //    JsonElement el;
        //    var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

        //    if (r.isError == false)
        //    {
        //        return System.Text.Json.JsonSerializer.Deserialize<ClassMasterListModel[]>(el.GetRawText());
        //    }
        //    else
        //    {
        //        return null;
        //    }

        //}

        public async Task<APIReturnModel> AddUpdateRazorpaygetwayDetail(PaymentGatewayModel paymentGatewayModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/RazorpaygetwayDetail/AddUpdateRazorpaygetwayDetail", paymentGatewayModel);
            return response;

        }
    }
}
