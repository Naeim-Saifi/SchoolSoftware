using System;
using System.Collections.Generic;
using System.Text;

namespace AIS.Model.Student
{
    public class StudentListModel
    {
      
        public int? schoolId { get; set; }
        public int? classId { get; set; }
        public string? className { get; set; }
        public string? sectionName { get; set; }

        public string? login_UserName { get; set; }

        public string? latestLoginDate { get; set; }

        public string? admissionNumber { get; set; }

        public string? studentName { get; set; }

        public string? fatherFullName { get; set; }

        public string? motherFullName { get; set; }

        public string? fatherContactNumber { get; set; }

        public string? rollNumber { get; set; }
        public DateTime dOB { get; set; }
        public string? genderStatusDetails { get; set; }
        public string? religionDetails { get; set; }
        public string? castCategoryDetails { get; set; }

        public string? bloodGroupDetails { get; set; }

        public string? uIDNumber { get; set; }
        public string? uIDTypeDetails { get; set; }
        public string? feeCategory { get; set; }
        public string? busstopname { get; set; }
        public string? sRNNumber { get; set; }

        public string? withdrawalNumber { get; set; }

        public string? activeStatusDetails { get; set; }
        public string? createdByUser { get; set; }       
        public DateTime createdDate { get; set; }



    }

    
    public class StudentDetailsListModel
    {
        public List<StudentListModel> studentListModels { get; set; }
    }

    public  class StudentRemarksListModel
    {
        public int? StudentID { get; set; }
        public string? StudentName { get; set; }
        public string? FatherName { get; set; }
        public int? RollNo { get; set; }
        public int? RemarksDescription { get; set; }
    }
}
