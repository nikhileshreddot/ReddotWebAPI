using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Redddot_BL_Services;
using Reddot_BL_Interface;
using Reddot_EF;
using Reddot_View_Model;

namespace ReddotWebAPI_II.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BespokeController : ControllerBase
    {


        Bespokes_Services _BespokeServices;
        // GET: api/<UsersController>
        public BespokeController()
        {
            _BespokeServices = new Bespokes_Services();

        }
        [HttpPost]
        public async Task<ActionResult> SAVEOSOR([FromBody] MD_BPWBSP bP)
        {

            var result = await _BespokeServices.SaveBespoke_SAP(bP);


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
