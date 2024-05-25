using AdminDashboard.Server.API_Service.Interface.MasterDataSetUp;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.MasterDataSetUp;
using AIS.Data.RequestResponseModel.QuestionSetUp;
using AIS.Data.RequestResponseModel.Registration.UserRegistration;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AIS.Data.GeneralConversion.GeneralConversion;

namespace AdminDashboard.Server.User_Pages.Registration.UserRegistration
{
    public class UserRegistrationBase : ComponentBase
    {

        public SfDialog DialogRef;
        public bool IsVisible { get; set; } = false;

        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        public IMasterDataSetupService masterDataSetupService { get; set; }



        public UserRegistrationViewMdel userRegistrationViewMdel = new UserRegistrationViewMdel();

        public List<UserRegistrationOutputModel> _UserRegistrationOutputModel = new List<UserRegistrationOutputModel>();

        //API Model

        public SfGrid<UserRegistrationOutputModel> sfRegistrationOutputModel;



        public List<string> _ToolBarSubjectList = (new List<string>() { "Save", "Print", "Search" });


        //public AnimationEffect ExpandEffect = AnimationEffect.SlideDown;
        //public AnimationEffect CollapseEffect = AnimationEffect.SlideUp;

        [Inject]
        public ISnackbar snackBar { get; set; }
        public SessionModel sessionModel { get; }

        public bool ddEnable = true;


        public void onOpen(Syncfusion.Blazor.Popups.BeforeOpenEventArgs args)
        {
            // setting maximum height to the Dialog
            args.MaxHeight = "750px";

        }
        public string DialogHeaderName = "Question Bank";// string.Empty;
        public SessionModel _sessionModel;


        public DialogEffect dialogAnimationEffect = DialogEffect.Zoom;













        public class StdClass
        {
            public int Id;
            public string Value;
        }

        public List<StdClass> ClassOptions = new List<StdClass>()
        {
     new StdClass{ Id = 1,Value="Class1"},
          new StdClass{ Id = 2,Value="Class2"},
               new StdClass{ Id = 3,Value="Class3"}



        };
        public class StdSection
        {
            public int Id;
            public string Value;
        }

        public List<StdSection> SectionOptions = new List<StdSection>()
        {
     new StdSection{ Id = 1,Value="A"},
          new StdSection{ Id = 2,Value="B"},
               new StdSection{ Id = 3,Value="C"}



        };

        public class Gender
        {
            public int Id;
            public string Value;
                
        }
        public List<Gender> GenderDetails = new List<Gender>()
        {
            new Gender{Id=1,Value="Male"},
            new Gender{Id=2,Value="Female"},
            new Gender{Id=3,Value="Not Available"},
        };


        public class Category
        {
            public int Id;
            public string Value;

        }
        public List<Category> CategoryDetails = new List<Category>()
        {
            new Category{Id=1,Value="General"},
            new Category{Id=2,Value="OBC"},
            new Category{Id=3,Value="OBC-||"},

                    new Category{Id=4,Value="BC-A"},
            new Category{Id=5,Value="BC-B"},
            new Category{Id=6,Value="SC"},
                       new Category{Id=7,Value="St"},
            new Category{Id=8,Value="Not Available"},
            new Category{Id=9,Value="Other"},
        };


        public class Religion
        {
            public int Id;
            public string Value;

        }
        public List<Religion> ReligionDetails = new List<Religion>()
        {
            new Religion{Id=1,Value="Muslim"},
            new Religion{Id=2,Value="Hindu"},
            new Religion{Id=3,Value="Chirstian"},

                    new Religion{Id=4,Value="Jain"},
            new Religion{Id=5,Value="Sikh"},
            new Religion{Id=6,Value="Not Available"},

        };




        public class StdType
        {
            public int Id;
            public string Value;
        }

        public List<StdType> StdTypeDetails = new List<StdType>()
        {
     new StdType{ Id = 1,Value="Day Boarding"},
          new StdType{ Id = 2,Value="Boarding"},
               new StdType{ Id = 3,Value="Day Scholar"}



        };
        public class FeeCategory
        {
            public int Id;
            public string Value;
        }

        public List<FeeCategory> FeeCategoryDetails = new List<FeeCategory>()
        {
     new FeeCategory{ Id = 1,Value="Full Free"},
          new FeeCategory{ Id = 2,Value="Half Free"},
          



        };


        public class TranportMode
        {
            public int Id;
            public string Value;
        }

        public List<TranportMode> TranportModeDetails = new List<TranportMode>()
        {
     new TranportMode{ Id = 1,Value="Own"},
          new TranportMode{ Id = 2,Value="School"},




        };



        public class SelectBusStop
        {
            public int Id;
            public string Value;
        }

        public List<SelectBusStop> SelectBusStopDetails = new List<SelectBusStop>()
        {
     new SelectBusStop{ Id = 1,Value="Nehru"},
          new SelectBusStop{ Id = 2,Value="Okhla"},




        };


