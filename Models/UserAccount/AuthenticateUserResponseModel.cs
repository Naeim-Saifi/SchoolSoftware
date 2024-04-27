using System;
using System.Collections.Generic;
using System.Text;

namespace AIS.Model.UserAccount
{
  public  class AuthenticateUserResponseModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
       public string LastName { get; set; }
        public string Email { get; set; }
        public int ActiveStatus { get; set; }
        public string ActiveStatusDetails { get; set; }
        public string PhoneNumber { get; set; }
        public string SchoolCode { get; set; }
        public int SchoolName { get; set; }
        public int RoleId { get; set; }
        public string UserRoleName { get; set; }
        public string LatestLoginDate { get; set; }
        
         
    }
}
