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

        //bool removeMe;

        //public bool RemoveMe
        //{
        //    get { return removeMe; }
        //}

        public Heart(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            LoadModel();
        }

        protected void LoadModel()
        {
            remove = false;

            heartEntity = mSceneMgr.CreateEntity("Heart.mesh");

            heartNode = mSceneMgr.CreateSceneNode();
            heartNode.Scale(5f, 5f, 5f);
            heartNode.AttachObject(heartEntity);
            mSceneMgr.RootSceneNode.AddChild(heartNode);

            physObj = new PhysObj(7, "Heart", 0.3f, 0.5f);
            physObj.SceneNode = heartNode;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));

            Physics.AddPhysObj(physObj);

            this.gameNode = heartNode;
        }

        public override void Update(FrameEvent evt)
        {
            //System.Console.WriteLine(physObj.CollisionList.Count);
            remove = IsCollidingWith("Player");
            //base.Update(evt);
        }

        private bool IsCollidingWith(string objName)
        {
            //System.Console.WriteLine(physObj.CollisionList.Count);
            bool isColliding = false;
            foreach (Contacts c in physObj.CollisionList)
            {
                System.Console.WriteLine("InCollision");
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
