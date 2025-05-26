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
    public class PurchaseAgreementController : ControllerBase
    {

        RDD_PO_Services _POServices;
        // GET: api/<UsersController>
        public PurchaseAgreementController()
        {
            _POServices = new RDD_PO_Services();

        }

        [HttpPost]
        public async Task<ActionResult> SAVEPurchaseagreement([FromBody] PurchaseAgreement bP)
        {

            var result = await _POServices.SavePurchaseAgreement(bP);


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


