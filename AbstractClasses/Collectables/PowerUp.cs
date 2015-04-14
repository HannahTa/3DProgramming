using System;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    abstract class PowerUp:Collectable
    {
        bool removeMe;

        public bool RemoveMe
        {
            get { return removeMe; }
        }

        protected Stat stat;
        public Stat Stat
        {
            set { stat = value; }
        }

        protected PowerUp(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            LoadModel();
        }

        protected int increase;
        virtual protected void LoadModel() 
        {
            removeMe = false;
            physObj = new PhysObj(7, "Gem", 0.3f, 0.5f);
            //physObj.Position = gameNode.Position;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));
            physObj.SceneNode = gameNode;

            Physics.AddPhysObj(physObj);
        }

        public override void Update(FrameEvent evt)
        {
            // Collision detection with the player goes here
            // (ignore until week 8) ...
            removeMe = IsCollidingWith("Player");
        }

        public bool IsCollidingWith(string objName)
        {
            bool isColliding = false;
            foreach (Contacts c in physObj.CollisionList)
            {
                if (c.colliderObj.ID == objName || c.collidingObj.ID == objName)
                {
                    isColliding = true;
                    //score.Increase(increase);
                    stat.Increase(increase);
                    //increase = 50;
                    Dispose();
                    break;
                }
            }
            return isColliding;
        }
    }
}
