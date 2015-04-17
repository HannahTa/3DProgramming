using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;

namespace RaceGame
{
    class EnemiesController : CharacterController
    {
        public EnemiesController(Character player)
        {
            speed = 50;
            character = player;
        }

        public override void Update(FrameEvent evt)
        {
            MovementsControl(evt);
            MouseControls();
            ShootingControls();
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


            if (accellerate)
            {
                move += move.NormalisedCopy * speed * 2;
            }
            else
            {
                move += move.NormalisedCopy * speed;
            }

            // Check the move field is no zero (Vector3.ZERO) and is not call the Move method from character
            if (move != Vector3.ZERO)
            {
                // pass the move field multiplied by evt.timeSinceLaseFrame
                move = move * evt.timeSinceLastFrame;
                character.Move(move);
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
