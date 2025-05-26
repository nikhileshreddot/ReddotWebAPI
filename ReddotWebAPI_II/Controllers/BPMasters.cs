using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Redddot_BL_Repository;
using Reddot_BL_Interface;
using Reddot_EF;
using Reddot_View_Model;
using System.Data;
using System.Reflection;


namespace ReddotWebAPI_II.Controllers
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

        [HttpPost]
        public async Task<ActionResult> SAVEBP([FromBody] BP_Master_SAP_VM bP)
        {
            

            if (bP.Code == null || bP.Code == "")
            {
                var result = await _BPServices.SaveBP_SAP(bP);
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
                var result = await _BPServices.UpdateBP_SAP(bP);
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


      
        [HttpGet("GetBPMaster")]
        // GET /api/Users/AddressId/123
        public async Task<ActionResult> GetBPMatser(string DbName, string Cardtype, Int64? pagesize, Int32? pageno, string type, string? Cardcode, string username)
        {

            
            var result = await _BPServices.GetBP_SAP(DbName,Cardtype,pagesize,pageno,type,Cardcode,username);
            
            
            return Ok(result);
            

        }

        [HttpGet("GetCustomersDue_Info")]
        // GET /api/Users/AddressId/123
        public async Task<ActionResult> GetBPMatser(string DbName,  string Cardcode)
        {


            var result = await _BPServices.Get_CustomersDue_Info(DbName,  Cardcode);


            return Ok(result);


        }

        [HttpGet("GetBESPOKEDATA")]
        // GET /api/Users/AddressId/123
        public async Task<ActionResult> GetBESPOKEDATA(string DbName, long? pagesize, int? pageno, string type, string? Cardcode, string username)
        {


            var result = await _BPServices.GetBESPOKE_SAP(DbName,pagesize,pageno,type, Cardcode,username);


            return Ok(result);


        }

    }



    
}
