using AIS.FrontEnd_07062021.Service.Interface;
using AIS.Model.MasterData;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AIS.Model;
using AIS.Model.GeneralConversion;
using Microsoft.AspNetCore.Components.Forms;
using AIS.Model.UserLogin;

namespace AdminDashboard.Server.Pages.MasterSetting
{
    public class ClassMasterBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }

        public SessionModel sessionModel { get; }
        [Inject]
        public IClassMasterService ClassMasterService { get; set; }

        public List<ClassMasterListModel> classMasterListModel = new List<ClassMasterListModel>();
        public ClassMasterModel classMasterModel { get; set; }

        public SessionModel _sessionModel;
        protected override async Task OnInitializedAsync()
        {
            classMasterModel = new ClassMasterModel();
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");

            await LoadClassData();

        }
        private async Task LoadClassData()
        {

            classMasterListModel = (await ClassMasterService.GetClassMasterList(_sessionModel.SchoolCode,_sessionModel.FinancialYear, _sessionModel.SchoolId, 0, 0, ReportType.All)).ToList();
        }

        public string _searchTerm = "";
        public IEnumerable<ClassMasterListModel> classMasterList() => classMasterListModel.Where(e => e.className.Contains(_searchTerm));

        public ClassMasterListModel selectedItem = new ClassMasterListModel();

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
            classMasterModel = new ClassMasterModel();
            LoadClassData();
            // StateHasChanged();
        }

        public bool visible;
        public string StatusValue { get; set; }
        //both side data binding editform using object 
        //public ClassMasterModel ClassMasterModel = new ClassMasterModel();
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
                classMasterModel.ActiveStatus = GenericClass.StatusConversion(StatusValue.ToString());
                classMasterModel.CreatedByUserId = _sessionModel.UserId;
                classMasterModel.ClassId = 0;
                classMasterModel.SchoolId = _sessionModel.SchoolId;
                classMasterModel.SchoolCode = _sessionModel.SchoolCode;
                classMasterModel.OperationType = OperationAction.Add;

                string msg = (await ClassMasterService.AddUpdateClassMaster(classMasterModel)).message.ToString();

                if (msg == "Class & Section Details already exist")
                {
                    snackBar.Add(msg, Severity.Error);
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);


                    //ClassMasterModel.Name = string.Empty;
                    //ClassMasterModel.DisplayOrder = 0;
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
                classMasterModel.ActiveStatus = GenericClass.StatusConversion(StatusValue.ToString()); ;
                classMasterModel.CreatedByUserId = _sessionModel.UserId;
                classMasterModel.ClassId = MasterId;
                classMasterModel.SchoolId = _sessionModel.SchoolId;
                classMasterModel.SchoolCode = _sessionModel.SchoolCode;
                classMasterModel.OperationType = OperationAction.Update;

                string msg = (await ClassMasterService.AddUpdateClassMaster(classMasterModel)).message.ToString();

                if (msg == "ClassMaster Details already exist")
                {
                    snackBar.Add(msg, Severity.Error);
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);
                    classMasterModel.ClassName = string.Empty;
                    classMasterModel.DisplayOrder = 0;
                    ActionName = "Save";
                    MasterId = 0;
                }

            }
        }
        private async Task Delete(int ClassMasterId)
        {
            if (ClassMasterId > 0)
            {
                classMasterModel.ActiveStatus = 4;
                classMasterModel.CreatedByUserId = _sessionModel.UserId;
                classMasterModel.ClassId = ClassMasterId;
                classMasterModel.SchoolId = _sessionModel.SchoolId;
                classMasterModel.SchoolCode = _sessionModel.SchoolCode;
                classMasterModel.ClassName = "Delete";
                classMasterModel.OperationType = OperationAction.Delete;
                string msg = (await ClassMasterService.AddUpdateClassMaster(classMasterModel)).message.ToString();

                if (msg == "ClassMaster Details already exist")
                {
                    snackBar.Add(msg, Severity.Error);
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);
                    classMasterModel.ClassName = string.Empty;
                    classMasterModel.DisplayOrder = 0;
                    ActionName = "Save";
                    MasterId = 0;
                    LoadClassData();
                }

            }
        }
        public MudMessageBox mbox { get; set; }
        string state = "Message box hasn't been opened yet";
        public async Task DeleteRecord(int ClassMasterId)
        {
            bool? result = await mbox.Show();
            state = result == null ? "Cancelled" : "Deleted!";
            if (state == "Deleted!")
            {
                await Delete(ClassMasterId);
            }

        }
        public async Task Edit(int ClassId)
        {
            string _StatusValue = "";
            ActionName = "Update";
            MasterId = ClassId;
            List<ClassMasterListModel> _ClassMasterListModel = new List<ClassMasterListModel>();
            _ClassMasterListModel = (await ClassMasterService.GetClassMasterList(_sessionModel.SchoolCode, _sessionModel.FinancialYear,_sessionModel.SchoolId, MasterId, 0, ReportType.GetByMasterId)).ToList();

            foreach (var _ClassMaster in _ClassMasterListModel)
            {
                classMasterModel.ClassName = _ClassMaster.className;
                classMasterModel.SectionName = _ClassMaster.sectionName;
                classMasterModel.Stength = _ClassMaster.stength;
                classMasterModel.DisplayOrder = _ClassMaster.displayOrder;
                StatusValue = _ClassMaster.activeStatusDetails.ToString();
                // GenericClass.Status StatusValue = (GenericClass.Status)Enum.Parse(typeof(GenericClass.Status), _ClassMaster.activeStatusDetails, true);
            }


        }
        public void Cancel()
        {
            ActionName = "Save";
            MasterId = 0;
            ClassMasterModel ClassMasterModel = null;
        }

    }
}
