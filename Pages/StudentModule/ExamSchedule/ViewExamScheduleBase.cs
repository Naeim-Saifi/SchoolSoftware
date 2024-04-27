using AdminDashboard.Server.Models.ExamSchedule;
using AdminDashboard.Server.Models.Holiday;
using AdminDashboard.Server.Models.MasterConfiguration.SubjectMaster;
using AdminDashboard.Server.Models.MasterConfiguration.UnitMaster;
using AdminDashboard.Server.Models.SyllabusSetup;
using AdminDashboard.Server.Service.Interface.ExamSchedule;
using AdminDashboard.Server.Service.Interface.Holiday;
using AdminDashboard.Server.Service.Interface.MasterConfiguration.SubjectMaster;
using AdminDashboard.Server.Service.Interface.MasterConfiguration.Syllabus;
using AdminDashboard.Server.Service.Interface.SyllabusSetup;
using AIS.FrontEnd_07062021.Service.Interface;
using AIS.Model.GeneralConversion;
using AIS.Model.MasterData;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.StudentModule.ExamSchedule
{
    public class ViewExamScheduleBase : ComponentBase
    {

        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        public IExamScheduleService examScheduleService { get; set; }
        public List<ExamScheduleListModel> examScheduleListModels = new List<ExamScheduleListModel>();
        public ExamScheduleViewModel examScheduleViewModel { get; set; }
        public SessionModel _sessionModel;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                examScheduleViewModel = new ExamScheduleViewModel();
                _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
                examScheduleListModels = (await examScheduleService.GetExamScheduleList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.UserId, "ExamScheduleStudent")).ToList();
                               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
