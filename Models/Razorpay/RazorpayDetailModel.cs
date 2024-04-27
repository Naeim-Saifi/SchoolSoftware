using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AIS.Model.Razorpay
{
    public class RazorpayDetailModel
    {
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Please Enter Valid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone is Required")]
        [Phone(ErrorMessage = "Please Enter Valid Mobile No.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Address is Required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Amount is Required")]
        public int DueAmount { get; set; }
        //public int Amount { get; set; } = 101;
        //public string OrderId { get; set; }
        //public string RazorpayKey { get; set; }
        //public string Currency { get; set; }

        //public string Description { get; set; }
        public string studentId { get; set; } = "2";
        public string LoginId { get; set; } = "GURU001_Admin";
        public string schoolCode { get; set; } = "GURU001";
        public string SchoolId { get; set; } = "2";
    }

    public class MerchantOrder
    {
        public string OrderId { get; set; }
        public string RazorpayKey { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
    }
}
