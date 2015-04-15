using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;

using PhysicsEng;

namespace RaceGame
{
    class SmallGem : Gem
    {
        Entity smallGemEntity;
        SceneNode smallGemNode;

        public SmallGem(SceneManager mSceneMgr, Stat score) : base(mSceneMgr, score)
        {
            this.mSceneMgr = mSceneMgr;
            increase = 50;
            LoadModel();
        }

        protected override void LoadModel()
        {
            smallGemEntity = mSceneMgr.CreateEntity("Gem.mesh");

            smallGemNode = mSceneMgr.CreateSceneNode();
            smallGemNode.AttachObject(smallGemEntity);
            //smallGemNode.Scale(2f, 2f, 2f);
            mSceneMgr.RootSceneNode.AddChild(smallGemNode);

            this.gameNode = smallGemNode;
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
