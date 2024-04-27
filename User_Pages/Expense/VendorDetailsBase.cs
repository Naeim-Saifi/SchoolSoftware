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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.User_Pages.Expense
{
    public class VendorDetailsBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;

        public VenderMasterViewModel _venderMasterViewModel = new VenderMasterViewModel();
        public VenderMasterAPIModel _venderMasterAPIModel = new VenderMasterAPIModel();

        [Inject]
        public IExpenseService _expenseService { get; set; }
        public List<VenderMasterListModel>  venderMasterList = new List<VenderMasterListModel>();
        public List<AccountTypeListModel> accountTypeList = new List<AccountTypeListModel>();

        public SfGrid<VenderMasterListModel> sfVenderMaster;

        public string OperationType = "";
        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        public List<string> classMasterItem = (new List<string>() { "Add Account", "Print", "ExportExcel", "Collapse All", "Expand All", "Search", "ColumnChooser" });
        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            AccountType_Input_Para_Model accountType_Input_Para_Model = new AccountType_Input_Para_Model()
            {
                financialYear = _sessionModel.FinancialYear,
                accountId = 0,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.All
            };

            accountTypeList = (await _expenseService.Get_AccountType_List(accountType_Input_Para_Model)).ToList();
            GetVendorMasterList();
        }

        private async void GetVendorMasterList()
        {
            Vendor_Input_Para_Model vendor_Input_Para_Model = new Vendor_Input_Para_Model()
            {
                financialYear = _sessionModel.FinancialYear,
                 vendorId = 0,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.All
            };

            venderMasterList = (await _expenseService.Get_VendorDetails_List(vendor_Input_Para_Model)).ToList();


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
        public void EditVenderMaster(CommandClickEventArgs<VenderMasterListModel> args)
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
                _venderMasterViewModel = new VenderMasterViewModel()
                {
                      AccountID  = args.RowData.accountID.ToString(),
                     AccountNumber = args.RowData.accountNumber,
                     AddressDetail = args.RowData.addressDetail,
                     BankName = args.RowData.bankName,
                     BranchNumber = args.RowData.branchName,
                     ContactName = args.RowData.contactName,
                     IfscCode = args.RowData.ifscCode,
                     Mobilenumber = args.RowData.mobilenumber,
                     OrganizationName = args.RowData.organizationName,
                     AccountTypeID =args.RowData.accountType.ToString(),
                     
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
                _venderMasterViewModel = new VenderMasterViewModel()
                {
                    AccountID = args.RowData.accountID.ToString(),
                    AccountNumber = args.RowData.accountNumber,
                    AddressDetail = args.RowData.addressDetail,
                    BankName = args.RowData.bankName,
                    BranchNumber = args.RowData.branchName,
                    ContactName = args.RowData.contactName,
                    IfscCode = args.RowData.ifscCode,
                    Mobilenumber = args.RowData.mobilenumber,
                    OrganizationName = args.RowData.organizationName,
                    AccountTypeID = args.RowData.accountType.ToString(),

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
                this.sfVenderMaster.ExportToExcelAsync();
            }
            else if (args.Item.Text == "Collapse All")
            {
                this.sfVenderMaster.CollapseAllGroupAsync();
            }
            else if (args.Item.Text == "Expand All")
            {
                this.sfVenderMaster.ExpandAllGroupAsync();
            }
        }
        [Inject]
        public ISnackbar snackBar { get; set; }
        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                _venderMasterAPIModel = new VenderMasterAPIModel()
                {
                    AccountID =Convert.ToInt32(_venderMasterViewModel.AccountID),
                    OrganizationName = _venderMasterViewModel.OrganizationName.Trim(),
                    AccountTypeID =Convert.ToInt16(_venderMasterViewModel.AccountTypeID),
                    ContactName = _venderMasterViewModel.ContactName.Trim(),
                    Mobilenumber = _venderMasterViewModel.Mobilenumber.Trim(),

                    AccountNumber = _venderMasterViewModel.AccountNumber,
                    AddressDetail = _venderMasterViewModel.AddressDetail.Trim(),
                    BankName = _venderMasterViewModel.BankName.Trim(),
                    BranchNumber = _venderMasterViewModel.BranchNumber.Trim(),
                    IfscCode = _venderMasterViewModel.IfscCode.Trim(),
                     
                    CreatedByUserId = _sessionModel.UserId,
                    UpdatedByUserId = _sessionModel.UserId,
                    SchoolCode = _sessionModel.SchoolCode,
                    FinancialYear = _sessionModel.FinancialYear,
                };

                if (OperationType == "Add")
                {
                    _venderMasterAPIModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    _venderMasterAPIModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    _venderMasterAPIModel.OperationType = OperationAction.Delete;
                }
                SaveVendorDetails(_venderMasterAPIModel);
            };
        }
        public APIReturnModel aPIReturnmodel;

        public async void SaveVendorDetails(VenderMasterAPIModel _venderMasterAPIModel)
        {
            aPIReturnmodel = (await _expenseService.CRUD_VendorDetails(_venderMasterAPIModel));
            if (aPIReturnmodel.status == false)
            {
                if (_venderMasterAPIModel.OperationType == OperationAction.Delete)
                {
                    snackBar.Add(aPIReturnmodel.message, Severity.Error);
                }
                else if (_venderMasterAPIModel.OperationType != OperationAction.Add)
                {
                    snackBar.Add(aPIReturnmodel.message, Severity.Info);
                }
                else
                {
                    snackBar.Add(aPIReturnmodel.message, Severity.Success);
                }

                HeaderText = "Save Record";
                GetVendorMasterList();
                ClearAllFields();
            }
            else
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Error);
            }
        }
        private void ClearAllFields()
        {

            _venderMasterViewModel=new VenderMasterViewModel();
            
        }
    }
}
