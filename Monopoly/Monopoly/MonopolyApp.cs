using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class MonopolyApp
    {
        string inputErrorMessage;
        string wanted_path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
        BasicBoard board = new BasicBoard();
        public List<Player> players = new List<Player>();
        public Dictionary<int, int> ownedRealState = new Dictionary<int, int>(); //key = location, value = ownerId
        public void initDefaultValues()
        {
            for(int i = 1; i <= 40; i++)
            {
                ownedRealState.Add(i, 0);
            }
        }

        public void SetProperty()
        {
            string final_path = wanted_path + "/csv/Property.csv";
            using (var reader = new StreamReader(@final_path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var property = new Property()
                    {
                        Position = int.Parse(values[9]),
                        Name = values[0],
                        PurchaseAmount = int.Parse(values[1]),
                        MortgageAmount = int.Parse(values[2]),
                        PropertyRent = int.Parse(values[3]),
                        Color = values[10],
                        Hotel = int.Parse(values[8])

                    };
                    property.HouseRent.Add(1, int.Parse(values[4]));
                    property.HouseRent.Add(2, int.Parse(values[5]));
                    property.HouseRent.Add(3, int.Parse(values[6]));
                    property.HouseRent.Add(4, int.Parse(values[7]));
                   
                    board.Locations.Add(property);
                    
                }
            }
            
            SetStation();
        }
        public void SetStation()
        {
            string final_path = wanted_path + "/csv/Station.csv";
            using (var reader = new StreamReader(@final_path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var railRoads = new RailRoads()
                    {
                        
                        Name = values[0],
                        PurchaseAmount = int.Parse(values[1]),
                        MortgageAmount = int.Parse(values[2])
                    };
                    railRoads.RailRoadRent.Add(1, int.Parse(values[3]));
                    railRoads.RailRoadRent.Add(2, int.Parse(values[4]));
                    railRoads.RailRoadRent.Add(3, int.Parse(values[5]));
                    railRoads.RailRoadRent.Add(4, int.Parse(values[6]));
                    railRoads.Position = int.Parse(values[7]);

                    board.Locations.Add(railRoads);
                    
                }
            }
           
            SetUtilities();
        }
        public void SetUtilities()
        {
            string final_path = wanted_path + "/csv/Uttilities.csv";
            using (var reader = new StreamReader(@final_path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var utilities = new Utilities()
                    {

                        Name = values[0],
                        PurchaseAmount = int.Parse(values[1]),
                        MortgageAmount = int.Parse(values[2]),
                        Position = int.Parse(values[3])
                    };
                    

                    board.Locations.Add(utilities);

                }
            }

            setTax();
        }
        public void setTax()
        {
            string final_path = wanted_path + "/csv/Tax.csv";
            using (var reader = new StreamReader(@final_path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var tax = new Tax()
                    {

                        Name = values[0],
                        Position = int.Parse(values[1]),
                        TaxFee = int.Parse(values[2])
                    };


                    board.Locations.Add(tax);

                }
            }
            setLottery();
        }
        public void setLottery()
        {
            string final_path = wanted_path + "/csv/Lottery.csv";
            using (var reader = new StreamReader(@final_path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var lottery = new Lottery()
                    {

                        Name = values[0],
                        Position = int.Parse(values[1]),
                    };


                    board.Locations.Add(lottery);

                }
            }
            setSpecialLocation();
        }
        public void setSpecialLocation()
        {
            string final_path = wanted_path + "/csv/SpecialLocation.csv";
            using (var reader = new StreamReader(@final_path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var specialLocation = new SpecialLocation()
                    {

                        Name = values[0],
                        Position = int.Parse(values[1]),
                    };


                    board.Locations.Add(specialLocation);

                }
            }
            board.Locations = board.Locations.OrderBy(o => o.Position).ToList();
        }
        public void PlayerInfo()
        {

            int playerNumber = TakeUserInput("Input Player Number: ", inputErrorMessage);
            if (playerNumber > 4)
            {
                Console.WriteLine("Player number can be atmost 4");
                PlayerInfo();
            }
            for (int i = 0; i < playerNumber; i++)
            {
                Console.Write("Input player {0} name: ", i + 1);

                Player player = new Player()
                {
                    Id = i + 1,
                    Name = Console.ReadLine(),
                    Position = 1,
                    Balance = 2000
                };

                players.Add(player);
            }

        }
        
       
        public void PlayerMovement()
        {
            foreach(Player player in players)
            {
                int press = TakeUserInput("Press 1 to Throw Dice, " + player.Name, inputErrorMessage);
                if (press == 1)
                {
                    Console.WriteLine("You were at -> {0} ({1})\n{2}", board.Locations[player.Position - 1].Name, player.Position, board.Locations[player.Position - 1].Print());
                    Console.WriteLine("Balance : {0}", player.Balance);
                    player.Position += Dice();
                    if(player.Position > 40)
                    {
                        player.Position = player.Position % 40;
                        player.Balance += 200;
                    }
                    else if (player.Position == 31)
                    {
                        Console.WriteLine("Go To Jail");
                        player.Position = 11;
                    }
                    else if(player.Position == 5)
                    {
                        player.Balance -= 200;
                    }
                    else if(player.Position == 39)
                    {
                        player.Balance -= 75;
                    }
                   
                    
                    else if (player.Position == 6 || player.Position == 16 || player.Position == 26 || player.Position == 36)
                    {
                        if (ownedRealState[player.Position] == 0)
                        {
                            Console.WriteLine("Now, You are at -> {0} ({1})\n{2}", board.Locations[player.Position - 1].Name, player.Position, board.Locations[player.Position - 1].Print());
                            Console.WriteLine("Do you want to buy {0} ?\n1.Yes\n2.No", board.Locations[player.Position - 1].Name);
                            int input = TakeUserInput("", inputErrorMessage);
                            if (input == 1)
                            {
                                if (player.Balance >= 200)
                                {
                                    ownedRealState[player.Position] = player.Id;
                                    player.Balance -= 200;
                                    Console.WriteLine("Balance : {0}", player.Balance);
                                }
                                else
                                {
                                    Console.WriteLine("You have not sufficient balance to buy {0}", board.Locations[player.Position - 1].Name);
                                    Console.WriteLine("Balance : {0}", player.Balance);
                                }

                            }
                            continue;
                        }
                        else
                        {
                            int count = 1;
                            for(int i = 6; i <= 36 ; i += 10)
                            {
                                if (ownedRealState[i] == player.Id)
                                {
                                    count++;
                                }
                            }
                            if(ownedRealState[player.Position] != player.Id)
                            {
                                players[ownedRealState[player.Position] - 1].Balance += 25 * count;
                                players[player.Id - 1].Balance -= 25 * count;
                            }

                        }
                    }
                    Console.WriteLine("Now, You are at -> {0} ({1})\n{2}", board.Locations[player.Position - 1].Name, player.Position, board.Locations[player.Position - 1].Print());
                    Console.WriteLine("Balance : {0}", player.Balance);

                }
                else
                {
                    continue;
                }
            }
            PlayerMovement();

        }
        public int Dice()
        {
            Random rnd = new Random();
            int diceOne = rnd.Next(1, 7);
            int diceTwo = rnd.Next(1, 7);
            int dice = diceOne + diceTwo;
            Console.WriteLine("Dice 1: {0} <-----> Dice 2: {1}", diceOne, diceTwo);
            return dice;
        }
        //public void viewMonopoly()
        //{
        //    foreach (Location VARIABLE in COLLECTION)
        //    {
                
        //    }

        //}

        public int TakeUserInput(string inputPrompt, string errorMessage)
        {
            Console.WriteLine(inputPrompt);
            var input = Console.ReadLine();
            try
            {
                return Convert.ToInt32(input);
            }
            catch (Exception ex)
            {
                Console.WriteLine(errorMessage);
                return TakeUserInput(inputPrompt, errorMessage);
            }
        }
    }
}
