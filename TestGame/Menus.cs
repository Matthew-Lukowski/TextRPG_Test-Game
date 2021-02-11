using Newtonsoft.Json;
using System;
using System.IO;

namespace TestGame {
    static class Menus {

        public static int enterednum;
        public static Player player = new Player();
        public static Location location = new Location();

        public static void StartGame() {

            Console.WriteLine("\tStart Your Adventure\n");
            Console.WriteLine("1) New Adventure.\n2) Continue Adventure\n3) Quit Game\n4) Credits");

            Choices(4);

            switch (enterednum) {
                case 1:
                    Console.Clear();
                    player.SetValues();
                    Console.WriteLine("Enter your name: ");
                    player.Name = Console.ReadLine();
                    Console.Clear();
                    player.Skillpoints = 3;
                    LevelUp();
                    Console.Clear();
                    location.Town();
                    break;
                case 2:
                    Console.Clear();
                    if (File.Exists(@"Save.json")) {
                        player = JsonConvert.DeserializeObject<Player>(File.ReadAllText(@"Save.json"));
                        location.Town();
                    } else {
                        Console.Clear();
                        Console.WriteLine("You don't have a save");
                        Console.ReadKey();
                        Console.Clear();
                        StartGame();
                    }
                    
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
                case 4:
                    Console.Clear();
                    Console.WriteLine("Game made by Brohamicus");
                    Console.WriteLine("Release Feb. 10, 2021");
                    Console.ReadKey();
                    Console.Clear();
                    StartGame();
                    break;
            }
        }

        public static void LevelUp() {
            while (player.Skillpoints > 0) {
                player.Currenthealth = player.Maxhealth;
                Console.WriteLine("Skillpoints: " + player.Skillpoints);
                Console.WriteLine("Choose what to level up:\n1) Strength\n2) Health\n3) Magic");
                Choices(3);
                Console.WriteLine("Choose how many points to allocate");
                switch (enterednum) {
                    case 1:
                        Choices(player.Skillpoints);
                        player.Strength = enterednum;
                        break;
                    case 2:
                        Choices(player.Skillpoints);
                        player.Maxhealth = enterednum * 5;
                        player.Currenthealth = enterednum * 5;
                        break;
                    case 3:
                        Choices(player.Skillpoints);
                        player.Magic = enterednum;
                        player.Maxmana = enterednum * 5;
                        player.Currentmana = enterednum * 5;
                        break;
                }
                player.Skillpoints = -enterednum;
                Console.Clear();
            }
            player.Level = 1;
            player.ExpForLevel /= 10;
        }

        public static void Choices(int choiceAmount) {
            string numAsString;
            
            do {
                numAsString = Console.ReadLine();
                if (numAsString.Equals(null)) {
                    numAsString = Console.ReadLine();
                }

                if(int.TryParse(numAsString, out int _1) == false
            || int.Parse(numAsString) > choiceAmount
            || int.Parse(numAsString) <= 0) { Console.WriteLine("Enter a valid number."); }

            } while (int.TryParse(numAsString, out int _2) == false
            || int.Parse(numAsString) > choiceAmount
            || int.Parse(numAsString) <= 0);

            enterednum = int.Parse(numAsString);
        }





    }
}
