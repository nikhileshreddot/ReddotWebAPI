using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddot_EF
{
    public partial class RDD_PO
    {
        //public string ProductType { get; set; }
        //public string CustomerType { get; set; }
        //public string BZ_SEG { get; set; }
        public string OurREf { get; set; }
        public Int64 docentry { get; set; }
        public Int64 RowNum { get; set; }
        public string bZ_SEG { get; set; }
        public string? DocStatus { get; set; }
        public Int64 TotalCount { get; set; }
        //public bool EditFlag { get; set; }
        //public string Errormsg { get; set; }
        public Int32 PO_ID { get; set; }
        //public Int32 Doc_Object { get; set; }
        //public Int32 Base_Obj { get; set; }
        //public Int64 Base_ID { get; set; }

       // public string DBCode { get; set; }
        public string DBName { get; set; }


        public DateTime? PostingDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        //public string DocStatus { get; set; }
        //public string AprovedBy { get; set; }
        public string CreatedBy { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string RefNo { get; set; }
        public string RDD_Project { get; set; }
        public string BusinesType { get; set; }
        public string ActualOdoono { get; set; }
        public string shipto { get; set; }
        public string InvPayTerms { get; set; }
        public string CustPayTerms { get; set; }
        //public string Forwarder { get; set; }
        public string SalesEmp { get; set; }

        //public string SlpName { get; set; }
        //public string Pay_Method_1 { get; set; }
        //public string Rcpt_check_No_1 { get; set; }
        //public DateTime Rcpt_check_Date_1 { get; set; }
        //public string Remarks_1 { get; set; }
        //public string Curr_1 { get; set; }
        //public decimal Amount_1 { get; set; }
        //public string Pay_Method_2 { get; set; }
        //public string Rcpt_check_No_2 { get; set; }
        //public DateTime Rcpt_check_Date_2 { get; set; }
        //public string Remarks_2 { get; set; }
        //public string Curr_2 { get; set; }
        //public decimal Amount_2 { get; set; }
        //public decimal Total_Bef_Tax { get; set; }
        //public decimal Total_Tx { get; set; }
        public decimal DocTotal { get; set; }
        //public decimal GP { get; set; }
        //public decimal GP_Per { get; set; }
        public string Remarks { get; set; }
        //public string Validate_Status { get; set; }
        //public string Post_SAP { get; set; }
        //public DateTime CreatedOn { get; set; }
        //public DateTime LastUpdatedOn { get; set; }
        //public string LastUpdatedBy { get; set; }
        //public char IsDraft { get; set; }
        //public string ObjType { get; set; }
        //public Int64 SAP_DocEntry { get; set; }
        //public string SAP_DocNum { get; set; }
        //public string IsWithholdingTaxApplicable { get; set; }
        //public string IsWithholdingVatApplicable { get; set; }
        //public string LPO_PI_Filepath { get; set; }
        //public Int32? APNumber { get; set; }
        //public string IsRedeemRequestOrder { get; set; }
        public string DocCur { get; set; }
        //public RDD_PO1 RDD_PO1 { get; set; }
        public string? Source { get; set; }
        public List<RDD_PO1> RDD_PO1List { get; set; }

        //public RDD_PO2 RDD_PO2 { get; set; }

        public List<RDD_PO2> RDD_PO2List { get; set; }



        //public string InternalId { get; set; }

        //public string UserRole { get; set; }

        /// <summary>
        /// public string BeSpokeProjectCode { get; set; }
        /// </summary>
        //public string IsSoftware { get; set; }
        public string enduser { get; set; }

        //public string WHVATCODE { get; set; }
        //public double WHVATPER { get; set; }
        //public double WHVATAMT { get; set; }

        //public string WHTAXCODE { get; set; }
        //public double WHTAXPER { get; set; }
        //public double WHTAXAMT { get; set; }

        //public string? ApprovalCode { get; set; }
    }
    public partial class RDD_PO1
    {

        
        //public Int64 PO_LineId { get; set; }
        //public Int32 PO_ID { get; set; }
        //public Int32 Base_Obj { get; set; }
        //public Int32 Base_Id { get; set; }
        //public Int32 Base_LinId { get; set; }
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscPer { get; set; }
        public decimal LineTotal { get; set; }
        public string TaxCode { get; set; }
        public string WhsCode { get; set; }
        //public decimal QtyInWhs { get; set; }
        //public decimal QtyAval { get; set; }
        //public string OpgRefAlpha { get; set; }
        //public decimal GP { get; set; }
        //public decimal GPPer { get; set; }
        //public decimal TaxRate { get; set; }
        public int lineNum { get; set; }
       // public string Currency { get; set; }
    }
    public partial class RDD_PO2 {
        //paymethod,refno, refdate,memo,currency, amount

        public string paymethod { get; set; }
        public string Chequeno { get; set; }

        public DateTime Paydate { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
       
        public string Remark { get; set; }
        
    }

}
