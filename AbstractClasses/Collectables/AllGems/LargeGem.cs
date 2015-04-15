using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;

using PhysicsEng;

namespace RaceGame
{
    class LargeGem : Gem
    {
        Entity largeGemEntity;
        SceneNode largeGemNode;

        public LargeGem(SceneManager mSceneMgr, Stat score) : base(mSceneMgr, score)
        {
            this.mSceneMgr = mSceneMgr;
            increase = 200;
            LoadModel();
        }

        protected override void LoadModel()
        {
            largeGemEntity = mSceneMgr.CreateEntity("Gem.mesh");

            largeGemNode = mSceneMgr.CreateSceneNode();
            largeGemNode.AttachObject(largeGemEntity);
            largeGemNode.Scale(3f, 3f, 3f);
            mSceneMgr.RootSceneNode.AddChild(largeGemNode);

            this.gameNode = largeGemNode;
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
