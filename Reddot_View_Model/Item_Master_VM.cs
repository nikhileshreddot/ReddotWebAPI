using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddot_View_Model
{
    public partial  class Item_Master_VM
    {

        public string DBList { get; set; }
        public string itmCode { get; set; }
        public string itmDesc { get; set; }
        public int mfrId { get; set; }
        public int itmGrpId { get; set; }
        public string itmGrpCode { get; set; }

        public string itmBU { get; set; }
        public string itmProductCategory { get; set; }
        public string itmPL { get; set; }
        public string itmProductGrp { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string HSCode { get; set; }
        public double Weight
        {
            get; set;
        }
        public Int64 SAPid { get; set; }
        public bool SaveFlag { get; set; }

        public bool EditFlag { get; set; }



        public string UserName { get; set; }
    }
}
