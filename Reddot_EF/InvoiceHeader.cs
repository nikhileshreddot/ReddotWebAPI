using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Reddot_EF
{
    public partial class InvoiceHeader
    {

        public int GroupNum { get; set; }
        public string paymnetgroup { get; set; }
        public string PayTerm { get; set; }
        public Int64 TotalCount { get; set; }
        public Int64 RowNum { get; set; }
        public int slpcode { get; set; }
        public string Source { get; set; }
        public string Country { get; set; }
        public string region { get; set; }
        public bool Transfer { get; set; }
        public string Account { get; set; }
        public string Name { get; set; }
        public string BIZ_SEG { get; set; }
        public string Reference { get; set; }
        public string InvoiceNo { get; set; }
        public string BizType { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string ProjectCode { get; set; }

        public DateTime ? Renewdt { get; set; }
        public string Renew { get; set; }
        public string RenewFrq { get; set; }
        public DateTime TxDate { get; set; }
        public string LPONumber { get; set; }

        public string DOCcur { get; set; }
        public Double Totalvalue { get; set; }

       // public Invoicedetails Invoicedetails { get; set; }
        public List<Invoicedetails> InvoicedetailsList { get; set; }

    }

    //Reference	cSimpleCode	Description_1	BU	BUGroup	WarehouseID	Cost	TavgCostUSD	useCost	TQuantity	TamtUSD	GPUSD	GPUSDActual	opgRebate1USD	Rebate	opgSelloutID
    public partial class Invoicedetails
    {
        public string cSimpleCode { get; set; }

        public string Description_1 { get; set; }
       
        public string WarehouseID { get; set; }
      

       
        public Double Vatpercent { get; set; }

        public Double TQuantity { get; set; }
        public Double TamtUSD { get; set; }
        public Double GPUSD { get; set; }
        public string VATGroup { get; set; }
        public Double opgRebate1USD { get; set; }
        public Double Rebate { get; set; }
        public string opgSelloutID { get; set; }

        //GPUSDActual	opgRebate1USD	Rebate	opgSelloutID

    }
}

