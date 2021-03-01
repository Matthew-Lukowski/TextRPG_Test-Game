using System;

namespace TestGame {

    [Serializable]
    class Player {

        public string Name { get; set; }
        private int maxhealth, maxmana;
        int currenthealth, currentmana;
        int strength, magic;
        int gold, skillpoints, exp, expForLevel, potions;
        int level;


        public int Strength { get => strength; set => strength += value; }
        public int Magic { get => magic; set => magic += value; }

        public int Skillpoints { get => skillpoints; set => skillpoints += value; }

        public int Gold { get => gold; set => gold += value; }
        public int Exp { get => exp; set { exp += value; 
                if(exp >= expForLevel){ exp -= expForLevel; skillpoints += 2; Menus.LevelUp(); } } }
        public int Potions { get => potions; set => potions += value; }
        public int ExpForLevel { get => expForLevel; set => expForLevel += value; }


        public int Maxhealth { get => maxhealth; set => maxhealth += value; }
        public int Maxmana { get => maxmana; set => maxmana += value; }

        public int Currenthealth { get => currenthealth; set { currenthealth += value; 
                if (currenthealth > maxhealth) currenthealth = maxhealth;} }
        public int Currentmana { get => currentmana; set { currentmana += value;
                if (currentmana > maxmana) currentmana = maxmana;} }

        public int Level { get => level; set => level += value; }

        public void SetValues() {
            Level = 0;
            maxhealth = 100;
            maxmana = 50;
            expForLevel = 95;
            strength = 10;
            magic = 10;
            potions = 5;

            Currenthealth = maxhealth;
            Currentmana = maxmana;
        }



        public void HurtPlayer(int x) {
            Currenthealth = -x;
            if (Currenthealth < 1) GameOver();
        }

        public void ShowStats() {
            Console.Clear();
            Console.WriteLine("Name: "+Name+"  Level: "+level);
            Console.WriteLine($"Health: {currenthealth}/{maxhealth}  Mana: {currentmana}/{maxmana}");
            Console.WriteLine($"Gold: {gold}  Experience: {exp}/{expForLevel}  Potions: {potions}");
            Console.WriteLine($"Strength: {strength}  Magic: {magic}");
            Console.ReadKey();
        }

        private void GameOver() {
            Console.Clear();
            Console.WriteLine(Name + "was killed");
            Console.ReadKey();
	        Environment.Exit(0);
        }

        public void DrinkPotion() {
            if (Potions > 0) {
                Potions = -1;
                Currenthealth = (40 + Maxhealth / 10);
                Currentmana = (20 + Maxmana / 5);
            } else {
                Console.Clear();
                Console.WriteLine("You don't have any potions");
                Console.ReadKey();
            }
        }

        public int CombatAction() {
            int dmg = 0;
            Random ran = new Random();
            Console.WriteLine("1) Attack enemy\n2) Cast Magic\n3) Drink a potion");
            Menus.Choices(3);
            switch (Menus.enterednum) {
                case 1:
                    dmg = ran.Next((int)(strength * 1.5), (int)(strength * 2));
                    Console.WriteLine("You hit for "+dmg);
                    break;
                case 2:
                    if (currentmana > 10) {
                        Currentmana = -10;
                        dmg = ran.Next(magic * 2, (int)(magic * 3.25));
                        Console.WriteLine("You hit for " + dmg);
                    } else {
                        Console.WriteLine("You don't have enough mana");
                        Console.ReadKey();
                        CombatAction();
                    }
                    break;
                case 3:
                    DrinkPotion();
                    break;
            }
            return dmg;
        }
       
    }
}
