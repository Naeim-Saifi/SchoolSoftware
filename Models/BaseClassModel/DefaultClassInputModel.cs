using System;
using System.Collections.Generic;
using System.Text;

namespace AIS.Data.BaseClass
{
    public class DefaultClassInputModel
    {
        public string SchoolCode { get; set; }
        public string FinancialYear { get; set; }
        public int? CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public string OperationType { get; set; }
    }
    public class DefaultInputModel
    {
        public string SchoolCode { get; set; }
        public string FinancialYear { get; set; }
        public int? CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public string OperationType { get; set; }
    }
    public class DefaultOutputModel
    {
        public string? createdByUser { get; set; }
        public string? updatedByUser { get; set; }
        public string? createdDate { get; set; }
        public string? updatedDate { get; set; }

    }

    public class DefaultInputParameterModel
    {
        public string SchoolCode { get; set; }
        public string FinancialYear { get; set; }
        public int? Userid { get; set; }
        public string OperationType { get; set; }
    }
}
