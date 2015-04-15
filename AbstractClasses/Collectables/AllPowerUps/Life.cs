using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    class Life : PowerUp
    {
        Entity liveEntity;
        SceneNode liveNode;

        public Life(SceneManager mSceneMgr, Stat life) : base(mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            this.stat = life;
            increase = 1;
            LoadModel();
        }

        protected override void LoadModel()
        {
            liveEntity = mSceneMgr.CreateEntity("Heart.mesh");

            liveNode = mSceneMgr.CreateSceneNode();
            liveNode.AttachObject(liveEntity);
            liveNode.Scale(10f, 10f, 10f);
            mSceneMgr.RootSceneNode.AddChild(liveNode);

            remove = false;

            physObj = new PhysObj(7, "Heart", 0.3f, 0.5f);
            physObj.AddForceToList(new WeightForce(physObj.InvMass));
            physObj.SceneNode = liveNode;
            physObj.Position = liveNode.Position;

            Physics.AddPhysObj(physObj);

            this.gameNode = liveNode;
            //base.LoadModel();
        }

        public override void Dispose()
        {
            Physics.RemovePhysObj(physObj);
            physObj = null;

            base.Dispose();
        }
    }
}
