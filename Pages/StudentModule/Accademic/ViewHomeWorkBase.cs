using AdminDashboard.Server.Models.HomeWork;
using AdminDashboard.Server.Service.Interface.HomeWork;
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.StudentModule.Accademic
{
    public class ViewHomeWorkBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;
        [Inject]
        public IHomeWorkService homeWorkService { get; set; }
        public List<HomeworkListModel> homeworkListModels = new List<HomeworkListModel>();
        public DateTime? fromValue { get; set; } = DateTime.Now;

        public DateTime? toValue { get; set; } = DateTime.Now;
        public string[] GroupedColumns = new string[] { "fromDate" };

        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            homeworkListModels = (await homeWorkService.GetHomeWorkList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, fromValue.ToString(), toValue.ToString(), _sessionModel.UserId, ReportType.All)).ToList();
        }
        public SfGrid<HomeworkListModel> HomeworkList_grid;
        public void BtnClick()
        {
            this.HomeworkList_grid.DetailExpandAll();
        }
    }
}
