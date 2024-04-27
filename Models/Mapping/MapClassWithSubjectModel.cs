using System;
using System.Collections.Generic;
using System.Text;

namespace AIS.Model.Mapping
{
   public class MapClassWithSubjectModel
    {
        public int SubjectId { get; set; }

        public int SchoolId { get; set; }

        public List<int> MasterSubjectId { get; set; }
        public List<string> MasterSubjectName { get; set; }
        public List<int> MasterClassId { get; set; }
        public List<string> MasterClassName  {get; set; }
       
        public int? ActiveStatus { get; set; }

        public int? CreatedByUserId { get; set; }

        public int? UpdatedByUserId { get; set; }

        public string OperationType { get; set; }
        

    }

    public class MapClassWithSubjectList
    {
        public int subjectId { get; set; }
        public int schoolId { get; set; }
        public int masterSubjectId { get; set; }
        public string masterSubjectName { get; set; }
        public int masterClassId { get; set; }
        public string masterClassName { get; set; }
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

