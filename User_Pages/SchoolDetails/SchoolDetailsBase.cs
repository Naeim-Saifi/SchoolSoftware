using AdminDashboard.Server.API_Service.Interface.QuestionSetup;
using AdminDashboard.Server.API_Service.Interface.SchoolDetails;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.QuestionSetUp;
using AIS.Data.RequestResponseModel.School;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AIS.Data.GeneralConversion.GeneralConversion;

namespace AdminDashboard.Server.User_Pages.SchoolDetails
{
    public class SchoolDetailsBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        public ISchoolDetailsService schoolDetailsService { get; set; }

        public SchoolDetailsViewModel schoolDetailsViewModel = new SchoolDetailsViewModel();
        public List<SchoolDetailsListModel> schoolDetailsListModels = new List<SchoolDetailsListModel>();

        public SessionModel sessionModel { get; }
        public SessionModel _sessionModel;
        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");

            SchoolDetails_Input_Para_Model schoolDetails_Input_Para_Model = new SchoolDetails_Input_Para_Model()
            {
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.All


            };

            schoolDetailsListModels = (await schoolDetailsService.GET_School_List(schoolDetails_Input_Para_Model)).ToList();

            if (schoolDetailsListModels.Count > 0)
            {
                //(author.Address != null) ? author.Address : "Unknown";
                //schoolDetailsViewModel.schoolRecognizeDetails = schoolDetailsListModels[0].schoolRecognizeDetails.ToString();

                schoolDetailsViewModel.schoolId = (schoolDetailsListModels[0].schoolId != null) ? schoolDetailsListModels[0].schoolId : 0;
                schoolDetailsViewModel.schoolName = (schoolDetailsListModels[0].schoolName != null) ? schoolDetailsListModels[0].schoolName.ToString() : "NA";
                schoolDetailsViewModel.schoolRecognizeDetails = (schoolDetailsListModels[0].schoolRecognizeDetails != null) ? schoolDetailsListModels[0].schoolRecognizeDetails.ToString() : "NA";
                schoolDetailsViewModel.chairmanName = (schoolDetailsListModels[0].chairmanName != null) ? schoolDetailsListModels[0].chairmanName.ToString() : "NA";
                schoolDetailsViewModel.chairmanContactNo = (schoolDetailsListModels[0].chairmanContactNo != null) ? schoolDetailsListModels[0].chairmanContactNo.ToString() : "NA";
                schoolDetailsViewModel.mailID = (schoolDetailsListModels[0].mailID != null) ? schoolDetailsListModels[0].mailID.ToString() : "NA";

                schoolDetailsViewModel.sMSAPIKey = (schoolDetailsListModels[0].sMSAPIKey != null) ? schoolDetailsListModels[0].sMSAPIKey.ToString() : "NA";
                schoolDetailsViewModel.sMSUSERID = (schoolDetailsListModels[0].sMSUSERID != null) ? schoolDetailsListModels[0].sMSUSERID.ToString() : "NA";
                schoolDetailsViewModel.fromMailIDPassword = (schoolDetailsListModels[0].fromMailIDPassword != null) ? schoolDetailsListModels[0].fromMailIDPassword.ToString() : "NA";
                schoolDetailsViewModel.sMSSenderID = (schoolDetailsListModels[0].sMSSenderID != null) ? schoolDetailsListModels[0].sMSSenderID.ToString() : "NA";
               
                
                schoolDetailsViewModel.sMSSenderID = (schoolDetailsListModels[0].sMSSenderID != null) ? schoolDetailsListModels[0].sMSSenderID.ToString() : "NA";
                schoolDetailsViewModel.sMSUSERID = (schoolDetailsListModels[0].sMSUSERID != null) ? schoolDetailsListModels[0].sMSUSERID.ToString() : "NA";
                schoolDetailsViewModel.sMSPassword = (schoolDetailsListModels[0].sMSPassword != null) ? schoolDetailsListModels[0].sMSPassword.ToString() : "NA";
                schoolDetailsViewModel.sMSAPIKey = (schoolDetailsListModels[0].sMSAPIKey != null) ? schoolDetailsListModels[0].sMSAPIKey.ToString() : "NA";
                schoolDetailsViewModel.fromMailId = (schoolDetailsListModels[0].fromMailId != null) ? schoolDetailsListModels[0].fromMailId.ToString() : "NA";
                schoolDetailsViewModel.fromMailIDPassword = (schoolDetailsListModels[0].fromMailIDPassword != null) ? schoolDetailsListModels[0].fromMailIDPassword.ToString() : "NA";
                schoolDetailsViewModel.onlinePaymentLink = (schoolDetailsListModels[0].onlinePaymentLink != null) ? schoolDetailsListModels[0].onlinePaymentLink.ToString() : "NA";
                schoolDetailsViewModel.schoolAddress1 = (schoolDetailsListModels[0].schoolAddress1 != null) ? schoolDetailsListModels[0].schoolAddress1.ToString() : "NA";

                schoolDetailsViewModel.schoolAddress1 = (schoolDetailsListModels[0].schoolAddress1 != null) ? schoolDetailsListModels[0].schoolAddress1.ToString() : "NA";
                schoolDetailsViewModel.schoolAddress2 = (schoolDetailsListModels[0].schoolAddress2 != null) ? schoolDetailsListModels[0].schoolAddress2.ToString() : "NA";
                schoolDetailsViewModel.pinCode = (schoolDetailsListModels[0].pinCode != null) ? schoolDetailsListModels[0].pinCode.ToString() : "NA";
                schoolDetailsViewModel.bankName = (schoolDetailsListModels[0].bankName != null) ? schoolDetailsListModels[0].bankName.ToString() : "NA";
                schoolDetailsViewModel.iFSCCode = (schoolDetailsListModels[0].iFSCCode != null) ? schoolDetailsListModels[0].iFSCCode.ToString() : "NA";
                schoolDetailsViewModel.accountNo = (schoolDetailsListModels[0].accountNo != null) ? schoolDetailsListModels[0].accountNo.ToString() : "NA";
                schoolDetailsViewModel.bankAddress = (schoolDetailsListModels[0].bankAddress != null) ? schoolDetailsListModels[0].bankAddress.ToString() : "NA";
                schoolDetailsViewModel.whatsApp_UserID = (schoolDetailsListModels[0].whatsApp_UserID != null) ? schoolDetailsListModels[0].whatsApp_UserID.ToString() : "NA";
                schoolDetailsViewModel.whatsApp_Password = (schoolDetailsListModels[0].whatsApp_Password != null) ? schoolDetailsListModels[0].whatsApp_Password.ToString() : "NA";
                schoolDetailsViewModel.whatsApp_APIKey = (schoolDetailsListModels[0].whatsApp_APIKey != null) ? schoolDetailsListModels[0].whatsApp_APIKey.ToString() : "NA";

            }
        }
        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                SchoolDetailsAPIModel schoolDetailsAPIModel = new SchoolDetailsAPIModel()
                {
                    schoolId = schoolDetailsViewModel.schoolId,
                    schoolRecognizeDetails = schoolDetailsViewModel.schoolRecognizeDetails,
                    chairmanName = schoolDetailsViewModel.chairmanName,
                    chairmanContactNo = schoolDetailsViewModel.chairmanContactNo,
                    mailID = schoolDetailsViewModel.mailID,
                    sMSSenderID = schoolDetailsViewModel.sMSSenderID,
                    sMSAPIKey = schoolDetailsViewModel.sMSAPIKey,
                    sMSPassword = schoolDetailsViewModel.sMSPassword,
                    sMSUSERID = schoolDetailsViewModel.sMSUSERID,
                    accountNo = schoolDetailsViewModel.accountNo,
                    bankAddress = schoolDetailsViewModel.bankAddress,
                    bankName = schoolDetailsViewModel.bankName,
                    fromMailId = schoolDetailsViewModel.fromMailId,
                    fromMailIDPassword = schoolDetailsViewModel.fromMailIDPassword,
                    iFSCCode = schoolDetailsViewModel.iFSCCode,
                    onlinePaymentLink = schoolDetailsViewModel.onlinePaymentLink,
                    pinCode = schoolDetailsViewModel.pinCode,
                    schoolAddress1 = schoolDetailsViewModel.schoolAddress1,
                    schoolAddress2 = schoolDetailsViewModel.schoolAddress2,
                    stateId = Convert.ToInt16(schoolDetailsViewModel.stateId),
                    whatsApp_APIKey = schoolDetailsViewModel.whatsApp_APIKey,
                    whatsApp_Password = schoolDetailsViewModel.whatsApp_Password,
                    whatsApp_UserID = schoolDetailsViewModel.whatsApp_UserID,
                    CreatedByUserId = _sessionModel.UserId,
                    OperationType = "Update",
                    FinancialYear = _sessionModel.FinancialYear,                     
                    SchoolCode = _sessionModel.SchoolCode,
                    schoolName = schoolDetailsViewModel.schoolName,
                    UpdatedByUserId = _sessionModel.UserId,
                 
                };
                SchoolDetails(schoolDetailsAPIModel);
            }
        }
        [Inject]
        public ISnackbar snackBar { get; set; }
        public APIReturnModel aPIReturnModel;
        private async void SchoolDetails(SchoolDetailsAPIModel schoolDetailsAPIModel)
        {
            try
            {
                if (schoolDetailsAPIModel.OperationType != "NA")
                {
                    aPIReturnModel = (await schoolDetailsService.CRUD_SchoolDetails(schoolDetailsAPIModel));

                    if (aPIReturnModel.status == false)
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Success);
                       
                        ClearData();
                        StateHasChanged();

                    }
                    else
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Error);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void ClearData()
        {

            SchoolDetailsViewModel schoolDetailsViewModel = null;

        }
        public class StateMaster
        {
            public int Id { get; set; }
            public string Text { get; set; }
        }
        public List<StateMaster> stateMaster = new List<StateMaster>()
        {
        new StateMaster(){ Id= 1, Text= "Andhra Pradesh" },
        new StateMaster(){ Id= 2, Text= "Arunachal Pradesh" },
        new StateMaster(){ Id= 3, Text= "Assam" },
        new StateMaster(){ Id= 4, Text= "Bihar" },
        new StateMaster(){ Id= 5, Text= "Chhattisgarh" },
        new StateMaster(){ Id= 6, Text= "Goa" },
        new StateMaster(){ Id= 7, Text= "Gujarat" },
        new StateMaster(){ Id= 8, Text= "Haryana" },
        new StateMaster(){ Id= 9, Text= "Himachal Pradesh" },
        new StateMaster(){ Id= 10, Text= "Jharkhand" },
        new StateMaster(){ Id= 11, Text= "Karnataka" },
        new StateMaster(){ Id= 12, Text= "Kerala" },
        new StateMaster(){ Id= 13, Text= "Madhya Pradesh" },
        new StateMaster(){ Id= 14, Text= "Maharashtra" },
        new StateMaster(){ Id= 15, Text= "Manipur" },
        new StateMaster(){ Id= 16, Text= "Meghalaya" },
        new StateMaster(){ Id= 17, Text= "Mizoram" },
        new StateMaster(){ Id= 18, Text= "Nagaland" },
        new StateMaster(){ Id= 19, Text= "Odisha" },
        new StateMaster(){ Id= 20, Text= "Punjab" },
        new StateMaster(){ Id= 21, Text= "Rajasthan" },
        new StateMaster(){ Id= 22, Text= "Sikkim" },
        new StateMaster(){ Id= 23, Text= "Tamil Nadu" },
        new StateMaster(){ Id= 24, Text= "Telangana" },
        new StateMaster(){ Id= 25, Text= "Tripura" },
        new StateMaster(){ Id= 26, Text= "Uttar Pradesh" },
        new StateMaster(){ Id= 27, Text= "Uttarakhand" },
        new StateMaster(){ Id= 28, Text= "West Bengal" },

        };

    }
}