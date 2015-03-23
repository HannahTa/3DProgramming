using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;

namespace RaceGame
{
    class MidGem : Gem
    {
        // Initialize the physiscs engine and add gravity and friction forces to it
        // At the end of the loadModel of the children classes you should pass the gameNode
        // to the physObj through the propert SceneNode of the physics object

        // Write in the Update method the cde for collision detection with the player as
        // explained in the video, just use the remove fild from Collectable setting it to
        // true, use the method increase from the score object to change the score and add
        // a Dispose() call before the break statement

        public MidGem(SceneManager mSceneMgr, Stat score):base(mSceneMgr, score)
        {
            increase = 100;
            LoadModel();
        }

        protected override void LoadModel()
        {
            base.LoadModel();
            // Load the geometry for the power up (gem) and the scene graph 
            // nodes for it using as usual the gameNode and gameEntity
        }
    }
}
