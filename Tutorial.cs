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
        //TimeIndex time;

        static public bool shoot;

        Environment environment;
        Player player;
        //Enemies enemies;

        Score stat;
        //Gem midGem;
        //PowerUp health;
        //DoubleScore doubleScore;

        Gun slowGun;

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
            PlayerStats playerStats = new PlayerStats();
            gameHMD = new GameInterface(mSceneMgr, mWindow, playerStats);
            //gameHMD.Time = new Timer();

            // Environment
            mSceneMgr.ShadowTechnique = ShadowTechnique.SHADOWTYPE_STENCIL_MODULATIVE;
            environment = new Environment(mSceneMgr, mWindow);

            // -Power-ups-
            stat = new Score();
            

            slowGun = new SlowGun(mSceneMgr);

            //health = new Health(mSceneMgr, stat);
            //health.SetPosition(new Vector3(100, 50, -50));

            //doubleScore = new DoubleScore(mSceneMgr);

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

            //Gems.Add(midGem);
            //PowerUps.Add(health);
            //AddPowerUp();
            
            

            // Player
            player = new Player(mSceneMgr);

            for (int i = 0; i < 5; i++)
            {
                AddGem(playerStats.Score);
            }

            //AddGem(playerStats.Score);
            //AddGem(playerStats.Score);
            AddCollGun();

            // Enemies
            //enemies = new Enemies(mSceneMgr);
            robots = new List<Enemies>();
            robotsToRemove = new List<Enemies>();
            AddRobot();

            //collGun = new CollectableGun(mSceneMgr, slowGun, player.PlayerArmoury);
            //collGun.SetPosition(new Vector3(50, 0, 0));

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

            if (gameHMD.ClockText == "0:00")
            {
                //this.DestroyScene();
                //this.Shutdown();
            }

            if (shoot)
            {
                if (player.PlayerArmoury.ActiveGun != null && player.PlayerArmoury.ActiveGun.Ammo.Value != 0)
                {
                    player.Shoot();
                    projList.Add(player.PlayerArmoury.ActiveGun.Projectile);
                }
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

            //foreach (PowerUp pu in PowerUps)
            //{
            //    pu.Update(evt);
            //    if (pu.RemoveMe)
            //    {
            //        powerUpsToRemove.Add(pu);
            //    }
            //}

            //foreach (PowerUp pu in powerUpsToRemove)
            //{
            //    PowerUps.Remove(pu);
            //    pu.Dispose();
            //}
            //powerUpsToRemove.Clear();

            gameHMD.Update(evt);
            
            player.Update(evt);
            //if (player.Model.RemoveMe)
            //{
            //    player.Stats.Health.Decrease(10);
            //    //player.Model.RemoveMe = false;
            //}
            
            mCamera.LookAt(player.Position);

            shoot = false;
        }

        /// <summary>
        /// This method destorys the scene
        /// </summary>
        protected override void DestroyScene()
        {
            //enemies.Model.DisposeModel();
            //player.Model.DisposeModel();
            base.DestroyScene();
            
            cameraNode.DetachAllObjects();
            cameraNode.Dispose();

            if (player != null)
            {
                player.Model.DisposeModel();
            }

            //if (enemies != null)
            //{
            //    enemies.Model.DisposeModel();
            //}
            foreach (Enemies e in robots)
            {
                e.Model.Dispose();
            }

            //if (midGem != null)
            //{
            //    midGem.Dispose();
            //}

            ////midGem.Dispose();

            foreach (Gem g in Gems)
            {
                g.Dispose();
            }

            foreach (CollectableGun cg in collGuns)
            {
                System.Console.WriteLine(cg.GameNode.Parent);
                cg.Dispose();
            }

            foreach (Projectile p in projList)
            {
                p.Dispose();
            }

            //foreach (PowerUp pu in PowerUps)
            //{
            //    pu.Dispose();
            //}

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
            Enemies enemies = new Enemies(mSceneMgr);
            enemies.Model.SetPosition(new Vector3(Mogre.Math.RangeRandom(0, 100), 100, Mogre.Math.RangeRandom(0, 100)));
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
                largeGem.SetPosition(new Vector3(Mogre.Math.RangeRandom(-200, 200), 50, Mogre.Math.RangeRandom(-200, 200)));
                Gems.Add(largeGem);
            }
            else if (count % 2 != 0)
            {
                Gem midGem = new MidGem(mSceneMgr, score);
                midGem.SetPosition(new Vector3(Mogre.Math.RangeRandom(-200, 200), 50, Mogre.Math.RangeRandom(-200, 200)));
                Gems.Add(midGem);
            }
            else
            {
                Gem smallGem = new SmallGem(mSceneMgr, score);
                smallGem.SetPosition(new Vector3(Mogre.Math.RangeRandom(-200, 200), 50, Mogre.Math.RangeRandom(-200, 200)));
                Gems.Add(smallGem);
            }
        }

        private void AddCollGun()
        {
            CollectableGun CollGun = new CollectableGun(mSceneMgr, slowGun, player.PlayerArmoury);
            CollGun.SetPosition(new Vector3(-100, 50, 50));
            collGuns.Add(CollGun);
        }

        private void AddPowerUp()
        {
            PowerUp powerUp = new Health(mSceneMgr, stat);
            //powerUp.SetPosition(new Vector3(Mogre.Math.RangeRandom(0, 100), 100, Mogre.Math.RangeRandom(0, 100)));
            powerUp.SetPosition(new Vector3(100, 50, 10));
            PowerUps.Add(powerUp);
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
        /// This method initilize the inputs reading from keyboard adn mouse
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