using System;
using System.ComponentModel.DataAnnotations;

namespace AIS.Model.SchoolSetup
{
    public class SchoolModel
    {
        public int SchoolId { get; set; }


        [Required(ErrorMessage = "School code requird")]
        [StringLength(10)]
        public string SchoolCode { get; set; }

        [Required(ErrorMessage = "School Name requird")]
        public string SchoolName { get; set; }


        public string SchoolEmail { get; set; }
        public string RegistrationNumber { get; set; }

        public string Tagline { get; set; }
        public string Description { get; set; }

        public string Logo { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public int? StateId { get; set; } //Master

        public int? CountryId { get; set; } //Master

        public string PinCode { get; set; }

        public string OwnerName { get; set; }

        [StringLength(10)]
        public string OwnerContactNo { get; set; }

        [EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address.")]
        public string OwnerEmail { get; set; }

        public int? Gender { get; set; } 

        public DateTime? DOB { get; set; } 

        public int? UIDType { get; set; }

        public string UIDNumber { get; set; }

        public string BankName { get; set; }

        public string IFSCCode { get; set; }

        public string AccountNo { get; set; }

        public string BankAddress { get; set; }

        public string PaymentLink { get; set; }

        public string AdditionalInfo { get; set; }

        public int? Rank { get; set; }

        public string OrganizationType { get; set; }
        public string AffilatedTo { get; set; }
        public string Tahsheel { get; set; }
        public string SMSUserID { get; set; }
        public string SMSPassword { get; set; }
        public string SMSAPIKey { get; set; }
        public string SMSSenderID { get; set; }
        public string FromMailId { get; set; }
        public string MailUserId { get; set; }
        public string MailPassword { get; set; }

        public int? ActiveStatus { get; set; }

        public int? CreatedByUserId { get; set; }

        public int? UpdatedByUserId { get; set; }

        // [Required(ErrorMessage = "either Add/Update is required")]

        //[Required(ErrorMessage = "Add or update for operation")]
        public string OperationType { get; set; }
        

    }

    public class SchoolListModel
    {
        public int schoolId { get; set; } = 0;
        public string schoolCode { get; set; }
        public string schoolName { get; set; }
        public string schoolEmail { get; set; }
        public string registrationNumber { get; set; }
        public string tagline { get; set; }
        public string description { get; set; }
        public string logo { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public int? stateId { get; set; }//Master
        public string stateName { get; set; }
        public int? countryId { get; set; } //Master
        public string country { get; set; } //Master
        public string pinCode { get; set; }

        public string ownerName { get; set; }       
        public string ownerContactNo { get; set; }       
        public string ownerEmail { get; set; }
        public string genderDetails { get; set; }
        public int? gender { get; set; }
        public string dob { get; set; }
        public int? uidType { get; set; }
        public string uidTypeDescription { get; set; }
        public string uidNumber { get; set; }        
        public string bankName { get; set; }
        public string ifscCode { get; set; }
        public string accountNo { get; set; }
        public string bankAddress { get; set; }
        public string paymentLink { get; set; }
        public string additionalInfo { get; set; }
        public int? rank { get; set; }
        public string organizationType { get; set; }
        public string affilatedTo { get; set; }
        public string tahsheel { get; set; }
        public string smsUserID { get; set; }
        public string smsPassword { get; set; }
        public string smsapiKey { get; set; }
        public string smsSenderID { get; set; }
        public string fromMailId { get; set; }
        public string mailUserId { get; set; }
        public string mailPassword { get; set; }
        public string activeStatusDetails { get; set; }
        public int? activeStatus { get; set; }
        public int? createdByUserId { get; set; }
        public string createdByUser { get; set; }
        public int? updatedByUserId { get; set; }
        public string updatedByUser { get; set; }
        public string updatedDate { get; set; }
        public string createdDate { get; set; }


    }
}
