
using AIS.Data.APIReturnModel;
//using AdminDashboard.Server.API_Service.Service.Inventory;
using AIS.Data.RequestResponseModel.Inventory.ItemMaster;
//using AIS.Data.RequestResponseModel.Inventory.ItemVender;
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
//using AdminDashboard.Server.API_Service.Interface.Inventory;
using System.Linq;
using AIS.Data.RequestResponseModel.MasterData.MasterConfiguration.Class;
using AIS.Data.RequestResponseModel.MasterData.MasterConfiguration.Section;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using AIS.Data.RequestResponseModel.MasterData.ExamConfiguration.Subject;
//using AdminDashboard.Server.API_Service.Interface.MasterData;

namespace AdminDashboard.Server.User_Pages.MasterData.ExamConfiguration
{
    public class SubjectBase : ComponentBase
    {
      


            public SubjectViewModel subjectViewModel = new SubjectViewModel();


            public List<SubjectOutputModel> _SubejctListModel = new List<SubjectOutputModel>();
            public SfGrid<SubjectOutputModel> sfSubjectDetails;
            public SubjectApiModel subjectApiModel { get; set; }



            [Inject]
            Blazored.SessionStorage.ISessionStorageService session { get; set; }
            public SessionModel _sessionModel;

            [Inject]
            public ISnackbar snackBar { get; set; }

            // [Inject]
            //   public IMasterDataService masterDataService { get; set; }

            public DialogEffect AnimationEffect = DialogEffect.Zoom;
            public string HeaderStyles { get; set; } = "e-background e-accent";
            public SfDialog DialogRef;

            List<string> IList = new List<string>();


            public bool IsVisible { get; set; } = false;
            public bool IsDeleteVisible { get; set; } = false;
            public string DialogHeaderName = string.Empty;
            public bool ddEnable = true;
            public string btncss = "";
            public string HeaderText = string.Empty;
            public string OperationType = "";

            public APIReturnModel aPIReturnModel;


            public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };

            public List<string> ToolBarItems = (new List<string>() { "Add Subject", "Print", "ExportExcel", "Collapse All", "Expand All", "Search" });





        public class MarksType
        {
            public int Id;
            public string Value;
        }

        public List<MarksType> MarksTypeDetails = new List<MarksType>()
        {
            new MarksType{Id=1,Value="Numeric"},
            new MarksType{Id=2,Value="Grad"},
        };



        public class SubjectType
        {
            public int Id;
            public string Value;
        }

        public List<SubjectType> SubjectTypeDetails = new List<SubjectType>()
        {
            new SubjectType{Id=1,Value="Co-Scholist"},
            new SubjectType{Id=2,Value="Scholist"},
        };




        protected override async Task OnInitializedAsync()
            {
                _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            //Master_CLass_List_Input_Para_Model master_CLass_List_Input_Para_Model = new Master_CLass_List_Input_Para_Model()
            //{
            //    classId = 0,
            //    userId = _sessionModel.UserId,
            //    financialYear = _sessionModel.FinancialYear,
            //    schoolCode = _sessionModel.SchoolCode,
            //    reportType = ReportType.All
            //};
            //_SubejctListModel = (await masterDataSetupService.GET_Master_ClassLIST(master_CLass_List_Input_Para_Model)).ToList();

            SubjectParaModel subjectParaModel = new SubjectParaModel()
                {
                    financialYear = _sessionModel.FinancialYear,
                    schoolCode = _sessionModel.SchoolCode,
                    SubjectId = 0,
                    userRoleId = _sessionModel.RoleId,
                    reportType = ReportType.All
                };

                //    _ClassListModel = (await masterDataService.GET_ClassDetails_List(classParaModel)).ToList();
            }







