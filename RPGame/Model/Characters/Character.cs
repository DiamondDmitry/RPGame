using System.Collections.Generic;

namespace RPGame.Model.Characters
{
    public class Character
    {
        public string Name { get; set; }
        public int Level { get; set; } = 1;
        public int HealthPoints { get; set; }
        public int MaxHealthPoints { get; set; }
        public int StrengthPoints { get; set; }
        public int DefencePoints { get; set; }
        public int AgilityPoints { get; set; }
        public int Coins { get; set; }
    }
}
