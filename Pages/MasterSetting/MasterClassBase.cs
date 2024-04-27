using AIS.FrontEnd_07062021.Service.Interface;
using AIS.Model.MasterData;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AIS.Model;
using AIS.Model.GeneralConversion;
using Microsoft.AspNetCore.Components.Forms;
using AIS.Model.UserLogin;

namespace AdminDashboard.Server.Pages.MasterSetting
{
    public class MasterClassListBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }

        public SessionModel sessionModel { get; }
        [Inject]
        public IMasterClassService masterClassService { get; set; }

        public List<MasterClassListModel> masterClassListModel = new List<MasterClassListModel>();
        public MasterClassModel masterClassModel { get; set; }

        public SessionModel _sessionModel;
        protected override async Task OnInitializedAsync()
        {
            masterClassModel = new MasterClassModel();
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
           
            await LoadClassData();

        }
        private async Task LoadClassData()
        {
            
            masterClassListModel = (await masterClassService.GetMasterClassList(_sessionModel.SchoolCode, _sessionModel.FinancialYear,_sessionModel.SchoolId, 0, 0, ReportType.GetBySchoolId)).ToList();
        }

        public string _searchTerm = "";
        public IEnumerable<MasterClassListModel> MasterClassList() => masterClassListModel.Where(e => e.name.Contains(_searchTerm));

        public MasterClassListModel selectedItem = new MasterClassListModel();

        //for sorting
        public bool enabled = true;
        int MasterId = 0;
        public string ActionName = "Save";
        [Inject]
        public ISnackbar snackBar { get; set; }

        public bool success;

        //for validation editform data
        public void OnValidSubmit()
        {
            success = true;
            SaveClass();
            masterClassModel = new MasterClassModel();
            LoadClassData();
            // StateHasChanged();
        }

        public bool visible;
        public string StatusValue { get; set; }
        //both side data binding editform using object 
        //public MasterClassModel masterClassModel = new MasterClassModel();
        public void SaveClass()

        {
            if (MasterId == 0)
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
        private async Task Save()
        {

            if (StatusValue.ToString() == "0" || StatusValue.ToString() == "")
            {
                snackBar.Add("Please Select Status data not save", Severity.Info);

            }
            else
            {
                //selectUserStatus = GenericClass.StatusConversion(status);
                masterClassModel.ActiveStatus = GenericClass.StatusConversion(StatusValue.ToString());
                masterClassModel.CreatedByUserId = _sessionModel.UserId;
                masterClassModel.MasterClassId = 0;
                masterClassModel.SchoolId = _sessionModel.SchoolId;
                masterClassModel.SchoolCode = _sessionModel.SchoolCode;
                masterClassModel.OperationType = OperationAction.Add;

                string msg = (await masterClassService.AddUpdateMasterClass(masterClassModel)).message.ToString();

                if (msg == "MasterClass Details already exist")
                {
                    snackBar.Add(msg, Severity.Error);
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);


                    //masterClassModel.Name = string.Empty;
                    //masterClassModel.DisplayOrder = 0;
                }

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
                masterClassModel.ActiveStatus = GenericClass.StatusConversion(StatusValue.ToString()); ;
                masterClassModel.CreatedByUserId = _sessionModel.UserId;
                masterClassModel.MasterClassId = MasterId;
                masterClassModel.SchoolId = _sessionModel.SchoolId;
                masterClassModel.SchoolCode = _sessionModel.SchoolCode;
                masterClassModel.OperationType = OperationAction.Update;

                string msg = (await masterClassService.AddUpdateMasterClass(masterClassModel)).message.ToString();

                if (msg == "MasterClass Details already exist")
                {
                    snackBar.Add(msg, Severity.Error);
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);
                    masterClassModel.Name = string.Empty;
                    masterClassModel.DisplayOrder = 0;
                    ActionName = "Save";
                    MasterId = 0;
                }

            }
        }
        private async Task Delete(int masterClassId)
        {
            if (masterClassId > 0)
            {
                masterClassModel.ActiveStatus = 4;
                masterClassModel.CreatedByUserId = _sessionModel.UserId;
                masterClassModel.MasterClassId = masterClassId;
                masterClassModel.SchoolId = _sessionModel.SchoolId;
                masterClassModel.SchoolCode = _sessionModel.SchoolCode;
                masterClassModel.Name = "Delete";
                masterClassModel.OperationType = OperationAction.Delete;
                string msg = (await masterClassService.AddUpdateMasterClass(masterClassModel)).message.ToString();

                if (msg == "MasterClass Details already exist")
                {
                    snackBar.Add(msg, Severity.Error);
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);
                    masterClassModel.Name = string.Empty;
                    masterClassModel.DisplayOrder = 0;
                    ActionName = "Save";
                    MasterId = 0;
                    LoadClassData();
                }

            }
        }
        public MudMessageBox mbox { get; set; }
        string state = "Message box hasn't been opened yet";
        public async Task DeleteRecord(int masterClassId)
        {
            bool? result = await mbox.Show();
            state = result == null ? "Cancelled" : "Deleted!";
            if (state == "Deleted!")
            {
                await Delete(masterClassId);
            }

        }
        public async Task Edit(int ClassId)
        {
            string _StatusValue = "";
            ActionName = "Update";
            MasterId = ClassId;
            List<MasterClassListModel> _masterClassListModel = new List<MasterClassListModel>();
            _masterClassListModel = (await masterClassService.GetMasterClassList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.SchoolId, MasterId, 0, ReportType.GetByMasterId)).ToList();

            foreach (var _MasterClass in _masterClassListModel)
            {
                masterClassModel.Name = _MasterClass.name;
                masterClassModel.Code = _MasterClass.code;
                masterClassModel.DisplayOrder = _MasterClass.displayOrder;
                StatusValue = _MasterClass.activeStatusDetails.ToString();
               // GenericClass.Status StatusValue = (GenericClass.Status)Enum.Parse(typeof(GenericClass.Status), _MasterClass.activeStatusDetails, true);
            }


        }
        public void Cancel()
        {
            ActionName = "Save";
            MasterId = 0;
            MasterClassModel masterClassModel = null;
        }

    }
}
