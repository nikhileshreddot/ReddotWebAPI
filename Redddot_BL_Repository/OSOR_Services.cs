using Reddot_BL_Interface;
using Reddot_DL_Interface;
using Reddot_DL_Repository;
using Reddot_EF;
using Reddot_View_Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redddot_BL_Services
{
    public class OSOR_Services : IOSOR_Services
    {

        IOSOR _SORRepo;

        public OSOR_Services()
        {
            _SORRepo = new OSOR_Repository();
        }

        

        public Task<List<Outcls1>> SaveSOR_SAP(RDD_OSOR_VM bP)
        {
            return _SORRepo.SaveSOR_SAP(bP);
        }
    }
}
