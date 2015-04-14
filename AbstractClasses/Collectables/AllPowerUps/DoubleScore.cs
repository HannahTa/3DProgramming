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
        //PhysObj physObj;

        //ModelElement midGemNode1;
        Entity doubleScoreEntity;
        SceneNode doubleScoreNode;

        // If PowerUp affects the health of the player,
        // add another parameter a Stat object called Health
        public DoubleScore(SceneManager mSceneMgr) : base(mSceneMgr)
        {
            increase = 10;
            // initialize the stat field stat.Value = 10;
            LoadModel();
        }

        protected override void LoadModel()
        {
            doubleScoreEntity = mSceneMgr.CreateEntity("Bomb.mesh");
            doubleScoreNode = mSceneMgr.CreateSceneNode();
            doubleScoreNode.AttachObject(doubleScoreEntity);

            mSceneMgr.RootSceneNode.AddChild(doubleScoreNode);

            this.gameNode = doubleScoreNode;

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
