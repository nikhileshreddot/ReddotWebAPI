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

namespace Redddot_BL_Repository
{
    public class Item_Master_Services : IItem_Master_Services
    {

        IItem_Master _ItemRepo;

        public Item_Master_Services()
        {
            _ItemRepo = new Item_Master_Repository();
        }

        public Task<List<Outcls1>> ConnectSAP(string DbName)
        {
            return _ItemRepo.ConnectSAP(DbName);
        }

        public async Task<DataSet> GetItem_GP_SAP(string dbname, string itemcode, string warehouse, string qtysell, string pricesell, string curr, string opgrebateid)
        {
            return await _ItemRepo.GetItem_GP_SAP(dbname, itemcode, warehouse, qtysell, pricesell, curr, opgrebateid);
        }

        public async Task<DataSet> GetItem_SAP(string DbName, Int64? pagesize, Int32? pageno, string type, string? Itemcode, string username)
        {
            return await _ItemRepo.GetItem_SAP(DbName,pagesize,pageno  ,type,Itemcode,username);
        }

        public async Task<DataSet> GetItem_Warehouse(string dbname, string itemcode)
        {
            return await _ItemRepo.GetItem_Warehouse(dbname, itemcode);     
        }

        public async Task<DataSet> Get_ActiveOPGSelloutList(string basedb, string rebatedb, string itemcode)
        {
            return await _ItemRepo.Get_ActiveOPGSelloutList(basedb, rebatedb, itemcode);
        }

        public async Task<List<ItemListSO>> itemListSOs(string prefix, string dbname, string? DispatchLocation, string? ProductType, string? CustomerType)
        {
            return await _ItemRepo.itemListSOs(prefix, dbname, DispatchLocation, ProductType, CustomerType);
        }

        public Task<List<Outcls1>> SaveItem_SAP(Item_Master_VM IM)
        {
            return _ItemRepo.SaveItem_SAP(IM);
        }

        public async Task<List<InvoiceHeader>> GetItem_SOR_GP_SAP_LIST(string DbName, long? pagesize, int? pageno, DateTime s_date, DateTime e_date, string? sortcoloumn, string? sortorder, string username,string types)
        {
            return await _ItemRepo.GetItem_SOR_GP_SAP_LIST(DbName, pagesize, pageno, s_date, e_date, sortcoloumn, sortorder, username,types);
        }

        public async Task<DataSet> GetFINALGP(string UserName,DateTime startdate,DateTime enddate)
        {
            return await _ItemRepo.GetFINALGP(UserName,startdate,enddate);
        }

        public async Task<DataSet> Get_Rebate_SAP_LIST(string DbName, long? pagesize, int? pageno, DateTime s_date, DateTime e_date, string? sortcoloumn, string? sortorder, string username)
        {
            return await _ItemRepo.Get_Rebate_SAP_LIST(DbName, pagesize, pageno, s_date, e_date, sortcoloumn, sortorder, username);
        }

        public async Task<List<JournalHeader>> GetJournal_Rebate_SAP_LIST(string DbName, long? pagesize, int? pageno, DateTime s_date, DateTime e_date, string? sortcoloumn, string? sortorder, string username,string types)
        {
            return await _ItemRepo.GetJournal_Rebate_SAP_LIST(DbName, pagesize, pageno, s_date, e_date, sortcoloumn, sortorder, username, types);
        }

        public async Task<DataSet> GetItem_SAP_CurrentDate(string DbName, long? pagesize, int? pageno, string type, string? Itemcode, string username)
        {
            return await _ItemRepo.GetItem_SAP_CurrentDate(DbName,pagesize,pageno,type,Itemcode,username);      
        }

        public async Task<DataSet> GetPaydrop(string Dbname)
        {
            return await _ItemRepo.GetPaydrop(Dbname);  
        }
    }
}
