using AdminDashboard.Server.Models.MasterConfiguration.SubjectMaster;
using AdminDashboard.Server.Models.MasterConfiguration.UnitMaster;
using AdminDashboard.Server.Models.SyllabusDetails;
using AdminDashboard.Server.Service.Interface.MasterConfiguration.SubjectMaster;
using AdminDashboard.Server.Service.Interface.MasterConfiguration.Syllabus;
using AdminDashboard.Server.Service.Interface.MasterData;
using AIS.Data.BaseClass;
using AIS.Data.RequestResponseModel.MasterData;
using AIS.Model;
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.Syllabus
{
    public class UnitMasterBase:ComponentBase
    {

        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }

        //this model is use for get data userdetails and finaciyal year.
        public SessionModel sessionModel { get; }

        [Inject]
        public IMasterDataService masterDataService { get; set; }
        //this is use for API Function
        [Inject]
        public IUnitMasterService unitmasterService { get; set; }
        [Inject]
        public ISubjectMasterService _subjectMasterService { get; set; }

        //this model use for send data to API ,binding view model with API model
        public MasterUnitAPIModel  unitMasterAPIModel { get; set; }
        //data binding for blazor page
        public MasterUnitViewModel  masterUnitViewModel = new MasterUnitViewModel();

        //list Model
        public List<MasterUnitListModel>  unitMasterListModels = new List<MasterUnitListModel>();
        public List<MasterClassListModel>  masterClassLists=new List<MasterClassListModel>();
        public List<MastersSubjectListModel>  mastersSubjectLists = new List<MastersSubjectListModel>();


        public SessionModel _sessionModel;
        public string OperationType = "";
        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        public List<string> classMasterItem = (new List<string>() { "Add Unit", "Print", "Search" });

        public SfGrid<MasterUnitListModel>  sfUnitGrid;
        public async Task CloseDialog()
        {
            IsVisible = false;
            await this.DialogRef.HideAsync();
        }
        public DialogEffect AnimationEffect = DialogEffect.Zoom;
        public string HeaderStyles { get; set; } = "e-background e-accent";
        public SfDialog DialogRef;
        public bool IsVisible { get; set; } = false;
        public string DialogHeaderName = string.Empty;
        public bool ddEnable = true;
        public void onOpen(Syncfusion.Blazor.Popups.BeforeOpenEventArgs args)
        {
            // setting maximum height to the Dialog
            args.MaxHeight = "750px";

        }
        public string btncss = "";
        public string HeaderText = string.Empty;
        public void EditBatchMaster(CommandClickEventArgs<MasterUnitListModel> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            if (buttontext == "Edit")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                IsVisible = true;
                DialogHeaderName = "Update Unit Details";
                HeaderText = "Update Record";
                OperationType = "Update";
                btncss = "e-flat e-info e-outline";
                ddEnable = false;
                masterUnitViewModel = new  MasterUnitViewModel()
                {
                    masterClassId =args.RowData.masterClassId.ToString(),
                    masterSubjectId = args.RowData.masterSubjectId.ToString(),
                    subjectUnitId=args.RowData.subjectUnitId,   
                    unitDescription = args.RowData.unitDescription,
                    unitTitle  = args.RowData.unitTitle,
                };
            }
            else
            {
                IsVisible = true;
                DialogHeaderName = "Delete Unit Details";
                OperationType = "Delete";
                HeaderText = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                ddEnable = false;
                masterUnitViewModel = new MasterUnitViewModel()
                {
                    masterClassId = args.RowData.masterClassId.ToString(),
                    masterSubjectId = args.RowData.masterSubjectId.ToString(),
                    subjectUnitId = args.RowData.subjectUnitId,
                    unitDescription = args.RowData.unitDescription,
                    unitTitle = args.RowData.unitTitle,
                };
            }
            //else
            //{
            //    navigationManager.NavigateTo($"/OnlineExam/OnlineTestDetails/{testId}");
            //}

        }
        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add Unit")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add New Unit Details";
                OperationType = "Add";
                HeaderText = "Add Unit";
                ddEnable = true;
                masterUnitViewModel.unitTitle = null;
                masterUnitViewModel.unitDescription = null;
                masterUnitViewModel.masterClassId = null;
                masterUnitViewModel.masterSubjectId = null;
            }
        }

       
        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            DefaultInputParameterModel defaultInputParameterModel = new DefaultInputParameterModel()
            {
                SchoolCode = _sessionModel.SchoolCode,
                FinancialYear = _sessionModel.FinancialYear,
                OperationType = ReportType.All,
                Userid = _sessionModel.UserId

            };
            GetUnitMasterList();           
            masterClassLists = (await masterDataService.GetMasterClassList(defaultInputParameterModel)).ToList();           
            subjectDetailsModel = (await _subjectMasterService.SubjectDetails(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.UserId, "All"));
            mapSubjectList = subjectDetailsModel.masterSubjectListModels;
            SubjectList(0);

        }

        private async void GetUnitMasterList()
        {
            DefaultInputParameterModel defaultInputParameterModel = new DefaultInputParameterModel()
            {
                SchoolCode = _sessionModel.SchoolCode,
                FinancialYear = _sessionModel.FinancialYear,
                OperationType = ReportType.All,
                Userid = _sessionModel.UserId

            };
            unitMasterListModels = (await unitmasterService.GetUnitMasterDetails(defaultInputParameterModel)).ToList();
        }
        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                unitMasterAPIModel = new MasterUnitAPIModel()
                {
                     subjectUnitId = masterUnitViewModel.subjectUnitId,
                     masterClassId =Convert.ToInt16(masterUnitViewModel.masterClassId),
                     masterSubjectId=Convert.ToInt16(masterUnitViewModel.masterSubjectId),
                     unitTitle= masterUnitViewModel.unitTitle,
                     unitDescription= masterUnitViewModel.unitDescription,
                      activeStatus=1,
                    createdByUserId = _sessionModel.UserId,
                    schoolCode = _sessionModel.SchoolCode,
                    FinancialYear = _sessionModel.FinancialYear,
                };

                if (OperationType == "Add")
                {
                    unitMasterAPIModel.operationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    unitMasterAPIModel.operationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    unitMasterAPIModel.operationType = OperationAction.Delete;
                }
                SaveUnitDetails(unitMasterAPIModel);
            };
        }
        public APIReturnModel aPIReturnmodel;
        [Inject]
        public ISnackbar snackBar { get; set; }

        public async void SaveUnitDetails(MasterUnitAPIModel unitMasterAPIModel)
        {
            aPIReturnmodel = (await unitmasterService.AddUpdateUnitMaster(unitMasterAPIModel));
            if (aPIReturnmodel.status == false)
            {
                if(unitMasterAPIModel.operationType == OperationAction.Delete)
                {
                    snackBar.Add(aPIReturnmodel.message, Severity.Warning);
                }
                else if(unitMasterAPIModel.operationType != OperationAction.Add) 
                { 
                    snackBar.Add(aPIReturnmodel.message, Severity.Info); 
                }   
                else
                {
                    snackBar.Add(aPIReturnmodel.message, Severity.Success);
                }

                HeaderText = "Save Record";
                GetUnitMasterList();
                ClearAllFields();
            }
            else
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Error);
            }
        }
        private void ClearAllFields()
        {

        }
        public void OnChangeClass(ChangeEventArgs<string, MasterClassListModel> args)
        {
            try
            {
                if (args.ItemData.masterClassId != 0 && args.ItemData.masterClassId != null)
                {
                    int classid = args.ItemData.masterClassId;
                    SubjectList(classid);

                    //_mapsubjectlistModel = subj.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
        public SubjectDetailsModel subjectDetailsModel;
        public List<MasterSubjectListModel> mapSubjectList = new List<MasterSubjectListModel>();
        public List<MapsubjectwithClassModel> _mapsubjectlistModel = new List<MapsubjectwithClassModel>();
        public List<MapsubjectwithClassModel> _mastersubjectlistModel = new List<MapsubjectwithClassModel>();
        private void SubjectList(int ClassID)
        {
            if (ClassID == 0)
            {
                _mastersubjectlistModel = subjectDetailsModel.mapsubjectwithClassModels;
                _mapsubjectlistModel = _mastersubjectlistModel.ToList();
            }
            else
            {
                _mapsubjectlistModel = _mastersubjectlistModel.Where(e => e.masterClassId == ClassID).ToList();
            }

        }
    
    }
}
