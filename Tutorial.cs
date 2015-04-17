using Mogre;
using Mogre.TutorialFramework;
using System;

using PhysicsEng;
using System.Collections.Generic;

namespace RaceGame
{
    class Tutorial : BaseApplication
    {
        GameInterface gameHMD;

        public int level = 1;
        int gemCount = 0;
        int hitCount = 0;
        
        static public LinkedList<Vector3> mWalkList = null;   //Containing the waypoints

        static public bool shoot;
        static public bool reload;
        static public bool win = false;

        Environment environment;
        static public Player player;

        Score stat;
        PlayerStats playerStats;

        List<Gem> Gems;
        List<Gem> gemsToRemove;

        List<PowerUp> PowerUps;
        List<PowerUp> powerUpsToRemove;

        List<CollectableGun> collGuns;
        List<CollectableGun> collGunsToRemove;

        List<Projectile> projList;
        List<Projectile> projListToRemove;

        List<Enemies> robots;
        List<Enemies> robotsToRemove;

        SceneNode cameraNode;

        Enemies enemies;

        static InputsManager inputsManager = InputsManager.Instance;

        Physics physics;

        public static void Main()
        {
            new Tutorial().Go();            // This method starts the rendering loop
        }

        /// <summary>
        /// This method create the initial scene
        /// </summary>
        protected override void CreateScene()
        {
            Vector3 pos = new Vector3(10, 0, 0);
            physics = new Physics();

            // GUI
            playerStats = new PlayerStats();
            gameHMD = new GameInterface(mSceneMgr, mWindow, playerStats);

            // Environment
            mSceneMgr.ShadowTechnique = ShadowTechnique.SHADOWTYPE_STENCIL_MODULATIVE;
            mSceneMgr.AmbientLight = ColourValue.White;     //??
            environment = new Environment(mSceneMgr, mWindow);

            // -Power-ups-
            stat = new Score();

            Gems = new List<Gem>();
            gemsToRemove = new List<Gem>();

            collGuns = new List<CollectableGun>();
            collGunsToRemove = new List<CollectableGun>();

            projList = new List<Projectile>();
            projListToRemove = new List<Projectile>();

            PowerUps = new List<PowerUp>();
            powerUpsToRemove = new List<PowerUp>();

            robots = new List<Enemies>();
            robotsToRemove = new List<Enemies>();
            // Player
            player = new Player(mSceneMgr);

            mWalkList = new LinkedList<Vector3>();
            mWalkList.AddLast(player.Position);

            for (int i = 0; i < 5; i++)
            {
                AddGem(playerStats.Score);
            }

            // -Camera-
            cameraNode = mSceneMgr.CreateSceneNode();
            cameraNode.AttachObject(mCamera);
            player.Model.GameNode.AddChild(cameraNode); // Camera on player

            // -Inputs controller-
            inputsManager.PlayerController = (PlayerController)player.Controller;

            // -Start timer-
            physics.StartSimTimer();    // Must be the last method in create
        }

