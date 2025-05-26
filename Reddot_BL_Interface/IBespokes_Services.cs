using Reddot_EF;
using Reddot_View_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddot_BL_Interface
{
    public interface IBespokes_Services
    {
        Task<List<Outcls1>> SaveBespoke_SAP(MD_BPWBSP BP);
    }
}
