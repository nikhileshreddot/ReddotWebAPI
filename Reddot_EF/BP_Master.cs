using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Reddot_EF
{
    public partial class BP_Master
    {
        public string CardCode { get; set; }
        public string CardName { get; set; }       
    }
    public partial class BP_Master_SAP
    {         
        public string CustomerName { get; set;}    
        public string Country { get; set;}
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
    }
}
