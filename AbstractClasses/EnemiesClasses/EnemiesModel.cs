using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;

using PhysicsEng;

namespace RaceGame
{
    class EnemiesModel : CharacterModel
    {
        PhysObj physObj;
        SceneNode controlNode;

        ModelElement enemyNode1;

        public EnemiesModel(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            LoadModelElements();
            AssembleModel();
            this.gameNode = enemyNode1.GameNode;
        }

        protected override void LoadModelElements()
        {
            // Load model
            enemyNode1 = new ModelElement(mSceneMgr, "Robot.mesh");
            //base.LoadModelElements();
        }

        protected override void AssembleModel()
        {
            // Attach and assemble model
            //mSceneMgr.RootSceneNode.AddChild(enemyNode1.GameNode);

            controlNode = mSceneMgr.CreateSceneNode();
            controlNode.AddChild(enemyNode1.GameNode);
            mSceneMgr.RootSceneNode.AddChild(controlNode);

            // Physics
            float radius = 10;
            controlNode.Position += radius * Vector3.UNIT_Y;
            enemyNode1.GameNode.Position += radius * Vector3.UNIT_Y;

            physObj = new PhysObj(radius, "Robot", 0.1f, 0.7f, 0.3f);
            physObj.SceneNode = controlNode;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));
            Physics.AddPhysObj(physObj);

            //base.AssembleModel();
        }
    }
}
