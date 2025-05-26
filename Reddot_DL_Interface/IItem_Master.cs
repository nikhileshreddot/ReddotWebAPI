using Reddot_EF;
using Reddot_View_Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddot_DL_Interface
{
    public interface IItem_Master
    {
        Task<List<Outcls1>> SaveItem_SAP(Item_Master_VM IM);
        Task<List<Outcls1>> ConnectSAP(string DbName);

        Task<List<ItemListSO>> itemListSOs(string prefix, string dbname, string ? DispatchLocation, string? ProductType, string? CustomerType);

        Task<DataSet> GetItem_Warehouse(string DbName,string itemcode);
        Task<DataSet> GetPaydrop(string Dbname);
        Task<DataSet> GetItem_SAP(string DbName, Int64? pagesize, Int32? pageno, string type, string? Itemcode, string username);

        Task<DataSet> GetItem_SAP_CurrentDate(string DbName, Int64? pagesize, Int32? pageno, string type, string? Itemcode, string username);
        Task<DataSet> GetFINALGP(string UserName,DateTime startdate,DateTime enddate);
        Task<DataSet> GetItem_GP_SAP(string dbname, string itemcode, string warehouse, string qtysell, string pricesell, string curr, string opgrebateid);
        Task<DataSet> Get_ActiveOPGSelloutList(string basedb, string rebatedb, string itemcode);

        Task<List<InvoiceHeader>> GetItem_SOR_GP_SAP_LIST(string DbName, Int64? pagesize, Int32? pageno, DateTime s_date, DateTime e_date, string? sortcoloumn, string? sortorder, string username,string types);

        Task<List<JournalHeader>> GetJournal_Rebate_SAP_LIST(string DbName, Int64? pagesize, Int32? pageno, DateTime s_date, DateTime e_date, string? sortcoloumn, string? sortorder, string username,string types);
        Task<DataSet> Get_Rebate_SAP_LIST(string DbName, Int64? pagesize, Int32? pageno, DateTime s_date, DateTime e_date, string? sortcoloumn, string? sortorder, string username);
    }
}
