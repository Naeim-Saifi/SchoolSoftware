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
    public class SubjectMasterBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }

        //this model is use for get data userdetails and finaciyal year.
        public SessionModel sessionModel { get; }

        //this is use for API Function
        [Inject]
        public IMasterDataService masterDataService { get; set; }

        //this model use for send data to API ,binding view model with API model
        public MasterSubjectAPIInputModel masterSubjectAPIInputModel { get; set; }
        //data binding for blazor page
        public SubjectMasterViewModel masterSubjectViewModel = new SubjectMasterViewModel();

        //list Model
        public List<MastersSubjectListModel> masterSubjectListModels = new List<MastersSubjectListModel>();

        public SessionModel _sessionModel;
        public string OperationType = "";

        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                masterSubjectAPIInputModel = new MasterSubjectAPIInputModel()
                {
                    subjectName = masterSubjectViewModel.subjectName,
                    subjectCode = masterSubjectViewModel.subjectCode,
                    displayOrder = Convert.ToInt16(masterSubjectViewModel.displayOrder),
                    masterSubjectId = masterSubjectViewModel.masterSubjectId,
                    CreatedByUserId = _sessionModel.UserId,
                    SchoolCode = _sessionModel.SchoolCode,
                    FinancialYear = _sessionModel.FinancialYear,
                };

                if (OperationType == "Add")
                {
                    masterSubjectAPIInputModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    masterSubjectAPIInputModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    masterSubjectAPIInputModel.OperationType = OperationAction.Delete;
                }
                SaveSubjectDetails(masterSubjectAPIInputModel);
            };
        }
        public APIReturnModel aPIReturnmodel;
        [Inject]
        public ISnackbar snackBar { get; set; }

        public async void SaveSubjectDetails(MasterSubjectAPIInputModel masterSubjectModel)
        {
            aPIReturnmodel = (await masterDataService.AddUpdateDeleteSubjectMaster(masterSubjectModel));
            if (aPIReturnmodel.status == false)
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Info);
                HeaderTest = "Save Record";
                GetSubjectList();
                ClearAllFields();
            }
            else
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Error);
            }
        }
        //Get Subject List
        private async Task GetSubjectList()
        {
            DefaultInputParameterModel defaultInputParameterModel = new DefaultInputParameterModel()
            {
                SchoolCode = _sessionModel.SchoolCode,
                FinancialYear = _sessionModel.FinancialYear,
                OperationType = ReportType.All
            };
            //Get all list from API.
            masterSubjectListModels = (await masterDataService.GetSubjectList(defaultInputParameterModel)).ToList();
        }
        private void ClearAllFields()
        {
            masterSubjectViewModel.subjectName = null;
            masterSubjectViewModel.subjectCode = null;
            masterSubjectViewModel.displayOrder = null;
        }
        protected override async Task OnInitializedAsync()
        {
            // System.Diagnostics.Debugger.Break();
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            //Get Subject List Details
            DefaultInputParameterModel defaultInputParameterModel = new DefaultInputParameterModel()
            {
                SchoolCode = _sessionModel.SchoolCode,
                FinancialYear = _sessionModel.FinancialYear,
                OperationType = ReportType.All
            };
            //Get all list from API.
            GetSubjectList();

            //masterSubjectListModels = (await masterDataService.GetMasterSubjectList(defaultInputParameterModel)).ToList();
        }
        public string HeaderTest = string.Empty;
        //for API Model to View model data binding.
        public void RowSelectHandler(RowSelectEventArgs<MastersSubjectListModel> args)
        {
            masterSubjectViewModel = new SubjectMasterViewModel()
            {
                subjectName = args.Data.subjectName,
                subjectCode = args.Data.subjectCode,
                displayOrder = Convert.ToInt16( args.Data.displayOrder),
                masterSubjectId = args.Data.masterSubjectId
            };
            HeaderTest = "Update Record";

        }
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
        public List<string> classMasterItem = (new List<string>() { "Add Subject", "Print", "Search" });

        public SfGrid<MastersSubjectListModel> sfSubjectMaster;
        public string btncss = "";
        public void EditSubjectMaster(CommandClickEventArgs<MastersSubjectListModel> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            if (buttontext == "Edit")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                IsVisible = true;
                DialogHeaderName = "Update Subject Details";
                HeaderTest = "Update Record";
                OperationType = "Update";
                btncss = "e-flat e-info e-outline";
                masterSubjectViewModel = new SubjectMasterViewModel()
                {
                    subjectName = args.RowData.subjectName,
                    subjectCode = args.RowData.subjectCode,
                    displayOrder = Convert.ToInt16( args.RowData.displayOrder),
                    masterSubjectId = args.RowData.masterSubjectId
                };
            }
            else
            {
                IsVisible = true;
                DialogHeaderName = "Delete Subject Details";
                OperationType = "Delete";
                HeaderTest = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                masterSubjectViewModel = new SubjectMasterViewModel()
                {
                    subjectName = args.RowData.subjectName,
                    subjectCode = args.RowData.subjectCode,
                    displayOrder = Convert.ToInt16( args.RowData.displayOrder),
                    masterSubjectId = args.RowData.masterSubjectId
                };
            }
            //else
            //{
            //    navigationManager.NavigateTo($"/OnlineExam/OnlineTestDetails/{testId}");
            //}

        }
        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add Subject")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add New Subject Details";
                OperationType = "Add";
                HeaderTest = "Add Subject";
                masterSubjectViewModel.subjectName = null;
                masterSubjectViewModel.subjectCode = null;
                masterSubjectViewModel.displayOrder = null;
            }
        }
    }
}
