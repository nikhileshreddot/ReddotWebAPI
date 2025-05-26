using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Redddot_BL_Repository;
using Reddot_View_Model;

namespace ReddotWebAPI_II.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BPVendorController : ControllerBase
    {


        BP_Master_Services _BPServices;


        // GET: api/<UsersController>


        public BPVendorController()
        {
            _BPServices = new BP_Master_Services();

        }

        [HttpPost]
        public async Task<ActionResult> SAVEBPVendor([FromBody] BP_Master_SAP_VENDOR_VM bP)
        {


            if (bP.Code == null || bP.Code == "")
            {
                var result = await _BPServices.SaveBP_Vendor_SAP(bP);
                if (result[0].Outtf == true)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            else
            {
                var result = await _BPServices.UpdateBP_Vendor_SAP(bP);
                if (result[0].Outtf == true)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }




            // await _BPServices.SaveChangesAsync();

        }

    }
}
