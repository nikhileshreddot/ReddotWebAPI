using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddot_View_Model
{
    public partial class BP_Master_VM
    {
        public string Cardcode { get; set; }
        public string Cardname { get; set; }
        public bool SaveFlag { get; set; }

        public bool EditFlag { get; set; }

        public string ErrorMessage { get; set; }

        public string UserName { get; set; }
    }
    public partial class BP_Master_SAP_VM
    {

        public string? Code { get; set; }
        public string CustomerName { get; set; }
        public string Country { get; set; }
        public string DbName { get; set; }
        public string Email { get; set; }
        public string TelephoneNo { get; set; }
        public string Address { get; set; }
        public string Street { get; set; }
        public string POBOX { get; set; }
        public string City { get; set; }
        public string PIN_TIN_TRN_Number { get; set; }
        public string CertificateofIncorporation { get; set; }
        public string VATNumber { get; set; }
        public string NatureOfBusiness { get; set; }
        public DateTime? QBEDate { get; set; }
        public string CardType { get; set; }

        public Int64 odooid { get; set; }
        public bool SaveFlag { get; set; }

        public bool EditFlag { get; set; }



        public string UserName { get; set; }
    }
    public partial class BP_Master_SAP_VENDOR_VM
    {
        [DefaultValue("3")]
        public string? KYSStatus { get; set; }
        public string? Code { get; set; }
        public string CustomerName { get; set; }
        public string Country { get; set; }
        public string DbName { get; set; }
        public string Email { get; set; }
        public string TelephoneNo { get; set; }
        public string Address { get; set; }
        public string Street { get; set; }
        public string POBOX { get; set; }
        public string City { get; set; }

        [DefaultValue("2")]
        public string? PIN_TIN_TRN_Number { get; set; }
        [DefaultValue("2")]
        public string? CertificateofIncorporation { get; set; }
        [DefaultValue("2")]
        public string? TradeLicense { get; set; }
        [DefaultValue("2")]
        public string? RegularLicense { get; set; }
        [DefaultValue("2")]
        public string? VATNumber { get; set; }
        [DefaultValue("2")]
        public string? Govtid { get; set; }
        [DefaultValue("1")]
        public string ? TypeofOrganisation { get; set; }
        [DefaultValue("1")]
        public string? KYCperiod { get; set; }
        public DateTime? KYCstartdate { get; set; }
        public DateTime? KYCexpirydate { get; set; }
        [DefaultValue("3")]
        public string? KYCexpirystatus { get; set; }
        public string? TradingName { get; set; }

        [DefaultValue("1")]
        public string? Noofdirectores { get; set; }


        /*
         
         TypeofOrganisation 
Company PIN/TIN/TRN Certificate
Company VAT certificate
Company Trade/Business License
Regulatory Licenece
ofiicial/Fovt. Issue idenety card
KYC period
KYS Status
KYC start date
KYC expiry date
KYC expiry status
Trading Name
No of directores

        */
        public string CardType { get; set; }

        public Int64 odooid { get; set; }
       

       

        
    }
}
