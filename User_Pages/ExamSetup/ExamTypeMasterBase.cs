using AdminDashboard.Server.API_Service.Interface.Exam;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.ExamMasterSetup;
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

namespace AdminDashboard.Server.User_Pages.ExamSetup
{
    public class ExamTypeMasterBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        public IExamMasterSetupService examMasterSetupService { get; set; }
        //list Model
        public List<Exam_Type_List_Output_Model> _examTypeList = new List<Exam_Type_List_Output_Model>();
        //data binding for blazor page
        public ExamTypeViewModel examViewModel = new  ExamTypeViewModel();
        
        public APIReturnModel aPIReturnModel;

        public Exam_Type_Master_Model examtypeAPIModel { get; set; }

        [Inject]
        public ISnackbar snackBar { get; set; }
        public SessionModel sessionModel { get; }

        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        public SfGrid<Exam_Type_List_Output_Model> sfExamListGrid;

        public List<string> toolBarItems = (new List<string>() { "Add ExamType", "Print", "Search" });
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

            Exam_Type_List_Input_Model exam_Type_List_Input_Model = new Exam_Type_List_Input_Model()
            {
                schoolCode = _sessionModel.SchoolCode,
                examTypeId = 0,
                financialYear = _sessionModel.FinancialYear,
                reportType = ReportType.All
            };
            _examTypeList = (await examMasterSetupService.GET_Exam_Type_MasterLIST(exam_Type_List_Input_Model)).ToList();

        }

        public void EditExamTypeMaster(CommandClickEventArgs<Exam_Type_List_Output_Model> args)
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
                examViewModel = new ExamTypeViewModel()
                {
                    displayOrder = args.RowData.displayOrder,
                    examType = args.RowData.examType,
                    examTypeId = args.RowData.examTypeId,
                    examTypeCode = args.RowData.examTypeCode,
                    examTypeDescription = args.RowData.examTypeDescription,
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
                examViewModel = new ExamTypeViewModel()
                {
                    displayOrder = args.RowData.displayOrder,
                    examType = args.RowData.examType,
                    examTypeId = args.RowData.examTypeId,
                    examTypeCode = args.RowData.examTypeCode,
                    examTypeDescription = args.RowData.examTypeDescription,
                };
            }

        }
        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add ExamType")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add ExamType Details";
                OperationType = "Add";
                HeaderText = "Add ExamType";
                ddEnable = true;
                examViewModel.examTypeId = 0;
                examViewModel.examType = null;
                examViewModel.examTypeCode = null;
                examViewModel.examTypeDescription = null;
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
                examtypeAPIModel = new Exam_Type_Master_Model()
                {
                     examTypeId = Convert.ToInt16(examViewModel.examTypeId),
                     examType = examViewModel.examType,
                     examTypeDescription = examViewModel.examTypeDescription,
                     examTypeCode = examViewModel.examTypeCode,
                      displayOrder = examViewModel.displayOrder,
                    CreatedByUserId = _sessionModel.UserId,
                    UpdatedByUserId = _sessionModel.UserId,
                    SchoolCode = _sessionModel.SchoolCode,
                    FinancialYear = _sessionModel.FinancialYear,
                     
                };

                if (OperationType == "Add")
                {
                    examtypeAPIModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    examtypeAPIModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    examtypeAPIModel.OperationType = OperationAction.Delete;
                }
                ExamTypeSave(examtypeAPIModel);
            };
        }

        private async void ExamTypeSave(Exam_Type_Master_Model examtypeAPIModel)
        {
            try
            {
                if (examtypeAPIModel.OperationType != "NA")
                {
                    aPIReturnModel = await examMasterSetupService.CRUD_ExamTypeMaster(examtypeAPIModel);

                    if (aPIReturnModel.status == false)
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Success);


                        Exam_Type_List_Input_Model exam_Type_List_Input_Model = new Exam_Type_List_Input_Model()
                        {
                            schoolCode = _sessionModel.SchoolCode,
                            examTypeId = 0,
                            financialYear = _sessionModel.FinancialYear,
                            reportType = ReportType.All
                        };
                        _examTypeList = (await examMasterSetupService.GET_Exam_Type_MasterLIST(exam_Type_List_Input_Model)).ToList();
                        StateHasChanged();
                        ClearData();
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

        }
        public async Task CloseDialog()
        {
            IsVisible = false;
            await this.DialogRef.HideAsync();
        }
    }
}