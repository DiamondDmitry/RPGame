﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGame.Model.Characters;

namespace RPGame.Helpers
{
    public static class Helpers
    {
        public static Player UseHealthPotion(this Player player)
        {
            Console.WriteLine();
            if (player.NumberOfHealthPotions <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You don't have Health Potions");
                Console.ResetColor();
            }
            else
            {
                player.NumberOfHealthPotions--;
                player.HealthPoints = player.MaxHealthPoints;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Health restored!");
                Console.ResetColor();
                //Console.WriteLine("Press any key to continue the battle");
                //Console.ReadKey();
            }
            return player;
        }
        public static Player CheckLevelUp(this Player player)
        {
            while (player.ExpPoints >= player.ExpToLevelUp) 
            {
                player.ExpPoints -= player.ExpToLevelUp;
                player.Level++;
                Console.ForegroundColor= ConsoleColor.Green;
                Console.WriteLine($"Level {player.CharClass} is UP!");
                Console.WriteLine($"Current level is: {player.Level}");
                Console.ResetColor();
            }
            return player;
        }
    }
}