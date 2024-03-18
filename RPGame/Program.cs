using System.Text;
using RPGame.Model;
using System.Numerics;
using System.Threading;
using RPGame.Model.Characters;
using RPGame.Helpers;

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

            //Console.WriteLine("Create new character.");
            //Console.Write("Enter your name: ");
            //string name = Console.ReadLine();
            string name = "Jedi";

            Player player = Player.CreatePlayer(name);
            bool loose = false;
            while (true)
            {
                if (loose)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{player.CharClass} is dead");
                    Console.ResetColor();
                    // Create player if character is dead
                    Console.WriteLine("Create new character.");
                    player = Player.CreatePlayer(name);
                }
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
                        loose = FightMonster(player);
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
                        Console.WriteLine("Thank you  for the game. Goodbye!");
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
        static bool FightMonster(Player player)
        {
            // Здесь вы можете реализовать механику боя с монстром
            // Создайте монстра, определите условия победы и поражения и обновите состояние игрока.

            List<Monster> monsterList = Monster.CreateMonsterList(player);
            Monster monster = Monster.SelectMonsterToFight(monsterList);
            int startPlayerHealthPoints = player.HealthPoints;
            Console.Clear();
            Console.WriteLine($"Monstr {monster.Name} is coming to fight you!");
            bool winner = false;
            int i = 0;
            while (!winner)
            {
                i++;
                Console.WriteLine($"\nRound {i}");
                monster.HealthPoints -= player.StrengthPoints;
                Console.WriteLine($"The {player.Name} hits the {monster.Name} and deals {player.StrengthPoints} damage!");
                Console.WriteLine($"Monster HP is {monster.HealthPoints}");
                if (monster.HealthPoints <= 0) 
                {
                    winner = true;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{player.Name} winner!");
                    Console.ResetColor();

                    // Update players stats
                    player.ExpPoints += i * 10;
                    player.CheckLevelUp();
                    player.Coins += monster.Coins;
                    player.HealthPoints = player.MaxHealthPoints;
                    continue;
                }

                player.HealthPoints -= monster.StrengthPoints;
                Console.WriteLine($"The {monster.Name} hits the {player.Name} and deals {monster.StrengthPoints} damage!");
                Console.WriteLine($"Player HP is {player.HealthPoints}");
                if (player.HealthPoints <= 0)
                {
                    winner = true;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Monster {monster.Name} winner!");
                    Console.ResetColor();
                    continue;
                }

                Console.Write("Press any key to next round or press 'h' to use health potion: ");
                if (Console.ReadKey().Key == ConsoleKey.H)
                {
                    player.UseHealthPotion();
                }
                Console.WriteLine();
            }

            if (player.HealthPoints <= 0)
            {
                // If character is dead
                return true;
            }
            else
            {
                // Restored character's health point
                player.HealthPoints = player.MaxHealthPoints;
                return false;
            }
        }

        //private static void UseHealthPotion1(Player player)
        //{
        //    if (player.NumberOfHealthPotions <= 0)
        //    {
        //        Console.ForegroundColor = ConsoleColor.Red;
        //        Console.WriteLine("You don't have Health Potions");
        //        Console.ResetColor();
        //    }
        //    else
        //    {
        //        player.NumberOfHealthPotions--;
        //        player.HealthPoints = player.MaxHealthPoints;
        //        Console.ForegroundColor = ConsoleColor.Green;
        //        Console.WriteLine("Health restored!");
        //        Console.ResetColor();
        //        //Console.WriteLine("Press any key to continue the battle");
        //        //Console.ReadKey();
        //    }
        //}

        static void VisitShop(Player player)
        {
            // Здесь вы можете реализовать магазин, где игрок может покупать предметы, оружие и броню.
            Console.Clear();
            Console.WriteLine("Welcome to the shop!");
        }

        static void DisplayInventory(Player player)
        {
            // Здесь вы можете отобразить инвентарь игрока, его текущее здоровье, атаку, золото и другие параметры.
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{player.Name}-{player.CharClass} inventory:");
            Console.WriteLine($"{player.CharClass} level is: {player.Level}");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Attack power: {player.StrengthPoints}");
            Console.WriteLine($"Defence points: {player.DefencePoints}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Number of health potions: {player.NumberOfHealthPotions}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Amount of Gold: {player.Coins}");
            Console.ResetColor();

            Console.WriteLine("Press any key to main menu");
            Console.ReadKey();
        }
    }
}