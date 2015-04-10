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

        protected Gem(SceneManager mSceneMgr, Stat score)
        {
            this.mSceneMgr = mSceneMgr;
            this.score = score;
        }

        protected virtual void LoadModel()
        {
            // The link with to phisics engine goes here
            // (ignore until week 8) ...
            //physObj = new PhysObj(10, "Gem", 0.1f, 0.5f);
            //physObj.SceneNode = gameNode;
            //physObj.AddForceToList(new WeightForce(physObj.InvMass));
            //physObj.AddForceToList(new FrictionForce(physObj));

            //Physics.AddPhysObj(physObj);
        }

        public override void Update(FrameEvent evt)
        {
            Animate(evt);
            //remove = IsCollidingWith("Player");
            // Collision detection with the player goes here
            // (ignore until week 8) ...
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
                    score.Increase(100);
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
