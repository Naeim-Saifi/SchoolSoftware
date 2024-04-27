using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Models.BaseClassModel
{
    public class BaseClassOutputModel
    {
        public int id { get; set; } = 0;
        public int? activeStatus { get; set; }
        public string activeStatusDetails { get; set; }
        public int? createdByUserId { get; set; }
        public string createdByUser { get; set; }
        public int? updatedByUserId { get; set; }
        public string updatedByUser { get; set; }
        public string createdDate { get; set; }
        public string updatedDate { get; set; }
    }
}
