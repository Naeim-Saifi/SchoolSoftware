//using AdminDashboard.Server.API_Service.Service.Inventory;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.Enquiry;
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
using AIS.Data.RequestResponseModel.MasterDataSetUp;
using AdminDashboard.Server.API_Service.Interface.MasterDataSetUp;
//using AdminDashboard.Server.API_Service.Interface.MasterData;


namespace AdminDashboard.Server.User_Pages.MasterData.MasterConfiguration
{
    public class SectionBase : ComponentBase
    {
   



            public SectionViewModel sectionViewModel = new SectionViewModel();
            public SfGrid<Master_Section_List_Output_Model> sfSectionDetails;

        public List<Master_Section_List_Output_Model> _section_List = new List<Master_Section_List_Output_Model>();
        public SectionApiModel sectionApiModel { get; set; }



            [Inject]
            Blazored.SessionStorage.ISessionStorageService session { get; set; }
            public SessionModel _sessionModel;

            [Inject]
            public ISnackbar snackBar { get; set; }

            [Inject]
            public IMasterDataSetupService masterDataSetupService { get; set; }

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

            public List<string> ToolBarItems = (new List<string>() { "Add Section", "Print", "ExportExcel", "Collapse All", "Expand All", "Search" });
         
            protected override async Task OnInitializedAsync()
            {
                _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            Master_Section_List_Input_Para_Model master_section_List_Input_Para_Model = new Master_Section_List_Input_Para_Model()
            {
                sectionId = 0,
                userId = _sessionModel.UserId,
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.All
            };
                _section_List = (await masterDataSetupService.GET_Master_SectionLIST(master_section_List_Input_Para_Model)).ToList();

            }
            public void EditItemDetail(CommandClickEventArgs<Master_Section_List_Output_Model> args)
            {


            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            if (buttontext == "Edit")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                IsVisible = true;
                DialogHeaderName = "Update Section Details";
                HeaderText = "Update Record";
                OperationType = "Update";
                btncss = "e-flat e-info e-outline";
                // ddEnable = false;
                sectionViewModel = new SectionViewModel()
                {
                    sectionId = args.RowData.sectionId,
                    sectionCode = args.RowData.sectionCode,
                    sectionName = args.RowData.sectionName,
                    displayOrder = args.RowData.displayOrder,


                };
            }
            else
            {
                IsVisible = true;
                DialogHeaderName = "Delete Section Details";
                OperationType = "Delete";
                HeaderText = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                ddEnable = false;
                sectionViewModel = new SectionViewModel()
                {

                    sectionId = args.RowData.sectionId,
                    sectionCode = args.RowData.sectionCode,
                    sectionName = args.RowData.sectionName,
                    displayOrder = args.RowData.displayOrder,

                };
            }
            //else
            //{
            //    navigationManager.NavigateTo($"/OnlineExam/OnlineTestDetails/{testId}");
            //}

        }


        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
            {
                if (args.Item.Text == "Add Section")
                {
                    //navigationManager.NavigateTo("/OnlineExam/TestList");
                    //perform your actions here
                    IsVisible = true;
                    OperationType = "";
                    btncss = "e-flat e-primary e-outline";
                    DialogHeaderName = "Add Section Details";
                    OperationType = "Add";
                    HeaderText = "Add Section";
                    ddEnable = true;

                }


            else if (args.Item.Text == "ExportExcel")
            {
                this.sfSectionDetails.ExportToExcelAsync();
            }
            else if (args.Item.Text == "Collapse All")
            {
                this.sfSectionDetails.CollapseAllGroupAsync();
            }
            else if (args.Item.Text == "Expand All")
            {
                this.sfSectionDetails.ExpandAllGroupAsync();
            }
        }

            public async void OnValidSubmit(EditContext contex)
            {
                bool isValid = contex.Validate();
                if (isValid)
                {
                sectionApiModel = new SectionApiModel()
                    {
                        SectionName = sectionViewModel.sectionName,
                        SectionCode = sectionViewModel.sectionCode,
                        DisplayOrder = sectionViewModel.displayOrder,




                    };
                    if (OperationType == "Add")
                    {
                        sectionApiModel.OperationType = OperationAction.Add;
                    }
                    else if (OperationType == "Update")
                    {
                    sectionApiModel.OperationType = OperationAction.Update;
                    }
                    //Delete Operation
                    else
                    {
                    sectionApiModel.OperationType = OperationAction.Delete;
                    }

                    SaveItemMasterDetails(sectionApiModel);

                }
                else
                {
                    // Form has invalid inputs.
                }

            }

            private async void SaveItemMasterDetails(SectionApiModel sectionApiModel)
            {
            try
            {
                if (sectionApiModel.OperationType != "NA")
                {
                   //    aPIReturnModel = await masterDataService.CRUD_SectionDetails(sectionApiModel);

                    if (aPIReturnModel.status == false)
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Success);


                        SectionParaModel sectionParaModel = new SectionParaModel()
                        {
                            financialYear = _sessionModel.FinancialYear,
                            schoolCode = _sessionModel.SchoolCode,
                            SectionId = 0,
                            userRoleId = _sessionModel.RoleId,
                            reportType = ReportType.All
                        };

                     //    _SectionListModel  = (await masterDataService.GET_SectionDetails_List(sectionParaModel)).ToList();

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
            sectionViewModel = new SectionViewModel();

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