using Microsoft.AspNetCore.Mvc;
using Redddot_BL_Services;
using Reddot_BL_Interface;
using Reddot_EF;
using Reddot_View_Model;

namespace ReddotWebAPI_II.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Credit : Controller
    {

        Credit_Services _CreditServices;
        // GET: api/<UsersController>
        public Credit()
        {
            _CreditServices = new Credit_Services();

        }
        

        [HttpPost]
        public async Task<ActionResult> SAVECreditBP([FromBody] CreditBP bP)
        {

            var result = await _CreditServices.SaveBP_Credit_SAP(bP);


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
