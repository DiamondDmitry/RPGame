using System.Numerics;
using System.Reflection;
using System.Text;

namespace RPGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.Unicode;
            Console.WriteLine("Welcome to the console RPG!");

            // Create player
            Player player = new Player();

            List<string> monsterNames = new List<string>
            {
                "Goblin",
                "Orc",
                "Gargoyle",
                "Centaur",
                "Ogre",
                "Cyclops",
                "Behemoth",
                "Dragon",
                "Hydra",
                "Griffin",
                "Minotaur",
                "Harpy",
                "Medusa",
                "Black Knight",
                "Bone Dragon",
            };


            while (true)
            {
                Console.WriteLine("\nMain menu:");
                Console.WriteLine("1. Fight with monster");
                Console.WriteLine("2. Go to the shop");
                Console.WriteLine("3. Inventory");
                Console.WriteLine("0. Exit");

                Console.Write("Choose action: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        // Start fight with monster
                        FightMonster(player, monsterNames);
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

        static void FightMonster(Player player, List<string> monsterNames)
        {
            // Здесь вы можете реализовать механику боя с монстром
            // Создайте монстра, определите условия победы и поражения и обновите состояние игрока.
            Console.Clear();
            Monster monster = new Monster();
            monster = CreateMonster(player, monsterNames);
            Console.WriteLine($"Monstr {monster.Name} is coming to fight you!");
            Console.WriteLine("Fight is starting");

            while (monster.HealthPoints > 0 || player.HealthPoints > 0)
            {
                monster.HealthPoints -= player.StrengthPoints;
                Console.WriteLine($"The player hits the {monster.StrengthPoints} and deals {player.StrengthPoints} damage!");

                if (monster.HealthPoints <= player.StrengthPoints) 
                {
                    monster.HealthPoints -= player.StrengthPoints;
                    Console.WriteLine($"{player.ClassName} winner!");
                    break;
                }

                player.HealthPoints -= monster.StrengthPoints;
                Console.WriteLine($"The monster hits the {player.ClassName} and deals {monster.StrengthPoints} damage!");

                if (player.HealthPoints <= monster.StrengthPoints)
                {
                    player.HealthPoints -= monster.StrengthPoints;
                    Console.WriteLine($"Monster {monster.Name} winner!");
                    break;
                }
            }
            
            Console.WriteLine($"You have fought a monster {monster.Name}!");
            
            Console.WriteLine(player.HealthPoints);
        }

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
            Console.WriteLine("Player inventory:");
        }

        static Monster CreateMonster(Player player, List<string> monsterNames)
        {
            Random random = new Random();
            Monster monster = new Monster();
            monster.Name = monsterNames[random.Next(monsterNames.Count)];
            monster.HealthPoints = player.HealthPoints;
            monster.StrengthPoints = player.StrengthPoints;

            return monster;
        }
    }
}