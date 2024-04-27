using AdminDashboard.Server.Models.Enquiry;
using AdminDashboard.Server.Service.Interface.Enquiry;
using AIS.FrontEnd_07062021.Service.Interface;
using AIS.Model;
using AIS.Model.MasterData;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Syncfusion.Blazor.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.Enquiry
{
    public class EnquiryBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;
        [Inject]
        public IMasterClassService masterClassService { get; set; }
        public List<MasterClassListModel> masterClassListModels = new List<MasterClassListModel>();

        [Inject]
        public IEnquiryService enquiryService { get; set; }

        public List<EnquiryListModel> enquiryListModels = new List<EnquiryListModel>();       
        
        private Boolean Check = false;
        public DialogSettings DialogParams = new DialogSettings { MinHeight = "460px", Width = "650px" };

        public string[] GroupedColumns = new string[] { "promotedClass" };

        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            masterClassListModels = (await masterClassService.GetMasterClassList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.SchoolId, 0, 1, "GetBySchoolId")).ToList();
            InputEnquiryModel inputEnquiryModel = new InputEnquiryModel()
            {
                financiyalYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                fromDate = "",
                todate = "",
                reportType = "ALL",
                userId = _sessionModel.UserId

            };
            enquiryListModels = (await enquiryService.GetEnquiryList(inputEnquiryModel)).ToList();
            // GridData = OrdersDetails.GetAllRecords();
            //LoadEnquiryData();
        }

        public async void LoadEnquiryData()
        {
            InputEnquiryModel inputEnquiryModel = new InputEnquiryModel()
            {
                financiyalYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                fromDate = "",
                todate = "",
                reportType = "ALL",
                userId = _sessionModel.UserId

            };
            enquiryListModels = (await enquiryService.GetEnquiryList(inputEnquiryModel)).ToList();
        }

        public APIReturnModel aPIReturnmodel;
        [Inject]
        public ISnackbar snackBar { get; set; }
        public SfGrid<EnquiryListModel> Grid;
        public async void ActionBeginHandler(ActionEventArgs<EnquiryListModel> args)
        {
            if (args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Save))
            {
                EnquiryModel enquiryModel = new EnquiryModel()
                {
                    enquiryID = args.Data.enquiryID,
                    address = args.Data.address,
                    emailID = args.Data.emailID,
                    currentClassID = Convert.ToInt32(args.Data.className),
                    promotedClassID = Convert.ToInt32(args.Data.promotedClass),
                    studentName = args.Data.studentName,
                    fatherName = args.Data.fatherName,
                    motherName = args.Data.motherName,
                    fMobileNo = args.Data.fMobileNo,
                    mMobileNo = args.Data.mMobileNo,
                    genderID = Convert.ToInt32(args.Data.gender),
                    previousSchoolName = args.Data.previousSchoolName,
                    dob = args.Data.dob,
                    visitingRemrks = args.Data.visitingRemrks,
                    visitingStatus = args.Data.visitingStatus,
                    visitingType = args.Data.visitingType,
                    //operationType = "Add",
                    schoolCode = _sessionModel.SchoolCode,
                    financialYear = _sessionModel.FinancialYear,
                    createdByUserId = _sessionModel.UserId

                };
                if (args.Action == "Add")
                {
                    enquiryModel.enquiryID=0;
                    enquiryModel.operationType = "Add";
                }
                else
                {
                    enquiryModel.operationType = "Update";
                } 
                   aPIReturnmodel = (await enquiryService.AddUpdateEnquiry(enquiryModel));
                    if (aPIReturnmodel.status == true)
                    {
                    snackBar.Add(aPIReturnmodel.message, Severity.Success);
                    //examScheduleViewModel.ExamType = null;
                    //examScheduleViewModel.MonthName = null;
                    //examScheduleViewModel.ClassDetails = null;
                    //examScheduleViewModel.SubjectName = null;
                    //examScheduleViewModel.UnitName = null;
                   
                    }
                    else
                    {
                        snackBar.Add(aPIReturnmodel.message, Severity.Info);
                        //LoadEnquiryData();
                        Grid.Refresh();
                    }
                      
            }
            if (args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Delete))
            {
                //LibraryService.DeleteBook(Args.Data.Id);
            }

        }

        public void ActionCompleteHandler(ActionEventArgs<EnquiryListModel> Args)
        {
            if (Args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Save))
            {
                //LibraryBooks = LibraryService.GetBooks(); //to fetch the updated data from db to Grid
                LoadEnquiryData();
            }
        }
          
        public class OrdersDetails
        {
            public int OrderID { get; set; }
            public int CustomerID { get; set; }
            public int Freight { get; set; }
            public int OrderDate { get; set; }
            public int ShipCountry { get; set; }

        }

        public class Gender
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }

     public  List<Gender> genderDetails = new List<Gender>
        {
            new Gender(){Id=1,Value="Male"},
            new Gender(){Id=2,Value="Female"},
        };

        public class VisitingStatus
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }

      public  List<VisitingStatus>  visitingStatus = new List<VisitingStatus>
        {
            new VisitingStatus(){Id=1,Value="Intrested"},
            new VisitingStatus(){Id=2,Value="Not Intrested"},
        };
        public class VisitingType
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }

        public List<VisitingType>  visitingTypes = new List<VisitingType>
        {
            new VisitingType(){Id=1,Value="Self Visit"},
            new VisitingType(){Id=2,Value="Visit by School"},
            new VisitingType(){Id=2,Value="Student refrence"},
        };
        public void ExportToExcel()
        {
            this.Grid.ExcelExport();
        }
        public class Country
        {
            public string ShipCountry { get; set; }
            public string ShipCity { get; set; }
        }
        List<Country> Name = new List<Country>
{
        new Country() { ShipCountry= "Denmark", ShipCity= "Berlin" },
        new Country() { ShipCountry= "Brazil", ShipCity= "Madrid" },
        new Country() { ShipCountry= "Germany", ShipCity = "Cholchester" },
        new Country() { ShipCountry= "Austria", ShipCity = "Marseille" },
        new Country() { ShipCountry= "Switzerland", ShipCity = "Tsawassen" },
    };
    }
}
