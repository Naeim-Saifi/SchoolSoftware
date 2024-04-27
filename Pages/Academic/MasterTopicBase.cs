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
    public class MasterTopicBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
       // public SessionModel sessionModel { get; }

        public SessionModel _sessionModel;

        [Inject]
        public IMasterClassService masterClassService { get; set; }

        
        [Inject]
        public IMasterTopicService masterTopicService { get; set; }

        public List<MasterTopicList> masterTopicList = new List<MasterTopicList>();

        protected override async Task OnInitializedAsync()
        {
            masterTopicModel = new MasterTopicModel();
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            await LoadClassData();

        }
        private async Task LoadClassData()
        {
            masterTopicList = (await masterTopicService.GetMasterTopicList(_sessionModel.SchoolId, 0, 0, ReportType.GetBySchoolId)).ToList();
        }

        public string _searchTerm = "";
        public IEnumerable<MasterTopicList> MasterTopicList() => masterTopicList.Where(e => e.topicTitle.Contains(_searchTerm));

        public MasterTopicList selectedItem = new MasterTopicList();

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
            masterTopicModel = new MasterTopicModel();
            LoadClassData();
            // StateHasChanged();
        }

        public bool visible;
        public string StatusValue { get; set; }
        //both side data binding editform using object 
        //public MasterTopicModel MasterTopicModel = new MasterTopicModel();
        public MasterTopicModel masterTopicModel { get; set; }
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
                masterTopicModel.ActiveStatus = GenericClass.StatusConversion(StatusValue.ToString());
                masterTopicModel.CreatedByUserId = _sessionModel.UserId;
                masterTopicModel.TopicTitle = "";
                masterTopicModel.SchoolId = _sessionModel.SchoolId;
                masterTopicModel.OperationType = OperationAction.Add;

                string msg = (await masterTopicService.AddUpdateMasterTopic(masterTopicModel)).message.ToString();

                if (msg == "MasterTopic Details already exist")
                {
                    snackBar.Add(msg, Severity.Error);
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);


                    //MasterTopicModel.Name = string.Empty;
                    //MasterTopicModel.DisplayOrder = 0;
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
                masterTopicModel.ActiveStatus = GenericClass.StatusConversion(StatusValue.ToString()); ;
                masterTopicModel.CreatedByUserId = _sessionModel.UserId;
                masterTopicModel.SubjectTopicId = MasterId;
                masterTopicModel.SchoolId = _sessionModel.SchoolId;
                masterTopicModel.OperationType = OperationAction.Update;

                string msg = (await masterTopicService.AddUpdateMasterTopic(masterTopicModel)).message.ToString();

                if (msg == "MasterTopic Details already exist")
                {
                    snackBar.Add(msg, Severity.Error);
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);
                    masterTopicModel.TopicTitle = string.Empty;
                    masterTopicModel.TopicDescription = string.Empty;
                    ActionName = "Save";
                    MasterId = 0;
                }

            }
        }
        private async Task Delete(int MasterTopicId)
        {


            if (MasterTopicId > 0)
            {
                masterTopicModel.ActiveStatus = 4;
                masterTopicModel.CreatedByUserId = 2;
                masterTopicModel.SubjectTopicId = MasterTopicId;
                masterTopicModel.SchoolId = 2;
                //masterTopicModel.Name = "Delete";

                masterTopicModel.OperationType = OperationAction.Delete;

                string msg = (await masterTopicService.AddUpdateMasterTopic(masterTopicModel)).message.ToString();

                if (msg == "MasterTopic Details already exist")
                {
                    snackBar.Add(msg, Severity.Error);
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);
                    masterTopicModel.TopicTitle = string.Empty;
                    masterTopicModel.TopicDescription = string.Empty;
                    ActionName = "Save";
                    MasterId = 0;
                    LoadClassData();
                }

            }
        }
        public MudMessageBox mbox { get; set; }
        string state = "Message box hasn't been opened yet";
        public async Task DeleteRecord(int MasterTopicId)
        {
            bool? result = await mbox.Show();
            state = result == null ? "Cancelled" : "Deleted!";
            if (state == "Deleted!")
            {
                await Delete(MasterTopicId);
            }

        }
        public async Task Edit(int ClassId)
        {
            string _StatusValue = "";
            ActionName = "Update";
            MasterId = ClassId;
            List<MasterTopicList> _masterTopicListModel = new List<MasterTopicList>();
            _masterTopicListModel = (await masterTopicService.GetMasterTopicList(_sessionModel.SchoolId, MasterId, 0, ReportType.GetByMasterId)).ToList();

            foreach (var _MasterTopic in _masterTopicListModel)
            {
                masterTopicModel.TopicTitle = _MasterTopic.topicTitle;
                masterTopicModel.TopicDescription = _MasterTopic.topicDescription;
                StatusValue = _MasterTopic.activeStatus.ToString();
                // GenericClass.Status StatusValue = (GenericClass.Status)Enum.Parse(typeof(GenericClass.Status), _MasterTopic.activeStatusDetails, true);
            }


        }
        public void Cancel()
        {
            ActionName = "Save";
            MasterId = 0;
            MasterTopicModel masterTopicModel = null;
        }

    }
}
