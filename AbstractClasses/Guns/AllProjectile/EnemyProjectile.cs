using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    class EnemyProjectile : Projectile
    {
        Entity eProjEntity;
        SceneNode eProjNode;

        public EnemyProjectile(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            this.healthDamage = 10;
            this.shieldDamage = 5;
            this.speed = 200;
            this.initialVelocity = speed * this.initialDirection;

            Load();
        }

        protected override void Load()
        {
            eProjEntity = mSceneMgr.CreateEntity("Sphere.mesh");
            eProjNode = mSceneMgr.CreateSceneNode();
            eProjNode.AttachObject(eProjEntity);

            eProjNode.Scale(new Vector3(0.5f, 0.5f, 0.5f));

            mSceneMgr.RootSceneNode.AddChild(eProjNode);

            this.gameNode = eProjNode;

            base.Load();
        }
    }
}
