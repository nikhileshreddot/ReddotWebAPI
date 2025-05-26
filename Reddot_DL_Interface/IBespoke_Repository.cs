using Reddot_EF;
using Reddot_View_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddot_DL_Interface
{
    public interface IBespoke_Repository
    {
        Task<List<Outcls1>> SaveBESPOKE_SAP(MD_BPWBSP BP);
    }
}
