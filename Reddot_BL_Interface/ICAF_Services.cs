using Reddot_EF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddot_BL_Interface
{
    public interface ICAF_Services
    {
        Task<List<RDD_CAF_Renewal>> GetCAF_LIST(string DbName, Int64? pagesize, Int32? pageno, DateTime s_date, DateTime e_date, string? sortcoloumn, string? sortorder, string username);
        Task<DataSet> GetKYC(string TransType, long? pagesize, int? pageno, string username);
    }
}
