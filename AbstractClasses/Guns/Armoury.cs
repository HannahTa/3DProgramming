using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;

namespace RaceGame
{
    class Armoury
    {
        bool gunChanged;

        public bool GunChanged
        {
            set { gunChanged = value; }
            get { return gunChanged; }
        }

        Gun activeGun;

        public Gun ActiveGun
        {
            set { activeGun = value; }
            get { return activeGun; }
        }

        List<Gun> collectedGuns;

        public List<Gun> CollectedGuns
        {
            get { return collectedGuns; }
        }

        public Armoury()
        {
            this.collectedGuns = new List<Gun>();
        }

        // Dispose of each gun in the collectedGuns list
        // and if the activeGun is not null dispose of it as well
        public void Dispose()
        {

            // Go thourgh all the guns which have been collected and delete them.
            foreach (Gun gun in collectedGuns)
            {
                gun.Dispose();
            }

            if (activeGun != null)
            {
                activeGun.Dispose();
            }
        }

        public void ChangeGun(Gun gun)
        {
            // Stores gun in activeGun
            // bool gunchanged to true
            activeGun = gun;
            gunChanged = true;
        }

        public void SwapGun(int index)
        {
            Gun tempGun;

            if (activeGun != null && collectedGuns != null)
            {
                tempGun = activeGun;
                index = index % collectedGuns.Count;    // Keeps index in the limits ??
                ChangeGun(collectedGuns[index]);
            }
        }

        public void AddGun(Gun gun)
        {
            bool add = true;

            foreach (Gun g in collectedGuns)
            {
                // Check if add is true? and whether the type of gun you are passing is in collected gun list
                //(g.GetType() == gun.GetType());
                //if they are both true then it calls reloadAmmo method for g
                // Call the changegun method passing g to it and then set add to false
                if (add == true && (g.GetType() == gun.GetType()))
                {
                    g.ReloadAmmo();
                    ChangeGun(g);
                    add = false;
                } 
            }

            if (add == true)
            {
                ChangeGun(gun);
                collectedGuns.Add(gun);
            }
            else
            {
                gun.Dispose();
            }

            // once foreach loop finished, check the add variable, if true then Call ChangeGun method, pass gun to it
            // and add the gun to the collectedGun list, else call the Dispose method from gun
        }
    }
}
