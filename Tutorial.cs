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

        Environment environment;
        Player player;
        Enemies enemies;

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
            Vector3 pos = new Vector3(50, 100, 0);
            physics = new Physics();

            PlayerStats playerStats = new PlayerStats();
            gameHMD = new GameInterface(mSceneMgr, mWindow, playerStats);

            mSceneMgr.ShadowTechnique = ShadowTechnique.SHADOWTYPE_STENCIL_MODULATIVE;

            environment = new Environment(mSceneMgr, mWindow);
            player = new Player(mSceneMgr);
            enemies = new Enemies(mSceneMgr);

            cameraNode = mSceneMgr.CreateSceneNode();
            cameraNode.AttachObject(mCamera);
            //player.Model.GameNode.AddChild(cameraNode); // Camera on player

            inputsManager.PlayerController = (PlayerController)player.Controller;
            
            enemies.Model.SetPosition(pos);
            //player.Model.SetPosition(pos);

            physics.StartSimTimer();    // Must be the last method in create
        }

        /// <summary>
        /// This method update the scene after a frame has finished rendering
        /// </summary>
        /// <param name="evt"></param>
        protected override void UpdateScene(FrameEvent evt)
        {
            base.UpdateScene(evt);
            physics.UpdatePhysics(0.01f);
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

            if (enemies != null)
            {
                enemies.Model.DisposeModel();
            }

            environment.Dispose();

            gameHMD.Dispose();
            physics.Dispose();
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