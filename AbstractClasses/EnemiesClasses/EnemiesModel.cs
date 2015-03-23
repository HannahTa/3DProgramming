using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;

namespace RaceGame
{
    class EnemiesModel : CharacterModel
    {
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
            mSceneMgr.RootSceneNode.AddChild(enemyNode1.GameNode);
            //base.AssembleModel();
        }
    }
}
