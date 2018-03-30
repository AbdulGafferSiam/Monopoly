using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class Utilities : RealState
    {
        public Dictionary<int, int> utilities;

        public Utilities()
        {
            utilities = new Dictionary<int, int>();
        }

        public override string Print()
        {
            return null;
        }
        
    }
}
