using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.OnlineExam
{
    public class TestListBase : ComponentBase
    {
        public List<string> ToolBarItems = (new List<string>() { "Print", "Search" });
        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        [Inject]
        NavigationManager navigationManager { get; set; }

        public SfGrid<TestListModel> sftestList;
        public string[] GroupedColumns = new string[] {  "testStatus", "subjectName" };
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;
        public class TestListModel
        {
            public int testID { get; set; }
            public string subjectName { get; set; }
            public string testName { get; set; }
            public string testStatus { get; set; }
            public string testTime { get; set; }
            public string testCompletedTime { get; set; }
            public string testRemarks { get; set; }
            public string totalMarks { get; set; }
            public string correctAnsweer { get; set; }
            public string worngAnsweer { get; set; }
            public string totalObtainMarks { get; set; }
        }


        public void OnCommandClicked(CommandClickEventArgs<TestListModel> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            int testId = args.RowData.testID;
            if(buttontext== "View Result")
            {
                navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
            }
            else
            {
                navigationManager.NavigateTo($"/OnlineExam/OnlineTestDetails/{testId}");
            }

        }
        public void ExportToExcel()
        {
            this.sftestList.ExcelExport();
        }
        public List<TestListModel> testListModels=new List<TestListModel>();
        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");

        testListModels = new List<TestListModel>()
        {
            new TestListModel(){ testID=1, subjectName="English", testName="Unit Test-I", testStatus="Pending",
                testCompletedTime="", testTime="10:20 PM" ,testRemarks="Need to do hard work", totalMarks="20",
                correctAnsweer="2", worngAnsweer="8", totalObtainMarks="15"},
            new TestListModel(){testID=12, subjectName="English", testName="Unit Test-II", testStatus="Completed",
                testCompletedTime="", testTime="10:20 PM" ,testRemarks="Need to do hard work", totalMarks="20",
                correctAnsweer="2", worngAnsweer="8", totalObtainMarks="15"}
        

        };   
        }
        public void RowBound(RowDataBoundEventArgs<TestListModel> Args)
        {
            if (Args.Data.testStatus == "Completed")
            {
                Args.Row.AddClass(new string[] { "e-removeEditcommand" });
            }
            else
            {
                Args.Row.AddClass(new string[] { "e-removeDeletecommand" });
            }
        }
    }
        
}
   
    

