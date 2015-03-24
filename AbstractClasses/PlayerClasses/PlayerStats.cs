using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaceGame
{
    class PlayerStats : CharacterStats
    {
        private Stat score;

        public Stat Score
        {
            get { return score; }
        }

        protected override void InitStats()
        {
            base.InitStats();
            score = new Score();

            score.InitValue(0);
            health.InitValue(100);
            shield.InitValue(100);
            lives.InitValue(3);
        }
    }
}
