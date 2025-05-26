using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddot_EF
{
    public partial class MD_BPWBSP
    {
        public Int64 BSPKID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string DBName { get; set; }
        public string Status { get; set; }
        public string Username { get; set; }
        public string? BSPKType { get; set; }

        public List<MR_BPWBSP> MR_BPWBSP1List { get; set; }

        public class MR_BPWBSP {

                       
            public string? BSPRJCD { get; set; }
            public string? BSPRJNM { get; set; }
            public DateTime? ISSUEDT { get; set; }/*satrrta dtae*/
            public string? TNDNO { get; set; } /*tender no*/
            public string?  PAYTERMC { get; set; } /*1*/
            public string? PAYTERM { get; set; } /*1 day*/
            public string? SCRTYPE { get; set; } /*pdc onac */
            public string? BSCDLVAL { get; set; } /*credit amount assignr*/
            public string? LIMITAMT { get; set; } /*Limit AMount*/
            public DateTime? LIMTEXPDT { get; set; }
            public DateTime? EXPDT { get; set; }
            public string? STATUS { get; set; }
           
            public string? RSTATUS { get; set; }
                                     

        }


    }
}
