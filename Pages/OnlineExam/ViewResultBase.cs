using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.OnlineExam
{
    public class ViewResultBase : ComponentBase
    {

        public List<string> ToolBarItems = (new List<string>() { "Print", "Back" });
        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        [Inject]
        NavigationManager navigationManager { get; set; }

        public SfGrid<ViewResultDetailsList> sfViewResult;
        //public string[] GroupedColumns = new string[] { "subjectName" };
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;

        [Parameter]
        public string Value { get; set; }
        public  class ViewResultDetailsList
        {
            public int QuestionId { get; set; }
            public string QuestionDetails { get; set; }
            public string SelectedAnsweer { get; set; }
            public string CorrectAnsweer { get; set; }
            public string AnsweerStatus { get; set; }
        }
        public List<ViewResultDetailsList>  viewResultDetailsLists = new List<ViewResultDetailsList>();
        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");

            viewResultDetailsLists = new List<ViewResultDetailsList>()
        {
            new ViewResultDetailsList(){ QuestionId=101, QuestionDetails="Uttrakhand, Uttar Pradesh, Bihar, West Bengal and Sikkim have common frontiers with---",
             CorrectAnsweer="China ", SelectedAnsweer="Bhutan ", AnsweerStatus="Incorrect"},
            new ViewResultDetailsList(){ QuestionId=102, QuestionDetails="Which one of the following is an item of working capital?",
             CorrectAnsweer="Money", SelectedAnsweer="Machines", AnsweerStatus="Incorrect" }
        };
        }
        public void ExportToExcel()
        {
            this.sfViewResult.ExcelExport();
        }
        public void OnCommandClicked(CommandClickEventArgs<ViewResultDetailsList> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.;
            //if (buttontext == "View Result")
            //{
            //    navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
            //}
            //else
            //{
            //    navigationManager.NavigateTo($"/OnlineExam/OnlineTestDetails/{testId}");
            //}

        }

        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
           
            
            if (args.Item.Text == "Back")
            {
                navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
            }
        }
    }
}
