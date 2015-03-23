using System;
using Mogre;

namespace RaceGame
{
    class Gun:MovableElement
    {
        protected int maxAmmo;

        protected Projectile projectile;
        public Projectile Projectile
        {
            set { projectile = value; }
        }

        protected Stat ammo;
        public Stat Ammo
        {
            get { return ammo; }
        }

        public Vector3 GunPosition()
        {
            SceneNode node = gameNode;
            try
            {
                while (node.ParentSceneNode.ParentSceneNode != null)
                {
                    node = node.ParentSceneNode;
                }
            }
            catch (System.AccessViolationException)
            { }

            return node.Position;
        }

        public Vector3 GunDirection()
        {
            SceneNode node = gameNode;
            try
            {
                while (node.ParentSceneNode.ParentSceneNode != null)
                {
                    node = node.ParentSceneNode;
                }
            }
            catch (System.AccessViolationException)
            { }

            Vector3 direction = node.LocalAxes * gameNode.LocalAxes.GetColumn(2);

            return direction;
        }

        virtual protected void LoadModel() { }
        virtual public void ReloadAmmo() { }
        virtual public void Fire() { }        
    }
}
