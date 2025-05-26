using Reddot_EF;
using Reddot_View_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddot_DL_Interface
{
    public interface ICredit_Reposiotry
    {
        Task<List<Outcls1>> SaveBP_Credit_SAP(CreditBP bP);
    }
}
