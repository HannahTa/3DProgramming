using System;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    class CollectableGun:Collectable
    {
        SceneNode collGunNode;
        Entity collGunEntity;

        Gun gun;
        public Gun Gun
        {
            get { return gun; }
        }

        Armoury playerArmoury;

        public Armoury PlayerArmoury
        {
            set { playerArmoury = value; }
        }

        public CollectableGun(SceneManager mSceneMgr, Gun gun, Armoury playerArmoury)
        {
            // Initialize here the mSceneMgr, the gun and the playerArmoury fields to the values passed as parameters
            this.mSceneMgr = mSceneMgr;
            this.gun = gun;
            this.playerArmoury = playerArmoury;

            mSceneMgr.RootSceneNode.AddChild(gun.GameNode);// <--

            physObj = new PhysObj(7, "Gun", 0.1f, 0.5f);
            physObj.AddForceToList(new WeightForce(physObj.InvMass));
            physObj.SceneNode = gun.GameNode;

            Physics.AddPhysObj(physObj);
            
            this.gameNode = gun.GameNode;       // Initialize the gameNode
        }

        public override void Update(FrameEvent evt)
        {
            Animate(evt);
            //Here goes the collision detection with the player
            // (ignore until week 8) ...

            // Remove = true
            remove = IsCollidingWith("Player");
            if (this.remove == true)
            {
                (gun.GameNode.Parent).RemoveChild(gun.GameNode.Name);
                playerArmoury.AddGun(gun);
                Dispose();
                // detach the gun model from current node and add it to player sub-scene-graph
                // Call Dispose before break
            }
            
            base.Update(evt);
        }

        public override void Animate(FrameEvent evt)
        {
            gameNode.Rotate(new Quaternion(Mogre.Math.AngleUnitsToRadians(evt.timeSinceLastFrame*10), Vector3.UNIT_Y));
        }

        public bool IsCollidingWith(string objName)
        {
            bool isColliding = false;
            foreach (Contacts c in physObj.CollisionList)
            {
                if (c.colliderObj.ID == objName || c.collidingObj.ID == objName)
                {
                    isColliding = true;
                    //System.Console.WriteLine("Collides");
                    Dispose();
                    break;
                }
            }
            return isColliding;
        }
    }
}
