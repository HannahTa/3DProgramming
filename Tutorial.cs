﻿using Mogre;
using Mogre.TutorialFramework;
using System;

using PhysicsEng;
using System.Collections.Generic;

namespace RaceGame
{
    class Tutorial : BaseApplication
    {
        GameInterface gameHMD;

        static public bool shoot;

        //Wall wall;
        Environment environment;
        Player player;
        //Enemies enemies;

        Score stat;
        Gem midGem;
        DoubleScore doubleScore;

        Heart heart;

        CollectableGun collGun;
        Gun slowGun;

        List<Gem> Gems;
        List<Gem> gemsToRemove;

        List<Enemies> robots;

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

            // Environment
            mSceneMgr.ShadowTechnique = ShadowTechnique.SHADOWTYPE_STENCIL_MODULATIVE;
            environment = new Environment(mSceneMgr, mWindow);

            // -Power-ups-
            stat = new Score();
            midGem = new MidGem(mSceneMgr, stat);
            midGem.SetPosition(new Vector3(-50, 100, 50));

            //doubleScore = new DoubleScore(mSceneMgr);

            Gems = new List<Gem>();
            gemsToRemove = new List<Gem>();

            Gems.Add(midGem);

            heart = new Heart(mSceneMgr);
            heart.SetPosition(new Vector3(-100, 50, 50));

            // Player
            player = new Player(mSceneMgr);
            
            // Enemies
            //enemies = new Enemies(mSceneMgr);
            //robots = new List<Enemies>();
            //AddRobot();

            slowGun = new SlowGun(mSceneMgr);
            collGun = new CollectableGun(mSceneMgr, slowGun, player.PlayerArmoury);
            collGun.SetPosition(new Vector3(50, 0, 0));

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

            //collGun.Update(evt);
            heart.Update(evt);

            if (shoot)
            {
                // shoot the gun
                player.Shoot();
            }

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

            gameHMD.Update(evt);
            player.Update(evt);
            
            mCamera.LookAt(player.Position);
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
            //foreach (Enemies e in robots)
            //{
            //    e.Model.Dispose();
            //}

            if (heart != null)
            {
                heart.Dispose();
            }

            if (midGem != null)
            {
                midGem.Dispose();
            }

            ////midGem.Dispose();
            foreach (Gem g in Gems)
            {
                g.Dispose();
            }

            collGun.Dispose();
            slowGun.Dispose();

            //doubleScore.Dispose();

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