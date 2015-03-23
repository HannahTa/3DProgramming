using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;

namespace RaceGame 
{
    class PowerUpSpeedGun : PowerUp
    {
        public PowerUpSpeedGun(SceneManager mSceneMgr, Stat Health):base(mSceneMgr)
        {
            // Initialize the state field inhearited from PowerUp and
            // increase field also inherited from PowerUp
        }

        protected override void LoadModel()
        {
            // Load the geometry for the power up and the scene graph nodes using gameNode/gameEntity

            base.LoadModel();
        }
    }
}
