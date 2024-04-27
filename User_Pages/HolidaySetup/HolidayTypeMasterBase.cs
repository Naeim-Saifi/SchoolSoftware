using AdminDashboard.Server.API_Service.Interface.HolidaySetUp;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.HolidaySetup;
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.User_Pages.HolidaySetup
{
    public class HolidayTypeMasterBase:ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;

        public HolidayViewModel _holidayViewModel = new HolidayViewModel();
        public HolidayModel _HolidayModel = new HolidayModel();
        [Inject]
        public IHolidaySetUpService  _holidayService { get; set; }
        public List<Holiday_Type_List_Output_Model> holidayListModels = new List<Holiday_Type_List_Output_Model>();

        public SfGrid<Holiday_Type_List_Output_Model> sfHolidayListGrid;
        public string OperationType = "";
        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        public List<string> classMasterItem = (new List<string>() { "Add Holiday", "Print", "Search" });
        protected override async Task OnInitializedAsync()
        {

            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");

            GetHolidayList();
        }

        private async void GetHolidayList()
        {
            Holiday_Type_List_Input_Para_Model Holiday_Input_Para_Model = new Holiday_Type_List_Input_Para_Model()
            {
                financialYear = _sessionModel.FinancialYear,
                holidayTypeId = 0,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.All
            };

            holidayListModels = (await _holidayService.GET_HolidayType_LIST(Holiday_Input_Para_Model)).ToList();
        }
        public async Task CloseDialog()
        {
            IsVisible = false;
            await this.DialogRef.HideAsync();
        }
        public DialogEffect AnimationEffect = DialogEffect.Zoom;
        public string HeaderStyles { get; set; } = "e-background e-accent";
        public SfDialog DialogRef;
        public int pageSize = 50;
        public bool IsVisible { get; set; } = false;
        public string DialogHeaderName = string.Empty;
        public bool ddEnable = true;
        public void onOpen(Syncfusion.Blazor.Popups.BeforeOpenEventArgs args)
        {
            // setting maximum height to the Dialog
            args.MaxHeight = "750px";

        }
        public string btncss = "";
        public string HeaderText = string.Empty;
        public void EditHolidayMaster(CommandClickEventArgs<Holiday_Type_List_Output_Model> args)
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
                _holidayViewModel = new HolidayViewModel()
                {
                      holidayTypeId = args.RowData.holidayTypeId,
                       holidayType = args.RowData.holidayType,
                     descriptionEnglish = args.RowData.descriptionEnglish,
                     descriptionHindi = args.RowData.descriptionHindi
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
                _holidayViewModel = new HolidayViewModel()
                {
                    holidayTypeId = args.RowData.holidayTypeId,
                    holidayType = args.RowData.holidayType,
                    descriptionEnglish = args.RowData.descriptionEnglish,
                    descriptionHindi = args.RowData.descriptionHindi
                };

            }
            //else
            //{
            //    navigationManager.NavigateTo($"/OnlineExam/OnlineTestDetails/{testId}");
            //}

        }

        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add Holiday")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add New Holiday Details";
                OperationType = "Add";
                HeaderText = "Add Holiday";
                ddEnable = true;

            }
        }

        [Inject]
        public ISnackbar snackBar { get; set; }

        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                _HolidayModel = new HolidayModel()
                {
                     holidayTypeId = _holidayViewModel.holidayTypeId,
                     holidayType = _holidayViewModel.holidayType.Trim().ToUpper(),
                     descriptionEnglish = _holidayViewModel.descriptionEnglish,
                     descriptionHindi = _holidayViewModel.descriptionHindi,
                     CreatedByUserId = _sessionModel.UserId,
                    SchoolCode = _sessionModel.SchoolCode,
                    FinancialYear = _sessionModel.FinancialYear,
                };

                if (OperationType == "Add")
                {
                    _HolidayModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    _HolidayModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    _HolidayModel.OperationType = OperationAction.Delete;
                }
                SaveHolidayDetails(_HolidayModel);
            };
        }
        public APIReturnModel aPIReturnmodel;
        public async void SaveHolidayDetails(HolidayModel HolidayModel)
        {
            aPIReturnmodel = await _holidayService.CRUD_HolidayTypeList(HolidayModel);
            if (aPIReturnmodel.status == false)
            {
                if (HolidayModel.OperationType == OperationAction.Delete)
                {
                    snackBar.Add(aPIReturnmodel.message, Severity.Error);
                }
                else if (HolidayModel.OperationType != OperationAction.Add)
                {
                    snackBar.Add(aPIReturnmodel.message, Severity.Info);
                }
                else
                {
                    snackBar.Add(aPIReturnmodel.message, Severity.Success);
                }

                HeaderText = "Save Record";
                GetHolidayList();
                ClearAllFields();
            }
            else
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Error);
            }
        }
        private void ClearAllFields()
        {

            _holidayViewModel.holidayType = null;
            _holidayViewModel.descriptionHindi= null;
            _holidayViewModel.descriptionEnglish = null;
        }
    }
}
