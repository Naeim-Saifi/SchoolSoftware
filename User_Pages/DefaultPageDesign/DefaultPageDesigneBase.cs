
using AdminDashboard.Server.API_Service.Interface.MasterDataSetUp;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.Enquiry;
using AIS.Data.RequestResponseModel.MasterDataSetUp;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AIS.Data.GeneralConversion.GeneralConversion;

namespace AdminDashboard.Server.User_Pages.DefaultPageDesign
{
    public class DefaultPageDesigneBase : ComponentBase
    {

        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;
        [Inject]
        public IMasterDataSetupService masterDataSetupService { get; set; }
        public EnquiryViewModel enquiryViewModel = new EnquiryViewModel();
        public List<Master_CLass_List_Output_Model> _classList = new List<Master_CLass_List_Output_Model>();
        public List<EnquiryListModel> _EnquiryDetailsList = new List<EnquiryListModel>();

        public SfGrid<EnquiryListModel> sfEnquiryDetails;
        public DialogEffect AnimationEffect = DialogEffect.Zoom;
        public string HeaderStyles { get; set; } = "e-background e-accent";
        public SfDialog DialogRef;
        public bool IsVisible { get; set; } = false;
        public bool IsDeleteVisible { get; set; } = false;
        public string DialogHeaderName = string.Empty;
        public bool ddEnable = true;
        public string btncss = "";
        public string HeaderText = string.Empty;
        public string OperationType = "";
        public APIReturnModel aPIReturnModel;

        public class Gender
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }

        public List<Gender> genderDetails = new List<Gender>
        {
            new Gender(){Id=1,Value="Male"},
            new Gender(){Id=2,Value="Female"},
        };
        public class VisitingStatus
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }

        public List<VisitingStatus> visitingStatus = new List<VisitingStatus>
        {
            new VisitingStatus(){Id=1,Value="Intrested"},
            new VisitingStatus(){Id=2,Value="Not Intrested"},
        };

        public class VisitingType
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }

        public List<VisitingType> visitingTypes = new List<VisitingType>
        {
            new VisitingType(){Id=1,Value="Self Visit"},
            new VisitingType(){Id=2,Value="Visit by School"},
            new VisitingType(){Id=2,Value="Student refrence"},
        };

        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            Master_CLass_List_Input_Para_Model master_CLass_List_Input_Para_Model = new Master_CLass_List_Input_Para_Model()
            {
                classId = 0,
                userId = _sessionModel.UserId,
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.All
            };
            _classList = (await masterDataSetupService.GET_Master_ClassLIST(master_CLass_List_Input_Para_Model)).ToList();
        }
        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                //EnquiryModel enquiryModel = new EnquiryModel()
                //{
                //    enquiryID = Convert.ToInt32(enquiryViewModel.enquiryID),
                //    studentName = enquiryViewModel.studentName,
                //    fatherName = enquiryViewModel.fatherName,
                //    fMobileNo = enquiryViewModel.fMobileNo,
                //    motherName = enquiryViewModel.motherName,
                //    mMobileNo = enquiryViewModel.mMobileNo,
                //    genderID = Convert.ToInt32(enquiryViewModel.Gender),
                //    previousSchoolName = enquiryViewModel.PreviousSchoolDetails,
                //    dob = enquiryViewModel.DOB.ToString(),
                //    visitingRemrks = enquiryViewModel.VisitingRemarks,
                //    visitingType = Convert.ToInt32(enquiryViewModel.VisitingType),
                //    visitingStatus = Convert.ToInt32(enquiryViewModel.VisitingStatus),
                //    emailID = enquiryViewModel.EmailId,
                //    currentClassID = Convert.ToInt32(enquiryViewModel.CurrentClass),
                //    promotedClassID = Convert.ToInt32(enquiryViewModel.NextClass),
                //    address = enquiryViewModel.Address,
                //    operationType = "Add",
                //    schoolCode = _sessionModel.SchoolCode,
                //    financialYear = _sessionModel.FinancialYear,
                //    createdByUserId = _sessionModel.UserId

                //};

                //SaveEnquiryDetails(enquiryModel);

            }
            else
            {
                // Form has invalid inputs.
            }

        }

        public List<string> EnquirytoolBarItems = (new List<string>() { "Add Enquiry", "Print", "ExportExcel", "Collapse All", "Expand All", "Search" });

        public void EditEnquiryDetail(CommandClickEventArgs<EnquiryListModel> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            if (buttontext == "Edit")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                IsVisible = true;
                DialogHeaderName = "Update Vendor Details";
                HeaderText = "Update Record";
                OperationType = "Update";
                btncss = "e-flat e-info e-outline";
                ddEnable = false;
                //_expenseDetailViewModel = new ExpenseDetailViewModel()
                //{
                //    AccountID = args.RowData.accountID,
                //    BaranchName = args.RowData.baranchName,
                //    ChequeNo = args.RowData.chequeNo,
                //    BankName = args.RowData.bankName,
                //    Description = args.RowData.description,
                //    ExpenseID = args.RowData.expenceID,
                //    InvoiceNo = args.RowData.invoiceNo,
                //    PaidAmount = Convert.ToInt32(args.RowData.paidAmount),
                //    PaymentMode = args.RowData.paymentMode,
                //    // TranDate = args.RowData.tranDate,

                //};


            }
            else
            {
                IsVisible = true;
                DialogHeaderName = "Delete Account Details";
                OperationType = "Delete";
                HeaderText = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                ddEnable = false;
                //_expenseDetailViewModel = new ExpenseDetailViewModel()
                //{
                //    AccountID = args.RowData.accountID,
                //    BaranchName = args.RowData.baranchName,
                //    ChequeNo = args.RowData.chequeNo,
                //    BankName = args.RowData.bankName,
                //    Description = args.RowData.description,
                //    ExpenseID = args.RowData.expenceID,
                //    InvoiceNo = args.RowData.invoiceNo,
                //    PaidAmount = Convert.ToInt32(args.RowData.paidAmount),
                //    PaymentMode = args.RowData.paymentMode,

                //};
            }
            //else
            //{
            //    navigationManager.NavigateTo($"/OnlineExam/OnlineTestDetails/{testId}");
            //}

        }
        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add Enquiry")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add Enquiry Details";
                OperationType = "Add";
                HeaderText = "Add Enquiry";
                ddEnable = true;

            }
            else if (args.Item.Text == "ExportExcel")
            {
                this.sfEnquiryDetails.ExportToExcelAsync();
            }
            else if (args.Item.Text == "Collapse All")
            {
                this.sfEnquiryDetails.CollapseAllGroupAsync();
            }
            else if (args.Item.Text == "Expand All")
            {
                this.sfEnquiryDetails.ExpandAllGroupAsync();
            }
        }


        public void ShowDialog()
        {
            IsVisible = true;
        }
        public void onOpen(Syncfusion.Blazor.Popups.BeforeOpenEventArgs args)
        {
            // setting maximum height to the Dialog
            args.MaxHeight = "750px";

        }
        [Inject]
        public ISnackbar snackBar { get; set; }
    }
}

