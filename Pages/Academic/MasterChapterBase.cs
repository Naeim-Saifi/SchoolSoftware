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
    public class MasterChapterListBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        //  public SessionModel sessionModel { get; }

        public SessionModel _sessionModel;


        [Inject]
        public IMasterChapterService masterChapterService { get; set; }

        public List<MasterChapterListModel> masterChapterListModel = new List<MasterChapterListModel>();

        protected override async Task OnInitializedAsync()
        {
            masterChapterModel = new MasterChapterModel();
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            await LoadChapterData();

        }
        private async Task LoadChapterData()
        {
            masterChapterListModel = (await masterChapterService.GetMasterChapterList(_sessionModel.SchoolId, 0, 0, ReportType.GetBySchoolId)).ToList();
        }

        public string _searchTerm = "";
        public IEnumerable<MasterChapterListModel> MasterChapterList() => masterChapterListModel.Where(e => e.chapterTitle.Contains(_searchTerm) | e.chapterDescription.Contains(_searchTerm));

        public MasterChapterListModel selectedItem = new MasterChapterListModel();

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
            masterChapterModel = new MasterChapterModel();
            LoadChapterData();
            // StateHasChanged();
        }

        public bool visible;
        public string StatusValue { get; set; }
        //both side data binding editform using object 
        //public MasterChapterModel MasterChapterModel = new MasterChapterModel();
        public MasterChapterModel masterChapterModel { get; set; }
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
                masterChapterModel.ActiveStatus = GenericClass.StatusConversion(StatusValue.ToString());
                masterChapterModel.CreatedByUserId = _sessionModel.UserId;
                //MasterChapterModel..MasterChapterId = 0;
                masterChapterModel.SchoolId = _sessionModel.SchoolId;
                masterChapterModel.OperationType = OperationAction.Add;

                string msg = (await masterChapterService.AddUpdateMasterChapter(masterChapterModel)).message.ToString();

                if (msg == "MasterChapter Details already exist")
                {
                    snackBar.Add(msg, Severity.Error);
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);


                    //MasterChapterModel.Name = string.Empty;
                    //MasterChapterModel.DisplayOrder = 0;
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
                masterChapterModel.ActiveStatus = GenericClass.StatusConversion(StatusValue.ToString()); ;
                masterChapterModel.CreatedByUserId = _sessionModel.UserId;
                masterChapterModel.SubjectChapterId = MasterId;
                masterChapterModel.SchoolId = _sessionModel.SchoolId;
                masterChapterModel.OperationType = OperationAction.Update;

                string msg = (await masterChapterService.AddUpdateMasterChapter(masterChapterModel)).message.ToString();

                if (msg == "MasterChapter Details already exist")
                {
                    snackBar.Add(msg, Severity.Error);
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);
                    masterChapterModel.ChapterTitle = string.Empty;
                    masterChapterModel.ChapterDescription = string.Empty;
                    ActionName = "Save";
                    MasterId = 0;
                }

            }
        }
        private async Task Delete(int MasterChapterId)
        {


            if (MasterChapterId > 0)
            {
                masterChapterModel.ActiveStatus = 4;
                masterChapterModel.CreatedByUserId = 2;
                masterChapterModel.SubjectChapterId = MasterChapterId;
                masterChapterModel.SchoolId = 2;
                //MasterChapterModel.Name = "Delete";

                masterChapterModel.OperationType = OperationAction.Delete;

                string msg = (await masterChapterService.AddUpdateMasterChapter(masterChapterModel)).message.ToString();

                if (msg == "MasterChapter Details already exist")
                {
                    snackBar.Add(msg, Severity.Error);
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);
                    masterChapterModel.ChapterTitle = string.Empty;
                    masterChapterModel.ChapterDescription = string.Empty;
                    ActionName = "Save";
                    MasterId = 0;
                    LoadChapterData();
                }

            }
        }
        public MudMessageBox mbox { get; set; }
        string state = "Message box hasn't been opened yet";
        public async Task DeleteRecord(int MasterChapterId)
        {
            bool? result = await mbox.Show();
            state = result == null ? "Cancelled" : "Deleted!";
            if (state == "Deleted!")
            {
                await Delete(MasterChapterId);
            }

        }
        public async Task Edit(int ClassId)
        {
            string _StatusValue = "";
            ActionName = "Update";
            MasterId = ClassId;
            List<MasterChapterListModel> _MasterChapterListModel = new List<MasterChapterListModel>();
            _MasterChapterListModel = (await masterChapterService.GetMasterChapterList(_sessionModel.SchoolId, MasterId, 0, ReportType.GetByMasterId)).ToList();

            foreach (var _MasterChapter in _MasterChapterListModel)
            {
                masterChapterModel.ChapterTitle = _MasterChapter.chapterTitle;
                masterChapterModel.ChapterDescription = _MasterChapter.chapterDescription;
                StatusValue = _MasterChapter.activeStatusDetails.ToString();
                // GenericClass.Status StatusValue = (GenericClass.Status)Enum.Parse(typeof(GenericClass.Status), _MasterChapter.activeStatusDetails, true);
            }


        }
        public void Cancel()
        {
            ActionName = "Save";
            MasterId = 0;
            MasterChapterModel masterChapterModel = null;
        }

    }
}
