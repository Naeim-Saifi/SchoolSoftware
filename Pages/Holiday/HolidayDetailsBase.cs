using AdminDashboard.Server.Models.Holiday;
using AdminDashboard.Server.Service.Interface.Holiday;
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Syncfusion.Blazor.Calendars;
using Syncfusion.Blazor.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.Holiday
{
    public class HolidayDetailsBase : ComponentBase
    {

        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;

        // Time Table service

        [Inject]
        public IHolidayService _holidayService { get; set; }
        public List<HolidayTypeListMaster> holidayTypeListMasters = new List<HolidayTypeListMaster>();
        public List<HolidayDetailsListModel>  holidayDetailsListModels = new List<HolidayDetailsListModel>();
        public List<MonthNameList>  monthNameLists = new List<MonthNameList>();

        public SfGrid<HolidayDetailsListModel> Grid;
        protected override async Task OnInitializedAsync()
        {

            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            holidayTypeListMasters = (await _holidayService.GetHolidayTypeMasterDetails(_sessionModel.SchoolCode, _sessionModel.FinancialYear, 0, ReportType.All)).ToList();
            holidayDetailsListModels = (await _holidayService.GetHolidayDetails(_sessionModel.SchoolCode, _sessionModel.FinancialYear, 0, ReportType.All)).ToList();
            monthNameLists= (await _holidayService.GetMonthNameList()).ToList();
        }

        public async void ActionBeginHandler(ActionEventArgs<HolidayDetailsListModel> args)
        {
            HolidayDetailsModel holidayDetailsModel = new HolidayDetailsModel();
            string month = "";
            if (args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Save))
            {

                if (args.Action == "Add")
                {
                    holidayDetailsModel.SchoolCode = _sessionModel.SchoolCode;
                    holidayDetailsModel.FinancialYear = _sessionModel.FinancialYear;
                    holidayDetailsModel.HolidayDayId = args.Data.holidayDayId;
                    holidayDetailsModel.HolidayTypeId = Convert.ToInt32(args.Data.holidayType);
                   // holidayDetailsModel.HolidayType = args.Data.holidayType;
                    holidayDetailsModel.MonthNameId = Convert.ToInt32(args.Data.monthNameDetails);
                    holidayDetailsModel.FromDate = args.Data.fromDate;
                    holidayDetailsModel.Todate = args.Data.todate;                    
                    holidayDetailsModel.CreatedByUserId = _sessionModel.UserId;
                    holidayDetailsModel.OperationType = OperationAction.Add;
                    await SaveHolidayDetails(holidayDetailsModel);
                }
                else
                {
                    holidayDetailsModel.SchoolCode = _sessionModel.SchoolCode;
                    holidayDetailsModel.FinancialYear = _sessionModel.FinancialYear;
                    holidayDetailsModel.HolidayDayId = args.Data.holidayDayId;
                    holidayDetailsModel.HolidayTypeId = Convert.ToInt32(args.Data.holidayType); 
                   // holidayDetailsModel.HolidayType = args.Data.holidayType;
                    holidayDetailsModel.MonthNameId = Convert.ToInt32(args.Data.monthNameDetails);
                    holidayDetailsModel.FromDate = args.Data.fromDate;
                    holidayDetailsModel.Todate = args.Data.todate;
                    holidayDetailsModel.CreatedByUserId = _sessionModel.UserId;
                    holidayDetailsModel.OperationType = OperationAction.Update;
                    await SaveHolidayDetails(holidayDetailsModel);
                }

            }
            else if (args.RequestType.ToString() == "Delete")
            {
                holidayDetailsModel.SchoolCode = _sessionModel.SchoolCode;
                holidayDetailsModel.FinancialYear = _sessionModel.FinancialYear;
                holidayDetailsModel.HolidayTypeId = Convert.ToInt32(args.Data.holidayType); 
                holidayDetailsModel.HolidayDayId = args.Data.holidayDayId;
                holidayDetailsModel.MonthNameId = Convert.ToInt32(args.Data.monthNameDetails);
                holidayDetailsModel.FromDate = args.Data.fromDate;
                holidayDetailsModel.Todate = args.Data.todate;
                holidayDetailsModel.CreatedByUserId = _sessionModel.UserId;

                holidayDetailsModel.OperationType = OperationAction.Delete;
                await SaveHolidayDetails(holidayDetailsModel);
            }

        }
        
        [Inject]
        public ISnackbar snackBar { get; set; }
        private async Task SaveHolidayDetails(HolidayDetailsModel holidayTypeMaster)
        {
            try
            {
                string msg = (await _holidayService.AddUpdateHolidayDetails(holidayTypeMaster)).message.ToString();
                if (msg == "Holiday Details created successfully")
                {
                    snackBar.Add(msg, Severity.Success);
                    holidayDetailsListModels = (await _holidayService.GetHolidayDetails(_sessionModel.SchoolCode, _sessionModel.FinancialYear, 0, ReportType.All)).ToList();
                    this.Grid.Refresh();
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);
                    holidayDetailsListModels = (await _holidayService.GetHolidayDetails(_sessionModel.SchoolCode, _sessionModel.FinancialYear, 0, ReportType.All)).ToList();
                    this.Grid.Refresh();
                }
            }
            catch (Exception ex)
            {
                snackBar.Add(ex.Message, Severity.Error);
            }
        }
        public void ExportToExcel()
        {
            //if (args.Item.Id == "Grid_excelexport")  //Id is combination of Grid's ID and itemname
            //{
                ExcelExportProperties ExportProperties = new ExcelExportProperties();
                ExcelHeader header = new ExcelHeader();
                header.HeaderRows = 2;
                List<ExcelCell> cell = new List<ExcelCell>
        {
                new ExcelCell() { RowSpan= 2,ColSpan=5 , Value= "PO Report from " + "" + " to " + "" + " Vendor All", Style = new ExcelStyle() { Bold = true, FontSize = 13, Italic= true }  }
            };

                List<ExcelRow> HeaderContent = new List<ExcelRow>
        {
                new ExcelRow() {  Cells = cell, Index = 1 }
            };
                header.Rows = HeaderContent;
                ExportProperties.Header = header;
                this.Grid.ExcelExport(ExportProperties);
            //}

            //this.Grid.ExcelExport();
        }
        private SfDateRangePicker<DateTime?> DateRange;
        public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Id == "Grid_excelexport")  //Id is combination of Grid's ID and itemname      
            {
                ExcelExportProperties ExportProperties = new ExcelExportProperties();
                ExcelHeader header = new ExcelHeader();
                header.HeaderRows = 2;
                List<ExcelCell> cell = new List<ExcelCell>
            {
                new ExcelCell() { RowSpan= 2,ColSpan=5 , Value= "PO Report from " + DateRange.StartDate.ToString() + " to " + DateRange.EndDate.ToString() + " Vendor All", Style = new ExcelStyle() { Bold = true, FontSize = 13, Italic= true }  }
            };

                List<ExcelRow> HeaderContent = new List<ExcelRow>
            {
                new ExcelRow() {  Cells = cell, Index = 1 }
            };
                header.Rows = HeaderContent;
                ExportProperties.Header = header;
                await this.Grid.ExcelExport(ExportProperties);
            }
        }
    }
}
