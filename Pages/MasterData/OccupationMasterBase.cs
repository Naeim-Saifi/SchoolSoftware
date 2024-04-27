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
    public class OccupationMasterBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }

        //this model is use for get data userdetails and finaciyal year.
        public SessionModel sessionModel { get; }

        //this is use for API Function
        [Inject]
        public IMasterDataService masterDataService { get; set; }

        //this model use for send data to API ,binding view model with API model
        public MasterOccuptionAPIInputModel masterOccupationAPIInputModel { get; set; }
        //data binding for blazor page
        public OccupationMasterViewModel masterOccupationViewModel = new OccupationMasterViewModel();

        //list Model
        public List<MasterOccuptionListModel> masterOccupationListModels = new List<MasterOccuptionListModel>();

        public SessionModel _sessionModel;
        public string OperationType = "";

        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                masterOccupationAPIInputModel = new MasterOccuptionAPIInputModel()
                {
                    occuptionName = masterOccupationViewModel.occuptionName,
                    occuptionCode = masterOccupationViewModel.occuptionCode,
                    displayOrder = Convert.ToInt16(masterOccupationViewModel.displayOrder),
                    masterOccuptionId = masterOccupationViewModel.masterOccuptionId,
                    CreatedByUserId = _sessionModel.UserId,
                    SchoolCode = _sessionModel.SchoolCode,
                    FinancialYear = _sessionModel.FinancialYear,
                };

                if (OperationType == "Add")
                {
                    masterOccupationAPIInputModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    masterOccupationAPIInputModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    masterOccupationAPIInputModel.OperationType = OperationAction.Delete;
                }
                SaveOccupationDetails(masterOccupationAPIInputModel);
            };
        }
        public APIReturnModel aPIReturnmodel;
        [Inject]
        public ISnackbar snackBar { get; set; }

        public async void SaveOccupationDetails(MasterOccuptionAPIInputModel masterOccupationModel)
        {
            aPIReturnmodel = (await masterDataService.AddUpdateDeleteOccuptionMaster(masterOccupationModel));
            if (aPIReturnmodel.status == false)
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Info);
                HeaderTest = "Save Record";
                GetOccupationList();
                ClearAllFields();
            }
            else
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Error);
            }
        }
        //Get Occupation List
        private async Task GetOccupationList()
        {
            DefaultInputParameterModel defaultInputParameterModel = new DefaultInputParameterModel()
            {
                SchoolCode = _sessionModel.SchoolCode,
                FinancialYear = _sessionModel.FinancialYear,
                OperationType = ReportType.All
            };
            //Get all list from API.
            masterOccupationListModels = (await masterDataService.GetOccuptionList(defaultInputParameterModel)).ToList();
        }
        private void ClearAllFields()
        {
            masterOccupationViewModel.occuptionName = null;
            masterOccupationViewModel.occuptionCode = null;
            masterOccupationViewModel.displayOrder = null;
        }
        protected override async Task OnInitializedAsync()
        {
            // System.Diagnostics.Debugger.Break();
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            //Get Occupation List Details
            DefaultInputParameterModel defaultInputParameterModel = new DefaultInputParameterModel()
            {
                SchoolCode = _sessionModel.SchoolCode,
                FinancialYear = _sessionModel.FinancialYear,
                OperationType = ReportType.All
            };
            //Get all list from API.
            GetOccupationList();

            //masterOccupationListModels = (await masterDataService.GetMasterOccupationList(defaultInputParameterModel)).ToList();
        }
        public string HeaderTest = string.Empty;
        //for API Model to View model data binding.
        public void RowSelectHandler(RowSelectEventArgs<MasterOccuptionListModel> args)
        {
            masterOccupationViewModel = new OccupationMasterViewModel()
            {
                occuptionName = args.Data.occuptionName,
                occuptionCode = args.Data.occuptionCode,
                displayOrder = Convert.ToInt16( args.Data.displayOrder),
                masterOccuptionId = args.Data.masterOccuptionId
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
        public List<string> classMasterItem = (new List<string>() { "Add Occupation", "Print", "Search" });

        public SfGrid<MasterOccuptionListModel> sfOccupationMaster;
        public string btncss = "";
        public void EditOccupationMaster(CommandClickEventArgs<MasterOccuptionListModel> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            if (buttontext == "Edit")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                IsVisible = true;
                DialogHeaderName = "Update Occupation Details";
                HeaderTest = "Update Record";
                OperationType = "Update";
                btncss = "e-flat e-info e-outline";
                masterOccupationViewModel = new OccupationMasterViewModel()
                {
                    occuptionName = args.RowData.occuptionName,
                    occuptionCode = args.RowData.occuptionCode,
                    displayOrder = Convert.ToInt16( args.RowData.displayOrder),
                    masterOccuptionId = args.RowData.masterOccuptionId
                };
            }
            else
            {
                IsVisible = true;
                DialogHeaderName = "Delete Occupation Details";
                OperationType = "Delete";
                HeaderTest = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                masterOccupationViewModel = new OccupationMasterViewModel()
                {
                    occuptionName = args.RowData.occuptionName,
                    occuptionCode = args.RowData.occuptionCode,
                    displayOrder = Convert.ToInt16( args.RowData.displayOrder),
                    masterOccuptionId = args.RowData.masterOccuptionId
                };
            }
            //else
            //{
            //    navigationManager.NavigateTo($"/OnlineExam/OnlineTestDetails/{testId}");
            //}

        }
        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add Occupation")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add New Occupation Details";
                OperationType = "Add";
                HeaderTest = "Add Occupation";
                masterOccupationViewModel.occuptionName = null;
                masterOccupationViewModel.occuptionCode = null;
                masterOccupationViewModel.displayOrder = null;
            }
        }
    }
}
