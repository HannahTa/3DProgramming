using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;

namespace RaceGame
{
    class GemSmall : Gem
    {
        public GemSmall(SceneManager mSceneMgr, Stat score):base(mSceneMgr, score)
        {
            // Initialize fields and call LoadModel method

            this.increase = 10; // Increase by 10 per small gem

            LoadModel();
        }

        protected override void LoadModel()
        {
            // Load geometry for the power up and scene graph nodes for it using as usual the gameNode and game Entity
            base.LoadModel();

            // gameNode/Entity
        }
    }
}
