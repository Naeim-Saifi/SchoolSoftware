using AdminDashboard.Server.Models.BaseClassModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AIS.Model.MasterUserAccount
{
  public  class UserRegistrationModel_old :BaseClassInputModel
    {

        public int UserId { get; set; } = 0;

        [Required]
        public string UserName { get; set; } = "ll";
        [Required]
        public int UserRoleId { get; set; } = 1;
        [Required]
        public string UserRoleName { get; set; } = "Owner";
        [Required]
        public string FirstName { get; set; } = "ss";

        public string MiddleName { get; set; } = "sss";

        [Required]
        public string LastName { get; set; } = "lll";
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "imransdin@gmail.com";

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "imrn@123";
        public string ProfileImage { get; set; }
    }


    public class UserRegistrationListModel:BaseClassOutputModel
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public int userRoleId { get; set; }
        public string userRoleName { get; set; }
        public string firstName { get; set; }

        public string middleName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string profileImage { get; set; }
    }
}
