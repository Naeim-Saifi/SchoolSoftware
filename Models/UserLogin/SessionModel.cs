using System;
using System.Collections.Generic;
using System.Text;

namespace AIS.Model.UserLogin
{
  public  class SessionModel
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
        public string FinancialYear { get; set; }
        public string SchoolName { get; set; }
        public int SchoolId { get; set; }
        public int classID { get; set; }
        public int sectionID { get; set; }
        public int RoleId { get; set; }
        public string UserRoleName { get; set; }
        public string SessionName { get; set; }
        public DateTime LatestLoginDate { get; set; }
    }
}
