using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;

namespace RaceGame
{
    class GameInterface : HMD
    {
        private PanelOverlayElement panel;
        private OverlayElement scoreText;
        private OverlayElement timeText;

        private OverlayElement healthBar;
        private OverlayElement shieldBar;
        private Overlay overlay3D;
        private Entity lifeEntity;
        private List<SceneNode> lives;
        private Timer time;

        public Timer Time
        {
            set { time = value; } 
        }

        private float hRatio;
        private float sRatio;
        private string score = "Score: ";
        private string timer = "Time Left: ";
        private float clock = 10000;

        public string clockText;
        public string ClockText
        {
            get { return clockText; }
        }

        public GameInterface(SceneManager mSceneMgr, RenderWindow mWindow, CharacterStats playerStats)
            : base(mSceneMgr, mWindow, playerStats)
        {
            Load("GameInterface");
        }

        protected override void Load(string name)
        {
            base.Load(name);

            lives = new List<SceneNode>();
            //time = new Timer();
            //time = new System.Timers.Timer(10000);
            time = new Mogre.Timer();
            //time.Milliseconds();

            healthBar = OverlayManager.Singleton.GetOverlayElement("HealthBar");
            hRatio = healthBar.Width / (float)characterStats.Health.Max;

            shieldBar = OverlayManager.Singleton.GetOverlayElement("ShieldBar");
            sRatio = shieldBar.Width / (float)characterStats.Shield.Max;

            scoreText = OverlayManager.Singleton.GetOverlayElement("ScoreText");
            scoreText.Caption = score;
            scoreText.Left = mWindow.Width * 0.5f;

            //OverlayManager.Singleton.CreateOverlayElement("timeText", "TimeText");
            timeText = OverlayManager.Singleton.GetOverlayElement("TimeText");
            timeText.Caption = timer;
            timeText.Left = mWindow.Width * 0.5f;
            timeText.Top = mWindow.Height * 0.05f;
            
            panel = (PanelOverlayElement)OverlayManager.Singleton.GetOverlayElement("GreenBackground");
            panel.Width = mWindow.Width;
            LoadOverlay3D();
        }

        private void LoadOverlay3D()
        {
            overlay3D = OverlayManager.Singleton.Create("3DOverlay");
            overlay3D.ZOrder = 15000;

            CreateHearts();

            overlay3D.Show();
        }

        private void CreateHearts()
        {
            for (int i = 0; i < characterStats.Lives.Value; i++)
            {
                AddHeart(i);
            }
        }

        private void AddHeart(int n)
        {
            SceneNode livesNode = CreateHeart(n);
            lives.Add(livesNode);
            overlay3D.Add3D(livesNode);
        }

        private void RemoveAndDestroyLife(SceneNode life)
        {
            overlay3D.Remove3D(life);
            lives.Remove(life);
            MovableObject heart = life.GetAttachedObject(0);
            life.DetachAllObjects();
            life.Dispose();
            heart.Dispose();
        }

        private SceneNode CreateHeart(int n)
        {
            lifeEntity = mSceneMgr.CreateEntity("Heart.mesh");
            lifeEntity.SetMaterialName("HeartHMD");
            SceneNode livesNode;
            livesNode = new SceneNode(mSceneMgr);
            livesNode.AttachObject(lifeEntity);
            livesNode.Scale(new Vector3(0.15f, 0.15f, 0.15f));
            livesNode.Position = new Vector3(3.5f, 3.8f, -6) - n * 0.5f * Vector3.UNIT_X;
            livesNode.SetVisible(true);
            return livesNode;
        }

        private string convertTime(float time)
        {
            string convTime;
            float secs = time / 1000f;
            int min = (int)(secs / 60);
            secs = (int)secs % 60f;
            if (secs < 10)
            {
                convTime = min + ":0" + secs;
            }
            else
            {
                convTime = min + ":" + secs;
            }
            return convTime;
        }

        public override void Update(FrameEvent evt)
        {
            base.Update(evt);

            Animate(evt);

            if (lives.Count > characterStats.Lives.Value && characterStats.Lives.Value >= 0)
            {
                SceneNode life = lives[lives.Count - 1];
                RemoveAndDestroyLife(life);

            }
            if (lives.Count < characterStats.Lives.Value)
            {
                AddHeart(characterStats.Lives.Value);
            }

            healthBar.Width = hRatio * characterStats.Health.Value;
            shieldBar.Width = sRatio * characterStats.Shield.Value;
            scoreText.Caption = score + ((PlayerStats)characterStats).Score.Value;

            timeText.Caption = timer + clockText;
            clockText = convertTime(clock - time.Milliseconds);
            //if (convertTime(clock - time.Milliseconds) == "0:00")
            //{
            //    clockText = "0:00";
            //}
            //else
            //{
            //    clockText = convertTime(clock - time.Milliseconds);
            //}
            //System.Console.WriteLine(clockText);

        }

        protected override void Animate(FrameEvent evt)
        {
            foreach (SceneNode sn in lives)
            {
                sn.Yaw(evt.timeSinceLastFrame);
            }
        }

        public override void Dispose()
        {
            List<SceneNode> toRemove = new List<SceneNode>();
            foreach (SceneNode life in lives)
            {
                toRemove.Add(life);
            }
            foreach (SceneNode life in toRemove)
            {
                RemoveAndDestroyLife(life);
            }
            lifeEntity.Dispose();
            toRemove.Clear();
            shieldBar.Dispose();
            healthBar.Dispose();
            scoreText.Dispose();
            timeText.Dispose();
            time.Dispose();
            panel.Dispose();
            overlay3D.Dispose();
            base.Dispose();
        }
    }
}
