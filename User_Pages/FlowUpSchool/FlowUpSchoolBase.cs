using AdminDashboard.Server.API_Service.Interface.FlowUpSchool;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.FlowUpSchool;
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

namespace AdminDashboard.Server.User_Pages.FlowUpSchool
{
    public class FlowUpSchoolBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }

        [Inject]
        public IFlowUpSchoolService flowUpSchoolService { get; set;}
        public APIReturnModel aPIReturnModel;
        public List<FlowUpSchoolOutPutModel> _flowupSchoolList = new List<FlowUpSchoolOutPutModel>();

        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        public SfGrid<FlowUpSchoolOutPutModel> sfFlowupSchoolList;

        public FlowUpSchoolViewModel flowUpSchoolViewModel=new FlowUpSchoolViewModel();

        public FollowUpSchoolViewModel followUpSchoolViewModel =new FollowUpSchoolViewModel();

        public FlowUpSchoolInputModel flowUpSchoolInputModel { get; set; } 

        [Inject]
        public ISnackbar snackBar { get; set; }
        public List<string> toolBarItems = (new List<string>() { "Add School", "Print", "Search" });
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

            FlowUpSchool_Input_Para_Model flowUpSchool_Input_Para_Model = new FlowUpSchool_Input_Para_Model()
            {
                schoolCode = _sessionModel.SchoolCode,
                 UserID = _sessionModel.UserId,
                financialYear = _sessionModel.FinancialYear,
                reportType = ReportType.All
            };
            _flowupSchoolList = (await flowUpSchoolService.GET_FlowUp_SchoolList(flowUpSchool_Input_Para_Model)).ToList();

        }
        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add School")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add New School Details";
                OperationType = "Add";
                HeaderText = "Add School";
                ddEnable = true;

            }
        }
       
        public bool IsVisibleFollowUpScreen=false;
        public void EditSchoolFlowUpDetails(CommandClickEventArgs<FlowUpSchoolOutPutModel> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;

            if (buttontext == "Follow Up")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                IsVisibleFollowUpScreen = true;
                DialogHeaderName = "Follow Up Details";
                HeaderText = "Follow Up Record";
                OperationType = "Update";
                btncss = "e-flat e-info e-outline";
                ddEnable = true;

                followUpSchoolViewModel = new FollowUpSchoolViewModel()
                {
                     SchoolID= args.RowData.schoolID,
                     School_Name = args.RowData.schoolName,
                     FollowupDate = DateTime.Now,
                     //FollowupStatus = args.RowData.schoolName,
                     Remarks = "",
                };
            }
               

         }

        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                flowUpSchoolInputModel = new FlowUpSchoolInputModel()
                {
                    SchoolID = Convert.ToInt16(flowUpSchoolViewModel.SchoolID),
                    School_Name = flowUpSchoolViewModel.School_Name,
                    Address = flowUpSchoolViewModel.Address,
                    ByRefrenceMobileNo = flowUpSchoolViewModel.ByRefrenceMobileNo,
                    ByRefrenceName = flowUpSchoolViewModel.ByRefrenceName,
                    CityName = flowUpSchoolViewModel.CityName,
                    Directore_MobileNo = flowUpSchoolViewModel.Directore_MobileNo,
                    Director_Name = flowUpSchoolViewModel.Director_Name,
                    EmailID = flowUpSchoolViewModel.EmailID,
                    Principal_MobileNo = flowUpSchoolViewModel.Principal_MobileNo,
                    Principal_Name = flowUpSchoolViewModel.Principal_Name,
                    StateName = flowUpSchoolViewModel.StateName,
                    visitingRemarks = flowUpSchoolViewModel.visitingRemarks,
                    Visiting_Status = flowUpSchoolViewModel.Visiting_Status,
                    CreatedByUserId = _sessionModel.UserId,
                    UpdatedByUserId = _sessionModel.UserId,
                    SchoolCode = _sessionModel.SchoolCode,
                    FinancialYear = _sessionModel.FinancialYear,

                };

                if (OperationType == "Add")
                {
                    flowUpSchoolInputModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    flowUpSchoolInputModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    flowUpSchoolInputModel.OperationType = OperationAction.Delete;
                }
                   FlowUpSchoolDetails(flowUpSchoolInputModel);
            }
        }

        public async void OnValidFollowSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                //flowUpSchoolInputModel = new FlowUpSchoolInputModel()
                //{
                //    SchoolID = Convert.ToInt16(flowUpSchoolViewModel.SchoolID),
                //    School_Name = flowUpSchoolViewModel.School_Name,
                //    Address = flowUpSchoolViewModel.Address,
                //    ByRefrenceMobileNo = flowUpSchoolViewModel.ByRefrenceMobileNo,
                //    ByRefrenceName = flowUpSchoolViewModel.ByRefrenceName,
                //    CityName = flowUpSchoolViewModel.CityName,
                //    Directore_MobileNo = flowUpSchoolViewModel.Directore_MobileNo,
                //    Director_Name = flowUpSchoolViewModel.Director_Name,
                //    EmailID = flowUpSchoolViewModel.EmailID,
                //    Principal_MobileNo = flowUpSchoolViewModel.Principal_MobileNo,
                //    Principal_Name = flowUpSchoolViewModel.Principal_Name,
                //    StateName = flowUpSchoolViewModel.StateName,
                //    visitingRemarks = flowUpSchoolViewModel.visitingRemarks,
                //    Visiting_Status = flowUpSchoolViewModel.Visiting_Status,
                //    CreatedByUserId = _sessionModel.UserId,
                //    UpdatedByUserId = _sessionModel.UserId,
                //    SchoolCode = _sessionModel.SchoolCode,
                //    FinancialYear = _sessionModel.FinancialYear,

                //};

                //if (OperationType == "Add")
                //{
                //    flowUpSchoolInputModel.OperationType = OperationAction.Add;
                //}
                //else if (OperationType == "Update")
                //{
                //    flowUpSchoolInputModel.OperationType = OperationAction.Update;
                //}
                ////Delete Operation
                //else
                //{
                //    flowUpSchoolInputModel.OperationType = OperationAction.Delete;
                //}
                //FlowUpSchoolDetails(flowUpSchoolInputModel);
            }
        }
        private async void FlowUpSchoolDetails(FlowUpSchoolInputModel flowUpSchoolInputModel)
        {
            try
            {
                if (flowUpSchoolInputModel.OperationType != "NA")
                {
                    aPIReturnModel = (await flowUpSchoolService.CRUD_FlowUpSchoolDetails(flowUpSchoolInputModel));

                    if (aPIReturnModel.status == false)
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Success);



                        FlowUpSchool_Input_Para_Model flowUpSchool_Input_Para_Model = new FlowUpSchool_Input_Para_Model()
                        {
                            schoolCode = _sessionModel.SchoolCode,
                            UserID = _sessionModel.UserId,
                            financialYear = _sessionModel.FinancialYear,
                            reportType = ReportType.All
                        };
                        _flowupSchoolList = (await flowUpSchoolService.GET_FlowUp_SchoolList(flowUpSchool_Input_Para_Model)).ToList();
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
            //chapterMasterViewModel = new ChapterMasterViewModel();
             flowUpSchoolViewModel=new FlowUpSchoolViewModel();
        }
        public async Task CloseDialog()
        {
            IsVisible = false;
            IsVisibleFollowUpScreen = false;
            await this.DialogRef.HideAsync();
        }
        public void onOpen(Syncfusion.Blazor.Popups.BeforeOpenEventArgs args)
        {
            // setting maximum height to the Dialog
            args.MaxHeight = "750px";

        }

        public async Task SaveFollowUpDetails()
        {
             
        }
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

        public class StateName
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }

        public List<StateName> stateName = new List<StateName>
        {
            new StateName(){Id=1,Value="Bihar"},
            new StateName(){Id=2,Value="Haryana"},
            new StateName(){Id=3,Value="Uttar Pradesh"},
        };
        public class CallResponseType
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }

        public List<CallResponseType> callResponseType = new List<CallResponseType>
        {
            new CallResponseType(){Id=1,Value="Intrested"},
            new CallResponseType(){Id=2,Value="Not Intrested"},
            new CallResponseType(){Id=3,Value="Number doesn't exist"},
            new CallResponseType(){Id=4,Value="Switched off"},
            new CallResponseType(){Id=5,Value="Number out of service"},
            new CallResponseType(){Id=6,Value="Invalid number"},
            new CallResponseType(){Id=7,Value="Already have app"},
            new CallResponseType(){Id=8,Value="Wrong number"},
             new CallResponseType(){Id=8,Value="He disconnected the phone call"},
        };
    }
}
