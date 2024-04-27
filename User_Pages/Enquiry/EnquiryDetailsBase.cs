
using AdminDashboard.Server.API_Service.Interface.Enquiry;
using AdminDashboard.Server.API_Service.Interface.MasterDataSetUp;
using AdminDashboard.Server.Pages.Enquiry;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.Enquiry;
using AIS.Data.RequestResponseModel.ExamMasterSetup;
using AIS.Data.RequestResponseModel.MasterDataSetUp;
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

namespace AdminDashboard.Server.User_Pages.Enquiry
{
    public class EnquiryDetailsBase : ComponentBase
    {

        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;
        [Inject]
        public IMasterDataSetupService masterDataSetupService { get; set; }
        [Inject]
        public IEnquiryService enquiryService { get; set; } 

        public EnquiryViewModel enquiryViewModel = new EnquiryViewModel();




        public List<Master_CLass_List_Output_Model> _classList = new List<Master_CLass_List_Output_Model>(); 

        public List<EnquiryListModel> _EnquiryDetailsList = new List<EnquiryListModel>();

        public EnquiryAPIModel enquiryAPIModel { get; set; }

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

            EnquiryParaModel enquiryParaModel = new EnquiryParaModel()
            {
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                EnquiryId = 0,
                userRoleId = _sessionModel.RoleId,
                reportType = ReportType.All
            };

            _EnquiryDetailsList = (await enquiryService.GET_EnquiryDetails_List(enquiryParaModel)).ToList();
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
                enquiryAPIModel = new EnquiryAPIModel()
                {
                     EnquiryID = Convert.ToInt16(enquiryViewModel.enquiryID),
                    StudentName = enquiryViewModel.studentName,
                    FatherName = enquiryViewModel.fatherName,
                    FMobileNo = enquiryViewModel.fMobileNo,
                    MotherName = enquiryViewModel.motherName,
                    MMobileNo = enquiryViewModel.mMobileNo,
                    GenderID = Convert.ToInt32(enquiryViewModel.Gender),
                    PreviousSchoolName = enquiryViewModel.PreviousSchoolDetails,
                    Dob = Convert.ToDateTime(enquiryViewModel.DOB),
                    VisitingRemrks = enquiryViewModel.VisitingRemarks,
                    VisitingType = Convert.ToInt32(enquiryViewModel.VisitingType),
                    VisitingStatus = Convert.ToInt32(enquiryViewModel.VisitingStatus),
                    EmailID = enquiryViewModel.EmailId,
                    //CurrentClassID = Convert.ToInt32(enquiryViewModel.CurrentClass),
                    PromotedClassID = Convert.ToInt32(enquiryViewModel.NextClass),
                    Address = enquiryViewModel.Address,
                    //operationType = "Add",
                    SchoolCode = _sessionModel.SchoolCode,
                    FinancialYear = _sessionModel.FinancialYear,
                    CreatedByUserId = _sessionModel.UserId

                };
                if (OperationType == "Add")
                {
                    enquiryAPIModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    enquiryAPIModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    enquiryAPIModel.OperationType = OperationAction.Delete;
                }
                
                 SaveEnquiryDetails(enquiryAPIModel);

            }
            else
            {
                // Form has invalid inputs.
            }

        }
        private async void SaveEnquiryDetails(EnquiryAPIModel enquiryAPIModel)
        {
            try
            {
                if (enquiryAPIModel.OperationType != "NA")
                {
                    aPIReturnModel = await enquiryService.CRUD_EnquiryDetails(enquiryAPIModel);

                    if (aPIReturnModel.status == false)
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Success);


                        EnquiryParaModel enquiryParaModel = new EnquiryParaModel()
                        {
                            financialYear = _sessionModel.FinancialYear,
                            schoolCode = _sessionModel.SchoolCode,
                            EnquiryId = 0,
                            userRoleId = _sessionModel.RoleId,
                            reportType = ReportType.All
                        };

                        _EnquiryDetailsList = (await enquiryService.GET_EnquiryDetails_List(enquiryParaModel)).ToList();

                        ClearData();
                        StateHasChanged();
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

        private void ClearData()
        {
             enquiryViewModel = new EnquiryViewModel();

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
                DialogHeaderName = "Update Enquiry Details";
                HeaderText = "Update Record";
                OperationType = "Update";
                btncss = "e-flat e-info e-outline";
               // ddEnable = false;
                enquiryViewModel = new EnquiryViewModel()
                {                    
                    enquiryID = args.RowData.enquiryID,
                    studentName = args.RowData.studentName,
                    fatherName = args.RowData.fatherName,
                    fMobileNo = args.RowData.fMobileNo,
                    motherName = args.RowData.motherName,
                    mMobileNo = args.RowData.mMobileNo,
                    Gender = args.RowData.genderID.ToString(),
                    PreviousSchoolDetails = args.RowData.previousSchoolName,
                    DOB = args.RowData.dob,
                    VisitingRemarks = args.RowData.visitingRemrks,
                    VisitingType = args.RowData.visitingType.ToString(),
                    VisitingStatus = args.RowData.visitingStatus.ToString(),
                    EmailId = args.RowData.emailID,                 
                    NextClass = args.RowData.previousClassid.ToString(),
                    Address = args.RowData.address,
                    
                };
            }
            else
            {
                IsVisible = true;
                DialogHeaderName = "Delete Enquiry Details";
                OperationType = "Delete";
                HeaderText = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                ddEnable = false;
                enquiryViewModel = new EnquiryViewModel()
                {

                    enquiryID = args.RowData.enquiryID,
                    studentName = args.RowData.studentName,
                    fatherName = args.RowData.fatherName,
                    fMobileNo = args.RowData.fMobileNo,
                    motherName = args.RowData.motherName,
                    mMobileNo = args.RowData.mMobileNo,
                    Gender = args.RowData.genderID.ToString(),
                    PreviousSchoolDetails = args.RowData.previousSchoolName,
                    DOB = args.RowData.dob,
                    VisitingRemarks = args.RowData.visitingRemrks,
                    VisitingType = args.RowData.visitingType.ToString(),
                    VisitingStatus = args.RowData.visitingStatus.ToString(),
                    EmailId = args.RowData.emailID,                   
                    NextClass = args.RowData.previousClassid.ToString(),
                    Address = args.RowData.address,

                };
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
        public async Task CloseDialog()
        {
            IsVisible = false;
            await this.DialogRef.HideAsync();
        }
        [Inject]
        public ISnackbar snackBar { get; set; }
    }
}
