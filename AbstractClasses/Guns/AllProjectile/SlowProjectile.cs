using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    class SlowProjectile : Projectile
    {
        Entity sProjEntity;
        SceneNode sProjNode;

        public SlowProjectile(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            this.healthDamage = 10;
            this.shieldDamage = 5;
            this.speed = 100;
            //this.initialVelocity = speed * this.initialDirection;

            Load();
            //this.gameNode = sProjNode;
        }

        protected override void Load()
        {
            sProjEntity = mSceneMgr.CreateEntity("Sphere.mesh");
            sProjNode = mSceneMgr.CreateSceneNode();
            sProjNode.AttachObject(sProjEntity);

            sProjNode.Scale(new Vector3(0.5f, 0.5f, 0.5f));

            mSceneMgr.RootSceneNode.AddChild(sProjNode);

            this.gameNode = sProjNode;

            base.Load();
        }
    }
}
