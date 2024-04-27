using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Models.Dashboard.Student
{
    public class PendingFeeDetailsModel
    {
        public string monthName { get; set; }
        public int? totalDue { get; set; }
    }
}
