
using Microsoft.AspNetCore.Mvc;
using Redddot_BL_Services;
using Reddot_View_Model;

namespace ReddotWebAPI_II.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OSOR : ControllerBase
    {

        OSOR_Services _OSORServices;
        // GET: api/<UsersController>
        public OSOR()
        {
            _OSORServices = new OSOR_Services();

        }

        [HttpPost]
        public async Task<ActionResult> SAVEOSOR([FromBody] RDD_OSOR_VM bP)
        {

            var result = await _OSORServices.SaveSOR_SAP(bP);


            if (result[0].Outtf == true)
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
