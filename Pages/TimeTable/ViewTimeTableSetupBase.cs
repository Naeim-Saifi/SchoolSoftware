using AdminDashboard.Server.Models.TimeTable;
using AdminDashboard.Server.Service.Interface.TimeTable;
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.TimeTable
{
    public class ViewTimeTableSetupBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;

        // Time Table service

        [Inject]
        public ITimeTableService _itimetableService { get; set; }
        public List<TimeTableListModel> timeTableListModels = new List<TimeTableListModel>();
        public SfGrid<TimeTableListModel> Grid;
        public string[] GroupedColumns = new string[] { "className" };
        protected override async Task OnInitializedAsync()
        {
           
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            timeTableListModels = (await _itimetableService.GetTimeTableList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, 0, ReportType.All)).ToList();
          
        }

        public void OnCommandClicked(CommandClickEventArgs<TimeTableListModel> args)
        {
            // Perform required operations here
            string index = args.RowData.timeTableID.ToString();

            try
            {

            }
            catch(Exception Ex)
            {

            }
            finally
            {

            }
        }

        public void ExportToExcel()
        { 
            this.Grid.ExcelExport();
        }
    }
}
