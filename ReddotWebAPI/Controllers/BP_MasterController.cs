using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Redddot_BL_Repository;
using Reddot_BL_Interface;
using Reddot_EF;
using System.Collections.Generic;
using System.Data;

namespace ReddotWebAPI.Controllers
{



    [Route("api/[controller]")]
    [ApiController]
    public class BP_MasterController : ControllerBase
    {
        IBP_Master_Services _BPServices;

        public BP_MasterController()
        {
            _BPServices = new BP_Master_Services();
        }

        [HttpGet]       
        public async Task<ActionResult<IEnumerable<BP_Master>>> Getusers()
        {
            string dbname = "SAPKE";
            var allUsers = _BPServices.GetBP(dbname);
            if (allUsers == null)
            {
                return NotFound();
            }

            return Ok(allUsers);
        }


    }
}
