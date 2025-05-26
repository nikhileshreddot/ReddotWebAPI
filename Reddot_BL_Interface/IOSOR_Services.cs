using Reddot_EF;
using Reddot_View_Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddot_BL_Interface
{
    public interface  IOSOR_Services
    {
        Task<List<Outcls1>> SaveSOR_SAP(RDD_OSOR_VM bP);
       
    }
}
