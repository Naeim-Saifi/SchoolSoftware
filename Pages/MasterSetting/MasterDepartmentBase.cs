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
    public class MasterDepartmentListBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel sessionModel { get; }
        public SessionModel _sessionModel;
        [Inject]
        public IMasterDepartmentService masterDepartmentService { get; set; }

        public List<MasterDepartmentListModel> masterDepartmentListModel = new List<MasterDepartmentListModel>();

     
        protected override async Task OnInitializedAsync()
        { 
            masterDepartmentModel = new MasterDepartmentModel();
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
          
            await LoadClassData();

        }
        private async Task LoadClassData()
        {
            masterDepartmentListModel = (await masterDepartmentService.GetMasterDepartmentList(_sessionModel.SchoolId, 0, 0, ReportType.GetBySchoolId)).ToList();
        }

        public string _searchTerm = "";
        public IEnumerable<MasterDepartmentListModel> MasterDepartmentList() => masterDepartmentListModel.Where(e => e.name.Contains(_searchTerm));

        public MasterDepartmentListModel selectedItem = new MasterDepartmentListModel();

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
            masterDepartmentModel = new MasterDepartmentModel();
            LoadClassData();
            // StateHasChanged();
        }

        public bool visible;
        public string StatusValue { get; set; }
        //both side data binding editform using object 
        //public MasterDepartmentModel MasterDepartmentModel = new MasterDepartmentModel();
        public MasterDepartmentModel masterDepartmentModel { get; set; }
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
                masterDepartmentModel.ActiveStatus = GenericClass.StatusConversion(StatusValue.ToString());
                masterDepartmentModel.CreatedByUserId = _sessionModel.UserId;
                masterDepartmentModel.MasterDepartmentId = 0;
                masterDepartmentModel.SchoolId = _sessionModel.SchoolId;
                masterDepartmentModel.OperationType = OperationAction.Add;

                string msg = (await masterDepartmentService.AddUpdateMasterDepartment(masterDepartmentModel)).message.ToString();

                if (msg == "MasterDepartment Details already exist")
                {
                    snackBar.Add(msg, Severity.Error);
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);


                    //MasterDepartmentModel.Name = string.Empty;
                    //MasterDepartmentModel.DisplayOrder = 0;
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
                masterDepartmentModel.ActiveStatus = GenericClass.StatusConversion(StatusValue.ToString()); ;
                masterDepartmentModel.CreatedByUserId = _sessionModel.UserId;
                masterDepartmentModel.MasterDepartmentId = MasterId;
                masterDepartmentModel.SchoolId = _sessionModel.SchoolId;
                masterDepartmentModel.OperationType = OperationAction.Update;

                string msg = (await masterDepartmentService.AddUpdateMasterDepartment(masterDepartmentModel)).message.ToString();

                if (msg == "MasterDepartment Details already exist")
                {
                    snackBar.Add(msg, Severity.Error);
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);
                    masterDepartmentModel.Name = string.Empty;
                    masterDepartmentModel.DisplayOrder = 0;
                    ActionName = "Save";
                    MasterId = 0;
                }

            }
        }
        private async Task Delete(int MasterDepartmentId)
        {


            if (MasterDepartmentId > 0)
            {
                masterDepartmentModel.ActiveStatus = 4;
                masterDepartmentModel.CreatedByUserId = _sessionModel.UserId;
                masterDepartmentModel.MasterDepartmentId = MasterDepartmentId;
                masterDepartmentModel.SchoolId = _sessionModel.SchoolId;
                masterDepartmentModel.Name = "Delete";

                masterDepartmentModel.OperationType = OperationAction.Delete;

                string msg = (await masterDepartmentService.AddUpdateMasterDepartment(masterDepartmentModel)).message.ToString();

                if (msg == "MasterDepartment Details already exist")
                {
                    snackBar.Add(msg, Severity.Error);
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);
                    masterDepartmentModel.Name = string.Empty;
                    masterDepartmentModel.DisplayOrder = 0;
                    ActionName = "Save";
                    MasterId = 0;
                    LoadClassData();
                }

            }
        }
        public MudMessageBox mbox { get; set; }
        string state = "Message box hasn't been opened yet";
        public async Task DeleteRecord(int MasterDepartmentId)
        {
            bool? result = await mbox.Show();
            state = result == null ? "Cancelled" : "Deleted!";
            if (state == "Deleted!")
            {
                await Delete(MasterDepartmentId);
            }

        }
        public async Task Edit(int ClassId)
        {
            string _StatusValue = "";
            ActionName = "Update";
            MasterId = ClassId;
            List<MasterDepartmentListModel> _masterDepartmentListModel = new List<MasterDepartmentListModel>();
            _masterDepartmentListModel = (await masterDepartmentService.GetMasterDepartmentList(_sessionModel.SchoolId, MasterId, 0, ReportType.GetByMasterId)).ToList();

            foreach (var _MasterDepartment in _masterDepartmentListModel)
            {
                masterDepartmentModel.Name = _MasterDepartment.name;
                masterDepartmentModel.DisplayOrder = _MasterDepartment.displayOrder;
                StatusValue = _MasterDepartment.activeStatusDetails.ToString();
                // GenericClass.Status StatusValue = (GenericClass.Status)Enum.Parse(typeof(GenericClass.Status), _MasterDepartment.activeStatusDetails, true);
            }


        }
        public void Cancel()
        {
            ActionName = "Save";
            MasterId = 0;
            MasterDepartmentModel masterDepartmentModel = null;
        }

    }
}
