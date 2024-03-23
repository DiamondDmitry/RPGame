﻿using RPGame.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGame.Model
{
    public class Invenory
    {
        public Weapon WeaponSlot { get; set; }
        public Armor ArmorSlot { get; set; }
        public int NumberOfHealthPotions { get; set; } = 3;
    }
}
