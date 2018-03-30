using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public abstract class Location
    {
        public int Position;
        public string Name;


        public abstract string Print();
    }
}
