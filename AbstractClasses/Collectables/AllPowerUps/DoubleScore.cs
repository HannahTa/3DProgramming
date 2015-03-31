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
        PhysObj physObj;
        SceneNode controlNode;

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
            this.gameNode = doubleScoreNode;
        }

        protected override void LoadModel()
        {
            //base.LoadModel();
            // Load model for power up here

            base.LoadModel();
            // Load the geometry for the power up (gem) and the scene graph 
            // nodes for it using as usual the gameNode and gameEntity
            doubleScoreEntity = mSceneMgr.CreateEntity("Bomb.mesh");
            doubleScoreNode = mSceneMgr.CreateSceneNode();
            doubleScoreNode.AttachObject(doubleScoreEntity);

            controlNode = mSceneMgr.CreateSceneNode();
            controlNode.AddChild(doubleScoreNode);
            mSceneMgr.RootSceneNode.AddChild(controlNode);

            // Physics
            float radius = 10;
            controlNode.Position += radius * Vector3.UNIT_Y;
            doubleScoreNode.Position += radius * Vector3.UNIT_Y;

            physObj = new PhysObj(radius, "DoubleScore", 0.1f, 0.7f, 0.3f);
            physObj.SceneNode = controlNode;
            physObj.Position = controlNode.Position;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));
            Physics.AddPhysObj(physObj);
        }
    }
}
