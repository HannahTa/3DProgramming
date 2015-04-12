using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;

namespace RaceGame
{
    class InputsManager
    {
        // Keyboard, mouse and inputs managers
        protected MOIS.Keyboard mKeyboard;
        protected MOIS.Mouse mMouse;
        protected MOIS.InputManager mInputMgr;

        PlayerController playerController;

        public PlayerController PlayerController
        {
            set { playerController = value; }
        }

        private InputsManager()
        {

        }

        private static InputsManager instance;

        public static InputsManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InputsManager();
                }
                return instance;
            }
        }

        public bool ProcessInput(FrameEvent ect)
        {
            Vector3 displacements = Vector3.ZERO;
            Vector3 angles = Vector3.ZERO;
            mKeyboard.Capture();
            mMouse.Capture();

            if (mKeyboard.IsKeyDown(MOIS.KeyCode.KC_A))
            {
                playerController.Left = true;
                displacements += new Vector3(-0.1f, 0, 0);
            }
            else
            {
                playerController.Left = false;
            }
            if (mKeyboard.IsKeyDown(MOIS.KeyCode.KC_D))
            {
                playerController.Right = true;
                displacements += new Vector3(0.1f, 0, 0);
                //System.Console.WriteLine("InputManager forward - d");
            }
            else
            {
                playerController.Right = false;
            }
            if (mKeyboard.IsKeyDown(MOIS.KeyCode.KC_W) )
            {
                playerController.Forward = true;
                displacements += new Vector3(0, 0, 0.1f);
                //System.Console.WriteLine("InputManager forward - w");
            }
            else
            {
                playerController.Forward = false;
            }
            if (mKeyboard.IsKeyDown(MOIS.KeyCode.KC_S))
            {
                playerController.Backward = true;
                displacements += new Vector3(0, 0, -0.1f);
            }
            else
            {
                playerController.Backward = false;
            }
            if (mKeyboard.IsKeyDown(MOIS.KeyCode.KC_SPACE))
            {
                playerController.Shoot = true;

            }
            else
            {
                playerController.Shoot = false;
            }
            //if (mKeyboard.IsKeyDown(MOIS.KeyCode.KC_E)) // Swap guns here
            //{
            //    playerController.
            //}
            
            //player.Move(displacements); //UNCOMMENT THIS!!


            //mMouse.MouseMoved .MouseMoved(MOIS.MouseButtonID.MB_Left)
            if (mMouse.MouseState.ButtonDown(MOIS.MouseButtonID.MB_Left))
            {
                angles.y = mMouse.MouseState.X.rel;
            }
            if (mMouse.MouseState.ButtonDown(MOIS.MouseButtonID.MB_Right))
            {
                angles.x = mMouse.MouseState.Y.rel;
            }
            playerController.Angles = angles;
            
            //playerController.Rotate(angles); UNCOMMENT THIS!!
            return true; 
        }

        public void InitInput(ref int windowHandle)
        {
            mInputMgr = MOIS.InputManager.CreateInputSystem((uint)windowHandle);
            mKeyboard = (MOIS.Keyboard)mInputMgr.CreateInputObject(MOIS.Type.OISKeyboard, true);
            mMouse = (MOIS.Mouse)mInputMgr.CreateInputObject(MOIS.Type.OISMouse, false);

            mKeyboard.KeyPressed += new MOIS.KeyListener.KeyPressedHandler(OnKeyPressed);
        }

        public bool OnKeyPressed(MOIS.KeyEvent arg)
        {
            switch (arg.key)
            {
                case MOIS.KeyCode.KC_SPACE:
                    {
                        Tutorial.shoot = true;
                        break; // For shooting!
                    }
                case MOIS.KeyCode.KC_E:
                    {
                        break;
                    }
                case MOIS.KeyCode.KC_ESCAPE:
                    {
                        return false;
                    }
            }
            return true;
        }
    }
}
