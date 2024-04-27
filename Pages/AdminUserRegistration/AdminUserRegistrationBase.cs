using AdminDashboard.Server.Models.UserRegistraion;
using AdminDashboard.Server.Service.Interface.AdminUserRegistration;
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorInputFile;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using AdminDashboard.Server.Models;

namespace AdminDashboard.Server.Pages.AdminUserRegistration
{
    public class AdminUserRegistrationBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }

        //databinding model
        public string userRole { get; set; }
        private Stream _fileStream = null;
        private string _selectedFileName = null; 
        private readonly IWebHostEnvironment _webHostEnvironment;

        string Message = "No file(s) selected";
        IReadOnlyList<IBrowserFile> selectedFiles;
        public AdminUserRegistrationModel userRegistrationModel { get; set; }
        [Inject]
        public IAdminUserRegistrationService _adminUserRegistrationService { get; set; }


        public SessionModel _sessionModel;

       public List<AdminUserRegistrationListModel> adminUserRegistrationListModel = new List<AdminUserRegistrationListModel>();

        public string _searchTerm = "";
        public IEnumerable<AdminUserRegistrationListModel> adminUserList() => adminUserRegistrationListModel.Where(e => e.loginUserId.Contains(_searchTerm));
        public AdminUserRegistrationListModel selectedItem = new AdminUserRegistrationListModel();
        protected override async Task OnInitializedAsync()
        {
            userRegistrationModel = new AdminUserRegistrationModel();
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");

            await LoadPaymentGatewayList();

        }

        private async Task LoadPaymentGatewayList()
        {

            adminUserRegistrationListModel = (await _adminUserRegistrationService.GetAdminUserRegistrationList(_sessionModel.SchoolCode, _sessionModel.SchoolId, 0, 0, ReportType.All)).ToList();
           

        }

        //public string _searchTerm = "";
        //public IEnumerable<PaymentGatewayListModel> paymentGatewayList() => paymentGatewayListModel.Where(e => e.contactName.Contains(_searchTerm));


        //public PaymentGatewayListModel selectedItem = new PaymentGatewayListModel();
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

            userRegistrationModel.ActiveStatus = GenericClass.StatusConversion(StatusValue1.ToString());
            userRegistrationModel.UserRoleName = userRole.ToString();
            userRegistrationModel.UserRoleId = GenericClass.UserRoleConversion(userRole.ToString());
            userRegistrationModel.CreatedByUserId = _sessionModel.UserId;
            userRegistrationModel.SchoolId = _sessionModel.SchoolId;
            userRegistrationModel.SchoolCode = _sessionModel.SchoolCode;
            userRegistrationModel.ProfileImage = _fileStream;
            if (userRegistrationModel.UserId > 0)

                userRegistrationModel.OperationType = OperationAction.Update;
            else

                userRegistrationModel.OperationType = OperationAction.Add;
            //========================================
            foreach (var file in selectedFiles)
            {
                Stream stream = file.OpenReadStream();
                MemoryStream ms = new MemoryStream();
                await stream.CopyToAsync(ms);
                stream.Close();

                //UploadedFile uploadedFile = new UploadedFile();
                userRegistrationModel.FileName = file.Name;
                userRegistrationModel.FileContent = ms.ToArray();
                ms.Close();
                string msg1 = (await _adminUserRegistrationService.AddUpdateAdminUserRegistration(userRegistrationModel)).message.ToString();
                
            }
            this.StateHasChanged();
            //===================
            string msg = (await _adminUserRegistrationService.AddUpdateAdminUserRegistration(userRegistrationModel)).message.ToString();

            if (msg == "Class & Section Details already exist")
            {
                snackBar.Add(msg, Severity.Error);
            }
            else
            {
                snackBar.Add(msg, Severity.Success);
                userRegistrationModel = null;
                //ClassMasterModel.Name = string.Empty;
                //ClassMasterModel.DisplayOrder = 0;
            }

        }

        //private async Task Save()
        //{

        //    //selectUserStatus = GenericClass.StatusConversion(status);

        //    userRegistrationModel.ActiveStatus = GenericClass.StatusConversion(StatusValue1.ToString());
        //    userRegistrationModel.UserRoleName = userRole.ToString();
        //    userRegistrationModel.UserRoleId = GenericClass.UserRoleConversion(userRole.ToString());
        //    userRegistrationModel.CreatedByUserId = _sessionModel.UserId;
        //    userRegistrationModel.SchoolId = _sessionModel.SchoolId;
        //    userRegistrationModel.SchoolCode = _sessionModel.SchoolCode;
        //    userRegistrationModel.ProfileImage = _fileStream;
        //    if(userRegistrationModel.UserId>0)

        //        userRegistrationModel.OperationType = OperationAction.Update;
        //    else

        //        userRegistrationModel.OperationType = OperationAction.Add;
        //    string msg = (await _adminUserRegistrationService.AddUpdateAdminUserRegistration(userRegistrationModel)).message.ToString();

        //    if (msg == "Class & Section Details already exist")
        //    {
        //        snackBar.Add(msg, Severity.Error);
        //    }
        //    else
        //    {
        //        snackBar.Add(msg, Severity.Success);
        //        userRegistrationModel = null;
        //        //ClassMasterModel.Name = string.Empty;
        //        //ClassMasterModel.DisplayOrder = 0;
        //    }

        //}
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
            Id = Id;
            //_sessionModel.SchoolCode, _sessionModel.SchoolId, GatewayDetailId, 0, ReportType.GetByMasterId
            List<AdminUserRegistrationListModel> adminUserRegistrationListModel = new List<AdminUserRegistrationListModel>();
            adminUserRegistrationListModel = (await _adminUserRegistrationService.GetAdminUserRegistrationList(_sessionModel.SchoolCode, _sessionModel.SchoolId, Id, 0, ReportType.GetByMasterId)).ToList();

            foreach (var _adminUserRegistrationListModel in adminUserRegistrationListModel)
            {
                userRegistrationModel.UserId = _adminUserRegistrationListModel.id;
                userRegistrationModel.UserName = _adminUserRegistrationListModel.loginUserId;
                userRegistrationModel.FirstName = _adminUserRegistrationListModel.firstName;
                userRegistrationModel.MiddleName = _adminUserRegistrationListModel.middleName;
                userRegistrationModel.LastName = _adminUserRegistrationListModel.lastName;
                userRegistrationModel.UserRoleName = _adminUserRegistrationListModel.userRoleName;
                userRegistrationModel.Email = _adminUserRegistrationListModel.email;
                StatusValue1 = _adminUserRegistrationListModel.activeStatusDetails.ToString();
                userRole = _adminUserRegistrationListModel.userRoleName.ToString();
                //GenericClass.Status StatusValue = (GenericClass.Status)Enum.Parse(typeof(GenericClass.Status), _ClassMaster.activeStatusDetails, true);
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
            //ActionName = "Save";
            //// MasterId = 0;
            //PaymentGatewayModel paymentGatewayModel = null;
        }
        public MudMessageBox mbox { get; set; }
        string state = "Message box hasn't been opened yet";

        string status;

        public async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            selectedFiles = e.GetMultipleFiles();
            Message = $"{selectedFiles.Count} file(s) selected";
            this.StateHasChanged();
        }




    }
}
