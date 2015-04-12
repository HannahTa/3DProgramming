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
        //ModelElement slowGunNode;

        SceneNode slowGunNode;
        Entity slowGunEntity;

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
            //slowGunNode = new ModelElement(mSceneMgr, "Sphere.mesh");
            slowGunEntity = mSceneMgr.CreateEntity("Sphere.mesh");

            slowGunNode = mSceneMgr.CreateSceneNode();
            slowGunNode.AttachObject(slowGunEntity);
            slowGunNode.Scale(1f, 1f, 1f);
            //mSceneMgr.RootSceneNode.AddChild(slowGunNode);
            
            this.gameNode = slowGunNode;
            base.LoadModel();
        }

        public override void Fire()
        {
            //base.Fire();
            if (ammo.Value == 0)
            {
                ReloadAmmo();   // Reloads if ammo.Value = 0 when trying to fire
            }
            else
            {
                Projectile SP = new SlowProjectile(mSceneMgr);
                SP.SetPosition(GunPosition() + 2 * GunDirection());
                ammo.Decrease(1);   // Decrease ammo by 1
            }
        }

        public override void ReloadAmmo()
        {
            int ammoReload;

            //base.ReloadAmmo();
            if (ammo.Value < maxAmmo)
            {
                ammoReload = maxAmmo - ammo.Value;  // Makes sure that ammo is not more than maxAmmo
                ammo.Increase(ammoReload);
            }
        }
    }
}
