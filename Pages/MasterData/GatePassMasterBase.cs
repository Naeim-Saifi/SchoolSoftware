using AdminDashboard.Server.Models.MasterData;
using AdminDashboard.Server.Service.Interface.MasterData;
using AIS.Data.BaseClass;
using AIS.Data.RequestResponseModel.MasterData;
using AIS.Model;
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.MasterData
{
    public class GatePassMasterBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }

        //this model is use for get data userdetails and finaciyal year.
        public SessionModel sessionModel { get; }

        //this is use for API Function
        [Inject]
        public IMasterDataService masterDataService { get; set; }

        //this model use for send data to API ,binding view model with API model
        public MasterStudentGatePassDetailsAPIInputModel masterStudentGatePassDetailsAPIInputModel { get; set; }
        //data binding for blazor page
        public MasterStudentGatePassDetailsViewModel masterStudentGatePassDetailsViewModel = new MasterStudentGatePassDetailsViewModel();

        //list Model
        public List<MasterStudentGatePassDetailsListModel> masterStudentGatePassDetailsListModels = new List<MasterStudentGatePassDetailsListModel>();

        public SessionModel _sessionModel;
        public string OperationType = "";

        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {

                masterStudentGatePassDetailsAPIInputModel = new MasterStudentGatePassDetailsAPIInputModel()
                {
                    admissionNo = masterStudentGatePassDetailsViewModel.admissionNo,
                    fatherName = masterStudentGatePassDetailsViewModel.fatherName,
                    nameOfStudent = masterStudentGatePassDetailsViewModel.nameOfStudent,
                    classSection = masterStudentGatePassDetailsViewModel.classSection,
                    address = masterStudentGatePassDetailsViewModel.address,
                    currentDateTime = masterStudentGatePassDetailsViewModel.currentDateTime,
                    rollNo = masterStudentGatePassDetailsViewModel.rollNo,
                    phoneNo = masterStudentGatePassDetailsViewModel.phoneNo,
                    visitorName = masterStudentGatePassDetailsViewModel.visitorName,
                    visitorMobileNo = masterStudentGatePassDetailsViewModel.visitorMobileNo,
                    remarks = masterStudentGatePassDetailsViewModel.remarks,
                    masterGatePassId = masterStudentGatePassDetailsViewModel.masterGatePassId,
                    displayOrder = Convert.ToInt16( masterStudentGatePassDetailsViewModel.displayOrder),
                    CreatedByUserId = _sessionModel.UserId,
                    SchoolCode = _sessionModel.SchoolCode,
                    FinancialYear = _sessionModel.FinancialYear,
                };

                if (OperationType == "Add")
                {
                    masterStudentGatePassDetailsAPIInputModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    masterStudentGatePassDetailsAPIInputModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    masterStudentGatePassDetailsAPIInputModel.OperationType = OperationAction.Delete;
                }
                SaveGatePassMasterDetails(masterStudentGatePassDetailsAPIInputModel);
            };
        }
        public APIReturnModel aPIReturnmodel;
        [Inject]
        public ISnackbar snackBar { get; set; }

        public async void SaveGatePassMasterDetails(MasterStudentGatePassDetailsAPIInputModel masterStudentGatePassDetailsAPIInputModel)
        {
            aPIReturnmodel = (await masterDataService.AddUpdateDeleteMasterStudentGatePassDetails(masterStudentGatePassDetailsAPIInputModel));
            if (aPIReturnmodel.status == false)
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Info);
                HeaderTest = "Save Record";
                GetGatePassMasterList();
                ClearAllFields();
            }
            else
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Error);
            }
        }
        //Get Subject List
        private async Task GetGatePassMasterList()
        {
            DefaultInputParameterModel defaultInputParameterModel = new DefaultInputParameterModel()
            {
                SchoolCode = _sessionModel.SchoolCode,
                FinancialYear = _sessionModel.FinancialYear,
                OperationType = ReportType.All
            };
            //Get all list from API.
            masterStudentGatePassDetailsListModels = (await masterDataService.GetMasterStudentGatePassDetailsList(defaultInputParameterModel)).ToList();
        }
        private void ClearAllFields()
        {
            masterStudentGatePassDetailsViewModel.displayOrder = null;
            masterStudentGatePassDetailsViewModel.admissionNo = 0;
            masterStudentGatePassDetailsViewModel.fatherName = null;
            masterStudentGatePassDetailsViewModel.nameOfStudent = null;
            masterStudentGatePassDetailsViewModel.rollNo = null;
            masterStudentGatePassDetailsViewModel.classSection = null;
            //masterStudentGatePassDetailsViewModel.currentDateTime = DateTime;
            masterStudentGatePassDetailsViewModel.phoneNo = null;
            masterStudentGatePassDetailsViewModel.visitorName = null;
            masterStudentGatePassDetailsViewModel.visitorMobileNo = null;
            masterStudentGatePassDetailsViewModel.remarks = null;

        }
        protected override async Task OnInitializedAsync()
        {
            // System.Diagnostics.Debugger.Break();
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");

            //Get all list from API.
            GetGatePassMasterList();

        }
        public string HeaderTest = string.Empty;
        //for API Model to View model data binding.
        //public void RowSelectHandler(RowSelectEventArgs<MasterExamTypeDetailsViewModel> args)
        //{
        //    examTypeDetailsViewModel = new MasterExamTypeDetailsViewModel()
        //    {
        //         examDescription = args.Data.examDescription,
        //        examName = args.Data.examName,
        //         displayOrder = args.Data.displayOrder,
        //         masterExamtypeId = args.Data.masterExamtypeId
        //    };
        //    HeaderTest = "Update Record";

        //}
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
        public void onOpen(Syncfusion.Blazor.Popups.BeforeOpenEventArgs args)
        {
            // setting maximum height to the Dialog
            args.MaxHeight = "750px";

        }
        public List<string> ToolBarItems = (new List<string>() { "Add", "Edit", "Print", "Search" });
        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        public List<string> classMasterItem = (new List<string>() { "Add Gate Pass", "Print", "Search" });

        public SfGrid<MasterStudentGatePassDetailsListModel> sfGatePassMaster;
        public string btncss = "";
        public void EditGatePassMaster(CommandClickEventArgs<MasterStudentGatePassDetailsListModel> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            if (buttontext == "Edit")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                IsVisible = true;
                DialogHeaderName = "Update Gate Pass Details";
                HeaderTest = "Update Record";
                OperationType = "Update";
                btncss = "e-flat e-info e-outline";
                masterStudentGatePassDetailsViewModel = new MasterStudentGatePassDetailsViewModel()
                {
                    admissionNo = args.RowData.admissionNo,
                    fatherName = args.RowData.fatherName,
                    nameOfStudent = args.RowData.nameOfStudent,
                    rollNo = args.RowData.rollNo,
                    address = args.RowData.address,
                    classSection = args.RowData.classSection,
                    phoneNo = args.RowData.phoneNo,
                    currentDateTime = args.RowData.currentDateTime,
                    visitorName = args.RowData.visitorName,
                    visitorMobileNo = args.RowData.visitorMobileNo,
                    remarks = args.RowData.remarks,
                    displayOrder = Convert.ToInt16( args.RowData.displayOrder),
                    masterGatePassId = args.RowData.masterStudentGatePassId,
                };
            }
            else
            {
                IsVisible = true;
                DialogHeaderName = "Delete Gate Pass Details";
                OperationType = "Delete";
                HeaderTest = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                masterStudentGatePassDetailsViewModel = new MasterStudentGatePassDetailsViewModel()
                {
                    admissionNo = args.RowData.admissionNo,
                    fatherName = args.RowData.fatherName,
                    nameOfStudent = args.RowData.nameOfStudent,
                    rollNo = args.RowData.rollNo,
                    address = args.RowData.address,
                    classSection = args.RowData.classSection,
                    phoneNo = args.RowData.phoneNo,
                    currentDateTime = args.RowData.currentDateTime,
                    visitorName = args.RowData.visitorName,
                    visitorMobileNo = args.RowData.visitorMobileNo,
                    remarks = args.RowData.remarks,
                    displayOrder = Convert.ToInt16( args.RowData.displayOrder),
                    masterGatePassId = args.RowData.masterStudentGatePassId,
                };
            }
            //else
            //{
            //    navigationManager.NavigateTo($"/OnlineExam/OnlineTestDetails/{testId}");
            //}

        }
        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add Gate Pass")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add New Gate Pass";
                OperationType = "Add";
                HeaderTest = "Add Gate Pass";
                masterStudentGatePassDetailsViewModel.displayOrder = null;
                masterStudentGatePassDetailsViewModel.admissionNo = 0;
                masterStudentGatePassDetailsViewModel.fatherName = null;
                masterStudentGatePassDetailsViewModel.nameOfStudent = null;
                masterStudentGatePassDetailsViewModel.rollNo = null;
                masterStudentGatePassDetailsViewModel.classSection = null;
                //masterStudentGatePassDetailsViewModel.currentDateTime = DateTime;
                masterStudentGatePassDetailsViewModel.phoneNo = null;
                masterStudentGatePassDetailsViewModel.visitorName = null;
                masterStudentGatePassDetailsViewModel.visitorMobileNo = null;
                masterStudentGatePassDetailsViewModel.remarks = null;

            }
        }
    }
}








