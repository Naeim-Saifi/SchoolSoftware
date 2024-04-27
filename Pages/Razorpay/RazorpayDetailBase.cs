using AIS.Model.Razorpay;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.Razorpay
{
    public class RazorpayDetailBase: ComponentBase
    {
        public RazorpayDetailModel razorpayDetailModel { get; set; }
        [Inject]
        public IConfiguration _iConfig { get; set; }

        protected override async Task OnInitializedAsync()
        {
            razorpayDetailModel = new RazorpayDetailModel();
            //_sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            //await LoadBatchData();
        }
        [Inject]
        NavigationManager navigationManager { get; set; }
        public async Task<string> OnValidSubmit()
        {
           
            string qstring="Address="+ razorpayDetailModel.Address+"&&Email="+ razorpayDetailModel.Email+ "&&Amount=" + razorpayDetailModel.DueAmount+ "&&Name=" + razorpayDetailModel.Name+ "&&PhoneNumber=" + razorpayDetailModel.PhoneNumber+ "&&studentId=" + razorpayDetailModel.studentId+ "&&schoolCode=" + razorpayDetailModel.schoolCode+ "&&SchoolId=" + razorpayDetailModel.SchoolId+ "&&LoginId=" + razorpayDetailModel.LoginId; 
            string Payment_Url = _iConfig.GetSection("PayUrl").GetSection("PaymentUrl").Value + "?" + qstring;
            navigationManager.NavigateTo(Payment_Url);
            //await js.InvokeVoidAsync("dotNetToJsSamples.printDotNetObject", codeBlock, merchantOrder);
            //navigationManager.NavigateTo("https://localhost:44307/?"+ qstring);
            return qstring;
            StateHasChanged();
        }
        public void Cancel()
        {
            //ActionName = "Save";
            //MasterId = 0;
            //MasterBatchModel masterBatchModel = null;
        }
    }
}
