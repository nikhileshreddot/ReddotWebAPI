using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddot_EF
{
    public class Item_Master
    {
        /*string DBList, string itmCode, string itmDesc, int mfrId, int itmGrpId, 
         * string itmGrpCode, string itmBU, string itmProductCategory, string itmPL, 
         * string itmProductGrp, double Lenght, double Width, double Height, double Weight
         * ,string HSCode*/

        public string DBList { get; set; } 
        public string itmCode { get; set; }
        public string itmDesc { get; set; }
        public int mfrId { get; set; }
        public int itmGrpId { get; set;}
        public string itmGrpCode { get; set; }

        public string itmBU { get; set; }
        public string itmProductCategory { get; set; }
        public string itmPL { get; set; }
        public string itmProductGrp { get; set;}
        public double Length { get; set; }

        public double Weight { get; set; }
        public double Width     { get; set; } 
        public double Height { get; set; }
        public string HSCode { get; set; }
       
    }
    public partial class ItemListSO
    {
        public string ItemCode { get; set; }

        public string ItemName { get; set; }
        public string DfltWH { get; set; }
        public string VatGourpSa { get; set; }
        public double OnHand { get; set; }
        public double ActalQty { get; set; }
        public double Rate { get; set; }
        public string ProductType { get; set; }
    }

}
