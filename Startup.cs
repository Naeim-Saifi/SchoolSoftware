using AdminDashboard.Server.API_Service.Interface.Dashboard;
using AdminDashboard.Server.API_Service.Interface.MasterDataSetUp;
using AdminDashboard.Server.API_Service.Interface.StudentSetUp;
using AdminDashboard.Server.API_Service.Interface.Syllabus;
using AdminDashboard.Server.API_Service.Service.Dashboard;
using AdminDashboard.Server.API_Service.Service.MasterDataSetUp;
using AdminDashboard.Server.API_Service.Service.StudentSetUp;
using AdminDashboard.Server.API_Service.Service.Syllabus;
using AdminDashboard.Server.API_Service.Service.HomeWork;
using AdminDashboard.Server.API_Service.Interface.HomeWork;
using AdminDashboard.Server.API_Service.Interface.TransactionSetUp;
using AdminDashboard.Server.API_Service.Service.TransactionSetUp;
using AdminDashboard.Server.API_Service.Interface.Employee;


using AdminDashboard.Server.Service.Implementation.AdminUserRegistration;
using AdminDashboard.Server.Service.Implementation.AISSchoolDetails;
using AdminDashboard.Server.Service.Implementation.Dashboard.Management;
using AdminDashboard.Server.Service.Implementation.Dashboard.Student;
using AdminDashboard.Server.Service.Implementation.Employee;
//using AdminDashboard.Server.Service.Implementation.Enquiry;
using AdminDashboard.Server.Service.Implementation.ExamSchedule;
using AdminDashboard.Server.Service.Implementation.FeeConfiguration;
using AdminDashboard.Server.Service.Implementation.Holiday;
using AdminDashboard.Server.Service.Implementation.HomeWork;
using AdminDashboard.Server.Service.Implementation.LocalStorage;
using AdminDashboard.Server.Service.Implementation.MasterConfiguration.SubjectMaster;
using AdminDashboard.Server.Service.Implementation.MasterConfiguration.Syllabus;
using AdminDashboard.Server.Service.Implementation.MasterData;
using AdminDashboard.Server.Service.Implementation.PaymentgatewaySetup;
using AdminDashboard.Server.Service.Implementation.PendingFee;
using AdminDashboard.Server.Service.Implementation.Razorpay;
using AdminDashboard.Server.Service.Implementation.SyllabusSetup;
using AdminDashboard.Server.Service.Implementation.TimeTable;
using AdminDashboard.Server.Service.Implementation.Transaction;
using AdminDashboard.Server.Service.Interface.AdminUserRegistration;
using AdminDashboard.Server.Service.Interface.AISSchoolDetails;
using AdminDashboard.Server.Service.Interface.Dashboard.Management;
using AdminDashboard.Server.Service.Interface.Dashboard.Student;
using AdminDashboard.Server.Service.Interface.Employee;
//using AdminDashboard.Server.Service.Interface.Enquiry;
using AdminDashboard.Server.Service.Interface.ExamSchedule;
using AdminDashboard.Server.Service.Interface.FeeConfiguration;
using AdminDashboard.Server.Service.Interface.Holiday;
using AdminDashboard.Server.Service.Interface.HomeWork;
using AdminDashboard.Server.Service.Interface.LocalStorage;
using AdminDashboard.Server.Service.Interface.MasterConfiguration.SubjectMaster;
using AdminDashboard.Server.Service.Interface.MasterConfiguration.Syllabus;
using AdminDashboard.Server.Service.Interface.MasterData;
using AdminDashboard.Server.Service.Interface.PaymentDetail;
using AdminDashboard.Server.Service.Interface.PendingFee;
using AdminDashboard.Server.Service.Interface.Razorpay;
using AdminDashboard.Server.Service.Interface.SyllabusSetup;
using AdminDashboard.Server.Service.Interface.TimeTable;
using AdminDashboard.Server.Service.Interface.Transaction;
using AdminDashboard.Server.Service.Interface.UserRegistration;
using AIS.FrontEnd_07062021.Service.Implementation;
using AIS.FrontEnd_07062021.Service.Interface;
using Blazored.SessionStorage;
using Create_Word_document.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor;
using MudBlazor.Services;
using Syncfusion.Blazor;
using System;
using AdminDashboard.Server.API_Service.Interface.TimeTableSetUp;
using AdminDashboard.Server.API_Service.Service.TimeTableSetUp;
using AdminDashboard.Server.API_Service.Interface.HolidaySetUp;
using AdminDashboard.Server.API_Service.Service.HolidaySetUp;
using AdminDashboard.Server.API_Service.Interface.Exam;
using AdminDashboard.Server.API_Service.Service.Exam;
using AdminDashboard.Server.API_Service.Service.Employee;
using static System.Net.WebRequestMethods;
using AdminDashboard.Server.API_Service.Interface.FlowUpSchool;
using AdminDashboard.Server.API_Service.Service.FlowUpSchool;
using AIS.Data.RequestResponseModel.TimeTableSetUp;
using AdminDashboard.Server.API_Service.Interface.QuestionSetup;
using AdminDashboard.Server.API_Service.Service.QuestionSetup;
using AdminDashboard.Server.API_Service.Interface.RemarksTypeMaster;
using AdminDashboard.Server.API_Service.Service.RemarksTypeMaster;
using AdminDashboard.Server.API_Service.Interface.Attendance;
using AdminDashboard.Server.API_Service.Service.Attendance;
using AdminDashboard.Server.API_Service.Interface.SchoolDetails;
using AdminDashboard.Server.API_Service.Service.SchoolDetails;
using AdminDashboard.Server.API_Service.Interface.Expense;
using AdminDashboard.Server.API_Service.Service.Expense;
using AdminDashboard.Server.API_Service.Interface.Enquiry;
using AdminDashboard.Server.API_Service.Service.Enquiry;

