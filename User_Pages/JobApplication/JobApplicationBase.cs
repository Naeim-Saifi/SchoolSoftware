using AIS.Data.RequestResponseModel.Enquiry;
using AIS.Data.RequestResponseModel.JobApplication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MudBlazor.Services;
using static AdminDashboard.Server.Pages.JobApplication.JobApplicationBase;
using MudBlazor;
using System.ComponentModel.DataAnnotations;

namespace AdminDashboard.Server.Pages.JobApplication
{
    public class JobApplicationBase : ComponentBase
    {



        public JobApplicationViewModel jobApplicationViewModel = new JobApplicationViewModel();
        public List<AcademicQualificationDetails> academicQualificationDetails = new List<AcademicQualificationDetails>();
        public List<TeachingExperienceDetail> teachingExperienceDetail = new List<TeachingExperienceDetail>();

        [Inject]
        public ISnackbar snackBar { get; set; }



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

        public class CasteCategory
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }

        public List<CasteCategory>  casteCategories = new List<CasteCategory>
        {
            new CasteCategory(){Id=1,Value="General"},
            new CasteCategory(){Id=2,Value="OBC"},
            new CasteCategory(){Id=2,Value="SC"},
            new CasteCategory(){Id=2,Value="ST"}
        };

        public class PositionAppliedFor
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }

        public List<PositionAppliedFor> positionDetails = new List<PositionAppliedFor>
        {
            new PositionAppliedFor(){Id=1,Value="Teacher"},
            new PositionAppliedFor(){Id=2,Value="Clerk"},
        };

        public class TotalTeachingExperience
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }

        public List<TotalTeachingExperience> experienceDetails = new List<TotalTeachingExperience>
        {
            new TotalTeachingExperience(){Id=1,Value="0 to 1 year"},
            new TotalTeachingExperience(){Id=2,Value="1 to 3 years"},
            new TotalTeachingExperience(){Id=2,Value="3 to 5 years"},
            new TotalTeachingExperience(){Id=2,Value="5 to 7 years"},
            new TotalTeachingExperience(){Id=2,Value="7 to 9 years"},
            new TotalTeachingExperience(){Id=2,Value="9 to 11 years"},
            new TotalTeachingExperience(){Id=2,Value="11 to 13 years"},
            new TotalTeachingExperience(){Id=2,Value="13 to 15 years"}
        };

        public class AdministrativeExperienceDetails
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }

        public List<AdministrativeExperienceDetails> administrativeExperienceDetails = new List<AdministrativeExperienceDetails>
        {
            new AdministrativeExperienceDetails(){Id=1,Value="Principal"},
            new AdministrativeExperienceDetails(){Id=2,Value="Vice-Principal"},
            new AdministrativeExperienceDetails(){Id=3,Value="Headmaster/Mistress"},
            new AdministrativeExperienceDetails(){Id=4,Value="Others"},
            new AdministrativeExperienceDetails(){Id=5,Value="None"},
        };

        public class Degree
        {
            public int DegreeID { get; set; }
            public string DegreeType { get; set; }
        }


        public List<Degree> degree = new List<Degree>
        {
        new Degree() { DegreeID=1, DegreeType= "10th" } ,
        new Degree() { DegreeID=2, DegreeType= "Twelfth" } ,
        new Degree() { DegreeID=3, DegreeType= "ITI" } ,
        new Degree() { DegreeID=4, DegreeType= "Diploma" },
        new Degree() { DegreeID=5, DegreeType= "Degree" } ,
        new Degree() { DegreeID=6, DegreeType= "Masters" } ,
        new Degree() { DegreeID=7, DegreeType= "Doctorate" } ,
        };

        public class University
        {
            public int UniversityID { get; set; }
            public string UniversityType { get; set; }
        }


        public List<University> university = new List<University>
        {
        new University() { UniversityID=1, UniversityType= "Private" } ,
        new University() { UniversityID=2, UniversityType= "Government" } ,
        new University() { UniversityID=3, UniversityType= "Deemed" } ,
        new University() { UniversityID=4, UniversityType= "Deemed To " },      
        };

        public class YearOfPassing
        {
            public int YearOfPassingID { get; set; }
            public string YearOfPassingYear { get; set; }
        }


        public List<YearOfPassing> yearOfPassing = new List<YearOfPassing>
        {
        new YearOfPassing() { YearOfPassingID=1, YearOfPassingYear= "2018" },
        new YearOfPassing() { YearOfPassingID=2, YearOfPassingYear= "2019" },
        new YearOfPassing() { YearOfPassingID=3, YearOfPassingYear= "2020" },
        new YearOfPassing() { YearOfPassingID=4, YearOfPassingYear= "2021" },
        new YearOfPassing() { YearOfPassingID=5, YearOfPassingYear= "2022" },
        new YearOfPassing() { YearOfPassingID=6, YearOfPassingYear= "2023" },
        new YearOfPassing() { YearOfPassingID=7, YearOfPassingYear= "2024" },
        new YearOfPassing() { YearOfPassingID=8, YearOfPassingYear= "2025" }
        };

        public class Grade
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }

        public List<Grade> grade = new List<Grade>
        {
            new Grade(){Id=1,Value="A"},
            new Grade(){Id=2,Value="B"},
            new Grade(){Id=3,Value="C"},
            new Grade(){Id=4,Value="D"},
        };


        public class FieldOfStudy
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }

        public List<FieldOfStudy> fieldOfStudy = new List<FieldOfStudy>
        {
            new FieldOfStudy(){Id=1,Value="Engineering"},
            new FieldOfStudy(){Id=2,Value="Medicine"},
            new FieldOfStudy(){Id=3,Value="Arts"},
            new FieldOfStudy(){Id=4,Value="Applied Sciences"},       
        };

        public class Institution
        {
            public int InstitutionID { get; set; }
            public string InstitutionType { get; set; }
        }


        public List<Institution> institution = new List<Institution>
        {
        new Institution() { InstitutionID=1, InstitutionType= "Government" } ,
        new Institution() { InstitutionID=2, InstitutionType= "Private" } ,
        new Institution() { InstitutionID=3, InstitutionType= "Semi-Government" } ,
        };

        public class Position
        {
            public int PositionID { get; set; }
            public string PositionType { get; set; }
        }


        public List<Position> position = new List<Position>
        {
        new Position() { PositionID=1, PositionType= "Teacher" } ,
        new Position() { PositionID=2, PositionType= "Technisian" } ,
        new Position() { PositionID=3, PositionType= "Store-Keeper" } ,
        new Position() { PositionID=4, PositionType= "Peon" } ,
        };


        public class YearsOfExperience
        {
            public int YearsOfExperienceID { get; set; }
            public string YearsOfExperienceType { get; set; }
        }


        public List<YearsOfExperience> yearsOfExperience = new List<YearsOfExperience>
        {
        new YearsOfExperience() { YearsOfExperienceID=1, YearsOfExperienceType= "0 to 1" } ,
        new YearsOfExperience() { YearsOfExperienceID=2, YearsOfExperienceType= "1 to 3" } ,
        new YearsOfExperience() { YearsOfExperienceID=3, YearsOfExperienceType= "3 to 5" } ,
        new YearsOfExperience() { YearsOfExperienceID=5, YearsOfExperienceType= "5 to 7" } ,
        new YearsOfExperience() { YearsOfExperienceID=6, YearsOfExperienceType= "7 to 9" } ,
        new YearsOfExperience() { YearsOfExperienceID=7, YearsOfExperienceType= "more than 9 years" } ,
        };




        //end Payment Model List

        //end Payment Model List


        //public async void OnValidSubmit(EditContext contex)
        //{
        //    bool isValid = contex.Validate();
        //    //if (isValid)
        //    //{
        //    //    enquiryAPIModel = new EnquiryAPIModel()
        //    //    {
        //    //        EnquiryID = Convert.ToInt16(enquiryViewModel.enquiryID),
        //    //        StudentName = enquiryViewModel.studentName,
        //    //        FatherName = enquiryViewModel.fatherName,
        //    //        FMobileNo = enquiryViewModel.fMobileNo,
        //    //        MotherName = enquiryViewModel.motherName,
        //    //        MMobileNo = enquiryViewModel.mMobileNo,
        //    //        GenderID = Convert.ToInt32(enquiryViewModel.Gender),
        //    //        PreviousSchoolName = enquiryViewModel.PreviousSchoolDetails,
        //    //        Dob = Convert.ToDateTime(enquiryViewModel.DOB),
        //    //        VisitingRemrks = enquiryViewModel.VisitingRemarks,
        //    //        VisitingType = Convert.ToInt32(enquiryViewModel.VisitingType),
        //    //        VisitingStatus = Convert.ToInt32(enquiryViewModel.VisitingStatus),
        //    //        EmailID = enquiryViewModel.EmailId,
        //    //        //CurrentClassID = Convert.ToInt32(enquiryViewModel.CurrentClass),
        //    //        PromotedClassID = Convert.ToInt32(enquiryViewModel.NextClass),
        //    //        Address = enquiryViewModel.Address,
        //    //        //operationType = "Add",
        //    //        SchoolCode = _sessionModel.SchoolCode,
        //    //        FinancialYear = _sessionModel.FinancialYear,
        //    //        CreatedByUserId = _sessionModel.UserId

        //    //    };
        //    //    if (OperationType == "Add")
        //    //    {
        //    //        enquiryAPIModel.OperationType = OperationAction.Add;
        //    //    }
        //    //    else if (OperationType == "Update")
        //    //    {
        //    //        enquiryAPIModel.OperationType = OperationAction.Update;
        //    //    }
        //    //    //Delete Operation
        //    //    else
        //    //    {
        //    //        enquiryAPIModel.OperationType = OperationAction.Delete;
        //    //    }

        //    //    SaveEnquiryDetails(enquiryAPIModel);

        //    //}
        //    //else
        //    //{
        //    //    // Form has invalid inputs.
        //    //}

        //}

        public async void OnValidSubmit()
        {
            // Check if all required fields are filled in
            if (ValidateForm(jobApplicationViewModel))
            {
                // All required fields are filled, perform form submission logic
                // For example, you can call an API to submit the form data
                await SaveFormData();
            }
            else
            {
                // Display a message indicating that all required fields need to be filled in
                snackBar.Add("Please fill in all required fields.", Severity.Error);
            }
        }

        // Method to validate the form
        private bool ValidateForm(JobApplicationViewModel model)
        {
            // Use reflection to get all properties of the model
            var properties = model.GetType().GetProperties();

            // Iterate over each property and check if it has the Required attribute
            foreach (var property in properties)
            {
                // Check if the property has the Required attribute
                var requiredAttribute = property.GetCustomAttributes(typeof(RequiredAttribute), true).FirstOrDefault() as RequiredAttribute;
                if (requiredAttribute != null)
                {
                    // Get the value of the property
                    var value = property.GetValue(model);

                    // If the value is null or empty, return false indicating validation failure
                    if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                    {
                        return false;
                    }
                }
            }

            // All required fields are filled, return true indicating validation success
            return true;
        }



        public async Task SaveFormData()
        {
            Console.WriteLine("Data is saved");
        }

        public SfGrid<AcademicQualificationDetails> sfacademicQualificationDetails;
       public SfGrid<TeachingExperienceDetail> sfteachingExperienceDetail;

        public async Task AddQualificationDetails()
        {
            // Check if all required fields are filled in
            if (!string.IsNullOrWhiteSpace(jobApplicationViewModel.Degree) &&
                !string.IsNullOrWhiteSpace(jobApplicationViewModel.University) &&
                !string.IsNullOrWhiteSpace(jobApplicationViewModel.YearOfPassing) &&
                !string.IsNullOrWhiteSpace(jobApplicationViewModel.Grade) &&
                !string.IsNullOrWhiteSpace(jobApplicationViewModel.FieldOfStudy))
            {
                // Add the qualification details
                academicQualificationDetails.Add(new AcademicQualificationDetails()
                {
                    Degree = jobApplicationViewModel.Degree,
                    University = jobApplicationViewModel.University,
                    YearOfPassing = jobApplicationViewModel.YearOfPassing,
                    Grade = jobApplicationViewModel.Grade,
                    FieldOfStudy = jobApplicationViewModel.FieldOfStudy
                });

                // Refresh the UI to reflect the changes
                sfacademicQualificationDetails.Refresh();
                StateHasChanged();

                // Clear the data from jobApplicationViewModel
                jobApplicationViewModel.Degree = "";
                jobApplicationViewModel.University = "";
                jobApplicationViewModel.YearOfPassing = "";
                jobApplicationViewModel.Grade = "";
                jobApplicationViewModel.FieldOfStudy = "";
            }
            else
            {
                // Display a message indicating that all required fields need to be filled in
                snackBar.Add("Please fill in all required fields.", Severity.Error);
            }
        }

        public async Task AddExperienceDetails()
        {
            // Check if all required fields are filled in
            if (!string.IsNullOrWhiteSpace(jobApplicationViewModel.Institution) &&
                !string.IsNullOrWhiteSpace(jobApplicationViewModel.Position) &&
                !string.IsNullOrWhiteSpace(jobApplicationViewModel.YearsOfExperience))
            {
                // Add the teaching experience details
                teachingExperienceDetail.Add(new TeachingExperienceDetail()
                {
                    Institution = jobApplicationViewModel.Institution,
                    Position = jobApplicationViewModel.Position,
                    YearsOfExperience = jobApplicationViewModel.YearsOfExperience,
                });

                // Refresh the UI to reflect the changes
                sfteachingExperienceDetail.Refresh();
                StateHasChanged();

                // Clear the data from jobApplicationViewModel
                jobApplicationViewModel.Institution = "";
                jobApplicationViewModel.Position = "";
                jobApplicationViewModel.YearsOfExperience = "";
            }
            else
            {
                // Display a message indicating that all required fields need to be filled in
                snackBar.Add("Please fill in all required fields.", Severity.Error);
            }

        }

    }
}