        public class BloodGroup
        {
            public int Id;
            public string Value;

        }
        public List<BloodGroup> BloodGroupDetails = new List<BloodGroup>()
        {
            new BloodGroup{Id=1,Value="A+"},
            new BloodGroup{Id=2,Value="B+"},
            new BloodGroup{Id=3,Value="AB+"},

                    new BloodGroup{Id=4,Value="O+"},
            new BloodGroup{Id=5,Value="A-"},
            new BloodGroup{Id=6,Value="B-"},

                            new BloodGroup{Id=4,Value="AB-"},
            new BloodGroup{Id=5,Value="O-"},
            new BloodGroup{Id=6,Value="Not Available" },

        };

        public class State
        {
            public int Id;
            public string Value;
        }

        public List<State> StateDetails = new List<State>()
        {
     new State{ Id = 1,Value="Dehli"},
          new State{ Id = 2,Value="Haryana"},




        };


        public class City
        {
            public int Id;
            public string Value;
        }

        public List<City> CityDetails = new List<City>()
        {
     new City{ Id = 1,Value="HAIZ KHAS"},
          new City{ Id = 2,Value="GUROGAON"},




        };




        public class AdmissionMode
        {
            public int Id;
            public string Value;
        }
        public List<AdmissionMode> AdmissionModelDetails = new List<AdmissionMode>()
        {
            new AdmissionMode{Id=1,Value="New Admission" },
            new AdmissionMode{Id=2,Value="Cancel Admission"},
            new AdmissionMode{Id=3,Value="Update Admission"},
            new AdmissionMode{Id=4,Value="Reactive Admission"},
            new AdmissionMode{Id=5,Value="Suspend Admission"},
            new AdmissionMode{Id=6,Value="TC Issue"}

        };



        public List<Master_CLass_List_Output_Model> _classList = new List<Master_CLass_List_Output_Model>();


        public List<Master_Map_Subject_With_Class_List_Output_Model> _subjectList = new List<Master_Map_Subject_With_Class_List_Output_Model>();


        int _classID = 0;
        public async Task OnChangeClass(ChangeEventArgs<string, Master_CLass_List_Output_Model> args)
        {
            try
            {
                if (args.ItemData.classId != 0)
                {
                    _classID = args.ItemData.classId;

                    Master_Map_Subject_With_ClassList_Input_Para_Model mapsubjectwithClass = new Master_Map_Subject_With_ClassList_Input_Para_Model()
                    {
                        classID = _classID,
                        schoolCode = _sessionModel.SchoolCode,
                        financialYear = _sessionModel.FinancialYear,
                        reportType = AIS.Data.GeneralConversion.GeneralConversion.ReportType.ClassId,
                        userId = _sessionModel.UserId
                    };

                    _subjectList = (await masterDataSetupService.GET_Mapp_SubjectwithClassLIST(mapsubjectwithClass)).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



        public List<Master_Section_List_Output_Model> _sectionList = new List<Master_Section_List_Output_Model>();

        int _sectionID = 0;
        public async Task OnChangeSection(ChangeEventArgs<string, Master_Section_List_Output_Model> args)
        {
            try
            {
                if (args.ItemData.sectionId != 0)
                {
                    _sectionID = args.ItemData.sectionId;


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            //Master_CLass_List_Input_Para_Model master_CLass_List_Input_Para_Model = new Master_CLass_List_Input_Para_Model()
            //{
            //    classId = 0,
            //    userId = _sessionModel.UserId,
            //    financialYear = _sessionModel.FinancialYear,
            //    schoolCode = _sessionModel.SchoolCode,
            //    reportType = ReportType.All
            //};
            //master_CLass_List = (await masterDataSetupService.GET_Master_ClassLIST(master_CLass_List_Input_Para_Model)).ToList();


            Master_CLass_List_Input_Para_Model master_CLass_List_Input_Para_Model = new Master_CLass_List_Input_Para_Model()
            {
                classId = 0,
                userId = _sessionModel.UserId,
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = AIS.Data.GeneralConversion.GeneralConversion.ReportType.All
            };
            _classList = (await masterDataSetupService.GET_Master_ClassLIST(master_CLass_List_Input_Para_Model)).ToList();



            Master_Section_List_Input_Para_Model master_section_List_Input_Para_Model = new Master_Section_List_Input_Para_Model()
            {
                sectionId = 0,
                userId = _sessionModel.UserId,
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = AIS.Data.GeneralConversion.GeneralConversion.ReportType.All
            };
            _sectionList = (await masterDataSetupService.GET_Master_SectionLIST(master_section_List_Input_Para_Model)).ToList();


            Master_Map_Subject_With_ClassList_Input_Para_Model master_Map_Subject_With_ClassList_Input_Para_Model = new Master_Map_Subject_With_ClassList_Input_Para_Model()
            {
                classID = 0,
                userId = _sessionModel.UserId,
                subjectID = 0,
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,

                reportType = ReportType.All
            };


            //  _UserRegistrationOutputModel = (await masterDataSetupService.GET_Mapp_SubjectwithClassLIST(master_Map_Subject_With_ClassList_Input_Para_Model)).ToList();

        }


        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };



        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
   













            }
            else
            {
                // Form has invalid inputs.
            }

        }
        public APIReturnModel aPIReturnModel;


 






        public async Task CloseDialog()
        {
            IsVisible = false;
            await this.DialogRef.HideAsync();
        }





    }
}