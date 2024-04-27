using AdminDashboard.Server.API_Service.Interface.TransactionSetUp;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.TransactionSetUp;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Grids.Internal;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AIS.Data.GeneralConversion.GeneralConversion;

namespace AdminDashboard.Server.User_Pages.Transaction
{
    public class PendingFeeBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        public ITransactionSetUpService transactionSetUp { get; set; }

        [Inject]
        public ITransactionSetUpService transactionSetUpService { get; set; }

        public PendingFeeFollowUpDetailsViewModel pendingFeeFollowUpDetailsViewModel=new PendingFeeFollowUpDetailsViewModel();
        public PendingFeeFollowUpDetailsAPIModel pendingFeeFollowUpDetailsAPIModel = new PendingFeeFollowUpDetailsAPIModel();
        public SfGrid<Pending_Fee_List_Output_Model> _pendingFee;
        public List<Pending_Fee_List_Output_Model> allStudentPendingFees = new List<Pending_Fee_List_Output_Model>();
        
        public List<PendingFeeFollowUpDetailsListModel>   pendingFeeFollowUpDetailsListModels = new List<PendingFeeFollowUpDetailsListModel>();


        public List<Transaction_List_Output_Model> _transaction_List = new List<Transaction_List_Output_Model>();
        public List<Transaction_Header_List_Output_Model> _transaction_Header_List=new List<Transaction_Header_List_Output_Model>();

        
        public List<string> toolBarPendingFee = (new List<string>() { "ExcelExport", "Collapse All", "Expand All", "Print", "Search", "ColumnChooser" });

        public SessionModel _sessionModel;
        public string[] GroupedColumns = new string[] { "fatherContactNumber" };
        public DialogEffect AnimationEffect = DialogEffect.Zoom;
        public string HeaderStyles { get; set; } = "e-background e-accent";
        public SfDialog DialogRef;
        public bool IsVisible { get; set; } = false;
        public bool IsEditVisible { get; set; } = false;
        public string DialogHeaderName = string.Empty;
        public bool ddEnable = true;
        public string btncss = "";
        public string HeaderText = string.Empty;
        public string OperationType = "";
        public SfGrid<Transaction_List_Output_Model> _sfgridTransaction;
        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");

