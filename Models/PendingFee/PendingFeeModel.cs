using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Models.PendingFee
{
    public class StudentPendingFeeModel
    {
        public List<AllStudentPendingFee> allStudentPendingFeesModels { get; set; }
        public List<MonthWisePendingList> monthWisePendingsModels { get; set; }
        public List<ClassAndSectionPendingList> classAndSectionPendingListsModels { get; set; }
        public List<ClassWisePending> classWisePendingModels { get; set; }
        public List<StudentWisePendingFee> studentWisePendingFeeModels { get; set; }

    }

    public class AllStudentPendingFee
    {
        public int userId { get; set; }
        public string className { get; set; }
        public string sectionName { get; set; }
        public string admissionNumber { get; set; }
        public string classSection { get; set; }
        public string studentName { get; set; }
        public string fatherFullName { get; set; }
        public string fatherContactNumber { get; set; }
        public string monthName { get; set; }
        public string feeCategory { get; set; }
        public string busStopName { get; set; }
        public int totalDue { get; set; }
        public int currentPaymentStatus { get; set; }
        public string paymentStatus { get; set; }
        public string listGeneratedDate { get; set; }
        public string lastDueDate { get; set; }
        public string monthOfPending { get; set; }
    }
    public class MonthWisePendingList
    {

        public string monthName { get; set; }
        public int noOfStudent { get; set; }
        public int pendingAmount { get; set; }
        public string pendingGeneratedMonth { get; set; }



    }
    public class ClassAndSectionPendingList
    {
        public int classId { get; set; }
        public string className { get; set; }
        public string sectionName { get; set; }
        public int studentCount { get; set; }
        public long pendingAmont { get; set; }

    }
    public class ClassWisePending
    {
        public int classid { get; set; }
        public string className { get; set; }
        public int studentCount { get; set; }
        public long pendingAmont { get; set; }

    }
    public class StudentWisePendingFee
    {
        public string monthName { get; set; }
        public long pendingAmount { get; set; }

    }
}
