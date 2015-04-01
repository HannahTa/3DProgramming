using System;
using Mogre;

namespace RaceGame
{
    class CollectableGun:Collectable
    {
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

            this.gameNode = gun.GameNode;       // Initialize the gameNode 
            //this.gameNode.Scale(1.5f);          // 
            // Initialize the gameNode here, scale it by 1.5f using the Scale funtion, and add as its child the gameNode contained in the Gun object.
            // Finally attach the gameNode to the sceneGraph.

            // Here goes the link to the physics engine
            // (ignore until week 8) ...
        }

        public override void Update(FrameEvent evt)
        {
            Animate(evt);
            //Here goes the collision detection with the player
            // (ignore until week 8) ...

            // Remove = true
            if (this.remove == true)
            {
                (gun.GameNode.Parent).RemoveChild(gun.GameNode.Name);
                playerArmoury.AddGun(gun);
            }
            
            base.Update(evt);
        }

        public override void Animate(FrameEvent evt)
        {
            gameNode.Rotate(new Quaternion(Mogre.Math.AngleUnitsToRadians(evt.timeSinceLastFrame*10), Vector3.UNIT_Y));
        }
    }
}
