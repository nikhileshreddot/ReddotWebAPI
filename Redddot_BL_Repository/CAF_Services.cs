using Reddot_BL_Interface;
using Reddot_DL_Interface;
using Reddot_DL_Repository;
using Reddot_EF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Redddot_BL_Services
{
    public class CAF_Services : ICAF_Services
    {

        ICAF_Repository _CAFRepo;

        public CAF_Services()
        {
            _CAFRepo = new CAF_Repository();
        }
        public Task<List<RDD_CAF_Renewal>> GetCAF_LIST(string DbName, long? pagesize, int? pageno, DateTime s_date, DateTime e_date, string? sortcoloumn, string? sortorder, string username)
        {
            return _CAFRepo.GetCAF_LIST(DbName, pagesize, pageno, s_date, e_date, sortcoloumn, sortorder, username);
        }

        public async Task<DataSet> GetKYC(string TransType, long? pagesize, int? pageno, string username)
        {
           return await _CAFRepo.GetKYC(TransType,pagesize,pageno, username);
        }
    }
}
