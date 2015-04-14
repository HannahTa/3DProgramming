using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;

using PhysicsEng;

namespace RaceGame
{
    class Player : Character
    {
        // Fields
        //PlayerModel model;
        //PlayerController controller;
        //PlayerStats stats;

        //PhysObj physObj;
        //SceneNode controlNode;

        Armoury playerArmoury;

        public Armoury PlayerArmoury
        {
            get { return playerArmoury; }
        }

        public Player(SceneManager mSceneMgr)
        {
            model = new PlayerModel(mSceneMgr);
            controller = new PlayerController(this);
            stats = new PlayerStats();
            playerArmoury = new Armoury();
        }

        public override void Update(FrameEvent evt)
        {
            //model.Animate();
            controller.Update(evt);

            if (playerArmoury.GunChanged == true)
            {
                ((PlayerModel)model).AttachGun(playerArmoury.ActiveGun);    // Changes the gun if true
                playerArmoury.GunChanged = false;       // If was always true, would continualy change guns
            }

            base.Update(evt);
        }
        
        public override void Shoot()
        {
            //base.Shoot();
            //System.Console.WriteLine(playerArmoury.ActiveGun);
            if (playerArmoury.ActiveGun != null)
            {
                playerArmoury.ActiveGun.Fire();
            } 
        }
    }
}
