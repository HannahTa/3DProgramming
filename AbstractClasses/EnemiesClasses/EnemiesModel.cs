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
        //PhysObj physObj;
        //SceneNode controlNode;

        ModelElement enemyNode1;

        public EnemiesModel(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            LoadModelElements();
            AssembleModel();
            //this.gameNode = enemyNode1.GameNode; //controlNode; //
            //this.SetPosition(new Vector3(0, 0, 100));
            
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
            mSceneMgr.RootSceneNode.AddChild(enemyNode1.GameNode);

            // Physics
            float radius = 8;

            physObj = new PhysObj(radius, "Robot", 0.1f, 0.7f, 0.3f);
            physObj.SceneNode = enemyNode1.GameNode;
            //physObj.Position = enemyNode1.GameNode.Position;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));
            
            Physics.AddPhysObj(physObj);

            this.gameNode = enemyNode1.GameNode;
        }

        
    }
}
