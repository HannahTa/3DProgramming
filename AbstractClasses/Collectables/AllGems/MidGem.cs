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
            base.LoadModel();
            remove = false;

            midGemEntity = mSceneMgr.CreateEntity("Gem.mesh");

            midGemNode = mSceneMgr.CreateSceneNode();
            midGemNode.AttachObject(midGemEntity);
            mSceneMgr.RootSceneNode.AddChild(midGemNode);
            midGemNode.SetPosition(50, 100, 50);

            physObj = new PhysObj(10, "Gem", 0.1f, 0.5f);
            physObj.SceneNode = midGemNode;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));

            Physics.AddPhysObj(physObj);

            this.gameNode = midGemNode;
        }

        protected void Update()
        {
            // Code for collision detection
            //remove = true
            remove = IsCollidingWith("Player");
            //method increase score object
            //add a dispose call before the break statement
        }
    }
}
