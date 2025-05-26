using Reddot_EF;
using Reddot_View_Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddot_BL_Interface
{
    public interface IRDD_PO_Service
    {

        Task<List<Outcls1>> SavePO_SAP(RDD_PO bP);

        Task<List<Outcls1>> SaveGRV_SAP(RDD_GRV bP);
        Task<List<Outcls1>> SavePurchaseAgreement(PurchaseAgreement purchaseAgreement);

        Task<List<RDD_PO>> GetItem_PO_SAP_LIST(string DbName, long? pagesize, int? pageno, DateTime s_date, DateTime e_date, string? sortcoloumn, string? sortorder, string username);

        Task<List<RDD_PO>> GetItem_GRV_SAP_LIST(string DbName, long? pagesize, int? pageno, DateTime s_date, DateTime e_date, string? sortcoloumn, string? sortorder, string username);
        Task<DataSet> GetItem_INV_RECON_SAP_LIST(string DbName, Int64? pagesize, Int32? pageno, DateTime s_date, DateTime e_date, string? sortcoloumn, string? sortorder, string username);
    }
}
