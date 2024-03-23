using System.Text;
using RPGame.Model;
using System.Numerics;
using System.Threading;
using RPGame.Model.Characters;
using RPGame_Helpers;
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
            //Console.WriteLine("Create new character.");
            //Console.Write("Enter your name: ");
            //string name = Console.ReadLine();
            string name = "Jedi";
            Player player = Player.CreatePlayer(name);
            
            while (true)
            {
                if (player.HealthPoints > 0 && player.Level < player.MaxLevel)
                {
                    Helpers.ColorWriteLine("\nMain menu:", ConsoleColor.Cyan);
                    Console.ForegroundColor = ConsoleColor.Yellow;
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
                            Helpers.ColorWriteLine("Thank you for the game. Goodbye!", ConsoleColor.Cyan);
                            Environment.Exit(0);
                            break;
                        default:
                            Helpers.ColorWriteLine("\nIncorrect select, choose from Menu!", ConsoleColor.Yellow);
                            break;
                    }
                }
                else
                {
                    if (player.Level >= player.MaxLevel)
                    {
                        Console.Clear();
                        Helpers.ColorWriteLine($"The {player.Name}-{player.CharClass} defeated all monsters and became the absolute winner, reaching {player.MaxLevel} level!", ConsoleColor.Green);
                    }
                    else
                    {
                        Console.Clear();
                        Helpers.ColorWriteLine($"The {player.Name}-{player.CharClass} fought bravely, but there were too many monsters!", ConsoleColor.Red);
                    }

                    Helpers.ColorWriteLine("\nStart new game?", ConsoleColor.Cyan);
                    Console.ForegroundColor = ConsoleColor.Yellow;
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
                            Helpers.ColorWriteLine("Thank you for the game. Goodbye!", ConsoleColor.Cyan);
                            Environment.Exit(0);
                            break;
                        default:
                            Helpers.ColorWriteLine("\nIncorrect select, choose from Menu!", ConsoleColor.Yellow);
                            break;
                    }
                }
            }
        }
        static void FightMonster(Player player)
        {
            List<Monster> packOfMonsters = Monster.GenerateMonsterPack(player);
            List<Monster> nearbyMonsters = Monster.FindNearbyMonsters(packOfMonsters);
            Monster monster = Monster.SelectMonsterToFight(nearbyMonsters);

            //winner = Helpers.Fight(player, monster);

            Console.Clear();
            Console.Write("Monster ");
            Helpers.ColorWrite(monster.Name, ConsoleColor.Red);
            Console.WriteLine(" is coming to fight you!");
            bool winner = false;
            int i = 0;
            while (!winner)
            {
                i++;
                Helpers.ColorWriteLine($"\nRound {i}", ConsoleColor.Cyan);
                
                // Player turn
                monster.HealthPoints -= player.StrengthPoints;
                Helpers.ColorWriteLine($"The {player.Name}-{player.CharClass} hits the {monster.Name} and deals {player.StrengthPoints} damage!", ConsoleColor.Yellow);
                Console.WriteLine($"Monster health left: {monster.HealthPoints}");
                
                // Check if player win
                if (monster.HealthPoints <= 0) 
                {
                    winner = true;
                    var expPoints = i * (monster.Level - player.Level + 1) * 2;
                    Helpers.ColorWriteLine($"{player.Name}-{player.CharClass} winner!", ConsoleColor.Green);
                    Helpers.ColorWriteLine($"{player.CharClass} gets {expPoints} experience points", ConsoleColor.Green);

                    // Update players Level
                    player.ExpPoints += expPoints;
                    Player.CheckLevelUp(player);

                    // Get coins reward
                    player.Coins += monster.Coins;

                    // Restored character's health point after win
                    player.HealthPoints = player.MaxHealthPoints;

                    DisplayInventory(player);

                    continue;
                }

                // Monster turn
                player.HealthPoints -= monster.StrengthPoints;
                Helpers.ColorWriteLine($"The {monster.Name} hits the {player.Name}-{player.CharClass} and deals {monster.StrengthPoints} damage!", ConsoleColor.Yellow);
                Console.WriteLine($"Player health left: {player.HealthPoints}");
                
                // Check if monster win
                if (player.HealthPoints <= 0)
                {
                    winner = true;
                    Helpers.ColorWriteLine($"Monster {monster.Name} winner!", ConsoleColor.Red);
                    continue;
                }

                // Access to health potions
                Console.WriteLine();
                Console.Write("Press any key to next round or press 'h' to use health potion: ");
                if (Console.ReadKey().Key == ConsoleKey.H)
                {
                    Player.UseHealthPotion(player);
                }
            }
        }

        static void VisitShop(Player player)
        {
            // Create shop with game items, like weapons, armor, health potions etc..
            List<Armor> armorList = Armor.GetArmorList(player);
            List<Weapon> weaponList = Weapon.GetWeaponList(player);
            Console.Clear();
            bool shoping = false;
            while (!shoping)
            {
                Helpers.ColorWriteLine("Welcome to the game shop!", ConsoleColor.Cyan);
                Helpers.ColorWriteLine($"You have {player.Coins} coins.", ConsoleColor .DarkYellow);
                Helpers.ColorWriteLine("What would you like to buy?", ConsoleColor.Cyan);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("1. List armors");
                Console.WriteLine("2. List weapons");
                Console.WriteLine("3. Buy health potion for 100 coins");
                Console.WriteLine("0. Exit");
                Console.ResetColor();
                Console.Write("Choose action: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Armor.BuyArmor(player, armorList);
                        break;
                    case "2":
                        Weapon.BuyWeapon(player, weaponList);
                        break;
                    case "3":
                        player.Invenory.NumberOfHealthPotions++;
                        break;
                    case "0":
                        Helpers.ColorWriteLine("Thank you for shoping!", ConsoleColor.Cyan);
                        shoping = true;
                        break;
                    default:
                        Helpers.ColorWriteLine("\nIncorrect select, choose from shop menu!", ConsoleColor.Yellow);
                        break;
                }
            }
        }

        static void DisplayInventory(Player player)
        {
            // Show player's inventory with different parameters.

            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------------------------");
            Helpers.ColorWriteLine($"{player.Name} - {player.CharClass} inventory:", ConsoleColor.Cyan);
            Helpers.ColorWriteLine($"{player.CharClass} level is: {player.Level}", ConsoleColor.DarkYellow);
            Helpers.ColorWriteLine($"Experience to next level: {player.ExpToLevelUp-player.ExpPoints}", ConsoleColor.Cyan);
            Helpers.ColorWriteLine($"Attack power: {player.StrengthPoints}", ConsoleColor.Yellow);
            Helpers.ColorWriteLine($"Defence points: {player.DefencePoints}", ConsoleColor.Blue);
            Helpers.ColorWriteLine($"Agility points: {player.AgilityPoints}", ConsoleColor.Yellow);
            Helpers.ColorWriteLine($"Health points: {player.HealthPoints}", ConsoleColor.Red);
            Helpers.ColorWriteLine($"Number of health potions: {player.Invenory.NumberOfHealthPotions}", ConsoleColor.Green);
            Helpers.ColorWriteLine($"Amount of Gold: {player.Coins}", ConsoleColor.DarkYellow);
            Console.WriteLine("----------------------------------------------------------------------");
            if (player.Invenory.ArmorSlot != null)
            {
                Helpers.ColorWriteLine($"Armor slot: {player.Invenory.ArmorSlot.Name}, " +
                                       $"Level: {player.Invenory.ArmorSlot.Level}, " +
                                       $"Defence: {player.Invenory.ArmorSlot.DefencePoints}, " +
                                       $"Agility: {player.Invenory.ArmorSlot.AgilityPoints}", ConsoleColor.Yellow);
                Console.WriteLine("----------------------------------------------------------------------");
            }
            if (player.Invenory.WeaponSlot != null)
            {
                Helpers.ColorWriteLine($"Weapon slot: {player.Invenory.WeaponSlot.Name}, " +
                                       $"Level: {player.Invenory.WeaponSlot.Level}, " +
                                       $"Attack: {player.Invenory.WeaponSlot.StrengthPoints}, " +
                                       $"Agility: {player.Invenory.WeaponSlot.AgilityPoints}", ConsoleColor.Yellow);
                Console.WriteLine("----------------------------------------------------------------------");
            }

            //Console.Write("Press any key to main menu ");
            //Console.ReadKey();
            Console.WriteLine();
        }
    }
}