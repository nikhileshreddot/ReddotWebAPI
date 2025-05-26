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
    public class RDD_PV_Services : IRDD_PV_Services
    {


        IRDD_PV_Repository _PVRepo;

        public RDD_PV_Services()
        {
            _PVRepo = new RDD_PV_Repository();
        }

        public Task<List<Outcls1>> SavePV_SAP(List<RDD_PV_V1> bP)
        {
            return _PVRepo.SavePV_SAP(bP);
        }
    }
}
