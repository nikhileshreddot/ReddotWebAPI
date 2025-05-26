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
    public class RDD_PV : ControllerBase
    {
        RDD_PV_Services rDD_PV_;
        public RDD_PV(){
            rDD_PV_= new RDD_PV_Services();
        }



        [HttpPost]
        public async Task<ActionResult> SAVEPV([FromBody] List<RDD_PV_V1> bP)
        {

            var result = await rDD_PV_.SavePV_SAP(bP);


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
