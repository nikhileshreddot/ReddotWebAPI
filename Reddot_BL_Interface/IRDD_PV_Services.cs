using Reddot_EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddot_BL_Interface
{
    public interface  IRDD_PV_Services
    {
        Task<List<Outcls1>> SavePV_SAP(List<RDD_PV_V1> bP);
    }
}
