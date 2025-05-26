using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Reddot_EF
{
    public partial class JournalHeader
    {
        public Int64 TotalCount { get; set; }
        public Int64 RowNum { get; set; }
        public string Source { get; set; }
        public string TransId { get; set; }
        public string BaseRef { get; set; }
        public string Memo { get; set; }
        public string ref1 { get; set; }
        public Double LocTotal { get; set; }

        public Double SysTotal { get; set; }
        public DateTime? TaxDate { get; set; }

        public string Year { get; set; }
        public string Month { get; set; }
        public string Project { get; set; }

        public string TransactionType { get; set; }
        public string TransactionName { get; set; }
        public List<Journaldetails> JournaldetailsList { get; set; }
    }
    public partial class Journaldetails
    {

      
      
        public string Line_ID { get; set; }
        public string Account { get; set; }
        public Double Debit { get; set; }
        public Double Credit { get; set; }
        public Double SYSCred { get; set; }
        public Double SYSDeb { get; set; }        
        public string AcctName { get; set; }
        public string ContraAct { get; set; }
        public string LineMemo { get; set; }
        public string Project { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string? BU { get; set; }
        public string? BUGroup { get; set; }
        public string? CountryMapped { get; set; }

    }
}
