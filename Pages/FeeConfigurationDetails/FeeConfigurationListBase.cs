using AdminDashboard.Server.Models.FeeConfigurationList;
using AdminDashboard.Server.Service.Interface.FeeConfiguration;
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Syncfusion.Blazor.Grids;
namespace AdminDashboard.Server.Pages.FeeConfigurationDetails
{
    public class FeeConfigurationListBase: ComponentBase
    {

        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }

        [Inject]
        public IFeeConfigurationListService feeConfigurationListService { get; set; }

        public SessionModel _sessionModel;
        public FeeConfigurationListModel feeConfigurationModels { get; set; }

        public List<FeeConfigurationModel> feeConfigurationListModel = new List<FeeConfigurationModel>();

        public SfGrid<FeeConfigurationModel> Grid;

        public string[] GroupedColumns = new string[] { "className", "categoryDetails" };
        protected override async Task OnInitializedAsync()
        {

            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            feeConfigurationModels = (await feeConfigurationListService.GetFeeConfigurationList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, 0, ReportType.All));
            feeConfigurationListModel = feeConfigurationModels.feeConfigurationListModels;
        }
        public void ExportToExcel()
        {
            this.Grid.ExcelExport();
        }
    }
}
