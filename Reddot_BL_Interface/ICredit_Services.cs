using Reddot_EF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddot_BL_Interface
{
    public interface ICredit_Services
    {

        Task<List<Outcls1>> SaveBP_Credit_SAP(CreditBP bP);

        
    }
}
