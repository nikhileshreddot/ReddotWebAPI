using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Redddot_BL_Repository;
using Reddot_BL_Interface;
using Reddot_EF;
//Abhijeet//

namespace RedDotWebAPI2.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class BPMasters : ControllerBase
    {
        BP_Master_Services _BPServices;
        

        // GET: api/<UsersController>

       
        public BPMasters()
        {
            _BPServices = new BP_Master_Services();
            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BP_Master>>> Getusers(string dbname)
        {
           
            var allUsers = _BPServices.GetBP(dbname);
            if (allUsers == null)
            {
                return NotFound();
            }

            return Ok(allUsers);
        }

        

    }
}
