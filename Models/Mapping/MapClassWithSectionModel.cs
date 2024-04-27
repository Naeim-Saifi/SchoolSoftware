using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIS.Model.Mapping
{
   public class MapClassWithSectionModel
    {

        public int ClassId { get; set; }

        public int SchoolId { get; set; }

        public string FinancialYear { get; set; }
        public List<int> MasterBatch { get; set; }
        public List<string> BatchName { get; set; }
        public List<int> MasterClassId { get; set; }
        public List<string> ClassName { get; set; }
        public List<int> MasterSectionId { get; set; }
        public List<string> SectionName { get; set; }
        public int? AssessmentMode { get; set; }

        //public DateTime? StartDate { get; set; }

        //public DateTime? EndDate { get; set; }

        public int? Stength { get; set; }

        //public int? ICardGroupId { get; set; }

        //public int? HouseId { get; set; }

        //public int? FeeId { get; set; }

        //public int? DisplayOrder { get; set; }

        public int? ActiveStatus { get; set; }

        public int? CreatedByUserId { get; set; }

        public int? UpdatedByUserId { get; set; }

        //public DateTime? CreatedDate { get; set; }

        //public DateTime? UpdatedDate { get; set; }

        //public string BatchName { get; set; }

        //public string ClassName { get; set; }

        //public string ClassCode { get; set; }

        //public string SectionName { get; set; }
        public string OperationType { get; set; }

    }

    public class MapClassWithSectionList
    {

        public int classId { get; set; }

        public int schoolId { get; set; }
        public int masterBatchId { get; set; }

        public string batchName { get; set; }
        public string masterClassName { get; set; }
        public string sectionName { get; set; }
        public string financialYear { get; set; }

        public int stength { get; set; }
        public int activeStatus { get; set; }
        public string activeStatusDetails { get; set; }
        public int createdByUserId { get; set; }
        public string createdByUser { get; set; }
        public int updatedByUserId { get; set; }

        public string updatedByUser { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime updatedDate { get; set; }

    }
}
