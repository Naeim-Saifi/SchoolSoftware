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
    public class DocumentMasterBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }

        //this model is use for get data userdetails and finaciyal year.
        public SessionModel sessionModel { get; }

        //this is use for API Function
        [Inject]
        public IMasterDataService masterDataService { get; set; }

        //this model use for send data to API ,binding view model with API model
        public MasterDocumentAPIInputModel masterDocumentAPIInputModel { get; set; }
        //data binding for blazor page
        public DocumentMasterViewModel masterDocumentViewModel = new DocumentMasterViewModel();

        //list Model
        public List<MasterDocumentListModel> masterDocumentListModels = new List<MasterDocumentListModel>();

        public SessionModel _sessionModel;
        public string OperationType = "";

        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                masterDocumentAPIInputModel = new MasterDocumentAPIInputModel()
                {
                    documentName = masterDocumentViewModel.documentName,
                    documentCode = masterDocumentViewModel.documentCode,
                    displayOrder = Convert.ToInt16(masterDocumentViewModel.displayOrder),
                    masterDocumentId = masterDocumentViewModel.masterDocumentId,
                    CreatedByUserId = _sessionModel.UserId,
                    SchoolCode = _sessionModel.SchoolCode,
                    FinancialYear = _sessionModel.FinancialYear,
                };

                if (OperationType == "Add")
                {
                    masterDocumentAPIInputModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    masterDocumentAPIInputModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    masterDocumentAPIInputModel.OperationType = OperationAction.Delete;
                }
                SaveDocumentDetails(masterDocumentAPIInputModel);
            };
        }
        public APIReturnModel aPIReturnmodel;
        [Inject]
        public ISnackbar snackBar { get; set; }

        public async void SaveDocumentDetails(MasterDocumentAPIInputModel masterDocumentModel)
        {
            aPIReturnmodel = (await masterDataService.AddUpdateDeleteDocumentMaster(masterDocumentModel));
            if (aPIReturnmodel.status == false)
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Info);
                HeaderTest = "Save Record";
                GetDocumentList();
                ClearAllFields();
            }
            else
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Error);
            }
        }
        //Get Document List
        private async Task GetDocumentList()
        {
            DefaultInputParameterModel defaultInputParameterModel = new DefaultInputParameterModel()
            {
                SchoolCode = _sessionModel.SchoolCode,
                FinancialYear = _sessionModel.FinancialYear,
                OperationType = ReportType.All
            };
            //Get all list from API.
            masterDocumentListModels = (await masterDataService.GetDocumentList(defaultInputParameterModel)).ToList();
        }
        private void ClearAllFields()
        {
            masterDocumentViewModel.documentName = null;
            masterDocumentViewModel.documentCode = null;
            masterDocumentViewModel.displayOrder = null;
        }
        protected override async Task OnInitializedAsync()
        {
            // System.Diagnostics.Debugger.Break();
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            //Get Document List Details
            DefaultInputParameterModel defaultInputParameterModel = new DefaultInputParameterModel()
            {
                SchoolCode = _sessionModel.SchoolCode,
                FinancialYear = _sessionModel.FinancialYear,
                OperationType = ReportType.All
            };
            //Get all list from API.
            GetDocumentList();

            //masterDocumentListModels = (await masterDataService.GetMasterDocumentList(defaultInputParameterModel)).ToList();
        }
        public string HeaderTest = string.Empty;
        //for API Model to View model data binding.
        public void RowSelectHandler(RowSelectEventArgs<MasterDocumentListModel> args)
        {
            masterDocumentViewModel = new DocumentMasterViewModel()
            {
                documentName = args.Data.documentName,
                documentCode = args.Data.documentCode,
                displayOrder =Convert.ToInt16( args.Data.displayOrder),
                masterDocumentId = args.Data.masterDocumentId
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
        public List<string> classMasterItem = (new List<string>() { "Add Document", "Print", "Search" });

        public SfGrid<MasterDocumentListModel> sfDocumentMaster;
        public string btncss = "";
        public void EditDocumentMaster(CommandClickEventArgs<MasterDocumentListModel> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            if (buttontext == "Edit")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                IsVisible = true;
                DialogHeaderName = "Update Document Details";
                HeaderTest = "Update Record";
                OperationType = "Update";
                btncss = "e-flat e-info e-outline";
                masterDocumentViewModel = new DocumentMasterViewModel()
                {
                    documentName = args.RowData.documentName,
                    documentCode = args.RowData.documentCode,
                    displayOrder = Convert.ToInt16( args.RowData.displayOrder),
                    masterDocumentId = args.RowData.masterDocumentId
                };
            }
            else
            {
                IsVisible = true;
                DialogHeaderName = "Delete Document Details";
                OperationType = "Delete";
                HeaderTest = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                masterDocumentViewModel = new DocumentMasterViewModel()
                {
                    documentName = args.RowData.documentName,
                    documentCode = args.RowData.documentCode,
                    displayOrder = Convert.ToInt16( args.RowData.displayOrder),
                    masterDocumentId = args.RowData.masterDocumentId
                };
            }
            //else
            //{
            //    navigationManager.NavigateTo($"/OnlineExam/OnlineTestDetails/{testId}");
            //}

        }
        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add Document")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add New Document Details";
                OperationType = "Add";
                HeaderTest = "Add Document";
                masterDocumentViewModel.documentName = null;
                masterDocumentViewModel.documentCode = null;
                masterDocumentViewModel.displayOrder = null;
            }
        }
    }
}
