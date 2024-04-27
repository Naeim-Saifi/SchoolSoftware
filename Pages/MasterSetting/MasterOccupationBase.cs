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
    public class MasterOccupationListBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel sessionModel { get; }

        public SessionModel _sessionModel;

        [Inject]
        public IMasterOccupationService masterOccupationService { get; set; }

        public List<MasterOccupationListModel> masterOccupationListModel = new List<MasterOccupationListModel>();

        protected override async Task OnInitializedAsync()
        {
           
            masterOccupationModel = new MasterOccupationModel();
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            await LoadClassData();

        }
        private async Task LoadClassData()
        {
            masterOccupationListModel = (await masterOccupationService.GetMasterOccupationList(_sessionModel.SchoolId, 0, 0, ReportType.GetBySchoolId)).ToList();
        }

        public string _searchTerm = "";
        public IEnumerable<MasterOccupationListModel> MasterOccupationList() => masterOccupationListModel.Where(e => e.name.Contains(_searchTerm));

        public MasterOccupationListModel selectedItem = new MasterOccupationListModel();

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
            masterOccupationModel = new MasterOccupationModel();
            LoadClassData();
            // StateHasChanged();
        }

        public bool visible;
        public string StatusValue { get; set; }
        //both side data binding editform using object 
        //public MasterOccupationModel MasterOccupationModel = new MasterOccupationModel();
        public MasterOccupationModel masterOccupationModel { get; set; }
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
                masterOccupationModel.ActiveStatus = GenericClass.StatusConversion(StatusValue.ToString());
                masterOccupationModel.CreatedByUserId = _sessionModel.UserId;
                masterOccupationModel.MasterOccupationId = 0;
                masterOccupationModel.SchoolId = _sessionModel.SchoolId;
                masterOccupationModel.OperationType = OperationAction.Add;

                string msg = (await masterOccupationService.AddUpdateMasterOccupation(masterOccupationModel)).message.ToString();

                if (msg == "MasterOccupation Details already exist")
                {
                    snackBar.Add(msg, Severity.Error);
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);


                    //MasterOccupationModel.Name = string.Empty;
                    //MasterOccupationModel.DisplayOrder = 0;
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
                masterOccupationModel.ActiveStatus = GenericClass.StatusConversion(StatusValue.ToString()); ;
                masterOccupationModel.CreatedByUserId = _sessionModel.SchoolId;
                masterOccupationModel.MasterOccupationId = MasterId;
                masterOccupationModel.SchoolId = _sessionModel.SchoolId;
                masterOccupationModel.OperationType = OperationAction.Update;

                string msg = (await masterOccupationService.AddUpdateMasterOccupation(masterOccupationModel)).message.ToString();

                if (msg == "MasterOccupation Details already exist")
                {
                    snackBar.Add(msg, Severity.Error);
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);
                    masterOccupationModel.Name = string.Empty;
                    masterOccupationModel.DisplayOrder = 0;
                    ActionName = "Save";
                    MasterId = 0;
                }

            }
        }
        private async Task Delete(int MasterOccupationId)
        {


            if (MasterOccupationId > 0)
            {
                masterOccupationModel.ActiveStatus = 4;
                masterOccupationModel.CreatedByUserId = _sessionModel.UserId;
                masterOccupationModel.MasterOccupationId = MasterOccupationId;
                masterOccupationModel.SchoolId = _sessionModel.SchoolId;
                masterOccupationModel.Name = "Delete";

                masterOccupationModel.OperationType = OperationAction.Delete;

                string msg = (await masterOccupationService.AddUpdateMasterOccupation(masterOccupationModel)).message.ToString();

                if (msg == "MasterOccupation Details already exist")
                {
                    snackBar.Add(msg, Severity.Error);
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);
                    masterOccupationModel.Name = string.Empty;
                    masterOccupationModel.DisplayOrder = 0;
                    ActionName = "Save";
                    MasterId = 0;
                    LoadClassData();
                }

            }
        }
        public MudMessageBox mbox { get; set; }
        string state = "Message box hasn't been opened yet";
        public async Task DeleteRecord(int MasterOccupationId)
        {
            bool? result = await mbox.Show();
            state = result == null ? "Cancelled" : "Deleted!";
            if (state == "Deleted!")
            {
                await Delete(MasterOccupationId);
            }

        }
        public async Task Edit(int ClassId)
        {
            string _StatusValue = "";
            ActionName = "Update";
            MasterId = ClassId;
            List<MasterOccupationListModel> _masterOccupationListModel = new List<MasterOccupationListModel>();
            _masterOccupationListModel = (await masterOccupationService.GetMasterOccupationList(_sessionModel.SchoolId, MasterId, 0, ReportType.GetByMasterId)).ToList();

            foreach (var _MasterOccupation in _masterOccupationListModel)
            {
                masterOccupationModel.Name = _MasterOccupation.name;
                masterOccupationModel.DisplayOrder = _MasterOccupation.displayOrder;
                StatusValue = _MasterOccupation.activeStatusDetails.ToString();
                // GenericClass.Status StatusValue = (GenericClass.Status)Enum.Parse(typeof(GenericClass.Status), _MasterOccupation.activeStatusDetails, true);
            }


        }
        public void Cancel()
        {
            ActionName = "Save";
            MasterId = 0;
            MasterOccupationModel masterOccupationModel = null;
        }

    }
}
