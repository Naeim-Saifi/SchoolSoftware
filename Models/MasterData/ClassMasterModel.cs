using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIS.Model.MasterData
{
    public class ClassMasterModel
    {
        public int ClassId { get; set; } = 0;
        public int SchoolId { get; set; }
        public string SchoolCode { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int? Stength { get; set; }
        public int? DisplayOrder { get; set; }
        public int? ActiveStatus { get; set; }
        public string ActiveStatusDetails { get; set; }
        public int? CreatedByUserId { get; set; }
        public string CreatedByUser { get; set; }
        public int? UpdatedByUserId { get; set; }
        public string UpdatedByUser { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string OperationType { get; set; }
    }

    public class ClassMasterListModel
    {
        public int classId { get; set; }
        public int schoolId { get; set; }
        public string schoolCode { get; set; }
        public string className { get; set; }
        public string sectionName { get; set; }
        public int? stength { get; set; }
        public int? displayOrder { get; set; }
        public int? activeStatus { get; set; }
        public string activeStatusDetails { get; set; }
        public int? createdByUserId { get; set; }
        public string createdByUser { get; set; }
        public int? updatedByUserId { get; set; }
        public string updatedByUser { get; set; }
        public string createdDate { get; set; }
        public string updatedDate { get; set; }
        public string OperationType { get; set; }
    }
    public class ClassMasterViewModel
    {
        public int ClassId { get; set; } = 0;         
        public string ClassName { get; set; }
        public int? Stength { get; set; }
        public int? DisplayOrder { get; set; }
       
    }
}
