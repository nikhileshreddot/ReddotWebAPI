using Microsoft.AspNetCore.Mvc;
using Redddot_BL_Repository;
using Reddot_View_Model;
using System.Data;
using System.Data.SqlClient;

namespace ReddotWebAPI_II.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemMasters : Controller
    {

        Item_Master_Services _IMServices;
        // GET: api/<UsersController>
        public ItemMasters()
        {
            _IMServices = new Item_Master_Services();

        }
        
        [HttpPost]
        public async Task<ActionResult> SAVEIM([FromBody] Item_Master_VM bP)
        {

            var result = await _IMServices.SaveItem_SAP(bP);


            if (result[0].Outtf == true)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
            // await _BPServices.SaveChangesAsync();

        }
        [HttpGet]
        public async Task<ActionResult> itemListSOs(string prefix, string dbname, string? DispatchLocation, string? ProductType, string? CustomerType)
        {

            var result = await _IMServices.itemListSOs(prefix,dbname,DispatchLocation,ProductType,CustomerType);


            if (result.Count>0)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
            // await _BPServices.SaveChangesAsync();

        }

        [HttpGet("GetItemMaster")]
        // GET /api/Users/AddressId/123
        public async Task<ActionResult> GetBPMatser(string DbName, Int64? pagesize, Int32? pageno, string type, string? Itemcode, string username)
        {


            var result = await _IMServices.GetItem_SAP(DbName,pagesize,pageno,type,Itemcode,username);


            if (result is not null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }


        }
        [HttpGet("GetItemMasterCurrent")]
        // GET /api/Users/AddressId/123
        public async Task<ActionResult> GetItemMasterCurrent(string DbName, Int64? pagesize, Int32? pageno, string type, string? Itemcode, string username)
        {


            var result = await _IMServices.GetItem_SAP_CurrentDate(DbName, pagesize, pageno, type, Itemcode, username);


            if (result is not null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }


        }


        [HttpGet("Get_ActiveOPGSelloutList")]
        public async Task<ActionResult> GetActiveOPGSelloutList(string basedb, string rebatedb, string itemcode)
        {
            var result=await _IMServices.Get_ActiveOPGSelloutList(basedb,rebatedb,itemcode);
            if(result is not null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpGet("Get_RebateList")]
        public async Task<ActionResult> GetRebateList(string DbName, long? pagesize, int? pageno, string s_date, string e_date, string? sortcoloumn, string? sortorder, string username)
        {
            var result = await _IMServices.Get_Rebate_SAP_LIST(DbName, pagesize, pageno, Convert.ToDateTime(s_date), Convert.ToDateTime(e_date), sortcoloumn, sortorder, username);
            if (result is not null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpGet("Get_JournalRebateList")]
        public async Task<ActionResult> GetJournalRebateList(string DbName, long? pagesize, int? pageno, string s_date, string e_date, string username,string types)
        {
            var result = await _IMServices.GetJournal_Rebate_SAP_LIST(DbName, pagesize, pageno, Convert.ToDateTime(s_date), Convert.ToDateTime(e_date), "T1.TaxDate", "ASC", username, types);
            if (result is not null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }


        [HttpGet("GetItem_GP_SAP")]
        // GET /api/Users/AddressId/123
        public async Task<ActionResult> GetItem_GP_SAP(string dbname, string itemcode, string warehouse, string qtysell, string pricesell, string curr, string opgrebateid)
        {


            var result = await _IMServices.GetItem_GP_SAP(dbname, itemcode, warehouse, qtysell, pricesell, curr, opgrebateid);


            if (result is not null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }


        }

        [HttpGet("GetPayTerm")]

        public async Task<ActionResult> GetPaydrop(string dbname)
        {
            var result = await _IMServices.GetPaydrop(dbname);

            if (result is not null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }


        }
        [HttpGet("GetItem_Warehouse_SAP")]
        // GET /api/Users/AddressId/123
        public async Task<ActionResult> GetItem_Warehouse_SAP(string dbname, string itemcode)
        {


            var result = await _IMServices.GetItem_Warehouse(dbname, itemcode);

            if (result is not null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }


        }

        [HttpGet("GetItemSORGPSAPLIST")]
        public async Task<ActionResult> GetItem_SOR_GP_SAP_LIST(string DbName, long? pagesize, int? pageno, string s_date, string e_date, string? sortcoloumn, string? sortorder, string username,string ? types)
        {

            var result = await _IMServices.GetItem_SOR_GP_SAP_LIST(DbName, pagesize, pageno, Convert.ToDateTime(s_date),Convert.ToDateTime(e_date), sortcoloumn, sortorder, username,types);


            if (result is not null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }


        }

        [HttpGet("GETCOUNTRYBUGPLIST")]
        public async Task<ActionResult> GETCOUNTRYBUGPLIST(string username, DateTime s_date, DateTime  e_date)
        {

            var result = await _IMServices.GetFINALGP(username,s_date,e_date);


            if (result is not null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }


        }
    }
}