namespace AdminDashboard.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddMudServices();
            services.AddProtectedBrowserStorage();
            services.AddBlazoredSessionStorage();
            services.AddSyncfusionBlazor();
            services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopCenter;
                config.SnackbarConfiguration.PreventDuplicates = false;
                config.SnackbarConfiguration.NewestOnTop = false;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 10000;
                config.SnackbarConfiguration.HideTransitionDuration = 500;
                config.SnackbarConfiguration.ShowTransitionDuration = 500;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
            });
            string baseAddress = "http://103.165.79.137:8003";
            //string baseAddress = "http://10.10.11.137:8003";

            //string baseAddress = Configuration.GetValue<string>("API");
            //string baseAddress = Configuration.GetValue<string>("API:APIUrl");
            //string baseAddress = "http://localhost:57736/";

            services.AddScoped<ILocalStorageService, LocalStorageService>();
            services.AddHttpClient<IMasterBatchService, MasterBatchService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
               
            });

            services.AddHttpClient<IMasterClassService, MasterClassService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<IMasterSectionService, MasterSectionService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<IMasterDepartmentService, MasterDepartmentService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<IMasterDocumentService, MasterDocumentService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<IMasterEmployeeService, MasterEmployeeService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });

            services.AddHttpClient<IMasterOccupationService, MasterOccupationService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<IMasterSubjectService, MasterSubjectService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });

            services.AddHttpClient<ISchoolSetupService, SchoolSetupService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<IUserAccountService, UserAccountService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<IMapClassWithSubjectService, MapClassWithSubjectService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<IMapClassWithSectionService, MapClassWithSectionService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<IMapTeacherWithSubjectService, MapTeacherWithSubjectService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<IStudentListService, StudentListService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<IMasterUnitService, MasterUnitService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<IMasterChapterService, MasterChapterService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<IMasterTopicService, MasterTopicService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<IClassMasterService, ClassMasterService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<IRazorpaygetwayService, RazorpaygetwayService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<IPaymentGatewayDetailService, PaymentGatewayDetailService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<IUserRegistrationService, UserRegistrationService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<IAdminUserRegistrationService, AdminUserRegistrationService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<IStudentDashboardService, StudentDashboardService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<IManagementDashboardService, ManagementDashboardService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<ITransactionService, TransactionService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<ISubjectMasterService, SubjectMasterService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<IUnitMasterService, UnitMasterService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<IEmployeeService, EmployeeService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<IPendingFeeService, PendingFeeService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<IHomeWorkService, HomeWorkService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<IFeeConfigurationListService, FeeConfigurationListService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<ITimeTableService, TimeTableService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<IHolidayService, HolidayService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<ISyllabusSetupService, SyllabusSetupService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<IExamScheduleService, ExamScheduleService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            
            services.AddHttpClient<IAISSchoolDetailsService,  AISSchoolDetailsService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<IMasterDataService, MasterDataService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });

            //New Setup 
            services.AddHttpClient<IDashboardService, DashboardService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<IStudentSetupService, StudentSetupService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });

            services.AddHttpClient<ISyllabusService, SyllabusService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<IMasterDataSetupService, MasterDataSetupService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<IHomeWorkSetupService, HomeWorkSetupService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<ITransactionSetUpService, TransactionSetupService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<ITimeTableSetUpService, TimeTableSetupService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<IHolidaySetUpService, HolidaySetupService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });

            services.AddHttpClient<IExamMasterSetupService, ExamMasterSetupService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });

            services.AddHttpClient<IEmployeeSetupService, EmployeeSetupService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<IHomeWorkSetupService, HomeWorkSetupService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });

            services.AddHttpClient<IFlowUpSchoolService, FlowUpSchoolService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });

            services.AddHttpClient<IQuestionsetup, QuestionSetupService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });

            services.AddHttpClient<IRemarksTypeMasterService, RemarksTypeMasterService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<IAttendanceService, AttendanceService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });

            services.AddHttpClient<ISchoolDetailsService, SchoolDetailsService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });

            services.AddHttpClient<IExpenseService, ExpenseService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            services.AddHttpClient<IEnquiryService,  EnquiryService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });

            services.AddSingleton<WordService>();
            services.AddSingleton<ExcelFormatTimeTable>();
            services.AddSingleton<WordFormatQuestionPaper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense
                ("NTU0MzM2QDMxMzkyZTM0MmUzME9wM2s1WHNINzFUeWlDNFEreVU3bUk1TExjTE1OY3ViZy9lZzBpRkNvNWM9");
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense
                ("MTI5ODQwMEAzMjMwMmUzNDJlMzBVbFVjUm05OUk5Nnlpa3lyYTFwVFp5MGJ0dWdPYTd6MjJSZDVJVzEybytnPQ ==");
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense
               ("Ngo9BigBOggjHTQxAR8/V1NHaF5cWWdCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdgWH5fcnRWRmJYV0JxW0A=");

            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
