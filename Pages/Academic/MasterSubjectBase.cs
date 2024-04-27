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
    public class MasterSubjectListBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel sessionModel { get; }

        public SessionModel _sessionModel;


        [Inject]
        public IMasterSubjectService masterSubjectService { get; set; }

        public List<MasterSubjectListModel> masterSubjectListModel = new List<MasterSubjectListModel>();

        protected override async Task OnInitializedAsync()
        {
            masterSubjectModel = new MasterSubjectModel();
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            await LoadClassData();

        }
        private async Task LoadClassData()
        {
            masterSubjectListModel = (await masterSubjectService.GetMasterSubjectList(_sessionModel.SchoolId, 0, 0, ReportType.GetBySchoolId)).ToList();
        }

        public string _searchTerm = "";
        public IEnumerable<MasterSubjectListModel> MasterSubjectList() => masterSubjectListModel.Where(e => e.subjectName.Contains(_searchTerm));

        public MasterSubjectListModel selectedItem = new MasterSubjectListModel();

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
            masterSubjectModel = new MasterSubjectModel();
            LoadClassData();
            // StateHasChanged();
        }

        public bool visible;
        public string StatusValue { get; set; }
        //both side data binding editform using object 
        //public MasterSubjectModel MasterSubjectModel = new MasterSubjectModel();
        public MasterSubjectModel masterSubjectModel { get; set; }
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
                masterSubjectModel.ActiveStatus = GenericClass.StatusConversion(StatusValue.ToString());
                masterSubjectModel.CreatedByUserId = _sessionModel.UserId;
                masterSubjectModel.MasterSubjectId = 0;
                masterSubjectModel.SchoolId = _sessionModel.SchoolId;
                masterSubjectModel.OperationType = OperationAction.Add;

                string msg = (await masterSubjectService.AddUpdateMasterSubject(masterSubjectModel)).message.ToString();

                if (msg == "MasterSubject Details already exist")
                {
                    snackBar.Add(msg, Severity.Error);
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);


                    //MasterSubjectModel.Name = string.Empty;
                    //MasterSubjectModel.DisplayOrder = 0;
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
                masterSubjectModel.ActiveStatus = GenericClass.StatusConversion(StatusValue.ToString()); ;
                masterSubjectModel.CreatedByUserId = _sessionModel.UserId;
                masterSubjectModel.MasterSubjectId = MasterId;
                masterSubjectModel.SchoolId = _sessionModel.SchoolId;
                masterSubjectModel.OperationType = OperationAction.Update;

                string msg = (await masterSubjectService.AddUpdateMasterSubject(masterSubjectModel)).message.ToString();

                if (msg == "MasterSubject Details already exist")
                {
                    snackBar.Add(msg, Severity.Error);
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);
                    masterSubjectModel.SubjectName = string.Empty;
                    masterSubjectModel.DisplayOrder = 0;
                    ActionName = "Save";
                    MasterId = 0;
                }

            }
        }
        private async Task Delete(int MasterSubjectId)
        {


            if (MasterSubjectId > 0)
            {
                masterSubjectModel.ActiveStatus = 4;
                masterSubjectModel.CreatedByUserId = 2;
                masterSubjectModel.MasterSubjectId = MasterSubjectId;
                masterSubjectModel.SchoolId = 2;
                //masterSubjectModel.Name = "Delete";

                masterSubjectModel.OperationType = OperationAction.Delete;

                string msg = (await masterSubjectService.AddUpdateMasterSubject(masterSubjectModel)).message.ToString();

                if (msg == "MasterSubject Details already exist")
                {
                    snackBar.Add(msg, Severity.Error);
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);
                    masterSubjectModel.SubjectName = string.Empty;
                    masterSubjectModel.DisplayOrder = 0;
                    ActionName = "Save";
                    MasterId = 0;
                    LoadClassData();
                }

            }
        }
        public MudMessageBox mbox { get; set; }
        string state = "Message box hasn't been opened yet";
        public async Task DeleteRecord(int MasterSubjectId)
        {
            bool? result = await mbox.Show();
            state = result == null ? "Cancelled" : "Deleted!";
            if (state == "Deleted!")
            {
                await Delete(MasterSubjectId);
            }

        }
        public async Task Edit(int ClassId)
        {
            string _StatusValue = "";
            ActionName = "Update";
            MasterId = ClassId;
            List<MasterSubjectListModel> _masterSubjectListModel = new List<MasterSubjectListModel>();
            _masterSubjectListModel = (await masterSubjectService.GetMasterSubjectList(_sessionModel.SchoolId, MasterId, 0, ReportType.GetByMasterId)).ToList();

            foreach (var _MasterSubject in _masterSubjectListModel)
            {
                masterSubjectModel.SubjectName = _MasterSubject.subjectName;
                masterSubjectModel.DisplayOrder = _MasterSubject.displayOrder;
                StatusValue= _MasterSubject.activeStatusDetails.ToString();
                // GenericClass.Status StatusValue = (GenericClass.Status)Enum.Parse(typeof(GenericClass.Status), _MasterSubject.activeStatusDetails, true);
            }


        }
        public void Cancel()
        {
            ActionName = "Save";
            MasterId = 0;
            MasterSubjectModel masterSubjectModel = null;
        }

    }
}
