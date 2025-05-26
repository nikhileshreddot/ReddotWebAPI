using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Reddot_EF
{
    public partial class RDD_PV_V1
    {

        public int id { get; set; }
        public string Erormsg { get; set; }       
        public int PVId { get; set; }
        [StringLength(30, ErrorMessage = "Country cannot be longer than 30 characters.")]
        public string Country { get; set; }
   
        public string RefNo { get; set; }
       
        public string VType { get; set; }
    
        [StringLength(30, ErrorMessage = "DBName cannot be longer than 30 characters.")]
        public string DBName { get; set; }

    
        [StringLength(30, ErrorMessage = "Currency cannot be longer than 30 characters.")]
        public string Currency { get; set; }
      
        public string VendorCode { get; set; }
        [StringLength(500, ErrorMessage = "Employee cannot be longer than 500 characters.")]
        public string VendorEmployee { get; set; }
        [StringLength(500, ErrorMessage = "Benificiary cannot be longer than 500 characters.")]
        public string Benificiary { get; set; }
        [Range(0, 999999999.99, ErrorMessage = "Request Amt cannot be longer than 9 characters.")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal RequestedAmt { get; set; }
        [Range(0, 999999999.99, ErrorMessage = "Approve Amt  cannot be longer than 9 characters.")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal ApprovedAmt { get; set; }
        [StringLength(500, ErrorMessage = "Being Pay cannot be longer than 500 characters.")]
        public string BeingPayOf { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? PayRequestDate { get; set; }
        public string BankCode { get; set; }
        [StringLength(500, ErrorMessage = "Bank Name cannot be longer than 500 characters.")]
        public string BankName { get; set; }
        [StringLength(30, ErrorMessage = "Pay Method cannot be longer than 500 characters.")]
        public string PayMethod { get; set; }
    
        public string PayRefNo { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? PayDate { get; set; }
           
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
      
        public List<RDD_PVLines> RDD_PVLinesDetails { get; set; }

  
        public string Purpose { get; set; }


    }
    public class RDD_PVLines
    {
               
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? Date { get; set; }
        public string Description { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal Amount { get; set; }
        public string Remarks { get; set; }          
    }
}
