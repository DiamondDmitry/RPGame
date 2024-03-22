using RPGame_Helpers;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace RPGame.Model.Characters
{
    public class Monster : Character
    {
        public Monster(int level, int healthPoints, int strengthPoints, int agilityPoints, int coins)
        {
            Random random = new Random();
            Name = monsterNames[random.Next(monsterNames.Count)];
            Level = level;
            HealthPoints = healthPoints;
            StrengthPoints = strengthPoints;
            AgilityPoints = agilityPoints;
            Coins = coins;
        }

        public static List<string> monsterNames = new List<string>
            {
                "Goblin",
                "Orc",
                "Skeleton",
                "Zombie",
                "Dragon",
                "Giant",
                "Troll",
                "Minotaur",
                "Hydra",
                "Griffin",
                "Harpy",
                "Cyclops",
                "Chimera",
                "Golem",
                "Lich",
                "Vampire",
                "Werewolf",
                "Ghost",
                "Medusa",
                "Manticore",
                "Phoenix",
                "Treant",
                "Siren",
                "Basilisk",
                "Naga",
                "Gargoyle",
                "Centaur",
                "Ogre",
                "Black Knight",
                "Bone Dragon"
            };

        public static List<Monster> GenerateMonsterPack(Player player)
        {
            Random random = new Random();
            List<Monster> packOfMonsters = new List<Monster>();

            while(packOfMonsters.Count < 15)
            {
                int level = player.Level + packOfMonsters.Count/5;
                if (level > player.MaxLevel) level = player.MaxLevel;

                int healthPoints = random.Next((player.HealthPoints / player.Level) * level, (player.HealthPoints / player.Level) * level * 2);
                int strengthPoints = random.Next((player.StrengthPoints / player.Level) / 2 * level, (player.StrengthPoints / player.Level) * level);
                int agilityPoints = random.Next(1, 4);
                int coins = level * random.Next(50, 200);

                Monster monster = new Monster(level, healthPoints, strengthPoints, agilityPoints, coins);
                packOfMonsters.Add(monster);
            }

            return packOfMonsters;
        }

        public static Monster Get5NearbyMonsters(List<Monster> packOfMonsters)
        {
            Random random = new Random();
            List<Monster> nearbyMonsters = new List<Monster>();

            for (int i = 0; i < 5; i++)
            {
                int rnd = random.Next(packOfMonsters.Count);
                nearbyMonsters.Add(packOfMonsters[rnd]);
                packOfMonsters.RemoveAt(rnd);
            }
            
            int n = 0;
            Console.WriteLine();
            Helpers.ColorWriteLine("Select number of monster to fight:", ConsoleColor.Cyan);
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (Monster monster in nearbyMonsters)
            {
                n++;
                Console.Write($"{n}. {monster.Name} | ");
                Console.Write($"Level: {monster.Level} | ");
                Console.Write($"Health: {monster.HealthPoints} | ");
                Console.Write($"Power: {monster.StrengthPoints} | ");
                Console.Write($"Agility: {monster.AgilityPoints} | ");
                Console.WriteLine($"Reward: {monster.Coins}");
            }
            Console.ResetColor();
            byte.TryParse(Console.ReadLine(), out byte monsterNumber);

            try
            {
                return nearbyMonsters[monsterNumber - 1];
            }
            catch (Exception)
            {
                Helpers.ColorWriteLine("Incorrect choice, you were attacked by a random monster.", ConsoleColor.Magenta);
                Console.ReadKey();
                return packOfMonsters[random.Next(packOfMonsters.Count)];
            }
        }
    }
}
