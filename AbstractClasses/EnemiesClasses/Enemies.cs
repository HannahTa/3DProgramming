using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;

namespace RaceGame
{
    class Enemies : Character
    {
        // Fields
        //EnemiesModel model;
        //EnemiesController controller;
        //EnemiesStats stats;

        public Enemies(SceneManager mSceneMgr)
        {
            model = new EnemiesModel(mSceneMgr);
            controller = new EnemiesController(this);
            stats = new EnemiesStats();
        }

        public override void Update(FrameEvent evt)
        {
            //model.Animate();
            controller.Update(evt);
            //model.RemoveMe
            base.Update(evt);
        }

        public override void Shoot()
        {
            base.Shoot();
        }
    }
}
