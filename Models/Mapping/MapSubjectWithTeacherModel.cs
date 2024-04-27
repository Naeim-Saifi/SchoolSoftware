using System;
using System.Collections.Generic;
using System.Text;

namespace AIS.Model.Mapping
{
  public  class MapSubjectWithTeacherModel
    {
        public int SubjectTeacherMappingId { get; set; }
        public string SchoolCode { get; set; }
        public string FinancialYear { get; set; }
        //public List<int> MasterSubjectId { get; set; }
        //public List<string> SubjectName { get; set; }
        public int? MasterSubjectId { get; set; }
        public string SubjectName { get; set; }
        public int? MasterClassId { get; set; }
        public string ClassName { get; set; }
        public int? MasterSectionId { get; set; }
        public string SectionName { get; set; }
        public int? TeacherUserId { get; set; }
        public string TeacherName { get; set; }
        public int? ActiveStatus { get; set; }
        public int? CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public string OperationType { get; set; }
    }


    public class MapSubjectWithTeacherList
    {
        public int subjectTeacherMappingId { get; set; }
        public int teacherUserId { get; set; }
        public string teacherName { get; set; }
        public int masterClassId { get; set; }
        public string classname { get; set; }
        public int masterSectionId { get; set; }
        public string sectionName { get; set; }
        public int? masterSubjectId { get; set; }
        public string subjectName { get; set; }
        public int activeStatus { get; set; }
        public string activeStatusDetails { get; set; }
        public int? createdByUserId { get; set; }
        public string createdByUser { get; set; }
        public string createdDate { get; set; }
        public int? updatedByUserId { get; set; }
        public string updatedDate { get; set; }
    }
}
