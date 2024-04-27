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
    public class MasterDocumentListBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel sessionModel { get; }

        public SessionModel _sessionModel;

        [Inject]
        public IMasterDocumentService masterDocumentService { get; set; }

        public List<MasterDocumentListModel> masterDocumentListModel = new List<MasterDocumentListModel>();

        protected override async Task OnInitializedAsync()
        {
            masterDocumentModel = new MasterDocumentModel();
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
           
            await LoadClassData();

        }
        private async Task LoadClassData()
        {
            masterDocumentListModel = (await masterDocumentService.GetMasterDocumentList(_sessionModel.SchoolId, 0, 0, ReportType.GetBySchoolId)).ToList();
        }

        public string _searchTerm = "";
        public IEnumerable<MasterDocumentListModel> MasterDocumentList() => masterDocumentListModel.Where(e => e.name.Contains(_searchTerm));

        public MasterDocumentListModel selectedItem = new MasterDocumentListModel();

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
            masterDocumentModel = new MasterDocumentModel();
            LoadClassData();
            // StateHasChanged();
        }

        public bool visible;
        public string StatusValue { get; set; }
        //both side data binding editform using object 
        //public MasterDocumentModel MasterDocumentModel = new MasterDocumentModel();
        public MasterDocumentModel masterDocumentModel { get; set; }
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
                masterDocumentModel.ActiveStatus = GenericClass.StatusConversion(StatusValue.ToString());
                masterDocumentModel.CreatedByUserId = _sessionModel.UserId;
                masterDocumentModel.MasterDocumentId = 0;
                masterDocumentModel.SchoolId = _sessionModel.SchoolId;
                masterDocumentModel.OperationType = OperationAction.Add;

                string msg = (await masterDocumentService.AddUpdateMasterDocument(masterDocumentModel)).message.ToString();

                if (msg == "MasterDocument Details already exist")
                {
                    snackBar.Add(msg, Severity.Error);
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);


                    //MasterDocumentModel.Name = string.Empty;
                    //MasterDocumentModel.DisplayOrder = 0;
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
                masterDocumentModel.ActiveStatus = GenericClass.StatusConversion(StatusValue.ToString()); ;
                masterDocumentModel.CreatedByUserId = _sessionModel.UserId;
                masterDocumentModel.MasterDocumentId = MasterId;
                masterDocumentModel.SchoolId = _sessionModel.SchoolId;
                masterDocumentModel.OperationType = OperationAction.Update;

                string msg = (await masterDocumentService.AddUpdateMasterDocument(masterDocumentModel)).message.ToString();

                if (msg == "MasterDocument Details already exist")
                {
                    snackBar.Add(msg, Severity.Error);
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);
                    masterDocumentModel.Name = string.Empty;
                    masterDocumentModel.DisplayOrder = 0;
                    ActionName = "Save";
                    MasterId = 0;
                }

            }
        }
        private async Task Delete(int MasterDocumentId)
        {


            if (MasterDocumentId > 0)
            {
                masterDocumentModel.ActiveStatus = 4;
                masterDocumentModel.CreatedByUserId = _sessionModel.UserId;
                masterDocumentModel.MasterDocumentId = MasterDocumentId;
                masterDocumentModel.SchoolId = _sessionModel.SchoolId;
                masterDocumentModel.Name = "Delete";

                masterDocumentModel.OperationType = OperationAction.Delete;

                string msg = (await masterDocumentService.AddUpdateMasterDocument(masterDocumentModel)).message.ToString();

                if (msg == "MasterDocument Details already exist")
                {
                    snackBar.Add(msg, Severity.Error);
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);
                    masterDocumentModel.Name = string.Empty;
                    masterDocumentModel.DisplayOrder = 0;
                    ActionName = "Save";
                    MasterId = 0;
                    LoadClassData();
                }

            }
        }
        public MudMessageBox mbox { get; set; }
        string state = "Message box hasn't been opened yet";
        public async Task DeleteRecord(int MasterDocumentId)
        {
            bool? result = await mbox.Show();
            state = result == null ? "Cancelled" : "Deleted!";
            if (state == "Deleted!")
            {
                await Delete(MasterDocumentId);
            }

        }
        public async Task Edit(int ClassId)
        {
            string _StatusValue = "";
            ActionName = "Update";
            MasterId = ClassId;
            List<MasterDocumentListModel> _masterDocumentListModel = new List<MasterDocumentListModel>();
            _masterDocumentListModel = (await masterDocumentService.GetMasterDocumentList(_sessionModel.SchoolId, MasterId, 0, ReportType.GetByMasterId)).ToList();

            foreach (var _MasterDocument in _masterDocumentListModel)
            {
                masterDocumentModel.Name = _MasterDocument.name;
                masterDocumentModel.DisplayOrder = _MasterDocument.displayOrder;
                StatusValue = _MasterDocument.activeStatusDetails.ToString();
                // GenericClass.Status StatusValue = (GenericClass.Status)Enum.Parse(typeof(GenericClass.Status), _MasterDocument.activeStatusDetails, true);
            }


        }
        public void Cancel()
        {
            ActionName = "Save";
            MasterId = 0;
            MasterDocumentModel masterDocumentModel = null;
        }

    }
}
