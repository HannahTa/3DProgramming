using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;

namespace RaceGame
{
    class EnemiesController : CharacterController
    {
        int random;
        int count = 0;

        public EnemiesController(Character player)
        {
            speed = 50;
            character = player;
        }

        public override void Update(FrameEvent evt)
        {
            //random = (int)Mogre.Math.RangeRandom(0, 7);
            
            //if (random == 0 && count == 0)
            //{
            //    right = true;
            //}
            //if (random == 1 && count == 0)
            //{
            //    left = true;
            //}
            //if (random == 2 && count == 0)
            //{
            //    forward = true;
            //}
            //if (random == 3 && count == 0)
            //{
            //    backward = true;
            //}
            //if (random == 4 && count == 0)
            //{
            //    right = true;
            //    forward = true;
            //}
            //if (random == 5 && count == 0)
            //{
            //    left = true;
            //    forward = true;
            //}
            //if (random == 6 && count == 0)
            //{
            //    right = true;
            //    backward = true;
            //}
            //if (random == 7 && count == 0)
            //{
            //    left = true;
            //    backward = true;
            //}

            //count++;

            //System.Console.WriteLine(count);

            MovementsControl(evt);
            MouseControls();
            ShootingControls();
            //base.Update(evt);

            //if (count == 750)
            //{
            //    count = 0;
            //    right = false;
            //    left = false;
            //    forward = false;
            //    backward = false;
            //}
            
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
