using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Models.Holiday
{
        public class HolidayDetailsModel
        {
        public int HolidayDayId { get; set; }
        public string SchoolCode { get; set; }
        public string FinancialYear { get; set; }
        public int MonthNameId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime Todate { get; set; }
        public int HolidayTypeId { get; set; }
        public int CreatedByUserId { get; set; }
        public string OperationType { get; set; }

    }
        public class HolidayDetailsListModel
        {

            public int holidayDayId { get; set; }
            public int monthNameId { get; set; }
            public string monthNameDetails { get; set; }
            public DateTime fromDate { get; set; }
            public DateTime todate { get; set; }
            public int holidayTypeId { get; set; }
            public string holidayType { get; set; }
            public int numberofDays { get; set; }
            public string holidayDescriptionHindi { get; set; }
            public string holidayDescriptionEnglish { get; set; }
            public string activeStatus { get; set; }
            public int createdByUserId { get; set; }
            public string createdDate { get; set; }
            public string updatedDate { get; set; }
            public string updatedByUserId { get; set; }



        }

        public class HolidayTypeMasterModel
        {
            public int HolidayTypeId { get; set; }
            public string SchoolCode { get; set; }
            public string FinancialYear { get; set; }
            public string HolidayType { get; set; }
            public string HolidayDescriptionHindi { get; set; }
            public string HolidayDescriptionEnglish { get; set; }
            public int ActiveStatus { get; set; }
            public int CreatedByUserId { get; set; }
            public int UpdatedByUserId { get; set; }
            public string OperationType { get; set; }

        }

        public class HolidayTypeListMaster
        {
            public int holidayTypeId { get; set; }
            public string holidayType { get; set; }
            public string holidayDescriptionHindi { get; set; }
            public string holidayDescriptionEnglish { get; set; }
            public int activeStatus { get; set; }
            public int createdByUserId { get; set; }
            public string createdDate { get; set; }
            public string updatedDate { get; set; }
            public int updatedByUserId { get; set; }
           

        }
    
        public class MonthNameList
        {
        public string MonthId { get; set; }
        public string MonthName { get; set; }
        }
}
