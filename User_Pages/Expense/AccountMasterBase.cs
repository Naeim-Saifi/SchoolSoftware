using AdminDashboard.Server.API_Service.Interface.Expense;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.Expense;
using AIS.Data.RequestResponseModel.TimeTableSetUp;
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.User_Pages.Expense
{
    public class AccountMasterBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;

        public AccountTypeMasterViewModel _accountTypeViewModel = new AccountTypeMasterViewModel();
        public AccountTypeMasterAPIModel _accountAPIModel = new AccountTypeMasterAPIModel();
        [Inject]
        public IExpenseService  _expenseService { get; set; }
        public List<AccountTypeListModel>  accountTypeList = new List<AccountTypeListModel>();

        public SfGrid<AccountTypeListModel> sfAccountMaster;
        public string OperationType = "";
        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        public List<string> classMasterItem = (new List<string>() { "Add Account", "Print", "ExportExcel", "Collapse All", "Expand All", "Search", "ColumnChooser" });
        protected override async Task OnInitializedAsync()
        {

            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            GetAccountMasterList();
        }

        private async void GetAccountMasterList()
        {
            AccountType_Input_Para_Model accountType_Input_Para_Model = new AccountType_Input_Para_Model()
            {
                financialYear = _sessionModel.FinancialYear,
                 accountId = 0,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.All
            };

            accountTypeList = (await _expenseService.Get_AccountType_List(accountType_Input_Para_Model)).ToList();
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
        public bool ddEnable = true;
        public void onOpen(Syncfusion.Blazor.Popups.BeforeOpenEventArgs args)
        {
            // setting maximum height to the Dialog
            args.MaxHeight = "750px";

        }
        public string btncss = "";
        public string HeaderText = string.Empty;
        public void EditAccountMaster(CommandClickEventArgs<AccountTypeListModel> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            if (buttontext == "Edit")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                IsVisible = true;
                DialogHeaderName = "Update Account Details";
                HeaderText = "Update Record";
                OperationType = "Update";
                btncss = "e-flat e-info e-outline";
                ddEnable = false;
                _accountTypeViewModel = new AccountTypeMasterViewModel()
                {
                     AccountTypeID = args.RowData.accountTypeID,
                     AccountType = args.RowData.accountType,
                     Description = args.RowData.description,
                    
                };


            }
            else
            {
                IsVisible = true;
                DialogHeaderName = "Delete Account Details";
                OperationType = "Delete";
                HeaderText = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                ddEnable = false;
                _accountTypeViewModel = new AccountTypeMasterViewModel()
                {
                    AccountTypeID = args.RowData.accountTypeID,
                    AccountType = args.RowData.accountType,
                    Description = args.RowData.description,

                };
            }
            //else
            //{
            //    navigationManager.NavigateTo($"/OnlineExam/OnlineTestDetails/{testId}");
            //}

        }

        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add Account")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add Account Master";
                OperationType = "Add";
                HeaderText = "Add Account";
                ddEnable = true;

            }
            else if (args.Item.Text == "ExportExcel")
            {
                this.sfAccountMaster.ExportToExcelAsync();
            }
            else if (args.Item.Text == "Collapse All")
            {
                this.sfAccountMaster.CollapseAllGroupAsync();
            }
            else if (args.Item.Text == "Expand All")
            {
                this.sfAccountMaster.ExpandAllGroupAsync();
            }
        }


        [Inject]
        public ISnackbar snackBar { get; set; }
        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                _accountAPIModel = new AccountTypeMasterAPIModel()
                {
                     AccountType = _accountTypeViewModel.AccountType.Trim().ToUpper(),
                     AccountTypeID = _accountTypeViewModel.AccountTypeID,
                     Description = _accountTypeViewModel.Description.Trim(),                   
                    CreatedByUserId = _sessionModel.UserId,
                    SchoolCode = _sessionModel.SchoolCode,
                    FinancialYear = _sessionModel.FinancialYear,
                };

                if (OperationType == "Add")
                {
                    _accountAPIModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    _accountAPIModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    _accountAPIModel.OperationType = OperationAction.Delete;
                }
                SaveAccountTypeDetails(_accountAPIModel);
            };
        }
        public APIReturnModel aPIReturnmodel;


        public async void SaveAccountTypeDetails(AccountTypeMasterAPIModel periodModel)
        {
            aPIReturnmodel = (await _expenseService.CRUD_AccountType(periodModel));
            if (aPIReturnmodel.status == false)
            {
                if (periodModel.OperationType == OperationAction.Delete)
                {
                    snackBar.Add(aPIReturnmodel.message, Severity.Error);
                }
                else if (periodModel.OperationType != OperationAction.Add)
                {
                    snackBar.Add(aPIReturnmodel.message, Severity.Info);
                }
                else
                {
                    snackBar.Add(aPIReturnmodel.message, Severity.Success);
                }

                HeaderText = "Save Record";
                GetAccountMasterList();
                ClearAllFields();
            }
            else
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Error);
            }
        }
        private void ClearAllFields()
        {

            _accountTypeViewModel.AccountTypeID = 0;
            _accountTypeViewModel.AccountType = null;
            _accountTypeViewModel.Description = null;
        }
    }
}