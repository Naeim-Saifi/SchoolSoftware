using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.Attendance
{
    public class ViewStudentAttendanceListBase : ComponentBase
    {

        public class StudentRegisterList
        {
            public string studentId { get; set; }
            public string studentName { get; set; }
            public string fatherName { get; set; }
            public string motherName { get; set; }
            public string rollNo { get; set; }
            public string present { get; set; }
            public string absent { get; set; }
            public string leave { get; set; }
            public string totalWorkingDays { get; set; }
            public string sunday { get; set; }
            public string holiday { get; set; }

            
        }
        public class StudentAttendanceStatusList
        {
            public string studentId { get; set; }
            public string Date { get; set; }
            public string status { get; set; }
             
        }
        public SfGrid<StudentRegisterList> sfStudentRegester;
        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        public List<string> ToolBarItems = (new List<string>() { "Select Class","Print", "Export","Search" });
        public void ExportToExcel()
        {
            this.sfStudentRegester.ExcelExport();
        }
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;
        public List<StudentRegisterList> studentRegisterLists { get; set; }
        public List<StudentAttendanceStatusList>  studentAttendanceStatusLists { get; set; }
        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            studentRegisterLists = new List<StudentRegisterList>()
            { new StudentRegisterList(){ studentId="1001", studentName="Md Imran", fatherName="Md Saleem",
              motherName="Naseema Begum", rollNo="01", present="20", absent="10", leave="3", holiday="2",
               sunday="4",  totalWorkingDays="26"
            },
            new StudentRegisterList(){ studentId="1002", studentName="Md Imran", fatherName="Md Saleem",
              motherName="Naseema Begum", rollNo="01", present="20", absent="10", leave="3", holiday="2",
               sunday="4",  totalWorkingDays="26"
            }};
            studentAttendanceStatusLists = new List<StudentAttendanceStatusList>()
            {
                new StudentAttendanceStatusList(){ studentId="1001", status="Present",Date="01-March-23"},
                new StudentAttendanceStatusList(){ studentId="1001", status="Absent",Date="02-March-23"},
                new StudentAttendanceStatusList(){ studentId="1001", status="Leave",Date="03-March-23"},
                new StudentAttendanceStatusList(){ studentId="1001", status="Holiday",Date="04-March-23"},
                new StudentAttendanceStatusList(){ studentId="1001", status="Present",Date="05-March-23"},
            };

        }
       
        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Export")
            {

                ExportToExcel();
                //perform your actions here
            }
        }


    }
}