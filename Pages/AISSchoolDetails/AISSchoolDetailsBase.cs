using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Syncfusion.Blazor.Grids;
using AdminDashboard.Server.Service.Interface.AISSchoolDetails;
using AIS.Data.RequestResponseModel.AISSchoolDetails;
using AIS.Model;
using MudBlazor;

namespace AdminDashboard.Server.Pages.AISSchoolDetails
{
    public class AISSchoolDetailsBase: ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;

        [Inject]
        public IAISSchoolDetailsService  aISSchoolDetailsService { get; set; }
        public List<SchoolDetailsListModel>  schoolDetailsListModels = new List<SchoolDetailsListModel>();

        public DialogSettings DialogParams = new DialogSettings { MinHeight = "475px", Width = "650px" };
        public APIReturnModel aPIReturnmodel;

        public bool IncludeLiterals { get; set; } = true;
        public string Value { get; set; } = "9867433221";

        [Inject]
        public ISnackbar snackBar { get; set; }
        public async void ActionBeginHandler(ActionEventArgs<SchoolDetailsListModel> args)
        {
            if (args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Save))
            {
                if (args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Save))
                {
                    SchoolDetailsModel  schoolDetailsModel = new SchoolDetailsModel()
                    {
                         Id = args.Data.id,
                        School_Name = args.Data.school_Name,
                        Address = args.Data.address,
                        Location = args.Data.location,
                        Director_Name = args.Data.director_Name,
                        Directore_MobileNo = args.Data.directore_MobileNo,
                        Principal_Name = args.Data.principal_Name,
                        Principal_MobileNo = args.Data.principal_MobileNo,
                        Visiting_Status = args.Data.visiting_Status,
                        visitingRemarks = args.Data.visitingRemarks,
                        EmailID = args.Data.emailID,
                        UserID = _sessionModel.UserId,
                        FinancialYear = _sessionModel.FinancialYear,
                        SchoolCode = _sessionModel.SchoolCode,
                        
                    };
                    if (args.Action == "Add")
                    {
                        schoolDetailsModel.Id = 0;
                        schoolDetailsModel.OperationType = "Add";
                    }
                    else
                    {
                        schoolDetailsModel.OperationType = "Update";
                    }
                    aPIReturnmodel = (await aISSchoolDetailsService.AddUpdateSchoolDetails(schoolDetailsModel));
                    if (aPIReturnmodel.status == true)
                    {
                        snackBar.Add(aPIReturnmodel.message, Severity.Success);
                        schoolDetailsModel.School_Name = null;
                        schoolDetailsModel.Address = null;
                        schoolDetailsModel.School_Name = null;
                        schoolDetailsModel.School_Name = null;
                        schoolDetailsModel.School_Name = null;
                        schoolDetailsModel.School_Name = null;
                        schoolDetailsModel.School_Name = null;                       

                    }
                    else
                    {
                        snackBar.Add(aPIReturnmodel.message, Severity.Info);
                        schoolDetailsModel.School_Name = null;
                        schoolDetailsModel.Address = null;
                        schoolDetailsModel.School_Name = null;
                        schoolDetailsModel.School_Name = null;
                        schoolDetailsModel.School_Name = null;
                        schoolDetailsModel.School_Name = null;
                        schoolDetailsModel.School_Name = null;
                        LoadSchoolDetails();
                        Grid.Refresh();
                    }

                }
                if (args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Delete))
                {
                    //LibraryService.DeleteBook(Args.Data.Id);
                }


            }
        }

        public async void LoadSchoolDetails()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            InputModel inputModel = new InputModel()
            {
                FinancialYear = _sessionModel.FinancialYear,
                SchoolCode = _sessionModel.SchoolCode,
                UserId = _sessionModel.UserId,
                fromDate = "",
                toDate = "",
                OperationType = "ALL"
            };
            schoolDetailsListModels = (await aISSchoolDetailsService.GetSchoolDetailsList(inputModel)).ToList();
            // GridData = OrdersDetails.GetAllRecords();
            //LoadEnquiryData();
        }
        public string[] GroupedColumns = new string[] { "location" };
        public void ActionCompleteHandler(ActionEventArgs<SchoolDetailsListModel> Args)
        {
            if (Args.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Save))
            {
                //LibraryBooks = LibraryService.GetBooks(); //to fetch the updated data from db to Grid
                LoadSchoolDetails();
            }
        }
        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            InputModel inputModel = new InputModel()
            {
                FinancialYear = _sessionModel.FinancialYear,
                SchoolCode = _sessionModel.SchoolCode,
                UserId = _sessionModel.UserId,
                fromDate = "",
                toDate = "",
               OperationType="ALL"
            };
            schoolDetailsListModels = (await aISSchoolDetailsService.GetSchoolDetailsList(inputModel)).ToList();
            // GridData = OrdersDetails.GetAllRecords();
            //LoadEnquiryData();

        }
        public void ExportToExcel()
        {
            this.Grid.ExcelExport();
        }
        public SfGrid<SchoolDetailsListModel> Grid;

        public class VisitingStatus
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }

        public List<VisitingStatus> visitingStatus = new List<VisitingStatus>
        {
            new VisitingStatus(){Id=1,Value="Intrested"},
            new VisitingStatus(){Id=2,Value="Not Intrested"},
        };
    }
}
