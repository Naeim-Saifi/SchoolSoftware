using AdminDashboard.Server.Models.BaseClassModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Models.UserRegistraion
{
    public class AdminUserRegistrationModel
    {

        public int UserId { get; set; } 

        [Required]
        public string UserName { get; set; }
        [Required]
        public int UserRoleId { get; set; }
        public string UserRoleName { get; set; }
        [Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } 
        public Stream ProfileImage { get; set; }
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }

        //BaseInputModel Property

        public int SchoolId { get; set; }
        public string SchoolCode { get; set; }
        public int? ActiveStatus { get; set; }

        public int? CreatedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }

        public string OperationType { get; set; }

    } 
    
    public class AdminUserRegistrationListModel:BaseClassOutputModel
    {

        public string loginUserId { get; set; }
        public int userRoleId { get; set; }
        public string userRoleName { get; set; }
        public string firstName { get; set; }

        public string middleName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string profileImage { get; set; }

        

    }
}
