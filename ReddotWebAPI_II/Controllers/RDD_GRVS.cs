using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Redddot_BL_Services;
using Reddot_EF;

namespace ReddotWebAPI_II.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RDD_GRVS : ControllerBase
    {

        RDD_PO_Services _POServices;
        // GET: api/<UsersController>
        public RDD_GRVS()
        {
            _POServices = new RDD_PO_Services();

        }

        [HttpPost]
        public async Task<ActionResult> SAVEGRV([FromBody] RDD_GRV bP)
        {

            var result = await _POServices.SaveGRV_SAP(bP);


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
