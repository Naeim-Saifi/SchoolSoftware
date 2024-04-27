using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Models.FeeConfigurationList
{
    
    public class FeeConfigurationModel
    {
       
        public string className { get; set; }
        public string categoryDetails { get; set; }
        public string headerDetails { get; set; }
        public int amount { get; set; }
        public int feeHeaderEditMode { get; set; }
        public string feeHeaderEditable { get; set; }
        public int monthID { get; set; }
        public string monthDetails { get; set; }

    }
    public class FeeConfigurationListModel
    {
        public List<FeeConfigurationModel> feeConfigurationListModels { get; set; }
    }
}
