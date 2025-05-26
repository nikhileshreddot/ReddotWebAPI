using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Redddot_BL_Services;

namespace ReddotWebAPI_II.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CAF : ControllerBase
    {

        CAF_Services _CAFServices;
        // GET: api/<UsersController>
        public CAF()
        {
            _CAFServices = new CAF_Services();

        }

        [HttpGet("GetCAFLIST")]
        public async Task<ActionResult> GetCAF_LIST(string DbName, long? pagesize, int? pageno, string s_date, string e_date, string? sortcoloumn, string? sortorder, string username)
        {

            var result = await _CAFServices.GetCAF_LIST(DbName, pagesize, pageno, Convert.ToDateTime(s_date), Convert.ToDateTime(e_date), sortcoloumn, sortorder, username);


            if (result is not null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }


        }

        [HttpGet("GetKYCLIST")]
        public async Task<ActionResult> GetKYC_LIST(string TransType, long? pagesize, int? pageno, string username)
        {

            var result = await _CAFServices.GetKYC(TransType, pagesize, pageno, username);


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
