using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class Property : RealState
    {
        public string Color;
        public int Hotel;
        public int PropertyRent;
        public Dictionary<int, int> HouseRent;

        public Property()
        {
            HouseRent = new Dictionary<int, int>();
        }

        public override string Print()
        {
            
            return string.Format("Color : {0}\nRent : {1} ", Color, PropertyRent);
            
        }
    }
}
