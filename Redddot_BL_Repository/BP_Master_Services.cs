using Reddot_BL_Interface;
using Reddot_DL_Interface;
using Reddot_DL_Repository;
using Reddot_EF;
using Reddot_View_Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Reddot_DL_Repository.Commonfunction;

namespace Redddot_BL_Repository
{
    public class BP_Master_Services : IBP_Master_Services
    {
        IBP_Master _BPRepo;

        public BP_Master_Services()
        {
            _BPRepo = new BP_Master_Repository();
        }
        public List<BP_Master> GetBP(string DBName)
        {
           return _BPRepo.GetBP(DBName);  
        }
        public Task<DataSet> Get_CustomersDue_Info(string Dbanme, string cardcode)
        {
            return _BPRepo.Get_CustomersDue_Info(Dbanme, cardcode);
        }


        public BP_Master_VM SaveBrch(BP_Master_VM BP)
        {
            throw new NotImplementedException();
        }

       public  Task<List<Outcls1>>SaveBP_SAP(BP_Master_SAP_VM bP)
        {
            return _BPRepo.SaveBP_SAP(bP);
        }
        public Task<List<Outcls1>> UpdateBP_SAP(BP_Master_SAP_VM bP)
        {
            return _BPRepo.UpdateBP_SAP(bP);
        }

        public async Task<DataSet> GetBP_SAP(string DbName, string Cardtype, Int64? pagesize, Int32? pageno, string type, string? Cardcode, string username)
        {
           return await _BPRepo.GetBP_SAP(DbName, Cardtype,pagesize,pageno,type,Cardcode,username);
        }

        public async Task<DataSet> GetBESPOKE_SAP(string DbName, long? pagesize, int? pageno, string type, string? Cardcode, string username)
        {
            return await _BPRepo.GetBESPOKE_SAP(DbName, pagesize, pageno, type, Cardcode, username);
        }

        public Task<List<Outcls1>> SaveBP_Vendor_SAP(BP_Master_SAP_VENDOR_VM bP)
        {
            return _BPRepo.SaveBP_Vendor_SAP(bP);
        }
        public Task<List<Outcls1>> UpdateBP_Vendor_SAP(BP_Master_SAP_VENDOR_VM bP)
        {
            return _BPRepo.UpdateBP_Vendor_SAP(bP);
        }

      
    }
}
