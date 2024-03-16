using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGame
{
    public class Player
    {
        public string ClassName { get; set; } = "Warrior";
        public int Level { get; set; } = 1;
        public int ExperiencePoints { get; set; } = 0;
        public int HealthPoints { get; set; } = 100;
        public int StrengthPoints { get; set; } = 20;
        public int Coins { get; set; } = 0;
    }
}
