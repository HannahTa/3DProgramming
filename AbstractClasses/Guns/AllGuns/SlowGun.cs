using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    class SlowGun : Gun
    {

        public SlowGun(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            this.maxAmmo = 10;
            this.ammo = new Stat();

            ammo.InitValue(maxAmmo);    // Lets ammo = maxAmmo

            LoadModel();
        }

        /**
         * DO NOT ATTACH THEM TO SCENE-GRAPH
         * 
         * 
         **/
        protected override void LoadModel()
        {
            base.LoadModel();
        }

        public override void Fire()
        {
            base.Fire();
            // Check is ammo size is 0, if it isn't create a 
            // new projectile of the correct type for the gun 
            // (use the inherited field for this)

        }

        public override void ReloadAmmo()
        {
            base.ReloadAmmo();
            // Checks whether the ammo.Value is less than maxAmmo, 
            // if it is, increase the value to a value of my choise, 
            // but that keep the ammo.Value less or equal to maxAmmo
        }
    }
}
