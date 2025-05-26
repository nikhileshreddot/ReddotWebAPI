using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddot_View_Model
{
    public partial class RDD_PO_VM
   
    {
       // public string? ProductType { get; set; }
        //public string? CustomerType { get; set; }
       // public string BZ_SEG { get; set; }
        public string? OurREf { get; set; }

        public Int32 SO_ID { get; set; }

        public string DBName { get; set; }
        public DateTime? PostingDate { get; set; }
        public DateTime DeliveryDate { get; set; }

        public string CreatedBy { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string RefNo { get; set; }
        public string RDD_Project { get; set; }
        public string BusinesType { get; set; }
        public string InvPayTerms { get; set; }
        public string CustPayTerms { get; set; }
        //public string Forwarder { get; set; }
        public string SalesEmp { get; set; }


      //  public string? APNumber { get; set; }
     //   public string IsRedeemRequestOrder { get; set; }
        public string DocCur { get; set; }

       // public string? ApprovalCode { get; set; }
        public List<RDD_PO1> RDD_PO1List { get; set; }
        //public string? WHVATCODE { get; set; }
        //public double? WHVATPER { get; set; }
        //public double? WHVATAMT { get; set; }

        //public string? WHTAXCODE { get; set; }
        //public double? WHTAXPER { get; set; }
        //public double? WHTAXAMT { get; set; }
        public List<RDD_PO2> RDD_PO2List { get; set; }
        public string Remarks { get; set; }
       // public string? BeSpokeProjectCode { get; set; }
        public string? enduser { get; set; }
    }


    public partial class RDD_PO1
    {



        public string ItemCode { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscPer { get; set; }
        public decimal LineTotal { get; set; }
        public string TaxCode { get; set; }
        public string WhsCode { get; set; }
        public int lineNum { get; set; }
      //  public string? OpgRefAlpha { get; set; }
        //public decimal GP { get; set; }
        //public decimal GPPer { get; set; }

    }

    public partial class RDD_PO2
    {
        public string Pay_Method { get; set; }
        public string? Rcpt_Check_No { get; set; }
        public DateTime Rcpt_Check_Date { get; set; }
        public string Curr_Id { get; set; }

        public decimal Allocated_Amt { get; set; }

        public string Remark { get; set; }


    }


}
