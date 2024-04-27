using System;
using System.Collections.Generic;
using System.Text;

namespace AIS.Model.Academic
{
  public  class MasterUnitModel
    {
        public int SubjectUnitId { get; set; }
        public int SchoolId { get; set; }
        public int MasterClassId { get; set; }       
        public int MasterSubjectId { get; set; }       
        public string UnitTitle { get; set; }
        public string UnitDescription { get; set; }
        public int? ActiveStatus { get; set; }
        public int? CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public string OperationType { get; set; }
    }
    public class MasterUnitListModel
    {
        public int schoolId { get; set; }
        public int masterClassId { get; set; }
        public string className { get; set; }
        public int masterSubjectId { get; set; }
        public string subjectName { get; set; }
        public int subjectUnitId { get; set; }
        public string unitTitle { get; set; }
        public string unitDescription { get; set; }
        public int? activeStatus { get; set; }
        public string activeStatusDetails { get; set; }
        public int? createdByUserId { get; set; }
        public string createdByUser { get; set; }
        public string createdDate { get; set; }
        public int? updatedByUserId { get; set; }
        public string updatedDate { get; set; }
    }
}
