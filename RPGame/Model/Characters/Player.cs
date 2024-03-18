using System;
using System.Xml.Linq;
namespace RPGame.Model.Characters
{
    public class Player : Character
    {
        public string CharClass { get; set; }
        public int Level { get; set; } = 1;
        public int ExpPoints { get; set; }
        public int ExpToLevelUp { get; set; } = 25;
        public const byte MaxLevel = 10;
        public int NumberOfHealthPotions { get; set; } = 3;
        public int ArmorSlot { get; set; }
        public int WeaponSlot { get; set; }

        public Player(string name, string charClass, int healthPoints, int maxHealthPoints, int strengthPoints, int agilityPoints)
        {
            Name = name;
            HealthPoints = healthPoints;
            StrengthPoints = strengthPoints;
            AgilityPoints = agilityPoints;
            CharClass = charClass;
            MaxHealthPoints = maxHealthPoints;
        }

        public static string[] charClassName = new string[4]
                {
                    "Warrior",
                    "Paladin",
                    "Wizard",
                    "Rogue",
                };

        public static Player CreatePlayer(string name)
        {
            string charClass = "empty";
            int healthPoints = 0;
            int strengthPoints = 0;
            int agilityPoints = 0;
            int maxHealthPoints = 0;

            while (charClass == "empty")
            {
                Console.WriteLine();
                Console.WriteLine("List of characters: ");
                byte i = 0;
                foreach (var className in charClassName)
                {
                    i++;
                    Console.WriteLine($"{i}. {className}");
                }
                Console.WriteLine();
                Console.Write($"{name} select your character number: ");
                string charClassNumber = Console.ReadLine();

                switch (charClassNumber)
                {
                    case "1":
                        charClass = charClassName[0];
                        healthPoints = 140;
                        maxHealthPoints = 140;
                        strengthPoints = 25;
                        agilityPoints = 0;
                        break;
                    case "2":
                        charClass = charClassName[1];
                        healthPoints = 120;
                        maxHealthPoints = 120;
                        strengthPoints = 20;
                        agilityPoints = 2;
                        break;
                    case "3":
                        charClass = charClassName[2];
                        healthPoints = 80;
                        maxHealthPoints = 80;
                        strengthPoints = 30;
                        agilityPoints = 5;
                        break;
                    case "4":
                        charClass = charClassName[3];
                        healthPoints = 100;
                        maxHealthPoints = 100;
                        strengthPoints = 15;
                        agilityPoints = 8;
                        break;

                    default:
                        Console.WriteLine("Incorrect select, try again!");
                        break;
                }
            }
            Console.WriteLine($"You selected {charClass}!");
            Player player = new Player(name, charClass, healthPoints, maxHealthPoints, strengthPoints, agilityPoints);
            return player;
        }

        // Увеличение уровня
        public static Player LevelUp(Player player)
        {
            return player;
        }
    }
}
