using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AIS.Model.Academic
{
  public  class MasterSubjectModel
    {
        public int MasterSubjectId { get; set; }
        public int SchoolId { get; set; }
        [Required(ErrorMessage = "Subject Name required")]
        public string SubjectName { get; set; }
        public string SubjectCode { get; set; }
        public int? MarksType { get; set; }
        public string MarksTypeDescription { get; set; }
        public int? SubjectType { get; set; }
        public string SubjectTypeDescription { get; set; }
        public int? DisplayOrder { get; set; }
        public int? ActiveStatus { get; set; }
        public int? CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }

       
        public string OperationType { get; set; }
    }

    public class MasterSubjectListModel
    {
        public int masterSubjectId { get; set; }
        public int schoolId { get; set; }
        public string subjectName { get; set; }
        public string subjectCode { get; set; }
        public int? marksType { get; set; }
        public string marksTypeDescription { get; set; }
        public int? subjectType { get; set; }
        public string subjectTypeDescription { get; set; }
        public int? displayOrder { get; set; }
        public int? activeStatus { get; set; }
        public string activeStatusDetails { get; set; }
        public int? createdByUserId { get; set; }
        public string createdByUser { get; set; }
        public string createdDate { get; set; }
        public int? updatedByUserId { get; set; }
        public string updatedDate { get; set; }

    }
}
