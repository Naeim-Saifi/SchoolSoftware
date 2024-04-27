using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AIS.Model.UserLogin
{
 public   class UserLogin
    {
        [Required]
        //public string UserId { get; set; } = "SCRPUBLIC_M004";
        public string UserId { get; set; } =  "GVALLEY_Sysadmin";
        [Required]
        public string UserPassword { get; set; }= "GVALLEY@Sysadmin";
        [Required]
        public string FinancialYear { get; set; } = "2021-23";
        //public string UserPassword { get; set; } = "SCRPUBLIC@M004";

    }
}
