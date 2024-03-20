using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGame.Items
{
    public class Weapon : Item
    {
        public int StrengthPoints { get; set; } = 0;
        public int AgilityPoints { get; set; } = 0;
        public Weapon(string name, int price, int strengthPoints, int agilityPoints)
        {
            Name = name;
            Price = price;
            StrengthPoints = strengthPoints;
            AgilityPoints = agilityPoints;
        }
    }
}