        /// <summary>
        /// This method update the scene after a frame has finished rendering
        /// </summary>
        /// <param name="evt"></param>
        protected override void UpdateScene(FrameEvent evt)
        {
            physics.UpdatePhysics(0.01f);
            base.UpdateScene(evt);

            //Levels
            if (gemCount == 5)
            {
                gemCount++;
                level++;
                gameHMD.leveltxt = "Level 2";
                AddCollGun();
                for (int i = 0; i < 10; i++)
                {
                    AddGem(playerStats.Score);
                }
                for (int i = 0; i < 5; i++)
                {
                    AddPowerUp(playerStats.Health, playerStats.Shield, playerStats.Lives);
                }
            }

            if (gemCount == 16)
            {
                gemCount++;
                level++;
                gameHMD.leveltxt = "Level 3";
                AddCollGun();
                for (int i = 0; i < 10; i++)
                {
                    AddGem(playerStats.Score);
                }
                AddRobot();
            }

            if (gemCount == 27)
            {
                gemCount++;
                level++;
                gameHMD.leveltxt = "Level 4";
                AddCollGun();
                AddCollGun();
                for (int i = 0; i < 15; i++)
                {
                    AddGem(playerStats.Score);
                }
                for (int i = 0; i < 3; i++)
                {
                    AddRobot();
                }
            }

            if (gemCount == 43)
            {
                win = true;
            }

            if (shoot)
            {
                if (player.PlayerArmoury.ActiveGun != null && player.PlayerArmoury.ActiveGun.Ammo.Value != 0)
                {
                    player.Shoot();
                    projList.Add(player.PlayerArmoury.ActiveGun.Projectile);
                }
            }

            if (reload)
            {
                player.PlayerArmoury.ActiveGun.ReloadAmmo();
            }

            foreach (Projectile p in projList)
            {
                p.Update(evt);
                if (p.RemoveMe)
                {
                    projListToRemove.Add(p);
                }
            }

            foreach (Projectile p in projListToRemove)
            {
                projList.Remove(p);
                p.Dispose();
            }
            projListToRemove.Clear();
            
            if (mWalkList.First.Value != player.Position)
            {
                mWalkList.RemoveFirst();
                mWalkList.AddLast(player.Position);
            }
            
            foreach (Enemies e in robots)
            {
                e.Update(evt);
                if (e.Model.RemoveMe)
                {
                    robotsToRemove.Add(e);
                }
            }
            

            foreach (Enemies e in robotsToRemove)
            {
                robots.Remove(e);
                e.Model.Dispose();
            }
            robotsToRemove.Clear();

            foreach (Gem g in Gems)
            {
                g.Update(evt);
                if (g.RemoveMe)
                {
                    gemsToRemove.Add(g);
                    gemCount++;
                }
            }

            foreach (Gem g in gemsToRemove)
            {
                Gems.Remove(g);
                g.Dispose();
            }
            gemsToRemove.Clear();

            foreach (CollectableGun cg in collGuns)
            {
                cg.Update(evt);
                if (cg.RemoveMe)
                {
                    collGunsToRemove.Add(cg);
                }
            }

            foreach (CollectableGun cg in collGunsToRemove)
            {
                collGuns.Remove(cg);
                cg.Dispose();
            }
            collGunsToRemove.Clear();

            foreach (PowerUp pu in PowerUps)
            {
                pu.Update(evt);
                if (pu.RemoveMe)
                {
                    powerUpsToRemove.Add(pu);
                }
            }

            foreach (PowerUp pu in powerUpsToRemove)
            {
                PowerUps.Remove(pu);
                pu.Dispose();
            }
            powerUpsToRemove.Clear();

            if (player.Model.RemoveMe)
            {
                if (playerStats.Shield.Value > 0 && hitCount == 0)
                {
                    playerStats.Shield.Decrease(5);
                }
                else if (playerStats.Health.Value == 0 && hitCount == 0)
                {
                    playerStats.Lives.Decrease(1);
                    playerStats.Shield.Increase(100);
                    playerStats.Health.Increase(100);
                }
                else if (playerStats.Health.Value > 0 && hitCount == 0)
                {
                    playerStats.Health.Decrease(5);
                }
                hitCount++;
            }

            player.Update(evt);
            gameHMD.Update(evt);

            if (hitCount == 50)
            {
                hitCount = 0;
            }

            mCamera.LookAt(player.Position);


            shoot = false;
            reload = false;
        }

        /// <summary>
        /// This method destorys the scene
        /// </summary>
        protected override void DestroyScene()
        {
            base.DestroyScene();
            
            cameraNode.DetachAllObjects();
            cameraNode.Dispose();

            if (player != null)
            {
                player.Model.DisposeModel();
            }

            foreach (Enemies e in robots)
            {
                e.Model.Dispose();
            }

            foreach (Gem g in Gems)
            {
                g.Dispose();
            }

            foreach (CollectableGun cg in collGuns)
            {
                cg.Dispose();
            }

            foreach (Projectile p in projList)
            {
                p.Dispose();
            }

            foreach (PowerUp pu in PowerUps)
            {
                pu.Dispose();
            }

            //collGun.Dispose();
            //slowGun.Dispose();

            environment.Dispose();

            gameHMD.Dispose();
            
            physics.Dispose();
        }

        /**
         * Adds robots into the game, mwahaha!
         */
        private void AddRobot()
        {
            enemies = new Enemies(mSceneMgr);
            enemies.Model.SetPosition(new Vector3(Mogre.Math.RangeRandom(-400, 400), 0, Mogre.Math.RangeRandom(-400, 400)));
            robots.Add(enemies);
            
        }

