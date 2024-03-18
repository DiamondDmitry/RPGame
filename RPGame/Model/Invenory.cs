using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGame.Model
{
    public class Invenory
    {
        public string ArmorSlot { get; set; }
        public string WeaponSlot { get; set; }
        public int NumberOfHealthPotions { get; set; } = 3;

        //public static Invenory CreateInventory() 
        //{
        //    Invenory invenory = new Invenory();
        //    return invenory;
        //}

    }
}
