using Superfarmer;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main(string[] args)
    {
        List<Player> players = new List<Player>();
        Game game = new Game();
        Player p1 = new Player();
        players.Add(p1);
        Player p2 = new Player();
        players.Add(p2);

        Console.WriteLine("SUPERFARMER");

        Console.WriteLine("Choose number of players: 2, 3 or 4");
        string numPlayers = Console.ReadLine();
        if (numPlayers == "3") {
            Player p3 = new Player();
            players.Add(p3);
        }
        if (numPlayers == "4")
        {
            Player p3 = new Player();
            players.Add(p3);
            Player p4 = new Player();
            players.Add(p4);
        }
        Console.Clear();
        Console.WriteLine("Starting game!");
        int currentPlayerIndex = 0;
        Player currentPlayer = players[currentPlayerIndex];
        while (true)
        {
            currentPlayer = players[currentPlayerIndex];
            Console.WriteLine($"\nTurn: Player {currentPlayerIndex + 1}");
            Console.WriteLine("Your inventory:");
            Console.WriteLine(currentPlayer);
            Console.WriteLine("\n1. Throw dices\n2. Trade\nChoose an option\n");
            string option = Console.ReadLine();
            Console.Clear();
            switch (option)
            {
                case "1":
                    Console.WriteLine("Throwing dices!");
                    game.ThrowDices(currentPlayer);
                    Console.WriteLine($"Player {currentPlayerIndex + 1} inventory:");
                    Console.WriteLine(currentPlayer);
                    break;
                case "2":
                    game.Trade(currentPlayer);
                    Console.WriteLine($"Player {currentPlayer.name} inventory:");
                    Console.WriteLine(currentPlayer);
                    break;
            }
            if (currentPlayer.horse > 0 && currentPlayer.cow > 0 && currentPlayer.pig > 0 && currentPlayer.sheep > 0 && currentPlayer.rabbit > 0)
            {
                Console.WriteLine($"THE END!\nWinner: Player {currentPlayerIndex + 1}");
                break;
            }
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
            Console.WriteLine("\n============ NEW TURN ============");
        }

    }
}
