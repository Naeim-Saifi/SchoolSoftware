using System;
using System.Collections.Generic;
using System.Text;

namespace AIS.Model.Academic
{
    public class MasterTopicModel
    {
        public int SubjectTopicId { get; set; }
        public int SchoolId { get; set; }
        public int SubjectChapterId { get; set; }
        public string TopicTitle { get; set; }
        public string TopicDescription { get; set; }
        public int? ActiveStatus { get; set; }
        public int? CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public string OperationType { get; set; }
        
    }
    public class MasterTopicList
    {
        public int schoolId { get; set; }
        public int masterClassId { get; set; }
        public string className { get; set; }
        public string masterSubjectId { get; set; }
        public string subjectName { get; set; }
        public int subjectUnitId { get; set; }
        public string unitTitle { get; set; }
        public string unitDescription { get; set; }
        public int subjectChapterId { get; set; }
        public string chapterTitle { get; set; }
        public string chapterDescription { get; set; }
        public int subjectTopicId { get; set; }
        public string topicTitle { get; set; }
        public string topicDescription { get; set; }
        public int? activeStatus { get; set; }
        public int? createdByUserId { get; set; }
        public int? updatedByUserId { get; set; }
        public string operationType { get; set; }

    }
}
