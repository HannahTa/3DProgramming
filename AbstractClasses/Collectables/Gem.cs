using System;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    class Gem : Collectable
    {
        // To implement gems, you need to generate a new class for each
        // type of gem you want to create in your game - The class should
        // inherit from the Gem class

        protected Stat score;
        protected int increase;

        //bool removeMe;

        //public bool RemoveMe
        //{
        //    get { return removeMe; }
        //}

        protected Gem(SceneManager mSceneMgr, Stat score)
        {
            this.mSceneMgr = mSceneMgr;
            this.score = score;
        }

        protected virtual void LoadModel()
        {
            remove = false;
            physObj = new PhysObj(7, "Gem", 0.3f, 0.5f);
            physObj.AddForceToList(new WeightForce(physObj.InvMass));
            physObj.SceneNode = gameNode;

            Physics.AddPhysObj(physObj);
        }

        public override void Update(FrameEvent evt)
        {
            Animate(evt);
            remove = IsCollidingWith("Player");
            //base.Update(evt);
        }

        public bool IsCollidingWith(string objName)
        {
            bool isColliding = false;
            foreach (Contacts c in physObj.CollisionList)
            {
                if (c.colliderObj.ID == objName || c.collidingObj.ID == objName)
                {
                    isColliding = true;
                    score.Increase(increase);
                    Dispose();
                    break;
                }
            }
            return isColliding;
        }

        public override void Animate(FrameEvent evt)
        {
            gameNode.Yaw(Mogre.Math.AngleUnitsToRadians(20) * evt.timeSinceLastFrame);
        }
    }
}
