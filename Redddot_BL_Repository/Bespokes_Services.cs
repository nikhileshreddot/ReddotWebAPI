using Reddot_BL_Interface;
using Reddot_DL_Interface;
using Reddot_DL_Repository;
using Reddot_EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redddot_BL_Services
{
    public class Bespokes_Services : IBespokes_Services
    {
        IBespoke_Repository _BespokeRepo;

        public Bespokes_Services()
        {
            _BespokeRepo = new Bespoke_Repository();
        }


        public Task<List<Outcls1>> SaveBespoke_SAP(MD_BPWBSP BP)
        {
           return _BespokeRepo.SaveBESPOKE_SAP(BP);
        }
    }
}
