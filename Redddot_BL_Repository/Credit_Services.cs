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
    public class Credit_Services :ICredit_Services
    {
        ICredit_Reposiotry _CreditRepo;

        public Credit_Services()
        {
            _CreditRepo = new Credit_Reposiotry();
        }

        public Task<List<Outcls1>> SaveBP_Credit_SAP(CreditBP bP)
        {
            return _CreditRepo.SaveBP_Credit_SAP(bP);
        }
    }
}
