using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    class Heart : Collectable
    {
        Entity heartEntity;
        SceneNode heartNode;

        bool removeMe;

        public bool RemoveMe
        {
            get { return removeMe; }
        }

        public Heart(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            LoadModel();
        }

        protected void LoadModel()
        {
            removeMe = false;

            heartEntity = mSceneMgr.CreateEntity("Heart.mesh");

            heartNode = mSceneMgr.CreateSceneNode();
            heartNode.AttachObject(heartEntity);
            heartNode.Scale(5f, 5f, 5f);
            mSceneMgr.RootSceneNode.AddChild(heartNode);

            physObj = new PhysObj(7, "Heart", 0.3f, 0.5f);
            physObj.AddForceToList(new WeightForce(physObj.InvMass));
            physObj.SceneNode = heartNode;

            Physics.AddPhysObj(physObj);

            this.gameNode = heartNode;
        }

        public override void Update(FrameEvent evt)
        {
            removeMe = IsCollidingWith("Player");
            //base.Update(evt);
        }

        private bool IsCollidingWith(string objName)
        {
            bool isColliding = false;
            foreach (Contacts c in physObj.CollisionList)
            {
                if (c.colliderObj.ID == objName || c.collidingObj.ID == objName)
                {
                    isColliding = true;
                    Dispose();
                    break;
                }
            }
            return isColliding;
        }

        public override void Dispose()
        {
            Physics.RemovePhysObj(physObj);
            physObj = null;

            heartNode.Parent.RemoveChild(heartNode);
            heartNode.DetachAllObjects();
            heartNode.Dispose();
            heartEntity.Dispose();
            base.Dispose();
        }
    }
}
