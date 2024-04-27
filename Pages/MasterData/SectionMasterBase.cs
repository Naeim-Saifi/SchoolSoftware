using AdminDashboard.Server.Models.MasterData;
using AdminDashboard.Server.Service.Interface.MasterData;
using AIS.Data.BaseClass;
using AIS.Data.RequestResponseModel.MasterData;
using AIS.Model;
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

namespace AdminDashboard.Server.Pages.MasterData
{
    public class SectionMasterBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }

        //this model is use for get data userdetails and finaciyal year.
        public SessionModel sessionModel { get; }

        //this is use for API Function
        [Inject]
        public IMasterDataService masterDataService { get; set; }

        //this model use for send data to API ,binding view model with API model
        public MasterSectionAPIInputModel masterSectionAPIInputModel { get; set; }
        //data binding for blazor page
        public SectionMasterViewModel SectionMasterViewModel = new SectionMasterViewModel();

        //list Model
        public List<MasterSectionListModel> masterSectionListModels = new List<MasterSectionListModel>();

        public SessionModel _sessionModel;
        public string OperationType = "";

        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                masterSectionAPIInputModel = new MasterSectionAPIInputModel()
                {
                    sectionName = SectionMasterViewModel.sectionName,
                    sectionCode = SectionMasterViewModel.sectionCode,
                    displayOrder = Convert.ToInt16(SectionMasterViewModel.displayOrder),
                    masterSectionId = SectionMasterViewModel.masterSectionId,
                    CreatedByUserId = _sessionModel.UserId,
                    SchoolCode = _sessionModel.SchoolCode,
                    FinancialYear = _sessionModel.FinancialYear,
                };

                if (OperationType == "Add")
                {
                    masterSectionAPIInputModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    masterSectionAPIInputModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    masterSectionAPIInputModel.OperationType = OperationAction.Delete;
                }
                SaveSectionDetails(masterSectionAPIInputModel);
            };
        }
        public APIReturnModel aPIReturnmodel;
        [Inject]
        public ISnackbar snackBar { get; set; }

        public async void SaveSectionDetails(MasterSectionAPIInputModel masterSectionModel)
        {
            aPIReturnmodel = (await masterDataService.AddUpdateDeleteSectionMaster(masterSectionModel));
            if (aPIReturnmodel.status == false)
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Info);
                HeaderTest = "Save Record";
                GetSectionList();
                ClearAllFields();
            }
            else
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Error);
            }
        }
        //Get Section List
        private async Task GetSectionList()
        {
            DefaultInputParameterModel defaultInputParameterModel = new DefaultInputParameterModel()
            {
                SchoolCode = _sessionModel.SchoolCode,
                FinancialYear = _sessionModel.FinancialYear,
                OperationType = ReportType.All
            };
            //Get all list from API.
            masterSectionListModels = (await masterDataService.GetSectionList(defaultInputParameterModel)).ToList();
        }
        private void ClearAllFields()
        {
            SectionMasterViewModel.sectionName = null;
            SectionMasterViewModel.sectionCode = null;
            SectionMasterViewModel.displayOrder = null;
        }
        protected override async Task OnInitializedAsync()
        {
            // System.Diagnostics.Debugger.Break();
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            //Get Section List Details
            DefaultInputParameterModel defaultInputParameterModel = new DefaultInputParameterModel()
            {
                SchoolCode = _sessionModel.SchoolCode,
                FinancialYear = _sessionModel.FinancialYear,
                OperationType = ReportType.All
            };
            //Get all list from API.
            GetSectionList();

            //masterSectionListModels = (await masterDataService.GetMasterSectionList(defaultInputParameterModel)).ToList();
        }
        public string HeaderTest = string.Empty;
        //for API Model to View model data binding.
        public void RowSelectHandler(RowSelectEventArgs<MasterSectionListModel> args)
        {
            SectionMasterViewModel = new SectionMasterViewModel()
            {
                sectionName = args.Data.sectionsName,
                sectionCode = args.Data.sectionCode,
                displayOrder = Convert.ToInt16( args.Data.displayOrder),
                masterSectionId = args.Data.masterSectionId
            };
            HeaderTest = "Update Record";

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
        public void onOpen(Syncfusion.Blazor.Popups.BeforeOpenEventArgs args)
        {
            // setting maximum height to the Dialog
            args.MaxHeight = "750px";

        }
        public List<string> ToolBarItems = (new List<string>() { "Add", "Edit", "Print", "Search" });
        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        public List<string> classMasterItem = (new List<string>() { "Add Section", "Print", "Search" });

        public SfGrid<MasterSectionListModel> sfSectionMaster;
        public string btncss = "";
        public void EditSectionMaster(CommandClickEventArgs<MasterSectionListModel> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            if (buttontext == "Edit")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                IsVisible = true;
                DialogHeaderName = "Update Section Details";
                HeaderTest = "Update Record";
                OperationType = "Update";
                btncss = "e-flat e-info e-outline";
                SectionMasterViewModel = new SectionMasterViewModel()
                {
                    sectionName = args.RowData.sectionsName,
                    sectionCode = args.RowData.sectionCode,
                    displayOrder = Convert.ToInt16( args.RowData.displayOrder),
                    masterSectionId = args.RowData.masterSectionId
                };
            }
            else
            {
                IsVisible = true;
                DialogHeaderName = "Delete Section Details";
                OperationType = "Delete";
                HeaderTest = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                SectionMasterViewModel = new SectionMasterViewModel()
                {
                    sectionName = args.RowData.sectionsName,
                    sectionCode = args.RowData.sectionCode,
                    displayOrder = Convert.ToInt16( args.RowData.displayOrder),
                    masterSectionId = args.RowData.masterSectionId
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
                DialogHeaderName = "Add New Section Details";
                OperationType = "Add";
                HeaderTest = "Add Section";
                SectionMasterViewModel.sectionName = null;
                SectionMasterViewModel.sectionCode = null;
                SectionMasterViewModel.displayOrder = null;
            }
        }
    }
}
