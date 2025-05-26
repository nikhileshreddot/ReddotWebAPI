using Reddot_EF;
using Reddot_View_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddot_DL_Interface
{
    public interface IRDD_PV_Repository
    {
        Task<List<Outcls1>> SavePV_SAP(List<RDD_PV_V1> bP);
    }
}
