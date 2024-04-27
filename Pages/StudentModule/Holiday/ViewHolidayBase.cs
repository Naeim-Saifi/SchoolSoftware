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

namespace AdminDashboard.Server.Pages.StudentModule.Holiday
{
    public class ViewHolidayBase : ComponentBase
    {

        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;

        // Time Table service

        [Inject]
        public IHolidayService _holidayService { get; set; }
        //public List<HolidayTypeListMaster> holidayTypeListMasters = new List<HolidayTypeListMaster>();
        public List<HolidayDetailsListModel> holidayDetailsListModels = new List<HolidayDetailsListModel>();
        public List<MonthNameList> monthNameLists = new List<MonthNameList>();
        public SfGrid<HolidayDetailsListModel> Grid;

        protected override async Task OnInitializedAsync()
        {

            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            //holidayTypeListMasters = (await _holidayService.GetHolidayTypeMasterDetails(_sessionModel.SchoolCode, _sessionModel.FinancialYear, 0, ReportType.All)).ToList();
            holidayDetailsListModels = (await _holidayService.GetHolidayDetails(_sessionModel.SchoolCode, _sessionModel.FinancialYear, 0, ReportType.All)).ToList();
            //monthNameLists = (await _holidayService.GetMonthNameList()).ToList();
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
    }
}
