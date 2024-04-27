using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.Attendance
{
    public class StudentAttendanceBase : ComponentBase
    {
        public SfGrid<StudentDetailsList> sfStudentAttendanceRegester;
        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        public List<string> ToolBarItems = (new List<string>() { "Select Class", "Print", "Export", "Search" });
        public class StudentDetailsList
        {
            public string studentId { get; set; }
            public string studentName { get; set; }
            public string fatherName { get; set; }
            public string motherName { get; set; }
            public string rollNo { get; set; } 
        }

        public void ExportToExcel()
        {
            this.sfStudentAttendanceRegester.ExcelExport();
        }
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;
        public List<StudentDetailsList>  studentDetailsLists { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            studentDetailsLists = new List<StudentDetailsList>()
            { new StudentDetailsList(){ studentId="1001", studentName="Md Imran", fatherName="Md Saleem",
              motherName="Naseema Begum", rollNo="01"
            },
            new StudentDetailsList(){ studentId="1002", studentName="Md Imran", fatherName="Md Saleem",
              motherName="Naseema Begum", rollNo="01",
            }};
           

        }

        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Export")
            {

                ExportToExcel();
                //perform your actions here
            }
        }
        public List<double> SelectedRowIndexes { get; set; }
        public double[] TotalValue { get; set; }
        public string SelectedValue;
       
        public async Task GetSelectedRecords(RowSelectEventArgs<StudentDetailsList> args)
        {
            SelectedRowIndexes = await this.sfStudentAttendanceRegester.GetSelectedRowIndexes();
            TotalValue = SelectedRowIndexes.ToArray();
            SelectedValue = "";
            foreach (var data in TotalValue)
            {
                SelectedValue = SelectedValue + " " + data;
            }
            StateHasChanged();
        }


    }
}
