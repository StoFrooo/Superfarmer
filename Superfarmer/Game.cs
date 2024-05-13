using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Superfarmer
{
    public class Game
    {
        private string[] dice1 = new string[] { "rabbit", "rabbit", "rabbit", "rabbit", "rabbit", "rabbit", "sheep", "sheep", "sheep", "pig", "cow", "wolf" };
        private string[] dice2 = new string[] { "rabbit", "rabbit", "rabbit", "rabbit", "rabbit", "rabbit", "sheep", "sheep", "pig", "pig", "horse", "fox" };

        private Random rand = new Random();

        public int rabbits;
        public int sheeps;
        public int pigs;
        public int cows;
        public int horses;
        public int smallDogs;
        public int largeDogs;

        public Game()
        {
            Reset();
        }

        public void Reset()
        {
            rabbits = 60;
            sheeps = 20;
            pigs = 15;
            cows = 10;
            horses = 5;
            smallDogs = 4;
            largeDogs = 2;
        }

        public void ThrowDices(Player player)
        {
            int index1 = rand.Next(dice1.Length);
            string animal1 = dice1[index1];
            int index2 = rand.Next(dice2.Length);
            string animal2 = dice2[index2];
            bool bonus = false;
            if (animal1 == animal2)
            {
                bonus = true;
            }
            if (animal1 == "wolf")
            {
                if (player.largeDog == 0)
                {
                    sheeps += player.sheep;
                    pigs += player.pig;
                    cows += player.cow;
                    player.sheep = 0;
                    player.pig = 0;
                    player.cow = 0;

                }
                else
                {
                    player.largeDog -= 1;
                    largeDogs += 1;
                }
                if (animal2 == "fox")
                {
                    if (player.smallDog == 0)
                    {
                        rabbits += player.rabbit - 1;
                        player.rabbit = 1;
                    }
                    else
                    {
                        player.smallDog -= 1;
                        smallDogs += 1;
                    }
                }
                else
                {
                    AssignAnimals(player, animal2);
                }
            }
            else
            {
                if (animal2 == "fox")
                {
                    if (player.smallDog == 0)
                    {
                        rabbits += player.rabbit - 1;
                        player.rabbit = 1;

                    }
                    else
                    {
                        player.smallDog -= 1;
                        smallDogs += 1;
                    }
                    AssignAnimals(player, animal1);
                }
                else
                {
                    if (bonus == true){ AssignAnimalsBonus(player, animal2, animal1); }
                    else
                    {
                        AssignAnimals(player, animal1);
                        AssignAnimals(player, animal2);
                    }
                }

            }

            Console.WriteLine($"Dice 1: {animal1}\nDice 2: {animal2}");

        }
        private void AssignAnimalsBonus(Player player, string animal1, string animal2)
        {
            int number = 0;
            switch (animal1)
            {
                case "rabbit":
                    number = (player.rabbit == 0) ? 1 : (player.rabbit == 1) ? 1 : player.rabbit / 2;
                    if (number > rabbits) {
                        number = rabbits;
                    }
                    player.rabbit += number;
                    rabbits -= number;

                    break;
                case "sheep":
                    number = (player.sheep == 0) ? 1 : (player.sheep == 1) ? 1 : player.sheep / 2;
                    if (number > sheeps)
                    {
                        number = sheeps;
                    }
                    player.sheep += number;
                    sheeps -= number;
                    break;
                case "pig":
                    number = (player.pig == 0) ? 1 : (player.pig == 1) ? 1 : player.pig / 2;
                    if (number > pigs)
                    {
                        number = pigs;
                    }
                    player.pig += number;
                    pigs -= number;
                    break;
                case "cow":
                    number = (player.cow == 0) ? 1 : (player.cow == 1) ? 1 : player.cow / 2;
                    if (number > cows)
                    {
                        number = cows;
                    }
                    player.cow += number;
                    cows -= number;
                    break;
                case "horse":
                    number = (player.horse == 0) ? 1 : (player.horse == 1) ? 1 : player.horse / 2;
                    if (number > horses)
                    {
                        number = horses;
                    }
                    player.horse += number;
                    horses -= number;
                    break;
            }
        }
        private void AssignAnimals(Player player, string animal)
        {
            int number = 0;
            switch (animal)
            {
                case "rabbit":
                    number = (player.rabbit == 0) ? 0 : (player.rabbit == 1) ? 1 : (player.rabbit % 2 != 0) ? player.rabbit / 2 + 1 : player.rabbit / 2;
                    if (number > rabbits)
                    {
                        number = rabbits;
                    }
                    player.rabbit += number;
                    break;
                case "sheep":
                    number = (player.sheep == 0) ? 0 : (player.sheep == 1) ? 1 : (player.sheep % 2 != 0) ? player.sheep / 2 + 1 : player.sheep / 2;
                    if (number > sheeps)
                    {
                        number = sheeps;
                    }
                    player.sheep += number;
                    break;
                case "pig":
                    number = (player.pig == 0) ? 0 : (player.pig == 1) ? 1 : (player.pig % 2 != 0) ? player.pig / 2 + 1 : player.pig / 2;
                    if (number > pigs)
                    {
                        number = pigs;
                    }
                    player.pig += number;
                    break;
                case "cow":
                    number = (player.cow == 0) ? 0 : (player.cow == 1) ? 1 : (player.cow % 2 != 0) ? player.cow / 2 + 1 : player.cow / 2;
                    if (number > cows)
                    {
                        number = cows;
                    }
                    player.cow += number;
                    break;
                case "horse":
                    number = (player.horse == 0) ? 0 : (player.horse == 1) ? 1 : (player.horse % 2 != 0) ? player.horse / 2 + 1 : player.horse / 2;
                    if (number > horses)
                    {
                        number = horses;
                    }
                    player.horse += number;
                    break;
            }
        }
        public void Trade(Player player)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(player);
                Console.WriteLine("\nWhat do you give:\n1.Rabbit\n2.Sheep\n3.Pig\n4.Cow\n5.Horse\n6.Small dog\n7.Large dog\nTO QUIT PRESS 8\n");
                string option = Console.ReadLine();
                if (option == "8")
                {
                    break;
                }
                Console.WriteLine("What do you want:\n1.Rabbit\n2.Sheep\n3.Pig\n4.Cow\n5.Horse\n6.Small dog\n7.Large dog\n");
                string option2 = Console.ReadLine();
                if (option == "1" && option2 == "2")
                {
                    if (player.rabbit > 5 && sheeps >= 1)
                    {
                        player.rabbit -= 6;
                        player.sheep += 1;
                        rabbits += 6;
                        sheeps -= 1;
                    }
                }
                if (option == "2" && option2 == "1")
                {
                    if (player.sheep > 0 && rabbits >= 6)
                    {
                        player.rabbit += 6;
                        player.sheep -= 1;
                        rabbits -= 6;
                        sheeps += 1;
                    }
                }
                if (option == "3" && option2 == "2")
                {
                    if (player.pig > 0 && sheeps >= 2)
                    {
                        player.pig -= 1;
                        player.sheep += 2;
                        pigs += 1;
                        sheeps -= 2;
                    }
                }
                if (option == "2" && option2 == "3")
                {
                    if (player.sheep > 1 && pigs >= 1)
                    {
                        player.pig += 1;
                        player.sheep -= 2;
                        pigs -= 1;
                        sheeps += 2;
                    }
                }
                if (option == "4" && option2 == "3")
                {
                    if (player.cow > 0 && pigs >= 3)
                    {
                        player.cow -= 1;
                        player.pig += 3;
                        cows += 1;
                        pigs -= 3;
                    }
                }
                if (option == "3" && option2 == "4")
                {
                    if (player.pig > 2 && cows >= 1)
                    {
                        player.cow += 1;
                        player.pig -= 3;
                        cows -= 1;
                        pigs += 3;
                    }
                }
                if (option == "5" && option2 == "4")
                {
                    if (player.horse > 0 && cows >= 2)
                    {
                        player.horse -= 1;
                        player.cow += 2;
                        horses += 1;
                        cows -= 2;
                    }
                }
                if (option == "4" && option2 == "5")
                {
                    if (player.cow > 1 && horses >= 1)
                    {
                        player.horse += 1;
                        player.cow -= 2;
                        horses -= 1;
                        cows += 2;
                    }
                }
                if (option == "6" && option2 == "2")
                {
                    if (player.smallDog > 0 && sheeps >= 1)
                    {
                        player.smallDog -= 1;
                        player.sheep += 1;
                        smallDogs += 1;
                        sheeps -= 1;
                    }
                }
                if (option == "2" && option2 == "6")
                {
                    if (player.sheep > 0 && smallDogs >= 1)
                    {
                        player.smallDog += 1;
                        player.sheep -= 1;
                        smallDogs -= 1;
                        sheeps += 1;
                    }
                }
                if (option == "4" && option2 == "7")
                {
                    if (player.cow > 0 && largeDogs >= 1)
                    {
                        player.cow -= 1;
                        player.largeDog += 1;
                        cows += 1;
                        largeDogs -= 1;
                    }
                }
                if (option == "7" && option2 == "4")
                {
                    if (player.largeDog > 0 && cows >= 1)
                    {
                        player.cow += 1;
                        player.largeDog -= 1;
                        cows -= 1;
                        largeDogs += 1;
                    }
                }
            }
        }
    }
}
