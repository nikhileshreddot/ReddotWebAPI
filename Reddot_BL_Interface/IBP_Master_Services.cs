using Reddot_EF;
using Reddot_View_Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Reddot_DL_Repository.Commonfunction;

namespace Reddot_BL_Interface
{
    public interface IBP_Master_Services
    {
        List<BP_Master> GetBP(string DBName);

        BP_Master_VM SaveBrch(BP_Master_VM BP);
        Task<DataSet> Get_CustomersDue_Info(string Dbanme, string cardcode);
        Task<List<Outcls1>> SaveBP_SAP(BP_Master_SAP_VM bP);


        Task<List<Outcls1>> UpdateBP_SAP(BP_Master_SAP_VM bP);
        Task<List<Outcls1>> UpdateBP_Vendor_SAP(BP_Master_SAP_VENDOR_VM bP);

        Task<List<Outcls1>> SaveBP_Vendor_SAP(BP_Master_SAP_VENDOR_VM bP);
        Task<DataSet> GetBP_SAP(string DbName, string Cardtype, Int64? pagesize, Int32? pageno, string type, string? Cardcode, string username);
        Task<DataSet> GetBESPOKE_SAP(string DbName, Int64? pagesize, Int32? pageno, string type, string? Cardcode, string username);
    }
}
