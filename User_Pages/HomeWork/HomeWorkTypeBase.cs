using AdminDashboard.Server.API_Service.Interface.HomeWork;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.HomeWorkSetUp;
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
using static AIS.Data.GeneralConversion.GeneralConversion;


namespace AdminDashboard.Server.User_Pages.HomeWork
{
    public class HomeWorkTypeBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        public IHomeWorkSetupService  homeWorkSetupService { get; set; }
        //list Model
        public List<HomeWorkType_OutPut_Model> _homeWorkType_list = new List<HomeWorkType_OutPut_Model>();
        //data binding for blazor page
        public HomeWorkTypeViewModel homeWorkTypeViewModel =
            new HomeWorkTypeViewModel();
        public APIReturnModel aPIReturnModel;
        public HomeWorkTypeAPIModel  homeWorkTypeAPIModel { get; set; }

        [Inject]
        public ISnackbar snackBar { get; set; }
        public SessionModel sessionModel { get; }

        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        public SfGrid<HomeWorkType_OutPut_Model> sfHomeWorkType;

        public List<string> toolBarItems = (new List<string>() { "Add HomeWork Type", "Print", "Search" });
        public DialogEffect AnimationEffect = DialogEffect.Zoom;
        public string HeaderStyles { get; set; } = "e-background e-accent";
        public SfDialog DialogRef;
        public bool IsVisible { get; set; } = false;
        public string DialogHeaderName = string.Empty;
        public bool ddEnable = true;
        public string btncss = "";
        public string HeaderText = string.Empty;
        //this model use for send data to API ,binding view model with API model

        public SessionModel _sessionModel;
        public string OperationType = "";

        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");

            HomeWork_Type_Input_Para_Model homeWork_Type_Input_Para_Model = new HomeWork_Type_Input_Para_Model()
            {
                schoolCode = _sessionModel.SchoolCode,
                 HomeWorkTypeId = 0,
                financialYear = _sessionModel.FinancialYear,
                reportType = ReportType.All
            };
            _homeWorkType_list = (await homeWorkSetupService.GET_HomeWorkType_LIST(homeWork_Type_Input_Para_Model)).ToList();

        }

        public void EditHomeWorkTypeMaster(CommandClickEventArgs<HomeWorkType_OutPut_Model> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            if (buttontext == "Edit")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                //click to open edit dialog
                IsVisible = true;
                DialogHeaderName = "Update Chapter Details";
                HeaderText = "Update Record";
                OperationType = "Update";
                btncss = "e-flat e-info e-outline";
                ddEnable = false;
                homeWorkTypeViewModel = new HomeWorkTypeViewModel()
                {
                    displayOrder = args.RowData.diaplayOrder,
                     homeWorkTypeCode = args.RowData.homeWorkCode,
                     homeworktypeId = args.RowData.homeworktypeId,
                     homeWorkTypeName = args.RowData.homeWorkTypeName,
                     
                };
            }
            else
            {
                IsVisible = true;
                DialogHeaderName = "Delete Chapter Details";
                OperationType = "Delete";
                HeaderText = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                ddEnable = false;
                homeWorkTypeViewModel = new HomeWorkTypeViewModel()
                {
                    displayOrder = args.RowData.diaplayOrder,
                    homeWorkTypeCode = args.RowData.homeWorkCode,
                    homeworktypeId = args.RowData.homeworktypeId,
                    homeWorkTypeName = args.RowData.homeWorkTypeName,

                };
            }

        }
        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add HomeWork Type")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add HomeWork Type Details";
                OperationType = "Add";
                HeaderText = "Add HomeWork Type";
                ddEnable = true;
                homeWorkTypeViewModel.displayOrder = 0;
                homeWorkTypeViewModel.homeWorkTypeName = null;
                homeWorkTypeViewModel.homeWorkTypeCode = null;
                homeWorkTypeViewModel.homeworktypeId = 0;
            }
        }

        public void onOpen(Syncfusion.Blazor.Popups.BeforeOpenEventArgs args)
        {
            // setting maximum height to the Dialog
            args.MaxHeight = "750px";

        }

        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                homeWorkTypeAPIModel = new HomeWorkTypeAPIModel()
                {
                     homeworktypeId = Convert.ToInt16(homeWorkTypeViewModel.homeworktypeId),
                    homeWorkTypeName = homeWorkTypeViewModel.homeWorkTypeName,
                    homeWorkTypeCode = homeWorkTypeViewModel.homeWorkTypeCode,
                    displayOrder = homeWorkTypeViewModel.displayOrder,
                    CreatedByUserId = _sessionModel.UserId,
                    UpdatedByUserId = _sessionModel.UserId,
                    SchoolCode = _sessionModel.SchoolCode,
                    FinancialYear = _sessionModel.FinancialYear,

                };

                if (OperationType == "Add")
                {
                    homeWorkTypeAPIModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    homeWorkTypeAPIModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    homeWorkTypeAPIModel.OperationType = OperationAction.Delete;
                }
                HomeWorkTypeSave(homeWorkTypeAPIModel);
            };
        }

        private async void HomeWorkTypeSave(HomeWorkTypeAPIModel homeWorkTypeAPIModel)
        {
            try
            {
                if (homeWorkTypeAPIModel.OperationType != "NA")
                {
                    aPIReturnModel = (await homeWorkSetupService.CRUD_HomeWorkType(homeWorkTypeAPIModel));
                    if (homeWorkTypeAPIModel.OperationType == OperationAction.Delete)
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Error);
                    }
                    else if (homeWorkTypeAPIModel.OperationType != OperationAction.Add)
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Info);
                    }
                    else
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Success);
                    }
                }
                else
                {
                    snackBar.Add(aPIReturnModel.message, Severity.Error);
                }

                HomeWork_Type_Input_Para_Model homeWork_Type_Input_Para_Model = new HomeWork_Type_Input_Para_Model()
                {
                    schoolCode = _sessionModel.SchoolCode,
                    HomeWorkTypeId = 0,
                    financialYear = _sessionModel.FinancialYear,
                    reportType = ReportType.All
                };
                _homeWorkType_list = (await homeWorkSetupService.GET_HomeWorkType_LIST(homeWork_Type_Input_Para_Model)).ToList();               
                ClearData();
                StateHasChanged();
            }
             
            catch (Exception ex)
            {

            }
        }
        private void ClearData()
        {
            homeWorkTypeViewModel.homeWorkTypeName = string.Empty;
            homeWorkTypeViewModel.homeWorkTypeCode = string.Empty;
            homeWorkTypeViewModel.displayOrder = 0;
        }
        public async Task CloseDialog()
        {
            IsVisible = false;
            await this.DialogRef.HideAsync();
        }
    }
}