        /*
         * Randomly loads three types of Gems (small, midium and large)
         **/
        private void AddGem(Stat score)
        {
            int count = (int)Mogre.Math.RangeRandom(0, 12);

            //Gem midGem = new MidGem(mSceneMgr, score);
            //Gem smallGem = new SmallGem(mSceneMgr, score);

            if (count > 10)
            {
                Gem largeGem = new LargeGem(mSceneMgr, score);
                largeGem.SetPosition(new Vector3(Mogre.Math.RangeRandom(-400, 400), 50, Mogre.Math.RangeRandom(-400, 400)));
                Gems.Add(largeGem);
            }
            else if (count % 2 != 0)
            {
                Gem midGem = new MidGem(mSceneMgr, score);
                midGem.SetPosition(new Vector3(Mogre.Math.RangeRandom(-400, 400), 50, Mogre.Math.RangeRandom(-400, 400)));
                Gems.Add(midGem);
            }
            else
            {
                Gem smallGem = new SmallGem(mSceneMgr, score);
                smallGem.SetPosition(new Vector3(Mogre.Math.RangeRandom(-400, 400), 50, Mogre.Math.RangeRandom(-400, 400)));
                Gems.Add(smallGem);
            }
        }

        private void AddCollGun()
        {
            Gun slowGun = new SlowGun(mSceneMgr);
            CollectableGun CollGun = new CollectableGun(mSceneMgr, slowGun, player.PlayerArmoury);
            CollGun.SetPosition(new Vector3(Mogre.Math.RangeRandom(-400, 400), 50, Mogre.Math.RangeRandom(-400, 400)));
            collGuns.Add(CollGun);
        }

        private void AddPowerUp(Stat health, Stat shield, Stat life)
        {
            int count = (int)Mogre.Math.RangeRandom(0, 13);

            if (count > 12)
            {
                PowerUp live = new Life(mSceneMgr, life);
                live.SetPosition(new Vector3(Mogre.Math.RangeRandom(-400, 400), 50, Mogre.Math.RangeRandom(-400, 400)));
                PowerUps.Add(live);
            }
            else if (count % 2 != 0)
            {
                PowerUp heart = new Health(mSceneMgr, health);
                heart.SetPosition(new Vector3(Mogre.Math.RangeRandom(-400, 400), 50, Mogre.Math.RangeRandom(-400, 400)));
                PowerUps.Add(heart);
            }
            else
            {
                PowerUp shield1 = new DoubleScore(mSceneMgr, shield);
                shield1.SetPosition(new Vector3(Mogre.Math.RangeRandom(-400, 400), 50, Mogre.Math.RangeRandom(-400, 400)));
                PowerUps.Add(shield1);
            }
        }

        /// <summary>
        /// This method create a new camera
        /// </summary>
        protected override void CreateCamera()
        {
            //base.CreateCamera();
            mCamera = mSceneMgr.CreateCamera("PlayerCam");
            mCamera.Position = new Vector3(0, 100, -300);
            mCamera.LookAt(new Vector3(0, 0, 0));
            mCamera.NearClipDistance = 5;
            mCamera.FarClipDistance = 1000;
            mCamera.FOVy = new Degree(70);

            mCameraMan = new CameraMan(mCamera);
            mCameraMan.Freeze = true;
        }

        /// <summary>
        /// This method create a new viewport
        /// </summary>
        protected override void CreateViewports()
        {
            //base.CreateViewports();
            Viewport viewport = mWindow.AddViewport(mCamera);
            viewport.BackgroundColour = ColourValue.Black;
            mCamera.AspectRatio = viewport.ActualWidth / viewport.ActualHeight;
        }

        /// <summary>
        /// This method set create a frame listener to handle events before, during or after the frame rendering
        /// </summary>
        protected override void CreateFrameListeners()
        {
            base.CreateFrameListeners();
            mRoot.FrameRenderingQueued +=
                new FrameListener.FrameRenderingQueuedHandler(inputsManager.ProcessInput);
        }

        /// <summary>
        /// This method initilize the inputs reading from keyboard and mouse
        /// </summary>
        protected override void InitializeInput()
        {
            base.InitializeInput();

            int windowHandle;
            mWindow.GetCustomAttribute("WINDOW", out windowHandle);
            inputsManager.InitInput(ref windowHandle);
        }

    }
}