using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AIS.Model.MasterData
{
  public  class MasterDepartmentModel
    {
        public int MasterDepartmentId { get; set; }
        [Required(ErrorMessage = "School Id requird")]
        public int SchoolId { get; set; }

        [Required(ErrorMessage = "School Id requird")]
        public string Name { get; set; }

        public int? DisplayOrder { get; set; }

        public int? ActiveStatus { get; set; }

        public int? CreatedByUserId { get; set; }

        public int? UpdatedByUserId { get; set; }

        public string OperationType { get; set; }
    }

    public class MasterDepartmentListModel
    {
        public int masterDepartmentId { get; set; }
        public int schoolId { get; set; }
        public string name { get; set; }
       
        public int? displayOrder { get; set; }
        public int? activeStatus { get; set; }
        public string activeStatusDetails { get; set; }
        public int? createdByUserId { get; set; }
        public string createdByUser { get; set; }
        public int? updatedByUserId { get; set; }
        public string updatedByUser { get; set; }
        public string createdDate { get; set; }
        public string updatedDate { get; set; }
    }

    public class DepartmentMasterViewModel
    {
        public int MasterDepartmentId { get; set; } = 0;
       
        [Required(ErrorMessage = "Department Name is requird")]
        public string DepartmentName { get; set; }

        public string? DisplayOrder { get; set; }

       
    }
}
