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
            Name = MonsterNamesList[random.Next(MonsterNamesList.Count)];
            Level = level;
            HealthPoints = healthPoints;
            StrengthPoints = strengthPoints;
            AgilityPoints = agilityPoints;
            Coins = coins;
        }

        private static List<string> MonsterNamesList = new List<string>
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
                int level = player.Level + packOfMonsters.Count/7;
                if (level > player.MaxLevel) level = player.MaxLevel;

                int healthPoints = random.Next(player.BaseHealthPoints * level * 2, player.BaseHealthPoints * level * 3);
                int strengthPoints = random.Next((player.BaseStrengthPoints / 2) * level, player.BaseStrengthPoints * level);
                int agilityPoints = random.Next(1, 4);
                int coins = level * random.Next(50, 100);

                Monster monster = new Monster(level, healthPoints, strengthPoints, agilityPoints, coins);
                packOfMonsters.Add(monster);
            }

            return packOfMonsters;
        }

        public static List<Monster> FindNearbyMonsters(List<Monster> packOfMonsters)
        {
            Random random = new Random();
            List<Monster> nearbyMonsters = new List<Monster>();

            for (int i = 0; i < 5; i++)
            {
                int rnd = random.Next(packOfMonsters.Count);
                nearbyMonsters.Add(packOfMonsters[rnd]);
                packOfMonsters.RemoveAt(rnd);
            }

            return nearbyMonsters;
        }
        
        public static Monster SelectMonsterToFight(List<Monster> nearbyMonsters)
        {
            Random random = new Random();
            int n = 0;
            Console.WriteLine();
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

            Helpers.ColorWriteLine("Select number of monster to fight:", ConsoleColor.Cyan);
            byte.TryParse(Console.ReadLine(), out byte monsterNumber);
            try
            {
                return nearbyMonsters[monsterNumber - 1];
            }
            catch (Exception)
            {
                Helpers.ColorWriteLine("Incorrect choice, you were attacked by a random monster.", ConsoleColor.Red);
                //Console.ReadKey();
                return nearbyMonsters[random.Next(nearbyMonsters.Count)];
            }
        }
    }
}
