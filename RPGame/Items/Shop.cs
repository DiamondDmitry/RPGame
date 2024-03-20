using RPGame.Model.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGame.Items
{
    public class Shop : Item
    {
        public Weapon WeaponList { get; set; }
        public Armor ArmorList { get; set; }
        public int NumberOfHealthPotions { get; set; }
        public static List<Shop> GenerateShop(Player player)
        {
            Random random = new Random();
            List<Shop> shop = new List<Shop>();

            List<Armor> armorList = new List<Armor>();
            List<Weapon> weaponList = new List<Weapon>();

            while (armorList.Count < 5)
            {
                //int level = player.Level + packOfMonsters.Count / 5;

                //string name = random.Next((player.HealthPoints / player.Level) * level, (player.HealthPoints / player.Level) * level * 2);
                //int price = random.Next((player.StrengthPoints / player.Level) / 2 * level, (player.StrengthPoints / player.Level) * level);
                //int defencePoints = random.Next(1, 4);
                //int agilityPoints = level * random.Next(50, 200);

                //Armor armor = new Armor(name, price, defencePoints, agilityPoints);
                //armorList.Add(armor);
            }

            return shop;
        }
    }
}
