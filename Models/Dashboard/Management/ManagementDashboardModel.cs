using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Models.Dashboard.Management
{
    public class ManagementDashboardModel
    {
        public long netFeeCollection { get; set; } = 0;
        public long netPendingFee { get; set; } = 0;
        public long netTotalStudent { get; set; } = 0;
        public List<TransactionTypeDetailsModel> transactionTypeDetails { get; set; }
        public List<ClassWisePendingFeeModel> classWisePendingFees { get; set; }
        public List<ClassWiseStudentCountModel> classWiseStudentCounts { get; set; }
        public List<ClassandSectionWiseStudentCountModel> classandSectionWiseStudentCounts { get; set; }
        public List<GenderwiseStudentCountModel> genderwiseStudentCounts { get; set; }
    }
}
