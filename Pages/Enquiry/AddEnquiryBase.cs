using AdminDashboard.Server.Models.Enquiry;
using AdminDashboard.Server.Service.Implementation.Enquiry;
using AdminDashboard.Server.Service.Interface.Enquiry;
using AIS.FrontEnd_07062021.Service.Implementation;
using AIS.FrontEnd_07062021.Service.Interface;
using AIS.Model;
using AIS.Model.MasterData;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.Enquiry
{
    public class AddEnquiryBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;
        [Inject]
        public IMasterClassService masterClassService { get; set; }
        public List<MasterClassListModel> masterClassListModels = new List<MasterClassListModel>();


        public enquiryViewModel enquiryViewModel = new enquiryViewModel();
        [Inject]
        public IEnquiryService enquiryService { get; set; }
        public APIReturnModel aPIReturnmodel;
        [Inject]
        public ISnackbar snackBar { get; set; }
        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            masterClassListModels = (await masterClassService.GetMasterClassList(_sessionModel.SchoolCode, _sessionModel.FinancialYear, _sessionModel.SchoolId, 0, 1, "GetBySchoolId")).ToList();

        }
        public class Gender
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }

        public List<Gender> genderDetails = new List<Gender>
        {
            new Gender(){Id=1,Value="Male"},
            new Gender(){Id=2,Value="Female"},
        };
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

        public class VisitingType
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }

        public List<VisitingType> visitingTypes = new List<VisitingType>
        {
            new VisitingType(){Id=1,Value="Self Visit"},
            new VisitingType(){Id=2,Value="Visit by School"},
            new VisitingType(){Id=2,Value="Student refrence"},
        };
        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                EnquiryModel enquiryModel = new EnquiryModel()
                {
                    enquiryID = Convert.ToInt32(enquiryViewModel.enquiryID),
                    studentName = enquiryViewModel.studentName,
                    fatherName = enquiryViewModel.fatherName,
                    fMobileNo = enquiryViewModel.fMobileNo,
                    motherName = enquiryViewModel.motherName,
                    mMobileNo = enquiryViewModel.mMobileNo,
                    genderID = Convert.ToInt32(enquiryViewModel.Gender),
                    previousSchoolName = enquiryViewModel.PreviousSchoolDetails,
                    dob = enquiryViewModel.DOB.ToString(),
                    visitingRemrks = enquiryViewModel.VisitingRemarks,
                    visitingType = Convert.ToInt32(enquiryViewModel.VisitingType),
                    visitingStatus = Convert.ToInt32(enquiryViewModel.VisitingStatus),
                    emailID = enquiryViewModel.EmailId,
                    currentClassID = Convert.ToInt32(enquiryViewModel.CurrentClass),
                    promotedClassID = Convert.ToInt32(enquiryViewModel.NextClass),
                    address = enquiryViewModel.Address,
                    operationType = "Add",
                    schoolCode = _sessionModel.SchoolCode,
                    financialYear = _sessionModel.FinancialYear,
                    createdByUserId = _sessionModel.UserId

                };

                SaveEnquiryDetails(enquiryModel);

            }
            else
            {
                // Form has invalid inputs.
            }

        }
        public async void SaveEnquiryDetails(EnquiryModel enquiryModel)
        {
            aPIReturnmodel = (await enquiryService.AddUpdateEnquiry(enquiryModel));
            if (aPIReturnmodel.status == true)
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Success);
                ClearEnquiryDetails();
            }
            else
            {
                snackBar.Add(aPIReturnmodel.message, Severity.Info);
                ClearEnquiryDetails();
            }
        }

        public void ClearEnquiryDetails()
        {
            enquiryViewModel = null;
        }
    }
}
