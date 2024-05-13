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

        private int rabbits;
        private int sheep;
        private int pigs;
        private int cows;
        private int horses;
        private int smallDogs;
        private int largeDogs;

        public Game()
        {
            Reset();
        }

        public void Reset()
        {
            rabbits = 60;
            sheep = 20;
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
                    player.sheep = 0;
                    player.pig = 0;
                    player.cow = 0;
                }
                else
                {
                    player.largeDog -= 1;
                }
                if (animal2 == "fox")
                {
                    if (player.smallDog == 0)
                    {
                        player.rabbit = 1;
                    }
                    else
                    {
                        player.smallDog -= 1;
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
                        player.rabbit = 1;
                    }
                    else
                    {
                        player.smallDog -= 1;
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
                    player.rabbit += number;
                    break;
                case "sheep":
                    number = (player.sheep == 0) ? 1 : (player.sheep == 1) ? 1 : player.sheep / 2;
                    player.sheep += number;
                    break;
                case "pig":
                    number = (player.pig == 0) ? 1 : (player.pig == 1) ? 1 : player.pig / 2;
                    player.pig += number;
                    break;
                case "cow":
                    number = (player.cow == 0) ? 1 : (player.cow == 1) ? 1 : player.cow / 2;
                    player.cow += number;
                    break;
                case "horse":
                    number = (player.horse == 0) ? 1 : (player.horse == 1) ? 1 : player.horse / 2;
                    player.horse += number;
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
                    player.rabbit += number;
                    break;
                case "sheep":
                    number = (player.sheep == 0) ? 0 : (player.sheep == 1) ? 1 : (player.sheep % 2 != 0) ? player.sheep / 2 + 1 : player.sheep / 2;
                    player.sheep += number;
                    break;
                case "pig":
                    number = (player.pig == 0) ? 0 : (player.pig == 1) ? 1 : (player.pig % 2 != 0) ? player.pig / 2 + 1 : player.pig / 2;
                    player.pig += number;
                    break;
                case "cow":
                    number = (player.cow == 0) ? 0 : (player.cow == 1) ? 1 : (player.cow % 2 != 0) ? player.cow / 2 + 1 : player.cow / 2;
                    player.cow += number;
                    break;
                case "horse":
                    number = (player.horse == 0) ? 0 : (player.horse == 1) ? 1 : (player.horse % 2 != 0) ? player.horse / 2 + 1 : player.horse / 2;
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
                    if (player.rabbit > 5)
                    {
                        player.rabbit -= 6;
                        player.sheep += 1;
                    }
                }
                if (option == "2" && option2 == "1")
                {
                    if (player.sheep > 0)
                    {
                        player.rabbit += 6;
                        player.sheep -= 1;
                    }
                }
                if (option == "3" && option2 == "2")
                {
                    if (player.pig > 0)
                    {
                        player.pig -= 1;
                        player.sheep += 2;
                    }
                }
                if (option == "2" && option2 == "3")
                {
                    if (player.sheep > 1)
                    {
                        player.rabbit += 1;
                        player.sheep -= 2;
                    }
                }
                if (option == "4" && option2 == "3")
                {
                    if (player.cow > 0)
                    {
                        player.cow -= 1;
                        player.pig += 3;
                    }
                }
                if (option == "3" && option2 == "4")
                {
                    if (player.pig > 2)
                    {
                        player.cow += 1;
                        player.pig -= 3;
                    }
                }
                if (option == "5" && option2 == "4")
                {
                    if (player.horse > 0)
                    {
                        player.horse -= 1;
                        player.cow += 2;
                    }
                }
                if (option == "4" && option2 == "5")
                {
                    if (player.cow > 1)
                    {
                        player.horse += 1;
                        player.cow -= 2;
                    }
                }
                if (option == "6" && option2 == "2")
                {
                    if (player.smallDog > 0)
                    {
                        player.smallDog -= 1;
                        player.sheep += 1;
                    }
                }
                if (option == "2" && option2 == "6")
                {
                    if (player.sheep > 0)
                    {
                        player.smallDog += 1;
                        player.sheep -= 1;
                    }
                }
                if (option == "4" && option2 == "7")
                {
                    if (player.cow > 0)
                    {
                        player.cow -= 1;
                        player.largeDog += 1;
                    }
                }
                if (option == "7" && option2 == "4")
                {
                    if (player.largeDog > 0)
                    {
                        player.cow += 1;
                        player.largeDog -= 1;
                    }
                }
            }
        }
    }
}
