using System.ComponentModel.DataAnnotations;

namespace AIS.Model.MasterData
{
    public class MasterBatchModel
    {

        public int? MasterBatchId { get; set; }
       // [Required(ErrorMessage = "School Id is requerid")]
        public int? SchoolId { get; set; }

        [Required(ErrorMessage = "Batch Name is requerid")]
        public string Name { get; set; }
        public string SchoolCode { get; set; }
        public int? DisplayOrder { get; set; }
        public int? ActiveStatus { get; set; }
        public int? CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }

        //[Required(ErrorMessage = "Add or update for operation")]
        public string OperationType { get; set; }
    }


    public class MasterBatchListModel
    {

        public int masterBatchId { get; set; }
        public int schoolId { get; set; }
        public string name { get; set; }
        public int? displayOrder { get; set; }
        public int? activeStatus { get; set; }
        public string activeStatusDetails { get; set; }
        public int? createdByUserId { get; set; }
        public string createdByUser { get; set; }
        public string createdDate { get; set; }
        public int? dpdatedByUserId { get; set; }
        public string dpdatedByUser { get; set; }
        public string updatedDate { get; set; }



    }

    public class MasterBatchViewModel
    {

        public int masterBatchId { get; set; }
        public int schoolId { get; set; }
        public string name { get; set; }
        public int? displayOrder { get; set; }
        public string activeStatusDetails { get; set; }
        public int? createdByUserId { get; set; }
        public string createdByUser { get; set; }
    
        public string updatedByUser { get; set; }
       



    }

}
