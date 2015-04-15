using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    class DoubleScore : PowerUp
    {
        Entity doubleScoreEntity;
        SceneNode doubleScoreNode;

        // If PowerUp affects the health of the player,
        // add another parameter a Stat object called Health
        public DoubleScore(SceneManager mSceneMgr, Stat shield) : base(mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            this.stat = shield;
            increase = 50;
            // initialize the stat field stat.Value = 10;
            LoadModel();
        }

        protected override void LoadModel()
        {
            doubleScoreEntity = mSceneMgr.CreateEntity("knot.mesh");
            
            doubleScoreNode = mSceneMgr.CreateSceneNode();
            doubleScoreNode.AttachObject(doubleScoreEntity);
            doubleScoreNode.Scale(new Vector3(0.09f, 0.09f, 0.09f));
            mSceneMgr.RootSceneNode.AddChild(doubleScoreNode);

            remove = false;

            physObj = new PhysObj(7, "Double", 0.3f, 0.5f);
            physObj.AddForceToList(new WeightForce(physObj.InvMass));
            physObj.SceneNode = doubleScoreNode;
            physObj.Position = doubleScoreNode.Position;

            Physics.AddPhysObj(physObj);

            this.gameNode = doubleScoreNode;

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
