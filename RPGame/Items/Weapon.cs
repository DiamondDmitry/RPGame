using RPGame.Model.Characters;
using RPGame_Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RPGame.Items
{
    public class Weapon : Item
    {
        public int StrengthPoints { get; set; } = 0;
        public int AgilityPoints { get; set; } = 0;

        private static List<string> WeaponsNameList = new List<string>
        {
            "Longsword",
            "Katana",
            "Rapier",
            "Scimitar",
            "Dagger",
            "Battle-axe",
            "Warhammer",
            "Mace",
            "Spear",
            "Glaive",
            "Claymore",
            "Greatsword",
            "Morningstar",
            "Trident",
            "Tomahawk",
            "Machete",
            "War pick"
        };
        public Weapon(int level, int price, int strengthPoints, int agilityPoints)
        {
            Random random = new Random();
            Name = WeaponsNameList[random.Next(WeaponsNameList.Count)];
            Level = level;
            Price = price;
            StrengthPoints = strengthPoints;
            AgilityPoints = agilityPoints;
        }

        public static List<Weapon> GetWeaponList(Player player)
        {
            Random random = new Random();
            List<Weapon> weaponList = new List<Weapon>();
            while (weaponList.Count < 5)
            {
                int level = player.Level + weaponList.Count / 3;
                int price = level * random.Next(75, 101);
                int strengthPoints = random.Next((player.BaseStrengthPoints / 4) * level, (player.BaseStrengthPoints / 3) * level);
                int agilityPoints = random.Next(1, 3);

                Weapon weapon = new Weapon(level, price, strengthPoints, agilityPoints);
                weaponList.Add(weapon);
            }
            return weaponList;
        }

        public static void BuyWeapon(Player player, List<Weapon> weaponList)
        {
            bool endShoping = false;
            while (!endShoping)
            {
                int n = 0;
                Helpers.ColorWriteLine("\nList of avaible weapons:", ConsoleColor.DarkYellow);
                Console.ForegroundColor = ConsoleColor.Yellow;
                foreach (Weapon weapon in weaponList)
                {
                    n++;
                    Console.Write($"{n}. {weapon.Name} | ");
                    Console.Write($"Level: {weapon.Level} | ");
                    Console.Write($"Power: {weapon.StrengthPoints} | ");
                    Console.Write($"Agility: {weapon.AgilityPoints} | ");
                    Console.WriteLine($"Price: {weapon.Price}");
                }
                Console.WriteLine("0. Exit to shop menu.");
                Console.ResetColor();
                Helpers.ColorWrite("Select number of weapon to buy: ", ConsoleColor.Cyan);
                if (byte.TryParse(Console.ReadLine(), out byte buyNumber))
                {
                    if (buyNumber == 0)
                    {
                        endShoping = true;
                        break;
                    }
                    else if (buyNumber > 0 && buyNumber <= n)
                    {
                        if (player.Coins >= weaponList[buyNumber - 1].Price)
                        {
                            if (player.Invenory.WeaponSlot != null)
                            {
                                player.StrengthPoints -= player.Invenory.WeaponSlot.StrengthPoints;
                                player.AgilityPoints -= player.Invenory.WeaponSlot.AgilityPoints;
                            }
                            player.Invenory.WeaponSlot = weaponList[buyNumber - 1];
                            player.Coins -= weaponList[buyNumber - 1].Price;
                            player.StrengthPoints += weaponList[buyNumber - 1].StrengthPoints;
                            player.AgilityPoints += weaponList[buyNumber - 1].AgilityPoints;
                            Helpers.ColorWriteLine($"\nYou bought strong {weaponList[buyNumber - 1].Name}!", ConsoleColor.Green);
                            Console.WriteLine();
                            endShoping = true;
                            break;
                        }
                        Helpers.ColorWriteLine("\nYou don't have enough money to buy this item!", ConsoleColor.DarkYellow);
                        endShoping = false;
                    }
                    else
                    {
                        Helpers.ColorWriteLine("\nIncorrect choice, select from armor list.", ConsoleColor.Red);
                        endShoping = false;
                    }
                }
                else
                {
                    Helpers.ColorWriteLine("\nIncorrect choice, select from armor list.", ConsoleColor.Red);
                    endShoping = false;
                }
            }
        }

    }
}