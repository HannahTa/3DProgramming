using System;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    abstract class PowerUp:Collectable
    {
        //bool removeMe;

        //public bool RemoveMe
        //{
        //    get { return removeMe; }
        //}

        protected Stat stat;
        public Stat Stat
        {
            set { stat = value; }
        }
        
        protected int increase;
        
        protected PowerUp(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            
            //LoadModel();
        }

        
        virtual protected void LoadModel() 
        {
            
        }

        public override void Update(FrameEvent evt)
        {
            // Collision detection with the player goes here
            // (ignore until week 8) ...
            remove = IsCollidingWith("Player");
            base.Update(evt);
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
