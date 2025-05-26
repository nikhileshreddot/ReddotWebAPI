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

namespace Redddot_BL_Services
{
    public partial class RDD_PO_Services : IRDD_PO_Service
    {

        IRDD_PO_Repository _SORRepo;

        public RDD_PO_Services()
        {
            _SORRepo = new RDD_PO_Repository();
        }

        public Task<List<RDD_PO>> GetItem_GRV_SAP_LIST(string DbName, long? pagesize, int? pageno, DateTime s_date, DateTime e_date, string? sortcoloumn, string? sortorder, string username)
        {
            return _SORRepo.GetItem_GRV_SAP_LIST(DbName, pagesize, pageno, s_date, e_date, sortcoloumn, sortorder, username);
        }

        public Task<DataSet> GetItem_INV_RECON_SAP_LIST(string DbName, long? pagesize, int? pageno, DateTime s_date, DateTime e_date, string? sortcoloumn, string? sortorder, string username)
        {
            return _SORRepo.GetItem_INV_RECON_SAP_LIST(DbName,pagesize,pageno,s_date,e_date, sortcoloumn,sortorder, username);
        }

        public Task<List<RDD_PO>> GetItem_PO_SAP_LIST(string DbName, long? pagesize, int? pageno, DateTime s_date, DateTime e_date, string? sortcoloumn, string? sortorder, string username)
        {
            return _SORRepo.GetItem_PO_SAP_LIST(DbName, pagesize, pageno, s_date, e_date, sortcoloumn, sortorder, username);            
        }

        public Task<List<Outcls1>> SaveGRV_SAP(RDD_GRV bP)
        {
            return _SORRepo.SaveGRV_SAP(bP);
        }

        public Task<List<Outcls1>> SavePO_SAP(RDD_PO bP)
        {
            return _SORRepo.SavePO_SAP(bP);
        }

        public Task<List<Outcls1>> SavePurchaseAgreement(PurchaseAgreement purchaseAgreement)
        {
           return _SORRepo.SavePurchaseAgreement(purchaseAgreement);
        }
    }
}
