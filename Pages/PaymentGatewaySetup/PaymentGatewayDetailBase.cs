using AdminDashboard.Server.Service.Interface.PaymentDetail;
using AdminDashboard.Server.Service.Interface.Razorpay;
using AIS.Model.GeneralConversion;
using AIS.Model.PaymentGatewaySetup;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.PaymentGatewaySetup
{
    public class PaymentGatewayDetailBase:ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        
        //databinding model
        
        public PaymentGatewayModel paymentGatewayModel { get; set; }
        [Inject]
        public IPaymentGatewayDetailService _paymentGatewayDetailService { get; set; }

        public SessionModel _sessionModel;

        public List<PaymentGatewayListModel> paymentGatewayListModel = new List<PaymentGatewayListModel>();

        protected override async Task OnInitializedAsync()
        {
            paymentGatewayModel = new PaymentGatewayModel();
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");

            await LoadPaymentGatewayList();

        }

        private async Task LoadPaymentGatewayList()
        {

            //paymentGatewayListModel = (await _paymentGatewayDetailService.GetPaymentGatewayList(_sessionModel.SchoolCode, _sessionModel.SchoolId, 0, 0, "GetBySchoolId")).ToList();
            paymentGatewayListModel = (await _paymentGatewayDetailService.GetPaymentGatewayList(_sessionModel.SchoolCode, _sessionModel.SchoolId, 0, 0, ReportType.All)).ToList();
            
        }

        public string _searchTerm = "";
        public IEnumerable<PaymentGatewayListModel> paymentGatewayList() => paymentGatewayListModel.Where(e => e.businessName.Contains(_searchTerm));
        

        public PaymentGatewayListModel selectedItem = new PaymentGatewayListModel();
        public bool enabled = true;
        int ID = 0;
        public string ActionName = "Save";
        [Inject]
        public ISnackbar snackBar { get; set; }

        public bool success;
        public void OnValidSubmit()
        {
            success = true;
            SaveClass();
            //PaymentGatewayDetailModel = new PaymentGatewayDetailModel();
            LoadPaymentGatewayList();
            StateHasChanged();
        }

        public bool visible;
        public string StatusValue { get; set; }
        public void SaveClass()

        {
            if (ID == 0)
            {
                //Save Record
                Save();
            }
            else
            {
                Update();
            }
            _searchTerm = "";


        }
        //for sorting

        private async Task Save()
        {

                //selectUserStatus = GenericClass.StatusConversion(status);

                paymentGatewayModel.ActiveStatus = 1;
                paymentGatewayModel.CreatedByUserId = _sessionModel.UserId;
                paymentGatewayModel.SchoolId = _sessionModel.SchoolId;
                paymentGatewayModel.SchoolCode = _sessionModel.SchoolCode;
                paymentGatewayModel.OperationType = OperationAction.Add;
                string msg = (await _paymentGatewayDetailService.AddUpdatePaymentGatewayDetail(paymentGatewayModel)).message.ToString();

                if (msg == "Class & Section Details already exist")
                {
                    snackBar.Add(msg, Severity.Error);
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);


                    //ClassMasterModel.Name = string.Empty;
                    //ClassMasterModel.DisplayOrder = 0;
                }

        }
        private async Task Update()
        {

            if (StatusValue.ToString() == "0" || StatusValue.ToString() == "")
            {
                snackBar.Add("Please Select Status data not save", Severity.Info);

            }
            else
            {
                //paymentGatewayModel.ActiveStatus = 1;
                //paymentGatewayModel.CreatedByUserId = _sessionModel.UserId;
                //paymentGatewayModel.id = ID;
                //classMasterModel.SchoolId = _sessionModel.SchoolId;
                //classMasterModel.SchoolCode = _sessionModel.SchoolCode;
                //classMasterModel.OperationType = OperationAction.Update;

                //string msg = (await ClassMasterService.AddUpdateClassMaster(classMasterModel)).message.ToString();

                //if (msg == "ClassMaster Details already exist")
                //{
                //    snackBar.Add(msg, Severity.Error);
                //}
                //else
                //{
                //    snackBar.Add(msg, Severity.Success);
                //    classMasterModel.ClassName = string.Empty;
                //    classMasterModel.DisplayOrder = 0;
                //    ActionName = "Save";
                //    MasterId = 0;
                //}

            }
        }
        public string StatusValue1 { get; set; }
        int GatewayDetailId = 0;
        public async Task Edit(int Id)
        {
            string _StatusValue = "";
            ActionName = "Update";
            GatewayDetailId = Id;
            List<PaymentGatewayListModel> paymentGatewayListModel = new List<PaymentGatewayListModel>();
            paymentGatewayListModel = (await _paymentGatewayDetailService.GetPaymentGatewayList(_sessionModel.SchoolCode, _sessionModel.SchoolId, GatewayDetailId, 0, ReportType.GetByMasterId)).ToList();

            foreach (var _paymentGatewayListModel in paymentGatewayListModel)
            {
                paymentGatewayModel.ActiveStatus = _paymentGatewayListModel.activeStatus;
                paymentGatewayModel.Api_Key = _paymentGatewayListModel.api_Key;
                paymentGatewayModel.BusinessName = _paymentGatewayListModel.businessName;
                paymentGatewayModel.BusinessType = _paymentGatewayListModel.businessType;
                paymentGatewayModel.ContactEmail = _paymentGatewayListModel.contactEmail;
                paymentGatewayModel.ContactName = _paymentGatewayListModel.contactName;
                paymentGatewayModel.ContactNo = _paymentGatewayListModel.contactNo;
                paymentGatewayModel.Secret_Key = _paymentGatewayListModel.secret_Key;
                paymentGatewayModel.WebsiteUrl = _paymentGatewayListModel.websiteUrl;
                StatusValue = _paymentGatewayListModel.activeStatusDetails.ToString();
                // GenericClass.Status StatusValue = (GenericClass.Status)Enum.Parse(typeof(GenericClass.Status), _ClassMaster.activeStatusDetails, true);
            }


        }
        public async Task DeleteRecord(int id)
        {
            //if (ClassMasterId > 0)
            //{
            //    classMasterModel.ActiveStatus = 4;
            //    classMasterModel.CreatedByUserId = _sessionModel.UserId;
            //    classMasterModel.ClassId = ClassMasterId;
            //    classMasterModel.SchoolId = _sessionModel.SchoolId;
            //    classMasterModel.SchoolCode = _sessionModel.SchoolCode;
            //    classMasterModel.ClassName = "Delete";
            //    classMasterModel.OperationType = OperationAction.Delete;
            //    string msg = (await ClassMasterService.AddUpdateClassMaster(classMasterModel)).message.ToString();

            //    if (msg == "ClassMaster Details already exist")
            //    {
            //        snackBar.Add(msg, Severity.Error);
            //    }
            //    else
            //    {
            //        snackBar.Add(msg, Severity.Success);
            //        classMasterModel.ClassName = string.Empty;
            //        classMasterModel.DisplayOrder = 0;
            //        ActionName = "Save";
            //        MasterId = 0;
            //        LoadClassData();
            //    }

            //}
        }
        public void Cancel()
        {
            ActionName = "Save";
           // MasterId = 0;
            PaymentGatewayModel paymentGatewayModel = null;
        }
        public MudMessageBox mbox { get; set; }
        string state = "Message box hasn't been opened yet";
       
       
    }
}