            Pending_Fee_List_Input_Para_Model pending_Fee_List_Input_Para_Model = new Pending_Fee_List_Input_Para_Model
            {
                classId = 0,
                sectionId = 0,
                userId = 0,
                schoolCode = _sessionModel.SchoolCode,
                financialYear = _sessionModel.FinancialYear,
                reportType = ReportType.All
            };
            allStudentPendingFees = (await transactionSetUpService.GET_PendingFee_List(pending_Fee_List_Input_Para_Model)).ToList();

        }
        public void CustomizeCell(QueryCellInfoEventArgs<Pending_Fee_List_Output_Model> args)
        {
            if (args.Column.Field == "flowupStatus")
            {
                if (args.Data.flowupStatus=="No")
                {
                    args.Cell.AddClass(new string[] { "below-No" });
                }
                else
                {
                    args.Cell.AddClass(new string[] { "above-Yes" });
                }
            }
            if (args.Column.Field == "flowupCount")
            {
                if (args.Data.flowupCount==0)
                {
                    args.Cell.AddClass(new string[] { "below-0 " });
                }
                else if(args.Data.flowupCount == 1)
                {
                    args.Cell.AddClass(new string[] { "below-1" });
                }
                else if (args.Data.flowupCount == 2)
                {
                    args.Cell.AddClass(new string[] { "below-3" });
                }
            }
        }
        public void ExportToExcel()
        {
            this._pendingFee.ExcelExport();
        }
        public void onOpen(Syncfusion.Blazor.Popups.BeforeOpenEventArgs args)
        {
            // setting maximum height to the Dialog
            args.MaxHeight = "750px";

        }
        public async Task CloseDialog()
        {
            IsVisible = false;
            await this.DialogRef.HideAsync();
        }

        public string studentName = "";
        public string fatherName = "";
        public string pendingFeeMonth = "";
        public string fatherMobileNo = "";
        public int netDueAmount = 0;
        public string currentDate = "";
        public string currentTime = "";
        public string CallEnd = "";
        public string NextFollowUpTime = "";
        public DateTime _NextFollowUpDate = DateTime.Now;

        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                CallEnd = DateTime.Now.ToString("h:mm tt");
                NextFollowUpTime = DateTime.Now.ToString("h:mm tt");
                _NextFollowUpDate = pendingFeeFollowUpDetailsViewModel.NextFollowUpDate;

                pendingFeeFollowUpDetailsAPIModel = new PendingFeeFollowUpDetailsAPIModel()
                {
                     PendingFeeFollowUpID = pendingFeeFollowUpDetailsViewModel.PendingFeeFollowUpID,
                    StudentID = pendingFeeFollowUpDetailsViewModel.StudentID,
                    FollowUpDate = pendingFeeFollowUpDetailsViewModel.FollowUpDate,
                    FollowUptime = pendingFeeFollowUpDetailsViewModel.FollowUptime,
                    NextFollowUpDate = _NextFollowUpDate,
                    NextFollowUpTime = NextFollowUpTime,
                    ResponseType = "NA",
                    FollowUpRemarks = pendingFeeFollowUpDetailsViewModel.FollowUpRemarks,
                    CallStatus = pendingFeeFollowUpDetailsViewModel.CallStatus,
                    CallStart = pendingFeeFollowUpDetailsViewModel.CallStart,
                    CallEnd= CallEnd,
                    fatherMobileNo = pendingFeeFollowUpDetailsViewModel.fatherContactNo,
                    CreatedByUserId = _sessionModel.UserId,
                    UpdatedByUserId = _sessionModel.UserId,
                    SchoolCode = _sessionModel.SchoolCode,
                    FinancialYear = _sessionModel.FinancialYear,
                };

                if (OperationType == "Add")
                {
                    pendingFeeFollowUpDetailsAPIModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    pendingFeeFollowUpDetailsAPIModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    pendingFeeFollowUpDetailsAPIModel.OperationType = OperationAction.Delete;
                }
                SavePendingFeeFollow(pendingFeeFollowUpDetailsAPIModel);
            };
        }
        
        public APIReturnModel aPIReturnModel;
        [Inject]
        public ISnackbar snackBar { get; set; }

        private async void SavePendingFeeFollow(PendingFeeFollowUpDetailsAPIModel PendingFeeFollowUpDetailsAPIModel)
        {
            try
            {
                if (PendingFeeFollowUpDetailsAPIModel.OperationType != "NA")
                {
                    aPIReturnModel = (await transactionSetUp.CRUD_PendingFeeFollowUpDetails(PendingFeeFollowUpDetailsAPIModel));

                    if (aPIReturnModel.status == false)
                    {
                        if (PendingFeeFollowUpDetailsAPIModel.OperationType == "Add")
                        {
                            snackBar.Add(aPIReturnModel.message, Severity.Success);
                        }
                        else if (PendingFeeFollowUpDetailsAPIModel.OperationType == "Update")
                        {
                            snackBar.Add(aPIReturnModel.message, Severity.Info);
                        }
                        else
                        {
                            snackBar.Add(aPIReturnModel.message, Severity.Error);
                        }
                    }
                    else
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Error);
                    }
                    
                }
            }
            catch (Exception ex)
            {

            }
        }
        private async void GetStudentTransactionDetails(int studentID)
        {

            Transaction_List_Input_Para_Model transaction_List_Input_Para_Model = new Transaction_List_Input_Para_Model()
            {
                fromDate = DateTime.Today.ToString(),
                toDate = DateTime.Today.ToString(),
                userId = studentID,
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.GetByStudentUserID
            };

            _transaction_List = (await transactionSetUp.GET_Transaction_List(transaction_List_Input_Para_Model)).ToList();

            Transaction_Header_List_Input_Para_Model transactionHeaderList = new Transaction_Header_List_Input_Para_Model()
            {
                fromDate = DateTime.Today.ToString(),
                toDate = DateTime.Today.ToString(),
                userId = studentID,
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.GetByStudentUserID
            };

            _transaction_Header_List = (await transactionSetUp.GET_TransactionHeader_List(transactionHeaderList)).ToList();
        }

       
        public void EditFlowUp(CommandClickEventArgs<Pending_Fee_List_Output_Model> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            int studentID = args.RowData.accountId;
            //int testId = args.RowData.testID;
            if (buttontext == "FlowUp")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                //click to open edit dialog
               
                IsEditVisible = true;
                DialogHeaderName = "Add Student Flow up Details";
                HeaderText = "Add Record";
                OperationType  = "Add";
                btncss = "e-flat e-info e-outline";
                ddEnable = false;

                studentName= args.RowData.studentName; 
                fatherName= args.RowData.fatherName;
                pendingFeeMonth = args.RowData.monthName;
                fatherMobileNo = args.RowData.fatherContactNumber;
                netDueAmount=args.RowData.netDueAmount;
                currentDate = DateTime.Now.ToString("dd MMMM yyyy");
                pendingFeeFollowUpDetailsViewModel.FollowUpDate = currentDate;

                currentTime = DateTime.Now.ToString("h:mm tt");
                pendingFeeFollowUpDetailsViewModel.FollowUptime = currentTime;
                pendingFeeFollowUpDetailsViewModel.CallStart = currentTime;
                pendingFeeFollowUpDetailsViewModel.StudentID = studentID;
                pendingFeeFollowUpDetailsViewModel.fatherContactNo = fatherMobileNo;
                GetStudentTransactionDetails( studentID);
                //topicMasterViewModel = new TopicMasterViewModel()
                //{
                //    classId = Convert.ToString(args.RowData.classId),
                //    subjectId = Convert.ToString(args.RowData.subjectID),
                //    topicId = args.RowData.topicID,
                //    chapterId = args.RowData.chapterID.ToString(),
                //    unitId = args.RowData.unitId.ToString(),
                //    topicDescription = args.RowData.topicDescription,
                //    topicTitle = args.RowData.topicTitle

                //};
            }
            else
            {
                //IsEditVisible = true;
                //DialogHeaderName = "Delete Topic Details";
                //OperationType = "Delete";
                //HeaderText = "Delete Record";
                //btncss = "e-flat e-danger e-outline";
                //ddEnable = false;
                //topicMasterViewModel = new TopicMasterViewModel()
                //{
                //    classId = Convert.ToString(args.RowData.classId),
                //    subjectId = Convert.ToString(args.RowData.subjectID),
                //    topicId = args.RowData.topicID,
                //    chapterId = args.RowData.chapterID.ToString(),
                //    unitId = args.RowData.unitId.ToString(),
                //    topicDescription = args.RowData.topicDescription,
                //    topicTitle = args.RowData.topicTitle

                //};
            }

        }


        public class CallStatus
        {
            public int ID { get; set; }
            public string Text { get; set; }
        }
       

        public List<CallStatus> callStatusList =new List<CallStatus>() 
        {
            new CallStatus() {  ID=1, Text="On Call Discuss"},
            new CallStatus() {  ID=2, Text="Invalid Number"},
            new CallStatus() {  ID=3, Text="Switch Off"},
             new CallStatus() {  ID=4, Text="Call Busy"},
            new CallStatus() {  ID=5, Text="Call Rejected"},
            new CallStatus() {  ID=6, Text="Not Answer"},
             new CallStatus() {  ID=7, Text="Pending For Call"},
            new CallStatus() {  ID=8, Text="Other Issue"},
            
        };

        public DateTime? DateValue { get; set; } = DateTime.Now.Date;
         
        public void onChange(Syncfusion.Blazor.Calendars.ChangedEventArgs<DateTime?> args)
        {
            DateValue = args.Value;
            pendingFeeFollowUpDetailsViewModel.NextFollowUpDate = Convert.ToDateTime(DateValue);
            StateHasChanged();
        }
        public void ToolBarClick(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Collapse All")
            {
                this._pendingFee.CollapseAllGroupAsync();
            }
            else if (args.Item.Text == "Expand All")
            {
                this._pendingFee.ExpandAllGroupAsync();
            }
            else if (args.Item.Text == "Excel Export")
            {


            //    ExcelExportProperties ExcelProperties = new ExcelExportProperties();
            //    ExcelHeader header = new ExcelHeader();
            //    header.HeaderRows = 2;
            //    List<ExcelCell> cell = new List<ExcelCell>
            //{
            //    new ExcelCell() { RowSpan= 2,ColSpan=6 , Value= "Pending Fee List", Style = new ExcelStyle() {  Bold = true, FontSize = 13, Italic= false, HAlign=ExcelHorizontalAlign.Center  }  }
            //};

            //    List<ExcelRow> HeaderContent = new List<ExcelRow>
            //{
            //    new ExcelRow() {  Cells = cell, Index = 1 }
            //};
            //    header.Rows = HeaderContent;
            //    ExcelProperties.Header = header;
            //    ExcelProperties.FileName = "Class_With_TopicMasterList.xlsx";
                this._pendingFee.ExcelExport();
            }
        }

    }
}
