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
        public Armor(string name, int price, int defencePoints, int agilityPoints)
        {
            Name = name;
            Price = price;
            DefencePoints = defencePoints;
            AgilityPoints = agilityPoints;
        }
    }
}
