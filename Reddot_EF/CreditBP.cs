using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddot_EF
{
    public partial class CreditBP
    {
        public string DBname { get; set; }
        public string code { get; set; }
        public string? Payterm { get; set; }
        public string? Priority { get; set; }
        public decimal? CreditAmount { get; set; }
        public DateTime? CrdExpDate{ get; set; }
        public DateTime? CrdupdateDate { get; set; }

        public int? Clextesionmonth { get; set; }
        public decimal? TempCreditAmt { get; set; }
        public DateTime? TempCrdExpDate{ get; set; }
        public string? Tstatus { get; set; }
        public string? TstatusRemark { get; set; }
        public string? TempRemark { get; set; }
        public string? Clstatus { get; set; }
        public string? ClstatusRemark { get; set; }
        public string  CreditType { get; set; }
        public string Username { get; set; }


    }
}
