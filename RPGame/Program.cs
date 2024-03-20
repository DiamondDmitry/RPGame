using System.Text;
using RPGame.Model;
using System.Numerics;
using System.Threading;
using RPGame.Model.Characters;
using RPGame.Helpers;
using RPGame.Items;

namespace RPGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.Unicode;
            Console.WriteLine("Welcome to the console RPG!");

            // Create new player
            Console.WriteLine("Create new character.");
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            //string name = "Jedi";
            Player player = Player.CreatePlayer(name);

            while (true)
            {
                if (player.HealthPoints > 0 && player.Level < player.MaxLevel)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\nMain menu:");
                    Console.WriteLine("1. Fight with monster");
                    Console.WriteLine("2. Go to the shop");
                    Console.WriteLine("3. Inventory");
                    Console.WriteLine("0. Exit");
                    Console.ResetColor();
                    Console.Write("Choose action: ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            // Start fight with monster
                            FightMonster(player);
                            break;
                        case "2":
                            // Going to the shop
                            VisitShop(player);
                            break;
                        case "3":
                            // Show players inventory
                            DisplayInventory(player);
                            break;
                        case "0":
                            // Game exit
                            Console.WriteLine("Thank you for the game. Goodbye!");
                            Environment.Exit(0);
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\nChoose from Menu!");
                            Console.ResetColor();
                            break;
                    }
                }
                else
                {
                    if (player.Level >= player.MaxLevel)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"The {player.Name}-{player.CharClass} defeated all monsters and became the absolute winner, reaching {player.MaxLevel} level!");
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"The {player.Name}-{player.CharClass} fought bravely, but there were too many monsters!");
                    }

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\nStart new game?");
                    Console.WriteLine("1. Yes");
                    Console.WriteLine("0. No");
                    Console.ResetColor();
                    Console.Write("Choose action: ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            // Create new player after end game
                            Console.Clear();
                            Console.WriteLine("Create new character.");
                            player = Player.CreatePlayer(name);
                            break;
                        case "0":
                            // Game exit
                            Console.WriteLine("Thank you for the game. Goodbye!");
                            Environment.Exit(0);
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\nChoose from Menu!");
                            Console.ResetColor();
                            break;
                    }
                }
            }
        }
        static void FightMonster(Player player)
        {
            List<Monster> packOfMonsters = Monster.GenerateMonsterPack(player);
            Monster monster = Monster.Get5NearbyMonsters(packOfMonsters);

            Console.Clear();
            Console.Write("Monster ");
            Console.ForegroundColor= ConsoleColor.Red;
            Console.Write(monster.Name);
            Console.ResetColor();
            Console.WriteLine(" is coming to fight you!");
            bool winner = false;
            int i = 0;
            while (!winner)
            {
                i++;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\nRound {i}");
                Console.ResetColor();
                
                // Player turn
                monster.HealthPoints -= player.StrengthPoints;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"The {player.Name}-{player.CharClass} hits the {monster.Name} and deals {player.StrengthPoints} damage!");
                Console.ResetColor();
                Console.WriteLine($"Monster health: {monster.HealthPoints}");
                
                // Check if player win
                if (monster.HealthPoints <= 0) 
                {
                    winner = true;
                    var expPoints = i * 2;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{player.Name}-{player.CharClass} winner!");
                    Console.WriteLine($"{player.CharClass} gets {expPoints} experience points");
                    Console.ResetColor();

                    // Update players Level
                    player.ExpPoints += expPoints;
                    player.CheckLevelUp();

                    // Get coins reward
                    player.Coins += monster.Coins;

                    // Restored character's health point after fight
                    player.HealthPoints = player.MaxHealthPoints;

                    DisplayInventory(player);

                    continue;
                }

                // Monster turn
                player.HealthPoints -= monster.StrengthPoints;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"The {monster.Name} hits the {player.Name}-{player.CharClass} and deals {monster.StrengthPoints} damage!");
                Console.ResetColor();
                Console.WriteLine($"Player health: {player.HealthPoints}");
                
                // Check if monster win
                if (player.HealthPoints <= 0)
                {
                    winner = true;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Monster {monster.Name} winner!");
                    Console.ResetColor();
                    continue;
                }

                // Access to health potions
                Console.WriteLine();
                Console.Write("Press any key to next round or press 'h' to use health potion: ");
                if (Console.ReadKey().Key == ConsoleKey.H)
                {
                    player.UseHealthPotion();
                }
            }
        }

        static void VisitShop(Player player)
        {
            // Здесь вы можете реализовать магазин, где игрок может покупать предметы, оружие и броню.
            Console.Clear();
            Console.WriteLine("Welcome to the game shop!");
            Shop shop = new Shop();

        }

        static void DisplayInventory(Player player)
        {
            // Show player's inventory with different parameters.
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{player.Name} - {player.CharClass} inventory:");
            Console.WriteLine($"{player.CharClass} level is: {player.Level}");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Attack power: {player.StrengthPoints}");
            Console.WriteLine($"Defence points: {player.DefencePoints}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Health points: {player.HealthPoints}");
            Console.WriteLine($"Number of health potions: {player.Invenory.NumberOfHealthPotions}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Amount of Gold: {player.Coins}");
            Console.ResetColor();
            Console.WriteLine("---------------------------------------------");

            Console.Write("Press any key to main menu ");
            Console.ReadKey();
            Console.WriteLine();
        }
    }
}