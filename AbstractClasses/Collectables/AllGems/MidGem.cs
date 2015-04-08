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
        SceneNode controlNode;

        //ModelElement midGemNode1;
        Entity midGemEntity;
        SceneNode midGemNode;

        //bool removeMe;
        
        //public bool RemoveMe
        //{
        //    get { return removeMe; }
        //}

        public MidGem(SceneManager mSceneMgr, Stat score):base(mSceneMgr, score)
        {
            this.mSceneMgr = mSceneMgr;
            increase = 100;
            LoadModel();
            this.gameNode = midGemNode;
        }

        protected override void LoadModel()
        {
            base.LoadModel();
            remove = false;

            // Load the geometry for the power up (gem) and the scene graph 
            // nodes for it using as usual the gameNode and gameEntity
            midGemEntity = mSceneMgr.CreateEntity("Gem.mesh");
            midGemNode = mSceneMgr.CreateSceneNode();
            midGemNode.AttachObject(midGemEntity);

            controlNode = mSceneMgr.CreateSceneNode();
            controlNode.AddChild(midGemNode);
            mSceneMgr.RootSceneNode.AddChild(controlNode);
            
            // Physics
            float radius = 1;
            controlNode.Position += radius * Vector3.UNIT_Y;
            midGemNode.Position += radius * Vector3.UNIT_Y;

            physObj = new PhysObj(radius, "MidGem", 0.1f, 0.7f, 0.3f);
            physObj.SceneNode = controlNode;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));
            Physics.AddPhysObj(physObj);
        }

        protected void Update()
        {
            // Code for collision detection
            //remove = true
            //method increase score object
            //add a dispose call before the break statement
            remove = IsCollidingWith("Player");
        }

        private bool IsCollidingWith(string objName)
        {
            bool isColliding = false;
            foreach (Contacts c in physObj.CollisionList)
            {
                if (c.colliderObj.ID == objName || c.collidingObj.ID == objName)
                {
                    isColliding = true;
                    break;
                }
            }
            return isColliding;
        }
    }
}
