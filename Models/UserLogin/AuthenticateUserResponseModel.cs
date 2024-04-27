using System;
using System.Collections.Generic;
using System.Text;

namespace AIS.Data.UserLogin
{
  public  class AuthenticateUserResponseModel
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
       public string lastName { get; set; }
        public string email { get; set; }
        public int activeStatus { get; set; }
        public string activeStatusDetails { get; set; }
        public string phoneNumber { get; set; }
        public string schoolCode { get; set; }
        public string schoolName { get; set; }
        public int schoolId { get; set; }
        public int classID { get; set; }
        public int sectionID { get; set; }
        public int roleId { get; set; }
        public string userRoleName { get; set; }
        public DateTime latestLoginDate { get; set; }
        
         
    }
}
