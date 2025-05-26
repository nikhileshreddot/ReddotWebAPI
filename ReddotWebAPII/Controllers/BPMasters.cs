using Microsoft.AspNetCore.Mvc;
using Redddot_BL_Repository;
using Reddot_BL_Interface;

namespace ReddotWebAPII.Controllers
{
    public class BPMasters : Controller
    {
        IBP_Master_Services _BPServices;

        public BPMastersController()
        {
            _BPServices = new BP_Master_Services();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
