using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Models.BaseClassModel
{
    public class BaseClassInputModel
    {
        public int SchoolId { get; set; }
        public string SchoolCode { get; set; }
        public int? ActiveStatus { get; set; }

        public int? CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }

        public string OperationType { get; set; }
    }
}
