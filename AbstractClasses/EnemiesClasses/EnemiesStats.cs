using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaceGame
{
    class EnemiesStats : CharacterStats
    {
        Stat score;

        protected override void InitStats()
        {
            base.InitStats();
            score = new Score();

            health.InitValue(100);
        }
    }
}
