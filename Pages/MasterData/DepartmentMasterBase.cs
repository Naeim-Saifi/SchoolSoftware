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
    public class DepartmentMasterBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }

        //this model is use for get data userdetails and finaciyal year.
        public SessionModel sessionModel { get; }

        //this is use for API Function
        [Inject]
        public IMasterDataService masterDataService { get; set; }

        //this model use for send data to API ,binding view model with API model
        public MasterDepartmentAPIInputModel masterDepartmentAPIInputModel { get; set; }
        //data binding for blazor page
        public DepartmentMasterViewModel masterDepartmentViewModel = new DepartmentMasterViewModel();

        //list Model
        public List<MasterDepartmentListModel> masterDepartmentListModels = new List<MasterDepartmentListModel>();

        public SessionModel _sessionModel;
        public string OperationType = "";

        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                masterDepartmentAPIInputModel = new MasterDepartmentAPIInputModel()
                {
                    deparmentName = masterDepartmentViewModel.deparmentName,
                    deparmentCode = masterDepartmentViewModel.deparmentCode,
                    displayOrder = Convert.ToInt16(masterDepartmentViewModel.displayOrder),
                    masterDeparmentId = masterDepartmentViewModel.masterDeparmentId,
                    CreatedByUserId = _sessionModel.UserId,
                    SchoolCode = _sessionModel.SchoolCode,
                    FinancialYear = _sessionModel.FinancialYear,
                };

                if (OperationType == "Add")
                {
                    masterDepartmentAPIInputModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    masterDepartmentAPIInputModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    masterDepartmentAPIInputModel.OperationType = OperationAction.Delete;
                }
                SaveDepartmentDetails(masterDepartmentAPIInputModel);
            };
        }
        public APIReturnModel aPIReturnmodel;
        [Inject]
        public ISnackbar snackBar { get; set; }

        public async void SaveDepartmentDetails(MasterDepartmentAPIInputModel masterDepartmentModel)
        {
            aPIReturnmodel = (await masterDataService.AddUpdateDeleteDepartmentMaster(masterDepartmentModel));
            if (aPIReturnmodel.status == false)
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Info);
                HeaderTest = "Save Record";
                GetDepartmentList();
                ClearAllFields();
            }
            else
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Error);
            }
        }
        //Get Department List
        private async Task GetDepartmentList()
        {
            DefaultInputParameterModel defaultInputParameterModel = new DefaultInputParameterModel()
            {
                SchoolCode = _sessionModel.SchoolCode,
                FinancialYear = _sessionModel.FinancialYear,
                OperationType = ReportType.All
            };
            //Get all list from API.
            masterDepartmentListModels = (await masterDataService.GetDepartmentList(defaultInputParameterModel)).ToList();
        }
        private void ClearAllFields()
        {
            masterDepartmentViewModel.deparmentName = null;
            masterDepartmentViewModel.deparmentCode = null;
            masterDepartmentViewModel.displayOrder = null;
        }
        protected override async Task OnInitializedAsync()
        {
            // System.Diagnostics.Debugger.Break();
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            //Get Department List Details
            DefaultInputParameterModel defaultInputParameterModel = new DefaultInputParameterModel()
            {
                SchoolCode = _sessionModel.SchoolCode,
                FinancialYear = _sessionModel.FinancialYear,
                OperationType = ReportType.All
            };
            //Get all list from API.
            GetDepartmentList();

            //masterDepartmentListModels = (await masterDataService.GetMasterDepartmentList(defaultInputParameterModel)).ToList();
        }
        public string HeaderTest = string.Empty;
        //for API Model to View model data binding.
        public void RowSelectHandler(RowSelectEventArgs<MasterDepartmentListModel> args)
        {
            masterDepartmentViewModel = new DepartmentMasterViewModel()
            {
                deparmentName = args.Data.departmentName,
                deparmentCode = args.Data.departmentCode,
                displayOrder = Convert.ToInt16( args.Data.displayOrder),
                masterDeparmentId = args.Data.masterDepartmentId
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
        public List<string> classMasterItem = (new List<string>() { "Add Department", "Print", "Search" });

        public SfGrid<MasterDepartmentListModel> sfDepartmentMaster;
        public string btncss = "";
        public void EditDepartmentMaster(CommandClickEventArgs<MasterDepartmentListModel> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            if (buttontext == "Edit")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                IsVisible = true;
                DialogHeaderName = "Update Department Details";
                HeaderTest = "Update Record";
                OperationType = "Update";
                btncss = "e-flat e-info e-outline";
                masterDepartmentViewModel = new DepartmentMasterViewModel()
                {
                    deparmentName = args.RowData.departmentName,
                    deparmentCode = args.RowData.departmentCode,
                    displayOrder = Convert.ToInt16( args.RowData.displayOrder),
                    masterDeparmentId = args.RowData.masterDepartmentId
                };
            }
            else
            {
                IsVisible = true;
                DialogHeaderName = "Delete Department Details";
                OperationType = "Delete";
                HeaderTest = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                masterDepartmentViewModel = new DepartmentMasterViewModel()
                {
                    deparmentName = args.RowData.departmentName,
                    deparmentCode = args.RowData.departmentCode,
                    displayOrder = Convert.ToInt16( args.RowData.displayOrder),
                    masterDeparmentId = args.RowData.masterDepartmentId
                };
            }
            //else
            //{
            //    navigationManager.NavigateTo($"/OnlineExam/OnlineTestDetails/{testId}");
            //}

        }
        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add Department")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add New Department Details";
                OperationType = "Add";
                HeaderTest = "Add Department";
                masterDepartmentViewModel.deparmentName = null;
                masterDepartmentViewModel.deparmentCode = null;
                masterDepartmentViewModel.displayOrder = null;
            }
        }
    }
}
