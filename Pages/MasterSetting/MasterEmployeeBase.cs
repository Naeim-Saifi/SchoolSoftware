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
    public class MasterEmployeeListBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel sessionModel { get; }

        public SessionModel _sessionModel;

        [Inject]
        public IMasterEmployeeService masterEmployeeService { get; set; }

        public List<MasterEmployeeListModel> masterEmployeeListModel = new List<MasterEmployeeListModel>();

        protected override async Task OnInitializedAsync()
        {
            masterEmployeeModel = new MasterEmployeeModel();
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
           
            await LoadClassData();

        }
        private async Task LoadClassData()
        {
            masterEmployeeListModel = (await masterEmployeeService.GetMasterEmployeeList(_sessionModel.SchoolId, 0, 0, ReportType.GetBySchoolId)).ToList();
        }

        public string _searchTerm = "";
        public IEnumerable<MasterEmployeeListModel> MasterEmployeeList() => masterEmployeeListModel.Where(e => e.name.Contains(_searchTerm));

        public MasterEmployeeListModel selectedItem = new MasterEmployeeListModel();

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
            masterEmployeeModel = new MasterEmployeeModel();
            LoadClassData();
            // StateHasChanged();
        }

        public bool visible;
        public string StatusValue { get; set; }
        //both side data binding editform using object 
        //public MasterEmployeeModel MasterEmployeeModel = new MasterEmployeeModel();
        public MasterEmployeeModel masterEmployeeModel { get; set; }
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
                masterEmployeeModel.ActiveStatus = GenericClass.StatusConversion(StatusValue.ToString());
                masterEmployeeModel.CreatedByUserId = _sessionModel.UserId;
                masterEmployeeModel.MasterEmployeeId = 0;
                masterEmployeeModel.SchoolId = _sessionModel.SchoolId;
                masterEmployeeModel.OperationType = OperationAction.Add;

                string msg = (await masterEmployeeService.AddUpdateMasterEmployee(masterEmployeeModel)).message.ToString();

                if (msg == "MasterEmployee Details already exist")
                {
                    snackBar.Add(msg, Severity.Error);
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);


                    //MasterEmployeeModel.Name = string.Empty;
                    //MasterEmployeeModel.DisplayOrder = 0;
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
                masterEmployeeModel.ActiveStatus = GenericClass.StatusConversion(StatusValue.ToString()); ;
                masterEmployeeModel.CreatedByUserId = _sessionModel.UserId;
                masterEmployeeModel.MasterEmployeeId = MasterId;
                masterEmployeeModel.SchoolId = _sessionModel.SchoolId;
                masterEmployeeModel.OperationType = OperationAction.Update;

                string msg = (await masterEmployeeService.AddUpdateMasterEmployee(masterEmployeeModel)).message.ToString();

                if (msg == "MasterEmployee Details already exist")
                {
                    snackBar.Add(msg, Severity.Error);
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);
                    masterEmployeeModel.Name = string.Empty;
                    masterEmployeeModel.DisplayOrder = 0;
                    ActionName = "Save";
                    MasterId = 0;
                }

            }
        }
        private async Task Delete(int MasterEmployeeId)
        {


            if (MasterEmployeeId > 0)
            {
                masterEmployeeModel.ActiveStatus = 4;
                masterEmployeeModel.CreatedByUserId = _sessionModel.UserId;
                masterEmployeeModel.MasterEmployeeId = MasterEmployeeId;
                masterEmployeeModel.SchoolId = _sessionModel.SchoolId;
                masterEmployeeModel.Name = "Delete";

                masterEmployeeModel.OperationType = OperationAction.Delete;

                string msg = (await masterEmployeeService.AddUpdateMasterEmployee(masterEmployeeModel)).message.ToString();

                if (msg == "MasterEmployee Details already exist")
                {
                    snackBar.Add(msg, Severity.Error);
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);
                    masterEmployeeModel.Name = string.Empty;
                    masterEmployeeModel.DisplayOrder = 0;
                    ActionName = "Save";
                    MasterId = 0;
                    LoadClassData();
                }

            }
        }
        public MudMessageBox mbox { get; set; }
        string state = "Message box hasn't been opened yet";
        public async Task DeleteRecord(int MasterEmployeeId)
        {
            bool? result = await mbox.Show();
            state = result == null ? "Cancelled" : "Deleted!";
            if (state == "Deleted!")
            {
                await Delete(MasterEmployeeId);
            }

        }
        public async Task Edit(int ClassId)
        {
            string _StatusValue = "";
            ActionName = "Update";
            MasterId = ClassId;
            List<MasterEmployeeListModel> _masterEmployeeListModel = new List<MasterEmployeeListModel>();
            _masterEmployeeListModel = (await masterEmployeeService.GetMasterEmployeeList(_sessionModel.SchoolId, MasterId, 0, ReportType.GetByMasterId)).ToList();

            foreach (var _MasterEmployee in _masterEmployeeListModel)
            {
                masterEmployeeModel.Name = _MasterEmployee.name;
                masterEmployeeModel.DisplayOrder = _MasterEmployee.displayOrder;
                StatusValue = _MasterEmployee.activeStatusDetails.ToString();
                // GenericClass.Status StatusValue = (GenericClass.Status)Enum.Parse(typeof(GenericClass.Status), _MasterEmployee.activeStatusDetails, true);
            }


        }
        public void Cancel()
        {
            ActionName = "Save";
            MasterId = 0;
            MasterEmployeeModel masterEmployeeModel = null;
        }

    }
}
