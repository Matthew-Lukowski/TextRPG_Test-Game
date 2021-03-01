using System;

namespace TestGame {
    class Enemy {

        readonly Random ran = new Random();

        public bool isAlive = true, hasHeal = true;
        public int curhealth, maxhealth;
        readonly int distanceIntoForest;

        public int Curhealth { get => curhealth; set => curhealth += value; }
        public int Strength { get; }


        public Enemy(int x) {
            distanceIntoForest = x;
            maxhealth = ran.Next(49 + (distanceIntoForest * Menus.player.Level), 75 + (distanceIntoForest * Menus.player.Level));
            curhealth = maxhealth;
            Strength = ran.Next(7 + distanceIntoForest + Menus.player.Level * 2, 13 + distanceIntoForest + Menus.player.Level * 2);
        }


        public void Attack() {

            int dmg = 0;

            if (curhealth > maxhealth / 2) {
                dmg = ran.Next((int)(Strength * .75), (int)(Strength * 1.25));
                Console.WriteLine("The enemy hits you for "+dmg);
            } else {
                if(hasHeal==true && ran.Next(1, 4) == 1) {
                    curhealth += (int)(maxhealth * .33);
                    Console.WriteLine("The enemy healed");
                    hasHeal = false;
                } else {
                    dmg = ran.Next((int)(Strength * .75), (int)(Strength * 1.25));
                    Console.WriteLine("The enemy hits you for " + dmg);
                }
            }
            Menus.player.HurtPlayer(dmg);        
        }

        public void Hurt(int dmg) {
            curhealth -= dmg;
            if (curhealth < 1) {
                Death();
            }
        }

        private void Death() {
            isAlive = false;
            Menus.player.Gold = ran.Next((Strength + maxhealth) / 15,(Strength + maxhealth) / 10);
            Menus.player.Exp = ran.Next((Strength+maxhealth)/8, (Strength + maxhealth) / 5);
            if (ran.Next(2) == 1) Menus.player.Potions = ran.Next(2);
            if (ran.Next(2) == 1) Menus.player.Potions = ran.Next(2);
        }
    }
}
