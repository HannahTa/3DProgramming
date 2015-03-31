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

        // Dispose of each gun in th collectedGuns list
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

                ChangeGun(collectedGuns[index]);

            }
        }
    }
}
