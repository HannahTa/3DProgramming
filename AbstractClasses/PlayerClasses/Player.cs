using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;

using PhysicsEng;

namespace RaceGame
{
    class Player : Character
    {
        // Fields
        //PlayerModel model;
        //PlayerController controller;
        //PlayerStats stats;

        //PhysObj physObj;
        //SceneNode controlNode;

        public Player(SceneManager mSceneMgr)
        {
            model = new PlayerModel(mSceneMgr);
            controller = new PlayerController(this);
            stats = new PlayerStats();
        }

        public override void Update(FrameEvent evt)
        {
            //model.Animate();
            controller.Update(evt);
            base.Update(evt);
        }
        
        public override void Shoot()
        {
            base.Shoot();
        }
    }
}
