using AIS.FrontEnd_07062021.Service.Interface;
using AIS.Model.GeneralConversion;
using AIS.Model.MasterData;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.ProtectedBrowserStorage;
using MudBlazor;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.MasterSetting
{
    public class MasterBatchListBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        public IMasterBatchService masterBatchService { get; set; }

        public List<MasterBatchListModel> masterBatchListModel = new List<MasterBatchListModel>();
        public MasterBatchModel masterBatchModel { get; set; }
        public SessionModel _sessionModel;
        protected override async Task OnInitializedAsync()
        {
            masterBatchModel = new MasterBatchModel();
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            await LoadBatchData();
        }

        public int SchoolId = 0;
        private async Task LoadBatchData()
        {

            masterBatchListModel = (await masterBatchService.GetMasterBatchList(_sessionModel.SchoolCode,_sessionModel.SchoolId, 0, 0, ReportType.GetBySchoolId)).ToList();
        }

        public string _searchTerm = "";
        public IEnumerable<MasterBatchListModel> MasterBatchList() => masterBatchListModel.Where(e => e.name.Contains(_searchTerm));

        public MasterBatchListModel selectedItem = new MasterBatchListModel();

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
            SaveBatch();
            masterBatchModel = new MasterBatchModel();
            LoadBatchData();
            StateHasChanged();
        }

        public bool visible;
        public void SaveBatch()

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

            if (StatusValue1.ToString() == "0" || StatusValue1.ToString() == "")
            {
                snackBar.Add("Please Select Status data not save", Severity.Info);

            }
            else
            {
                //selectUserStatus = GenericClass.StatusConversion(status);
                masterBatchModel.ActiveStatus = GenericClass.StatusConversion(StatusValue1.ToString());
                masterBatchModel.CreatedByUserId = _sessionModel.UserId;
                masterBatchModel.MasterBatchId = 0;
                masterBatchModel.SchoolId = _sessionModel.SchoolId;
                masterBatchModel.SchoolCode = _sessionModel.SchoolCode;
                masterBatchModel.OperationType = OperationAction.Add;

                string msg = (await masterBatchService.AddUpdateMasterBatch(masterBatchModel)).message.ToString();

                if (msg == "MasterBatch Details already exist")
                {
                    snackBar.Add(msg, Severity.Error);
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);
                    //masterBatchModel.Name = string.Empty;
                    //masterBatchModel.DisplayOrder = 0;
                }

            }
        }
        private async Task Update()
        {

            if (StatusValue1.ToString() == "0" || StatusValue1.ToString() == "")
            {
                snackBar.Add("Please Select Status data not save", Severity.Info);

            }
            else
            {
                masterBatchModel.ActiveStatus = GenericClass.StatusConversion(StatusValue1.ToString()); ;
                masterBatchModel.CreatedByUserId = _sessionModel.UserId;
                masterBatchModel.MasterBatchId = MasterId;
                masterBatchModel.SchoolId = _sessionModel.SchoolId;
                masterBatchModel.SchoolCode = _sessionModel.SchoolCode;
                masterBatchModel.OperationType = OperationAction.Update;

                string msg = (await masterBatchService.AddUpdateMasterBatch(masterBatchModel)).message.ToString();

                if (msg == "MasterBatch Details already exist")
                {
                    snackBar.Add(msg, Severity.Error);
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);
                    masterBatchModel.Name = string.Empty;
                    masterBatchModel.DisplayOrder = 0;
                    ActionName = "Save";
                    MasterId = 0;
                }

            }
        }
        private async Task Delete(int masterBatchId)
        {
            if (masterBatchId > 0)
            {
                masterBatchModel.ActiveStatus = 4;
                masterBatchModel.CreatedByUserId = _sessionModel.UserId;
                masterBatchModel.MasterBatchId = masterBatchId;
                masterBatchModel.SchoolId = _sessionModel.SchoolId;
                masterBatchModel.SchoolCode = _sessionModel.SchoolCode;
                masterBatchModel.Name = "Delete";

                masterBatchModel.OperationType = OperationAction.Delete;

                string msg = (await masterBatchService.AddUpdateMasterBatch(masterBatchModel)).message.ToString();

                if (msg == "MasterBatch Details already exist")
                {
                    snackBar.Add(msg, Severity.Error);
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);
                    masterBatchModel.Name = string.Empty;
                    masterBatchModel.DisplayOrder = 0;
                    ActionName = "Save";
                    MasterId = 0;
                    LoadBatchData();
                }
            }
        }
        public MudMessageBox mbox { get; set; }
        string state = "Message box hasn't been opened yet";
        public async Task DeleteRecord(int masterBatchId)
        {
            bool? result = await mbox.Show();
            state = result == null ? "Cancelled" : "Deleted!";
            if (state == "Deleted!")
            {
                await Delete(masterBatchId);
            }

        }

        public string StatusValue1 { get; set; }
        public async Task Edit(int batchId)
        {
            string _StatusValue = "";
            ActionName = "Update";
            MasterId = batchId;
            List<MasterBatchListModel> _masterBatchListModel = new List<MasterBatchListModel>();
            _masterBatchListModel = (await masterBatchService.GetMasterBatchList(_sessionModel.SchoolCode, _sessionModel.SchoolId, MasterId, 0, ReportType.GetByMasterId)).ToList();

            foreach (var _MasterBatch in _masterBatchListModel)
            {
                masterBatchModel.Name = _MasterBatch.name;
                masterBatchModel.DisplayOrder = _MasterBatch.displayOrder;
                StatusValue1 = _MasterBatch.activeStatusDetails.ToString();
               // GenericClass.Status StatusValue = (GenericClass.Status)Enum.Parse(typeof(GenericClass.Status), _MasterBatch.activeStatusDetails, true);
            }
            
        }
        public void Cancel()
        {
            ActionName = "Save";
            MasterId = 0;
            MasterBatchModel masterBatchModel = null;
        }

    }
}
