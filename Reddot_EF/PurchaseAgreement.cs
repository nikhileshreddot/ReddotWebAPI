using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebPages;

namespace Reddot_EF
{
    public partial class PurchaseAgreement
    {

      public string BpCode { get; set; }
public string? BpName { get; set; }
public DateTime StartDate { get; set; }
public DateTime EndDate { get; set; }   
public DateTime ? TermDate { get; set; }
public string Descript {  get; set; }

public string Status {  get; set; } 
 
 


        public string  DBname { get; set; }
        public List<PurchaseAgreementDetails> PurchaseAgreementDetails { get; set; }
    }

    public partial class PurchaseAgreementDetails
    {
       
public Int64 AgrLineNum { get; set; }   
public string ItemCode { get; set; }    
public string ItemName { get; set; }    
//public int ItemGroup { get; set; }   
public int PlanQty { get; set; }    
public string FreeTxt { get; set; }
public string? U_RebateAE { get; set; }  
public string? U_RebateKE { get; set; }
public string? U_RebateUG { get; set; }
public string? U_RebateTZ { get; set; }
public string? U_RebateZM { get; set; }
public string? U_RebateTRI { get; set; }
public string? U_RebateML { get; set; }
public string? U_RebateBT { get; set; }
public string? U_RebateMAU { get; set; }
public string? U_RebateSA {  get; set; }
public string? U_RebateLLC { get; set; }
public string UomCode { get; set; }
        

    }
}
