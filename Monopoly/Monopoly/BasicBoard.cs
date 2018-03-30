using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class BasicBoard
    {
        public List<Location> Locations;
        public Dictionary<int, string> Chance;
        public Dictionary<int, string> CommunityChest;

        public BasicBoard()
        {
            Locations = new List<Location>();
            Chance = new Dictionary<int, string>();
            CommunityChest = new Dictionary<int, string>();
           
        }

    }
}
