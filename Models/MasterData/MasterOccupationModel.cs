using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AIS.Model.MasterData
{
    public class MasterOccupationModel
    {
        public int MasterOccupationId { get; set; }
       
        public int SchoolId { get; set; }

        [Required(ErrorMessage = "Name requird")]
        public string Name { get; set; }

        public int? DisplayOrder { get; set; }

        public int? ActiveStatus { get; set; }

        public int? CreatedByUserId { get; set; }

        public int? UpdatedByUserId { get; set; }
       
        public string OperationType { get; set; }

        
    }

    public class MasterOccupationListModel
    {
        public int masterOccupationId { get; set; }
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


    public class OccupationMasterViewModel
    {
        public int MasterOccupationId { get; set; }= 0;

        [Required(ErrorMessage = "Occupation is requird")]
        public string OccupationName { get; set; }

        public string? DisplayOrder { get; set; }

       

    }
}
