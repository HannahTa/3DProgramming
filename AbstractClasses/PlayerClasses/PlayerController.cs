using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;

namespace RaceGame
{
    class PlayerController : CharacterController
    {
        public PlayerController(Character player)
        {
            speed = 100;
            character = player;
        }

        public override void Update(FrameEvent evt)
        {
            MovementsControl(evt);
            MouseControls();
            ShootingControls();
            //System.Console.WriteLine("in update");
            
            //base.Update(evt);
        }

        private void MovementsControl(FrameEvent evt)
        {
            Vector3 move = Vector3.ZERO;

            if (forward)
            {
                move += character.Model.Forward; // backward = - forward, right = - left
            }
            if (left)
            {
                move += character.Model.Left;
            }
            if (right)
            {
                move -= character.Model.Left;
            }
            if (backward)
            {
                move -= character.Model.Forward;
            }
            if (up)
            {
                move += character.Model.Up;
            }
            if (down)
            {
                move -= character.Model.Up;
            }

            move += move.NormalisedCopy * speed;

            if (accellerate)
            {
                move += move * 2;
            }
            //else
            //{
            //    move += move.NormalisedCopy * speed;
            //}

            // Check the move field is no zero (Vector3.ZERO) and is not call the Move method from character
            if (move != Vector3.ZERO)
            {
                // pass the move field multiplied by evt.timeSinceLaseFrame
                character.Move(move * evt.timeSinceLastFrame);
            }
        }

        private void MouseControls()
        {
            character.Model.GameNode.Yaw(Mogre.Math.AngleUnitsToRadians(angles.y));
        }

        private void ShootingControls()
        {

        }
    }
}
