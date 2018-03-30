using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class RailRoads : RealState
    {
        public Dictionary<int, int> RailRoadRent;
        public RailRoads()
        {
            RailRoadRent = new Dictionary<int, int>();
        }

        public override string Print()
        {
            return null;
        }
    }
}
