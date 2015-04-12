using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;

using PhysicsEng;

namespace RaceGame
{
    class MidGem : Gem
    {
        //PhysObj physObj;

        //ModelElement midGemNode1;
        Entity midGemEntity;
        SceneNode midGemNode;

        public MidGem(SceneManager mSceneMgr, Stat score):base(mSceneMgr, score)
        {
            this.mSceneMgr = mSceneMgr;
            increase = 100;
            LoadModel();
            //this.gameNode = midGemNode;
        }

        protected override void LoadModel()
        {
            midGemEntity = mSceneMgr.CreateEntity("Gem.mesh");

            midGemNode = mSceneMgr.CreateSceneNode();
            midGemNode.AttachObject(midGemEntity);
            midGemNode.Scale(2f, 2f, 2f);
            mSceneMgr.RootSceneNode.AddChild(midGemNode);

            this.gameNode = midGemNode;
            base.LoadModel();
        }

        public override void Dispose()
        {
            base.Dispose();
            Physics.RemovePhysObj(physObj);
            physObj = null;
        }
    }
}
