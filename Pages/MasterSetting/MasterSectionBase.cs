using AIS.FrontEnd_07062021.Service.Interface;
using AIS.Model.GeneralConversion;
using AIS.Model.MasterData;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.MasterSetting
{
    public class MasterSectionListBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel sessionModel { get; }

        public SessionModel _sessionModel;

        [Inject]
        public IMasterSectionService masterSectionService { get; set; }

        public List<MasterSectionListModel> masterSectionListModel = new List<MasterSectionListModel>();

        protected override async Task OnInitializedAsync()
        {
            masterSectionModel = new MasterSectionModel();
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            
            await LoadClassData();

        }
        private async Task LoadClassData()
        {
            masterSectionListModel = (await masterSectionService.GetMasterSectionList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.SchoolId, 0, 0, ReportType.GetBySchoolId)).ToList();
        }

        public string _searchTerm = "";
        public IEnumerable<MasterSectionListModel> MasterSectionList() => masterSectionListModel.Where(e => e.name.Contains(_searchTerm));

        public MasterSectionListModel selectedItem = new MasterSectionListModel();

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
            masterSectionModel = new MasterSectionModel();
            LoadClassData();
            // StateHasChanged();
        }

        public bool visible;
        public string StatusValue { get; set; }
        //both side data binding editform using object 
        //public MasterSectionModel MasterSectionModel = new MasterSectionModel();
        public MasterSectionModel masterSectionModel { get; set; }
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
                masterSectionModel.ActiveStatus = GenericClass.StatusConversion(StatusValue.ToString());
                masterSectionModel.CreatedByUserId = _sessionModel.UserId;
                masterSectionModel.MasterSectionId = 0;
                masterSectionModel.SchoolId = _sessionModel.SchoolId;
                masterSectionModel.SchoolCode = _sessionModel.SchoolCode;
                masterSectionModel.OperationType = OperationAction.Add;

                string msg = (await masterSectionService.AddUpdateMasterSection(masterSectionModel)).message.ToString();

                if (msg == "MasterSection Details already exist")
                {
                    snackBar.Add(msg, Severity.Error);
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);

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
                masterSectionModel.ActiveStatus = GenericClass.StatusConversion(StatusValue.ToString()); ;
                masterSectionModel.CreatedByUserId = _sessionModel.UserId;
                masterSectionModel.MasterSectionId = MasterId;
                masterSectionModel.SchoolId = _sessionModel.SchoolId;
                masterSectionModel.SchoolCode = _sessionModel.SchoolCode;
                masterSectionModel.OperationType = OperationAction.Update;

                string msg = (await masterSectionService.AddUpdateMasterSection(masterSectionModel)).message.ToString();

                if (msg == "MasterSection Details already exist")
                {
                    snackBar.Add(msg, Severity.Error);
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);
                    masterSectionModel.Name = string.Empty;
                    masterSectionModel.DisplayOrder = 0;
                    ActionName = "Save";
                    MasterId = 0;
                }

            }
        }
        private async Task Delete(int MasterSectionId)
        {


            if (MasterSectionId > 0)
            {
                masterSectionModel.ActiveStatus = 4;
                masterSectionModel.CreatedByUserId = 2;
                masterSectionModel.MasterSectionId = MasterSectionId;
                masterSectionModel.SchoolId = 2;
                masterSectionModel.SchoolCode = _sessionModel.SchoolCode;
                masterSectionModel.Name = "Delete";

                masterSectionModel.OperationType = OperationAction.Delete;

                string msg = (await masterSectionService.AddUpdateMasterSection(masterSectionModel)).message.ToString();

                if (msg == "MasterSection Details already exist")
                {
                    snackBar.Add(msg, Severity.Error);
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);
                    masterSectionModel.Name = string.Empty;
                    masterSectionModel.DisplayOrder = 0;
                    ActionName = "Save";
                    MasterId = 0;
                    LoadClassData();
                }

            }
        }
        public MudMessageBox mbox { get; set; }
        string state = "Message box hasn't been opened yet";
        public async Task DeleteRecord(int MasterSectionId)
        {
            bool? result = await mbox.Show();
            state = result == null ? "Cancelled" : "Deleted!";
            if (state == "Deleted!")
            {
                await Delete(MasterSectionId);
            }

        }
        public async Task Edit(int ClassId)
        {
            string _StatusValue = "";
            ActionName = "Update";
            MasterId = ClassId;
            List<MasterSectionListModel> _masterSectionListModel = new List<MasterSectionListModel>();
            _masterSectionListModel = (await masterSectionService.GetMasterSectionList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.SchoolId, MasterId, 0, ReportType.GetByMasterId)).ToList();

            foreach (var _MasterSection in _masterSectionListModel)
            {
                masterSectionModel.Name = _MasterSection.name;
                masterSectionModel.Code = _MasterSection.code;
                masterSectionModel.DisplayOrder = _MasterSection.displayOrder;
                StatusValue= _MasterSection.activeStatusDetails.ToString();
                // GenericClass.Status StatusValue = (GenericClass.Status)Enum.Parse(typeof(GenericClass.Status), _MasterSection.activeStatusDetails, true);
            }


        }
        public void Cancel()
        {
            ActionName = "Save";
            MasterId = 0;
            MasterSectionModel masterSectionModel = null;
        }

    }
}
