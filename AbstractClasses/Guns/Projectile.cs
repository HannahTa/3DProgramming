using System;
using System.Collections.Generic;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    abstract class Projectile:MovableElement
    {
        Timer time;
        protected int maxTime = 5000;
        protected Vector3 initialVelocity;
        protected float speed;
        protected Vector3 initialDirection;
        public Vector3 InitialDirection
        {
            set
            {
                initialDirection = value;
                physObj.Velocity = speed * initialDirection;
            }
        }
        protected float healthDamage;
        public float HealthDamage
        {
            get { return healthDamage; }
        }

        protected float shieldDamage;
        public float ShieldDamage
        {
            get { return shieldDamage; }
        }

        virtual protected void Load() 
        {
            // Physics
            float radius = 10;

            //sProjNode.Position -= radius * Vector3.UNIT_Y;

            physObj = new PhysObj(radius, "Projectile", 0.1f, 0.7f, 0.3f);
            physObj.SceneNode = gameNode;
            physObj.Position = gameNode.Position;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));
            physObj.AddForceToList(new FrictionForce(physObj));//Friction

            Physics.AddPhysObj(physObj);
        }

        protected Projectile()
        {
            time = new Timer();
        }

        public override void Dispose()
        {
            base.Dispose();
            this.remove = true;
        }

        virtual public void Update(FrameEvent evt) 
        {
            // Projectile collision detection goes here
            // (ignore until week 8) ...

            if (!remove && time.Milliseconds > maxTime)
            {
                Dispose();

                remove = true;
            }
            else
            {
                remove = IsCollidingWith("Robot");
            }
        }

        public bool IsCollidingWith(string objName)
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
    }
}
