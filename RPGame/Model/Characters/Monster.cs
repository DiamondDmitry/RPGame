using System.Collections.Generic;
using System.Xml.Linq;

namespace RPGame.Model.Characters
{
    public class Monster : Character
    {
        public Monster(string name, int healthPoints, int strengthPoints, int agilityPoints, int coins)
        {
            Name = name;
            HealthPoints = healthPoints;
            StrengthPoints = strengthPoints;
            AgilityPoints = agilityPoints;
            Coins = coins;
        }

        public static List<string> monsterNames = new List<string>
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

        public static List<Monster> CreateMonsterList(Player player)
        {
            Random random = new Random();
            List<Monster> monsterList = new List<Monster>();

            for (int i = 0; i < 5; i++)
            {
                string name = monsterNames[random.Next(monsterNames.Count)];
                int healthPoints = random.Next(player.HealthPoints, player.HealthPoints * 2);
                int strengthPoints = random.Next(player.StrengthPoints / 2, player.StrengthPoints);
                int agilityPoints = random.Next(1, 3);
                int coins = player.Level * random.Next(50, 200);
                Monster monster = new Monster(name, healthPoints, strengthPoints, agilityPoints, coins);
                monsterList.Add(monster);
            }

            return monsterList;
        }

        public static Monster SelectMonsterToFight(List<Monster> monsterList)
        {
            byte i = 0;
            Console.WriteLine("Select number of monster to fight:");
            foreach (Monster monster in monsterList)
            {
                i++;
                Console.Write($"{i}. {monster.Name} | ");
                Console.Write($"Health: {monster.HealthPoints} | ");
                Console.Write($"Power: {monster.StrengthPoints} | ");
                Console.Write($"Agility: {monster.AgilityPoints} | ");
                Console.WriteLine($"Reward: {monster.Coins}");
            }
            byte.TryParse(Console.ReadLine(), out byte monsterNumber);

            return monsterList[monsterNumber - 1];

        }
    }
}
