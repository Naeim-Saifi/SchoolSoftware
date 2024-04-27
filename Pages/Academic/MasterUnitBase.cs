using AIS.FrontEnd_07062021.Service.Interface;
using AIS.Model.Academic;
using AIS.Model.GeneralConversion;
using AIS.Model.MasterData;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.Academic
{
    public class MasterUnitListBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
      //  public SessionModel sessionModel { get; }

        public SessionModel _sessionModel;


        [Inject]
        public IMasterUnitService masterUnitService { get; set; }

        public List<MasterUnitListModel> masterUnitListModel = new List<MasterUnitListModel>();

        protected override async Task OnInitializedAsync()
        {
            masterUnitModel = new MasterUnitModel();
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            await LoadUnitData();

        }
        private async Task LoadUnitData()
        {
            masterUnitListModel = (await masterUnitService.GetMasterUnitList(_sessionModel.SchoolId, 0, 0, ReportType.GetBySchoolId)).ToList();
        }

        public string _searchTerm = "";
        public IEnumerable<MasterUnitListModel> MasterUnitList() => masterUnitListModel.Where(e => e.unitTitle.Contains(_searchTerm)| e.unitDescription.Contains(_searchTerm));

        public MasterUnitListModel selectedItem = new MasterUnitListModel();

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
            masterUnitModel = new MasterUnitModel();
            LoadUnitData();
            // StateHasChanged();
        }

        public bool visible;
        public string StatusValue { get; set; }
        //both side data binding editform using object 
        //public MasterUnitModel MasterUnitModel = new MasterUnitModel();
        public MasterUnitModel masterUnitModel { get; set; }
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
                masterUnitModel.ActiveStatus = GenericClass.StatusConversion(StatusValue.ToString());
                masterUnitModel.CreatedByUserId = _sessionModel.UserId;
                //MasterUnitModel..MasterUnitId = 0;
                masterUnitModel.SchoolId = _sessionModel.SchoolId;
                masterUnitModel.OperationType = OperationAction.Add;

                string msg = (await masterUnitService.AddUpdateMasterUnit(masterUnitModel)).message.ToString();

                if (msg == "MasterUnit Details already exist")
                {
                    snackBar.Add(msg, Severity.Error);
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);


                    //MasterUnitModel.Name = string.Empty;
                    //MasterUnitModel.DisplayOrder = 0;
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
                masterUnitModel.ActiveStatus = GenericClass.StatusConversion(StatusValue.ToString()); ;
                masterUnitModel.CreatedByUserId = _sessionModel.UserId;
                masterUnitModel.SubjectUnitId = MasterId;
                masterUnitModel.SchoolId = _sessionModel.SchoolId;
                masterUnitModel.OperationType = OperationAction.Update;

                string msg = (await masterUnitService.AddUpdateMasterUnit(masterUnitModel)).message.ToString();

                if (msg == "MasterUnit Details already exist")
                {
                    snackBar.Add(msg, Severity.Error);
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);
                    masterUnitModel.UnitTitle = string.Empty;
                    masterUnitModel.UnitDescription = string.Empty;
                    ActionName = "Save";
                    MasterId = 0;
                }

            }
        }
        private async Task Delete(int MasterUnitId)
        {


            if (MasterUnitId > 0)
            {
                masterUnitModel.ActiveStatus = 4;
                masterUnitModel.CreatedByUserId = 2;
                masterUnitModel.SubjectUnitId = MasterUnitId;
                masterUnitModel.SchoolId = 2;
                //MasterUnitModel.Name = "Delete";

                masterUnitModel.OperationType = OperationAction.Delete;

                string msg = (await masterUnitService.AddUpdateMasterUnit(masterUnitModel)).message.ToString();

                if (msg == "MasterUnit Details already exist")
                {
                    snackBar.Add(msg, Severity.Error);
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);
                    masterUnitModel.UnitTitle = string.Empty;
                    masterUnitModel.UnitDescription = string.Empty;
                    ActionName = "Save";
                    MasterId = 0;
                    LoadUnitData();
                }

            }
        }
        public MudMessageBox mbox { get; set; }
        string state = "Message box hasn't been opened yet";
        public async Task DeleteRecord(int MasterUnitId)
        {
            bool? result = await mbox.Show();
            state = result == null ? "Cancelled" : "Deleted!";
            if (state == "Deleted!")
            {
                await Delete(MasterUnitId);
            }

        }
        public async Task Edit(int ClassId)
        {
            string _StatusValue = "";
            ActionName = "Update";
            MasterId = ClassId;
            List<MasterUnitListModel> _MasterUnitListModel = new List<MasterUnitListModel>();
            _MasterUnitListModel = (await masterUnitService.GetMasterUnitList(_sessionModel.SchoolId, MasterId, 0, ReportType.GetByMasterId)).ToList();

            foreach (var _MasterUnit in _MasterUnitListModel)
            {
                masterUnitModel.UnitTitle = _MasterUnit.unitTitle;
                masterUnitModel.UnitDescription = _MasterUnit.unitDescription;
                StatusValue = _MasterUnit.activeStatusDetails.ToString();
                // GenericClass.Status StatusValue = (GenericClass.Status)Enum.Parse(typeof(GenericClass.Status), _MasterUnit.activeStatusDetails, true);
            }


        }
        public void Cancel()
        {
            ActionName = "Save";
            MasterId = 0;
            MasterUnitModel masterUnitModel = null;
        }

    }
}
