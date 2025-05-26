using Microsoft.AspNetCore.Mvc;
using Redddot_BL_Services;
using Reddot_BL_Interface;
using Reddot_EF;
using Reddot_View_Model;

namespace ReddotWebAPI_II.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RDD_POS : Controller
    {

        RDD_PO_Services _POServices;
        // GET: api/<UsersController>
        public RDD_POS()
        {
            _POServices = new RDD_PO_Services();

        }

        [HttpPost]
        public async Task<ActionResult> SAVEPO([FromBody] RDD_PO bP)
        {

            var result = await _POServices.SavePO_SAP(bP);


            if (result[0].Outtf == true)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }


        }

        [HttpGet("GetItemPOSAPLIST")]
        public async Task<ActionResult> GetItem_POSAP_LIST(string DbName, long? pagesize, int? pageno, string s_date, string e_date, string? sortcoloumn, string? sortorder, string username)
        {

            var result = await _POServices.GetItem_PO_SAP_LIST(DbName, pagesize, pageno, Convert.ToDateTime(s_date), Convert.ToDateTime(e_date), sortcoloumn, sortorder, username);


            if (result is not null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }


        }
        [HttpGet("GetItemGRVSAPLIST")]
        public async Task<ActionResult> GetItem_GRVSAP_LIST(string DbName, long? pagesize, int? pageno, string s_date, string e_date, string? sortcoloumn, string? sortorder, string username)
        {

            var result = await _POServices.GetItem_GRV_SAP_LIST(DbName, pagesize, pageno, Convert.ToDateTime(s_date), Convert.ToDateTime(e_date), sortcoloumn, sortorder, username);


            if (result is not null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }


        }


        [HttpGet("GetINVRECONSAPLIST")]
        public async Task<ActionResult> GetINVSAP_LIST(string DbName, long? pagesize, int? pageno, string s_date, string e_date, string? sortcoloumn, string? sortorder, string username)
        {

            var result = await _POServices.GetItem_INV_RECON_SAP_LIST(DbName, pagesize, pageno, Convert.ToDateTime(s_date), Convert.ToDateTime(e_date), sortcoloumn, sortorder, username);


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
