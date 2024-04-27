using AdminDashboard.Server.Service.Interface.MasterData;
using AIS.Data.BaseClass;
using AIS.Data.RequestResponseModel.MasterData;
using AIS.Model;
using AIS.Model.GeneralConversion;
using AdminDashboard.Server.Models.MasterData;
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
    public class ClassMasterBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }

        //this model is use for get data userdetails and finaciyal year.
        public SessionModel sessionModel { get; }

        //this is use for API Function
        [Inject]
        public IMasterDataService masterDataService { get; set; }

        //this model use for send data to API ,binding view model with API model
        public MasterClassAPIInputModel masterClassAPIInputModel { get; set; }
        //data binding for blazor page
        public MasterClassViewModel masterClassViewModel = new MasterClassViewModel();

        //list Model
        public List<MasterClassListModel> masterClassListModels = new List<MasterClassListModel>();

        public SessionModel _sessionModel;
        public string OperationType = "";

        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                masterClassAPIInputModel = new MasterClassAPIInputModel()
                {
                    className = masterClassViewModel.className,
                    classCode = masterClassViewModel.classCode,
                    displayOrder = Convert.ToInt16(masterClassViewModel.displayOrder),
                    masterClassId = Convert.ToInt16( masterClassViewModel.masterClassId),
                    CreatedByUserId = _sessionModel.UserId,
                    SchoolCode = _sessionModel.SchoolCode,
                    FinancialYear = _sessionModel.FinancialYear,
                };
                
                if(OperationType == "Add")
                {
                    masterClassAPIInputModel.OperationType = OperationAction.Add;
                }
                else if(OperationType == "Update")
                {
                    masterClassAPIInputModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    masterClassAPIInputModel.OperationType = OperationAction.Delete;
                }
                SaveClassDetails(masterClassAPIInputModel);
            };
        }
        public APIReturnModel aPIReturnmodel;
        [Inject]
        public ISnackbar snackBar { get; set; }

        public async void SaveClassDetails(MasterClassAPIInputModel masterClassModel)
        {
            aPIReturnmodel = (await masterDataService.AddUpdateDeleteMasterClass(masterClassModel));
            if (aPIReturnmodel.status == false)
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Info);
                HeaderTest = "Save Record";
                GetClassList();
                ClearAllFields();
            }
            else
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Error);
            }
        }
        //Get Class List
        private async Task GetClassList()
        {
            DefaultInputParameterModel defaultInputParameterModel = new DefaultInputParameterModel()
            {
                SchoolCode = _sessionModel.SchoolCode,
                FinancialYear = _sessionModel.FinancialYear,
                OperationType = ReportType.All
            };
            //Get all list from API.
            masterClassListModels = (await masterDataService.GetMasterClassList(defaultInputParameterModel)).ToList();
        }
        private void ClearAllFields()
        {
            masterClassViewModel.className = null;
            masterClassViewModel.classCode = null;
            masterClassViewModel.displayOrder = null;
        }
        protected override async Task OnInitializedAsync()
        {
           // System.Diagnostics.Debugger.Break();
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            //Get Class List Details
            DefaultInputParameterModel defaultInputParameterModel = new DefaultInputParameterModel()
            {
                SchoolCode = _sessionModel.SchoolCode,
                FinancialYear = _sessionModel.FinancialYear,
                OperationType = ReportType.All
            };
            //Get all list from API.
            GetClassList();

            //masterClassListModels = (await masterDataService.GetMasterClassList(defaultInputParameterModel)).ToList();
        }
        public string HeaderTest = string.Empty;
        //for API Model to View model data binding.
        public void RowSelectHandler(RowSelectEventArgs<MasterClassListModel> args)
        {
            masterClassViewModel = new MasterClassViewModel()
            {
                classCode = args.Data.classCode,
                className = args.Data.className,
                displayOrder = Convert.ToInt16( args.Data.displayOrder),
                masterClassId = args.Data.masterClassId
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
        public List<string> classMasterItem = (new List<string>() { "Add Class", "Print", "Search" });

        public SfGrid<MasterClassListModel> sfClassMaster;
        public string btncss = "";
        public void EditClassMaster(CommandClickEventArgs<MasterClassListModel> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            if (buttontext == "Edit")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                IsVisible = true;
                DialogHeaderName = "Update Class Details";
                HeaderTest = "Update Record";
                OperationType = "Update";
                btncss = "e-flat e-info e-outline";
                masterClassViewModel = new MasterClassViewModel()
                {
                    classCode = args.RowData.classCode,
                    className = args.RowData.className,
                    displayOrder = Convert.ToInt16( args.RowData.displayOrder),
                    masterClassId = args.RowData.masterClassId
                };
            }
            else
            {
                IsVisible = true;
                DialogHeaderName = "Delete Class Details";
                OperationType = "Delete";
                HeaderTest = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                masterClassViewModel = new MasterClassViewModel()
                {
                    classCode = args.RowData.classCode,
                    className = args.RowData.className,
                    displayOrder = Convert.ToInt16( args.RowData.displayOrder),
                    masterClassId = args.RowData.masterClassId
                };
            }
            //else
            //{
            //    navigationManager.NavigateTo($"/OnlineExam/OnlineTestDetails/{testId}");
            //}

        }
        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add Class")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add New Class Details";
                OperationType = "Add";
                HeaderTest = "Add Class";
                masterClassViewModel.className = null;
                masterClassViewModel.classCode = null;
                masterClassViewModel.displayOrder = null;
            }
        }
    }
}



