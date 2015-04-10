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
        PhysObj physObj;
        SceneNode controlNode;

        Entity sProjEntity;
        SceneNode sProjNode;

        public SlowProjectile(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            this.healthDamage = 10;
            this.shieldDamage = 5;
            this.speed = 10;
            this.initialVelocity = speed * this.initialDirection;

            Load();
            this.gameNode = sProjNode;
        }

        protected override void Load()
        {
            base.Load();

            sProjEntity = mSceneMgr.CreateEntity("Sphere.mesh");
            sProjNode = mSceneMgr.CreateSceneNode();
            sProjNode.AttachObject(sProjEntity);

            sProjNode.Scale(new Vector3(5, 5, 5));

            controlNode = mSceneMgr.CreateSceneNode();
            controlNode.AddChild(sProjNode);
            mSceneMgr.RootSceneNode.AddChild(controlNode);

            // Physics
            float radius = 10;
            //controlNode.Position += radius * Vector3.UNIT_Y;
            //sProjNode.Position += radius * Vector3.UNIT_Y;

            physObj = new PhysObj(radius, "SlowProj", 0.1f, 0.7f, 0.3f);
            physObj.SceneNode = controlNode;
            physObj.Position = controlNode.Position;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));
            Physics.AddPhysObj(physObj);
        }

        protected void Update()
        {

        }
    }
}
