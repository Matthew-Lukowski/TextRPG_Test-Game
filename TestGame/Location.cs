using Newtonsoft.Json;
using System;
using System.IO;

namespace TestGame {
    class Location {

        int distanceIntoForest = 0;
        readonly Random ran = new Random();

        public void Town() {
            Console.Clear();
            Console.WriteLine(Menus.player.Name + " entered the Town");
            Console.WriteLine("You can see the \n1) Inn \n2) Shop \n3) Forest\n4) Yourself");
            Menus.Choices(4);
            int x = Menus.enterednum;
            if (x == 1) {
                Inn();
            } else if (x == 2) {
                Shop();
            } else if (x == 3) {
                Forest();
            } else if (x == 4) {
                Menus.player.ShowStats();
                Town();
            }
        }

        public void Inn() {
            Console.Clear();
            Console.WriteLine(Menus.player.Name + " entered the Inn");
            Console.WriteLine("You can \n1) Buy a room \n2) Save \n3) Leave");
            Menus.Choices(3);
            switch (Menus.enterednum) {
                case 1:
                    Console.WriteLine("You have " + Menus.player.Gold + "Gold");
                    Console.WriteLine("1) 10g for the cheap room\n2) 20g for the expensive room\n3) Don't buy a room");
                    Menus.Choices(3);
                    switch (Menus.enterednum) {
                        case 1:
                            if (Menus.player.Gold >= 10) {
                                Menus.player.Gold = -10;
                                Menus.player.Currenthealth = (int)(Menus.player.Maxhealth * .5);
                                Menus.player.Currentmana = (int)(Menus.player.Maxmana * .5);
                                Console.Clear();
                                Inn();
                            } else {
                                Console.Clear();
                                Console.WriteLine("You cant afford the room");
                                Console.ReadKey();
                                Console.Clear();
                                Inn();
                            }
                            break;
                        case 2:
                            if (Menus.player.Gold >= 20) {
                                Menus.player.Gold = -20;
                                Menus.player.Currenthealth = Menus.player.Maxhealth;
                                Menus.player.Currentmana = Menus.player.Maxmana;
                                Console.Clear();
                                Inn();
                            } else {
                                Console.Clear();
                                Console.WriteLine("You cant afford the room");
                                Console.ReadKey();
                                Console.Clear();
                                Inn();
                            }
                            break;
                        case 3:
                            Console.Clear();
                            Inn();
                            break;
                    }

                    break;
                case 2:
                    //save character
                    using (StreamWriter file = File.CreateText(@"Save.json")) {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(file, Menus.player);
                    }
                    Inn();
                    break;
                case 3:
                    Town();
                    break;
            }


        }

