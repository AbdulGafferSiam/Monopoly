using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class Program
    {

        static void Main(string[] args)
        {
            MonopolyApp monopoly = new MonopolyApp();
            monopoly.SetProperty();
            monopoly.initDefaultValues();
            monopoly.PlayerInfo();
            monopoly.PlayerMovement();
            Console.ReadKey();
        }
    }
}