            public void EditItemDetail(CommandClickEventArgs<SubjectOutputModel> args)
            {


                // Perform required operations here
                string buttontext = args.CommandColumn.ButtonOption.Content;
                //int testId = args.RowData.testID;
                if (buttontext == "Edit")
                {
                    //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                    IsVisible = true;
                    DialogHeaderName = "Update Subejct Details";
                    HeaderText = "Update Record";
                    OperationType = "Update";
                    btncss = "e-flat e-info e-outline";
                // ddEnable = false;
                subjectViewModel = new SubjectViewModel()
                    {
               
                        subjectCode = args.RowData.subjectCode,
                        subjectName = args.RowData.subjectName,
                        subjectType = args.RowData.subjectType,
                    masrksType = args.RowData.masrksType,


                };
                }
                else
                {
                    IsVisible = true;
                    DialogHeaderName = "Delete Subejct Details";
                    OperationType = "Delete";
                    HeaderText = "Delete Record";
                    btncss = "e-flat e-danger e-outline";
                    ddEnable = false;
                    subjectViewModel = new SubjectViewModel()
                    {

                        subjectCode = args.RowData.subjectCode,
                        subjectName = args.RowData.subjectName,
                        subjectType = args.RowData.subjectType,
                        masrksType = args.RowData.masrksType,


                    };
                }
                //else
                //{
                //    navigationManager.NavigateTo($"/OnlineExam/OnlineTestDetails/{testId}");
                //}

            }


            public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
            {
                if (args.Item.Text == "Add Subject")
                {
                    //navigationManager.NavigateTo("/OnlineExam/TestList");
                    //perform your actions here
                    IsVisible = true;
                    OperationType = "";
                    btncss = "e-flat e-primary e-outline";
                    DialogHeaderName = "Add Subject Details";
                    OperationType = "Add";
                    HeaderText = "Add Subject";
                    ddEnable = true;

                }
                else if (args.Item.Text == "ExportExcel")
                {
                    this.sfSubjectDetails.ExportToExcelAsync();
                }
                else if (args.Item.Text == "Collapse All")
                {
                    this.sfSubjectDetails.CollapseAllGroupAsync();
                }
                else if (args.Item.Text == "Expand All")
                {
                    this.sfSubjectDetails.ExpandAllGroupAsync();
                }
            }




            public async void OnValidSubmit(EditContext contex)
            {
                bool isValid = contex.Validate();
                if (isValid)
                {
                subjectApiModel = new SubjectApiModel()
                    {
                        SubjectName = subjectViewModel.subjectName,
                        SubjectCode = subjectViewModel.subjectCode,
                        MasrksType = subjectViewModel.masrksType,
                        SubjectType=subjectViewModel.subjectType,





                    };
                    if (OperationType == "Add")
                    {
                    subjectApiModel.OperationType = OperationAction.Add;
                    }
                    else if (OperationType == "Update")
                    {
                    subjectApiModel.OperationType = OperationAction.Update;
                    }
                    //Delete Operation
                    else
                    {
                    subjectApiModel.OperationType = OperationAction.Delete;
                    }

                    SaveItemMasterDetails(subjectApiModel);

                }
                else
                {
                    // Form has invalid inputs.
                }

            }

            private async void SaveItemMasterDetails(SubjectApiModel subjectApiModel)
            {
                try
                {
                    if (subjectApiModel.OperationType != "NA")
                    {
                        //  aPIReturnModel = await masterDataService.CRUD_ClassDetails(classApiModel);

                        if (aPIReturnModel.status == false)
                        {
                            snackBar.Add(aPIReturnModel.message, Severity.Success);


                        SubjectParaModel subjectParaModel = new SubjectParaModel()
                            {
                                financialYear = _sessionModel.FinancialYear,
                                schoolCode = _sessionModel.SchoolCode,
                                SubjectId = 0,
                                userRoleId = _sessionModel.RoleId,
                                reportType = ReportType.All
                            };

                            //   _ClassListModel  = (await masterDataService.GET_ClassDetails_List(classParaModel)).ToList();

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
            subjectViewModel = new SubjectViewModel();

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
        }
    }