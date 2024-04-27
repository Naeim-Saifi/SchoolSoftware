using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Models.Dashboard.Management
{
    public class ClassWisePendingFeeModel
    {
        public string className { get; set; }
        public long studentCount { get; set; }
        public long pendingAmont { get; set; }
    }
}
