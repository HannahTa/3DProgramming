using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;

namespace RaceGame
{
    class HMD
    {
        protected RenderWindow mWindow;
        protected SceneManager mSceneMgr;

        protected Overlay overlay;
        protected CharacterStats characterStats;

        protected HMD(SceneManager mSceneMgr, RenderWindow mWindow, CharacterStats characterStat)
        {
            this.mWindow = mWindow;
            this.mSceneMgr = mSceneMgr;
            this.characterStats = characterStat;
        }

        virtual protected void Load(string name)
        {
            this.overlay = OverlayManager.Singleton.GetByName(name);
            overlay.Show();
        }

        virtual protected void Animate(FrameEvent evt) { }

        virtual public void Update(FrameEvent evt)
        {
            Animate(evt);
        }

        virtual public void Dispose()
        {
            if (overlay != null)
                overlay.Dispose();
        }
    }
}
