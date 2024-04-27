using AIS.FrontEnd_07062021.Service.Interface;
using AIS.Model.GeneralConversion;
using AIS.Model.SchoolSetup;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.SchoolSetup
{
    public class AddEditSchoolBase:ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel sessionModel { get; }
        [Inject]
        public ISchoolSetupService schoolSetupService { get; set; }

        [Inject]
        public ISnackbar snackBar { get; set; }
        public SessionModel _sessionModel;
        public SchoolModel schoolModel { get; set; }
        public bool success;
        public void OnValidSubmit()
        {
            success = true;
            //SaveBatch();
            //masterBatchModel = new MasterBatchModel();
            //LoadBatchData();
            // StateHasChanged();
        }
        public List<SchoolListModel> schoolListModel = new List<SchoolListModel>();
        protected override async Task OnInitializedAsync()
        {
            schoolModel = new SchoolModel();
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
         

            schoolListModel = (await schoolSetupService.GetMasterSchoolList(2, 0, 0, _sessionModel.SchoolCode, ReportType.GetBySchoolId)).ToList();

            foreach (var _SchoolList in schoolListModel)
            {


                schoolModel.SchoolId = _SchoolList.schoolId;
                schoolModel.SchoolCode = _SchoolList.schoolCode;
                schoolModel.SchoolName = _SchoolList.schoolName;
                schoolModel.SchoolEmail = _SchoolList.schoolEmail;
                schoolModel.RegistrationNumber = _SchoolList.registrationNumber;
                schoolModel.Tagline = _SchoolList.tagline;
                schoolModel.Description = _SchoolList.description;
                schoolModel.Logo = _SchoolList.logo;
                schoolModel.Address1 = _SchoolList.address1;
                schoolModel.Address2 = _SchoolList.address2;
                schoolModel.City = _SchoolList.city;
                schoolModel.StateId = _SchoolList.stateId;
                //schoolModel.stateName = _SchoolList;
                //schoolModel.ountryId = _SchoolList.countryId;
                //schoolModel.country = _SchoolList.;
                schoolModel.PinCode = _SchoolList.pinCode;
                schoolModel.OwnerName = _SchoolList.ownerName;
                schoolModel.OwnerContactNo = _SchoolList.ownerContactNo;
                schoolModel.OwnerEmail = _SchoolList.ownerEmail;
                //schoolModel.genderDetails = _SchoolList.gender;
                //schoolModel.ender = _SchoolList.;
                if (_SchoolList.dob != null)
                {
                    //DateTime dob1 = Convert.ToDateTime(_SchoolList.dob);
                   // schoolModel.DOB = Convert.ToDateTime(_SchoolList.dob);
                }
                schoolModel.UIDType = _SchoolList.uidType;
                schoolModel.Description = _SchoolList.uidTypeDescription;
                schoolModel.UIDNumber = _SchoolList.uidNumber;
                schoolModel.BankName = _SchoolList.bankName;
                schoolModel.IFSCCode = _SchoolList.ifscCode;
                schoolModel.AccountNo = _SchoolList.accountNo;
                schoolModel.BankAddress = _SchoolList.bankAddress;
                schoolModel.PaymentLink = _SchoolList.paymentLink;
                schoolModel.AdditionalInfo = _SchoolList.additionalInfo;
                schoolModel.Rank = _SchoolList.rank;
                schoolModel.OrganizationType = _SchoolList.organizationType;
                schoolModel.AffilatedTo = _SchoolList.affilatedTo;
                schoolModel.Tahsheel = _SchoolList.tahsheel;
                schoolModel.SMSUserID = _SchoolList.smsUserID;
                schoolModel.SMSPassword = _SchoolList.smsPassword;
                schoolModel.SMSAPIKey = _SchoolList.smsapiKey;
                schoolModel.SMSSenderID = _SchoolList.smsSenderID;
                schoolModel.FromMailId = _SchoolList.fromMailId;
                schoolModel.MailUserId = _SchoolList.mailUserId;
                schoolModel.MailPassword = _SchoolList.mailPassword;

            }
            //await LoadBatchData();

        }
        public string StatusValue { get; set; }
        public string StateId { get; set; }
        public string CountryId { get; set; }
        public void Cancel()
        {
            //ActionName = "Save";
            //MasterId = 0;
            //MasterBatchModel masterBatchModel = null;
        }

      public  IList<IBrowserFile> files = new List<IBrowserFile>();
        public void UploadFiles(InputFileChangeEventArgs e)
        {
            foreach (var file in e.GetMultipleFiles())
            {
                files.Add(file);
            }
            //TODO upload the files to the server
        }


        public void UpdateSchoolDetaild()
        {
            try
            {
                schoolModel.UpdatedByUserId = _sessionModel.UserId;
                schoolModel.OperationType = OperationAction.Update;
                schoolSetupService.AddUpdateMasterSchool(schoolModel);
            }
            catch (Exception ex)
            {
                snackBar.Add(ex.Message, Severity.Error);
            }

        }
    }
}
