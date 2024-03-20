using System;
using System.Xml.Linq;
namespace RPGame.Model.Characters
{
    public class Player : Character
    {
        public string CharClass { get; set; }
        public int ExpPoints { get; set; }
        public int ExpToLevelUp { get; set; } = 25;
        public byte MaxLevel { get; set; } = 10;
        public Invenory Invenory { get; set; }
        public Player(string name, string charClass, int healthPoints, int maxHealthPoints, int strengthPoints, int agilityPoints, Invenory invenory)
        {
            Name = name;
            HealthPoints = healthPoints;
            StrengthPoints = strengthPoints;
            AgilityPoints = agilityPoints;
            CharClass = charClass;
            MaxHealthPoints = maxHealthPoints;
            Invenory = invenory;
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
                        strengthPoints = 30;
                        agilityPoints = 0;
                        break;
                    case "2":
                        charClass = charClassName[1];
                        healthPoints = 120;
                        maxHealthPoints = 120;
                        strengthPoints = 24;
                        agilityPoints = 2;
                        break;
                    case "3":
                        charClass = charClassName[2];
                        healthPoints = 80;
                        maxHealthPoints = 80;
                        strengthPoints = 30;
                        agilityPoints = 4;
                        break;
                    case "4":
                        charClass = charClassName[3];
                        healthPoints = 100;
                        maxHealthPoints = 100;
                        strengthPoints = 20;
                        agilityPoints = 5;
                        break;

                    default:
                        Console.WriteLine("Incorrect select, try again!");
                        break;
                }
            }
            Console.WriteLine($"You selected {charClass}!");
            Invenory invenory = new Invenory();
            Player player = new Player(name, charClass, healthPoints, maxHealthPoints, strengthPoints, agilityPoints, invenory);
            return player;
        }
    }
}
