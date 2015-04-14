using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    class Health : PowerUp
    {
        Entity healthEntity;
        SceneNode healthNode;

        public Health(SceneManager mSceneMgr, Stat health) : base(mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            increase = 5;
            LoadModel();
        }

        protected override void LoadModel()
        {
            healthEntity = mSceneMgr.CreateEntity("Heart.mesh");

            healthNode = mSceneMgr.CreateSceneNode();
            healthNode.AttachObject(healthEntity);
            healthNode.Scale(5f, 5f, 5f);
            mSceneMgr.RootSceneNode.AddChild(healthNode);

            this.gameNode = healthNode;
            base.LoadModel();
        }

        public override void Dispose()
        {
            Physics.RemovePhysObj(physObj);
            physObj = null;

            base.Dispose();
        }
    }
}
