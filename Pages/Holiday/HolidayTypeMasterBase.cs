using AdminDashboard.Server.Service.Interface.Holiday;
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.Server.Models.Holiday;
using MudBlazor;

namespace AdminDashboard.Server.Pages.Holiday
{
    public class HolidayTypeMasterBase: ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;

        // Time Table service

        [Inject]
        public IHolidayService _holidayService { get; set; }
        public List<HolidayTypeListMaster>  holidayTypeListMasters = new List<HolidayTypeListMaster>();
        public SfGrid<HolidayTypeListMaster> Grid;
        protected override async Task OnInitializedAsync()
        {

            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            holidayTypeListMasters = (await _holidayService.GetHolidayTypeMasterDetails(_sessionModel.SchoolCode, _sessionModel.FinancialYear, 0, ReportType.All)).ToList();

        }
        public async void ActionBeginHandler(ActionEventArgs<HolidayTypeListMaster> args)
        {
            HolidayTypeMasterModel holidayTypeMaster = new HolidayTypeMasterModel();
            if (args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Save))
            {
                if (args.Action == "Add")
                {
                    holidayTypeMaster.SchoolCode = _sessionModel.SchoolCode;
                    holidayTypeMaster.FinancialYear = _sessionModel.FinancialYear;
                    holidayTypeMaster.HolidayTypeId = 0;
                    holidayTypeMaster.HolidayType = args.Data.holidayType.ToUpper();
                    holidayTypeMaster.HolidayDescriptionEnglish = args.Data.holidayDescriptionEnglish.ToUpper();
                    holidayTypeMaster.HolidayDescriptionHindi = args.Data.holidayDescriptionHindi;
                    holidayTypeMaster.CreatedByUserId = _sessionModel.UserId;
                    holidayTypeMaster.OperationType = OperationAction.Add;
                    await SaveHolidayTypeDetails(holidayTypeMaster);
                }
                else
                {
                    holidayTypeMaster.SchoolCode = _sessionModel.SchoolCode;
                    holidayTypeMaster.FinancialYear = _sessionModel.FinancialYear;
                    holidayTypeMaster.HolidayTypeId = args.Data.holidayTypeId;
                    holidayTypeMaster.HolidayType = args.Data.holidayType.ToUpper();
                    holidayTypeMaster.HolidayDescriptionEnglish = args.Data.holidayDescriptionEnglish.ToUpper();
                    holidayTypeMaster.HolidayDescriptionHindi = args.Data.holidayDescriptionHindi;
                    holidayTypeMaster.CreatedByUserId = _sessionModel.UserId;
                    holidayTypeMaster.OperationType = OperationAction.Update;
                    await SaveHolidayTypeDetails(holidayTypeMaster);
                }

            }
            else if (args.RequestType.ToString() == "Delete")
            {
                holidayTypeMaster.SchoolCode = _sessionModel.SchoolCode;
                holidayTypeMaster.FinancialYear = _sessionModel.FinancialYear;
                holidayTypeMaster.HolidayTypeId = args.Data.holidayTypeId;
                holidayTypeMaster.HolidayType = args.Data.holidayType.ToUpper();
                holidayTypeMaster.HolidayDescriptionEnglish = args.Data.holidayDescriptionEnglish.ToUpper();
                holidayTypeMaster.HolidayDescriptionHindi = args.Data.holidayDescriptionHindi;
                holidayTypeMaster.CreatedByUserId = _sessionModel.UserId;
                holidayTypeMaster.OperationType = OperationAction.Delete;
                await SaveHolidayTypeDetails(holidayTypeMaster);
            }
        }
        private async Task SaveHolidayTypeDetails(HolidayTypeMasterModel  holidayTypeMaster)
        {
            try
            {
                string msg = (await _holidayService.AddUpdateHolidayTypeMaster(holidayTypeMaster)).message.ToString();
                if (msg == "Period Master created successfully")
                {
                    snackBar.Add(msg, Severity.Success);
                    holidayTypeListMasters = (await _holidayService.GetHolidayTypeMasterDetails(_sessionModel.SchoolCode, _sessionModel.FinancialYear, 0, ReportType.All)).ToList();
                    this.Grid.Refresh();
                }
                else
                {
                    snackBar.Add(msg, Severity.Success);
                    holidayTypeListMasters = (await _holidayService.GetHolidayTypeMasterDetails(_sessionModel.SchoolCode, _sessionModel.FinancialYear, 0, ReportType.All)).ToList();
                    this.Grid.Refresh();
                }
            }
            catch (Exception ex)
            {
                snackBar.Add(ex.Message, Severity.Error);
            }
        }



        [Inject]
        public ISnackbar snackBar { get; set; }
        public void ExportToExcel()
        {
            this.Grid.ExcelExport();
        }
    }
}
