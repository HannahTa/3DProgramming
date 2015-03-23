using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaceGame
{
    class Score : Stat
    {
        public override void Increase(int val)
        {
            value = value + val;
        }
    }
}
