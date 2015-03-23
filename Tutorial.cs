using Mogre;
using Mogre.TutorialFramework;
using System;

using PhysicsEng;
using System.Collections.Generic;

namespace RaceGame
{
    class Tutorial : BaseApplication
    {
        Player player;
        Enemies enemies;

        SceneNode cameraNode;

        static InputsManager inputsManager = InputsManager.Instance;

        Physics physics;

        //ManualObject manObj;

        public static void Main()
        {
            new Tutorial().Go();            // This method starts the rendering loop
        }

        /// <summary>
        /// This method create the initial scene
        /// </summary>
        protected override void CreateScene()
        {
            Vector3 pos = new Vector3(50, 0, 0);

            physics = new Physics();

            player = new Player(mSceneMgr);
            enemies = new Enemies(mSceneMgr);

            enemies.Model.SetPosition(pos);

            cameraNode = mSceneMgr.CreateSceneNode();
            cameraNode.AttachObject(mCamera);
            
            player.Model.GameNode.AddChild(cameraNode); // Camera on player

            inputsManager.PlayerController = (PlayerController)player.Controller;

            // Did not work
            //player.Controller = (PlayerController)player.Controller; // I think..
            
            physics.StartSimTimer();    // Must be the last method in create
        }

        /// <summary>
        /// This method destorys the scene
        /// </summary>
        protected override void DestroyScene()
        {
            //enemies.Model.DisposeModel();
            //player.Model.DisposeModel();


            if (player != null)
            {
                player.Model.DisposeModel();
            }

            if (enemies != null)
            {
                enemies.Model.DisposeModel();
            }

            physics.Dispose();
            //base.DestroyScene();
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
        /// This method update the scene after a frame has finished rendering
        /// </summary>
        /// <param name="evt"></param>
        protected override void UpdateScene(FrameEvent evt)
        {
            physics.UpdatePhysics(0.01f);
            player.Update(evt);
            mCamera.LookAt(player.Position);
            base.UpdateScene(evt);
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