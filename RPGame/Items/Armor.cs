using RPGame.Model.Characters;
using RPGame_Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGame.Items
{
    public class Armor : Item
    {
        public int DefencePoints { get; set; }
        public int AgilityPoints { get; set; }

        private static List<string> ArmorNameList = new List<string>
        {
            "Plate armor",
            "Chainmail",
            "Splint armor",
            "Brigandine",
            "Leather armor",
            "Adamant armor",
            "Padded armor",
            "Chain shirt",
            "Breastplate",
            "Gold set armor",
            "Helmet",
            "Gauntlets",
            "Greaves",
            "Sabatons",
            "Shield",
            "Cloak",
            "Plate greaves",
            "Scale gauntlets",
            "Kite shield",
            "Tower shield"
        };
        public Armor(int level, int price, int defencePoints, int agilityPoints)
        {
            Random random = new Random();
            Name = ArmorNameList[random.Next(ArmorNameList.Count)];
            Level = level;
            Price = price;
            DefencePoints = defencePoints;
            AgilityPoints = agilityPoints;
        }

        public static List<Armor> GetArmorList(Player player)
        {
            Random random = new Random();
            List<Armor> armorList = new List<Armor>();
            while (armorList.Count < 5)
            {
                int level = player.Level + armorList.Count / 3;
                int price = level * random.Next(75, 101);
                int defencePoints = random.Next((player.BaseHealthPoints / 4) * level, (player.BaseHealthPoints / 3) * level);
                int agilityPoints = random.Next(1, 3);

                Armor armor = new Armor(level, price, defencePoints, agilityPoints);
                armorList.Add(armor);
            }
            return armorList;
        }

        public static void BuyArmor(Player player, List<Armor> armorList) 
        {
            bool endShoping = false;
            while(!endShoping)
            {
                int n = 0;
                Helpers.ColorWriteLine("\nSelect armor to buy:", ConsoleColor.DarkYellow);
                Console.ForegroundColor = ConsoleColor.Yellow;
                foreach (Armor armor in armorList)
                {
                    n++;
                    Console.Write($"{n}. {armor.Name} | ");
                    Console.Write($"Level: {armor.Level} | ");
                    Console.Write($"Defence: {armor.DefencePoints} | ");
                    Console.Write($"Agility: {armor.AgilityPoints} | ");
                    Console.WriteLine($"Price: {armor.Price}");
                }
                Console.WriteLine("0. Exit to shop menu.");
                Console.ResetColor();
                Helpers.ColorWrite("Select number of Armor to buy: ", ConsoleColor.Cyan);
                if (byte.TryParse(Console.ReadLine(), out byte buyNumber))
                {
                    if (buyNumber == 0)
                    {
                        endShoping = true;
                        Console.WriteLine();
                        break;
                    }
                    else if (buyNumber >0 && buyNumber <= n)
                    {
                        if (player.Coins >= armorList[buyNumber - 1].Price)
                        {
                            if (player.Invenory.WeaponSlot != null)
                            {
                                player.DefencePoints -= player.Invenory.ArmorSlot.DefencePoints;
                                player.AgilityPoints -= player.Invenory.ArmorSlot.AgilityPoints;
                            }
                            player.Invenory.ArmorSlot = armorList[buyNumber - 1];
                            player.Coins -= armorList[buyNumber - 1].Price;
                            player.DefencePoints += armorList[buyNumber - 1].DefencePoints;
                            player.AgilityPoints += armorList[buyNumber - 1].AgilityPoints;
                            Helpers.ColorWriteLine($"\nYou bought strong {armorList[buyNumber-1].Name}!", ConsoleColor.Green);
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
