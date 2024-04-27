using AdminDashboard.Server.API_Service.Interface.RemarksTypeMaster;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.RemarksTypeMaster;
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


namespace AdminDashboard.Server.User_Pages.MasterData
{
    public class RemaksTypeDescriptionBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;

        public RemarksTypeDescriptionViewModel remarksTypeViewModel = new RemarksTypeDescriptionViewModel();
        RemarksTypeDescriptionAPIModel remarksTypeAPIModel = new RemarksTypeDescriptionAPIModel();
        [Inject]
        public IRemarksTypeMasterService _remarksTypeService { get; set; }
        public List<RemarksTypeDescriptionListModel> _remarksTypeDescriptionList = new List<RemarksTypeDescriptionListModel>();
        public List<RemarksTypeListModel> _remarksTypeList = new List<RemarksTypeListModel>();

        public SfGrid<RemarksTypeDescriptionListModel> sfremarksTypeList;
        public string OperationType = "";
        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        public List<string> toolBarListItems = (new List<string>() { "Add Remarks Type", "Print", "Search" });
        public DialogEffect AnimationEffect = DialogEffect.Zoom;
        public string HeaderStyles { get; set; } = "e-background e-accent";
        public SfDialog DialogRef;
        public int pageSize = 50;
        protected override async Task OnInitializedAsync()
        {

            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");

            GetRemarksTypeList();
        }
        private async void GetRemarksTypeList()
        {


            RemarksType_Input_Para_Model remarksType_Input_Para_Model = new RemarksType_Input_Para_Model()
            {
                financialYear = _sessionModel.FinancialYear,
                remarksTypeID = 0,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.All
            };

            _remarksTypeList = (await _remarksTypeService.Get_RemarksTypeList_List(remarksType_Input_Para_Model)).ToList();

            RemarksTypeDescription_Input_Para_Model remarksTypeDescription_Input_Para_Model = new RemarksTypeDescription_Input_Para_Model()
            {
                financialYear = _sessionModel.FinancialYear,
                 RemarksTypeDescriptionID = 0,
                schoolCode = _sessionModel.SchoolCode,


                reportType = ReportType.All
            };

            _remarksTypeDescriptionList = (await _remarksTypeService.Get_RemarksTypeDescription(remarksTypeDescription_Input_Para_Model)).ToList();
        }
        public bool IsVisible { get; set; } = false;
        public string DialogHeaderName = string.Empty;
        public bool ddEnable = true;
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
        public string btncss = "";
        public string HeaderText = string.Empty;

        public void EditRemarksTypeList(CommandClickEventArgs<RemarksTypeDescriptionListModel> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            if (buttontext == "Edit")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                IsVisible = true;
                DialogHeaderName = "Update Holiday Details";
                HeaderText = "Update Record";
                OperationType = "Update";
                btncss = "e-flat e-info e-outline";
                ddEnable = false;
                remarksTypeViewModel = new RemarksTypeDescriptionViewModel()
                {
                     remarksTypeDescriptionID = args.RowData.remarksTypeDescriptionID,
                       remarksTypeId = args.RowData.remarksTypeId,
                      displayOrder = args.RowData.displayOrder,
                     remarksTypeTitle = args.RowData.remarksTypeDescriptionTitle,
                     remarksTypeDescriptionTitle = args.RowData.remarksTypeDescriptionTitle,
                     remarksTypeDescriptionTitleDetails = args.RowData.remarksTypeDescriptionTitleDetails,
                };
            }
            else
            {
                IsVisible = true;
                DialogHeaderName = "Delete Holiday Details";
                OperationType = "Delete";
                HeaderText = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                ddEnable = false;
                remarksTypeViewModel = new RemarksTypeDescriptionViewModel()
                {
                    remarksTypeDescriptionID = args.RowData.remarksTypeDescriptionID,
                    remarksTypeId = args.RowData.remarksTypeId,
                    displayOrder = args.RowData.displayOrder,
                    remarksTypeTitle = args.RowData.remarksTypeDescriptionTitle,
                    remarksTypeDescriptionTitle = args.RowData.remarksTypeDescriptionTitle,
                    remarksTypeDescriptionTitleDetails = args.RowData.remarksTypeDescriptionTitleDetails,
                };

            }


        }

        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add Remarks Type")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add New Remarks Type";
                OperationType = "Add";
                HeaderText = "Add Remarks Type";
                ddEnable = true;
                ClearAllFields();
            }
        }

        [Inject]
        public ISnackbar snackBar { get; set; }

        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                remarksTypeAPIModel = new RemarksTypeDescriptionAPIModel()
                {
                    remarksTypeDescriptionID = Convert.ToInt16(remarksTypeViewModel.remarksTypeDescriptionID),
                    remarksTitleID =Convert.ToString(remarksTypeViewModel.remarksTypeId),
                    remarksTypeDescriptionTitle = remarksTypeViewModel.remarksTypeDescriptionTitle,
                    remarksTypeDescriptionTitleDetails = remarksTypeViewModel.remarksTypeDescriptionTitleDetails,
                    displayOrder=Convert.ToInt16(remarksTypeViewModel.displayOrder),
                    CreatedByUserId = _sessionModel.UserId,
                    UpdatedByUserId = _sessionModel.UserId,
                    SchoolCode = _sessionModel.SchoolCode,
                    FinancialYear = _sessionModel.FinancialYear,
                };

                if (OperationType == "Add")
                {
                    remarksTypeAPIModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    remarksTypeAPIModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    remarksTypeAPIModel.OperationType = OperationAction.Delete;
                }
                SaveRemarksType(remarksTypeAPIModel);
            };
        }
        public APIReturnModel aPIReturnmodel;
        public async void SaveRemarksType(RemarksTypeDescriptionAPIModel remarksTypeAPIModel)
        {
            aPIReturnmodel = await _remarksTypeService.CRUD_RemarksTypeDescription(remarksTypeAPIModel);
            if (aPIReturnmodel.status == false)
            {
                if (remarksTypeAPIModel.OperationType == OperationAction.Delete)
                {
                    snackBar.Add(aPIReturnmodel.message, Severity.Error);
                }
                else if (remarksTypeAPIModel.OperationType != OperationAction.Add)
                {
                    snackBar.Add(aPIReturnmodel.message, Severity.Info);
                }
                else
                {
                    snackBar.Add(aPIReturnmodel.message, Severity.Success);
                }

                HeaderText = "Save Record";
                GetRemarksTypeList();
                ClearAllFields();
                StateHasChanged();
            }
            else
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Error);
            }
        }
        private void ClearAllFields()
        {

            remarksTypeViewModel.displayOrder = 0;            
            remarksTypeViewModel.remarksTypeDescriptionID = 0;
            remarksTypeViewModel.remarksTypeDescriptionTitle = null;
            remarksTypeViewModel.remarksTypeDescriptionTitleDetails = null;
        }
    }
}