        public void Shop() {
            Console.Clear();
            Console.WriteLine(Menus.player.Name + " entered the Shop");
            Console.WriteLine("You can upgrade your items for 10g \n1) Sword\n2) Armor\n3) Staff\n4) Buy potions\n5) Leave the shop");
            Menus.Choices(5);
            switch (Menus.enterednum) {
                case 1:
                    if (Menus.player.Gold >= 10) {
                        Menus.player.Gold = -10;
                        Menus.player.Strength = 1;
                    } else {
                        Console.Clear();
                        Console.WriteLine("You cant afford the upgrade");
                        Console.ReadKey();
                        Console.Clear();
                        Shop();
                    }
                    break;
                case 2:
                    if (Menus.player.Gold >= 10) {
                        Menus.player.Gold = -10;
                        Menus.player.Maxhealth = 5;
                        Menus.player.Currenthealth = 5;

                    } else {
                        Console.Clear();
                        Console.WriteLine("You cant afford the upgrade");
                        Console.ReadKey();
                        Console.Clear();
                        Shop();
                    }
                    break;
                case 3:
                    if (Menus.player.Gold >= 10) {
                        Menus.player.Gold = -10;
                        Menus.player.Magic = 1;
                    } else {
                        Console.Clear();
                        Console.WriteLine("You cant afford the upgrade");
                        Console.ReadKey();
                        Console.Clear();
                        Shop();
                    }
                    break;
                case 4:
                    Console.Clear();
                    Console.WriteLine("How many potions would you like to buy?\nEach potion is 5g");

                    string numAsString;
                    do {
                        numAsString = Console.ReadLine();
                        if (numAsString.Equals(null) || numAsString.Equals(string.Empty)) numAsString = Console.ReadLine();

                        if (int.TryParse(numAsString, out int _1) == false
                    || int.Parse(numAsString) < 0) { Console.WriteLine("Enter a valid number."); }

                    } while (int.TryParse(numAsString, out int _2) == false
                    || int.Parse(numAsString) < 0);

                    int potionAmount = int.Parse(numAsString);

                    if (potionAmount > Menus.player.Gold * 5) {
                        Console.Clear();
                        Console.WriteLine("You can't afford this");
                        Console.ReadKey();
                        Shop();
                    } else {
                        Menus.player.Potions = potionAmount;
                        Menus.player.Gold = -(potionAmount * 5);
                        Console.Clear();
                        Shop();
                    }
                    break;
                case 5:
                    Console.Clear();
                    Town();
                    break;
            }


        }
        public void Forest() {
            Console.Clear();
            Console.WriteLine(Menus.player.Name + " entered the Forest");
            Console.WriteLine("You are currently in level " + distanceIntoForest + " of the forest");
            Console.WriteLine("1) Explore further\n2) View Yourself\n3) Drink a potion\n4) Head back to the town\n5) Rest");
            Menus.Choices(5);
            switch (Menus.enterednum) {
                case 1:
                    distanceIntoForest++;
                    Battle();
                    break;
                case 2:
                    Menus.player.ShowStats();
                    Forest();
                    break;
                case 3:
                    Menus.player.DrinkPotion();
                    Forest();
                    break;
                case 4:
                    if (distanceIntoForest > 0) {
                        if (ran.Next(1, 11) < distanceIntoForest) {
                            Battle();
                        }
                        distanceIntoForest--;
                        Forest();
                    } else {
                        Town();
                    }
                    break;
                case 5:
                    if (ran.Next(3) == 2) {
                        Battle();
                    } else {
                        Menus.player.Currenthealth = 20 + Menus.player.Maxhealth/10;
                        Menus.player.Currentmana = 10 + Menus.player.Currentmana/5;
                        Console.WriteLine("You lay down to rest");
                        Console.ReadKey();
                        Forest();
                    }
                    break;
            }
        }

        public void Battle() {
            Enemy enemy = new Enemy(distanceIntoForest);
            Console.Clear();
            Console.WriteLine("You have encountered an enemy");
            Console.ReadKey();
            Console.Clear();


            while (enemy.isAlive) {
                Console.Clear();
                Console.WriteLine($"Health: {Menus.player.Currenthealth}/{Menus.player.Maxhealth}  Mana:{Menus.player.Currentmana}/{Menus.player.Maxmana}\n");
                Console.WriteLine($"Enemy Health: {enemy.Curhealth}\n");

                if (ran.Next(1, 3) == 2) {
                    Console.WriteLine("Enemy has first move\n");
                    enemy.Attack();
                    Console.WriteLine($"\nHealth: {Menus.player.Currenthealth}/{Menus.player.Maxhealth}\n");
                    enemy.Hurt(Menus.player.CombatAction());
                    Console.ReadKey();
                } else {
                    Console.WriteLine("You have first move\n");
                    enemy.Hurt(Menus.player.CombatAction());
                    if (enemy.isAlive == true) {
                        enemy.Attack();
                        Console.WriteLine($"\nHealth: {Menus.player.Currenthealth}/{Menus.player.Maxhealth}\n");
                    }
                    Console.ReadKey();
                }
            }
            Forest();
        }
    }
}
