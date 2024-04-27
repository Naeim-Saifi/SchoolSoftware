using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS.Model.GeneralConversion
{
   public static class GenericClass
    {
        public  enum Status { Active = 1, Suspend = 2, ReActive = 3, Delete = 4, Update=5 }
       
        public static List<string> StatusDetails = new List<string>()
        { "Active", "Suspend" , "ReActive" , "Delete","Update" };

        

        public static List<string> AffilatedTo = new List<string>()
        { "CBSE Board", "Haryana Board" , "Bihar Board" , "UP Board","Pre Primary","Non Affilated","Bachpan Gropu","KID Zee" };

        public static List<string> OrganationType = new List<string>()
        { "School", "Institute" , "Other"  };

        public static List<string> Title = new List<string>()
        { "Mr.", "Ms." , "Mrs." ,"Miss" };

        public static List<string> Gender = new List<string>()
        { "Male", "Female" , "Other"  };

        public static List<string> MaritalStatus = new List<string>()
        { "Single", "Married" , "Widowed","Divorced" ,"Separated" };

        public static List<string> ReligionList = new List<string>()
        { "Hinduism", "Islam" , "Christianity","Sikhism" ,"Other" };

        public static List<string> CastCategory = new List<string>()
        { "General", "OBC" , "SC","ST" ,"Other" };

        public static List<string> BloodGroup = new List<string>()
        { "A+", "A-" , "B+","B-" ,"O+" ,"O-","AB+","AB-"};

        public static List<string> UIDTypeList = new List<string>()
        { "Passport", "PAN Card" , "Voter ID","Driving License" ,"Aaddhar Card","Other"};

        public static List<string> StateList = new List<string>()
        { "Bihar", "Haryana" , "Jharkhand" ,"Rajasthan","Delhi" };

        public static List<string> UserRole = new List<string>()
        { "Owner", "Admin" , "Management" ,"Principal","Student","Teacher" };
        public static int StatusConversion(string status)
        {
            int num = 0;
            if (status.ToLower() == "active")
                num = 1;
            else if (status.ToLower() == "suspend")
                num = 2;
            else if (status.ToLower() == "reActive")
                num = 3;
            else if (status.ToLower() == "delete")
                num = 4;
            else if (status.ToLower() == "update")
                num = 5;
            return num;
        }

        public static int UserRoleConversion(string status)
        {
            int RoleId = 0;
             if (status.ToLower() == "owner")
                RoleId = 1;
            else if (status.ToLower() == "admin")
                RoleId = 2;
            else if (status.ToLower() == "management")
                RoleId = 3;
            else if (status.ToLower() == "principal")
                RoleId = 4;
            else if (status.ToLower() == "student")
                RoleId = 5;
            else if (status.ToLower() == "teacher")
                RoleId = 6;
            return RoleId;
        }

        public static readonly string Add = " Add";
        public static readonly string Update = " Update";
        public static readonly string Delete = " Delete";
        public static readonly string Suspend = " Suspend";
        public static readonly string ReActive = " Re-Active";
    }

    

    public static class GenderDetails
    {
       
    }
    public static class OperationAction
    {
        public static readonly string Add = "Add";
        public static readonly string Update = "Update";
        public static readonly string Delete = "Delete";
        public static readonly string Suspend = "Suspend";
        public static readonly string ReActive = "Re-Active";
    }

    public static class ReportType
    {
        public static readonly string All = "All";
        public static readonly string OnlySubjectName = "OnlySubjectName";
        public static readonly string GetByMasterId = "GetByMasterId";
        public static readonly string GetBySchoolId = "GetBySchoolId";
        public static readonly string GetBySchoolCode = "SchoolCode";
        public static readonly string GetByStudentUserID = "GetByStudentUserID";
        public static readonly string AllStudentDetails = "AllStudentDetails";
        public static readonly string GetTeacherClass = "GetTeacherClass";
        public static readonly string GetTeacherSection = "GetTeacherSection";
        public static readonly string SubjectTeacherList = "SubjectTeacherList";
        public static readonly string GetClassAndSectionList = "GetClassAndSectionList";
        public static readonly string StudentTimeTable = "StudentTimeTable";
        public static readonly string TeacherTimeTable = "TeacherTimeTable";
        public static readonly string MonthWise = "MonthWise";
        public static readonly string ClassWise = "ClassWise";
        public static readonly string paymentMode = "PaymentMode";
        public static readonly string MonthWiseMode = "MonthWiseMode";
        public static readonly string BusStopWise = "BusStopWise";

        public static readonly string UserWiseFeeCollection = "GetUserWiseFeeCollection";
        public static readonly string PaymentModeFeeCollection = "GetPaymentModeFeeCollection";
        public static readonly string DateWiseFeeCollection = "GetDateWiseFeeCollection";
        
    }
    
    public static class AttendanceReportType
    {
        public static readonly string TodayAttendanceList = "DateWiseAttendanceList_Count";
        public static readonly string AttendanceList_UserWise = "StudentAttendanceList_UserIDWise";
        public static readonly string StudentAttendanceList_MonthWise = "StudentAttendanceList_MonthWise";
    }
    public static class ExpenseReportType
    {
        public static readonly string PaymentAccountType = "PaymentAccountType";
        public static readonly string AttendanceList_UserWise = "StudentAttendanceList_UserIDWise";
        public static readonly string StudentAttendanceList_MonthWise = "StudentAttendanceList_MonthWise";
    }

}
