using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AIS.Model.MasterData
{
  public  class MasterSectionModel
    {
        public int MasterSectionId { get; set; }
       
        public int SchoolId { get; set; }
        public string SchoolCode { get; set; }

        [Required(ErrorMessage = "School Name requird")]
        public string Name { get; set; }
        [Required(ErrorMessage = "School Code requird")]
        public string Code { get; set; }
        public int? DisplayOrder { get; set; }
        public int? ActiveStatus { get; set; }
        public int? CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
        public string OperationType { get; set; }
    }

    public class MasterSectionListModel
    {
        public int masterSectionId { get; set; }
        public int schoolId { get; set; }
        public string name { get; set; }
        public string code { get; set; }
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

    public class SectionMasterViewModel
    {
        public int MasterSectionId { get; set; } = 0;
        [Required(ErrorMessage = "Section Name requird")]
        public string SectionName { get; set; }
        [Required(ErrorMessage = "Section Code requird")]
        public string SectionCode { get; set; }
        public string? DisplayOrder { get; set; }
        
    }
}
