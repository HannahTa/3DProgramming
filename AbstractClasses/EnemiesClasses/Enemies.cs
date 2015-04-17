using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;

namespace RaceGame
{
    class Enemies : Character
    {
        // Fields
        //EnemiesModel model;
        //EnemiesController controller;
        //EnemiesStats stats;

        float mDistance = 0.0f;                 //Left to travel
        Vector3 mDirection = Vector3.ZERO;      //Direction the object is moving
        Vector3 mDestination = Vector3.ZERO;    //Detingation the object is moving towards
        //LinkedList<Vector3> mWalkList = null;   //Containing the waypoints

        public Enemies(SceneManager mSceneMgr)
        {
            model = new EnemiesModel(mSceneMgr);
            controller = new EnemiesController(this);
            stats = new EnemiesStats();
        }

        public override void Update(FrameEvent evt)
        {
            //model.Animate();
            controller.Update(evt);
            FrameStarted(evt);
            model.RemoveMe = model.IsCollidingWith("Projectile");
            if (model.RemoveMe)
            {
                model.Dispose();
            }
            base.Update(evt);
        }

        public override void Shoot()
        {
            base.Shoot();
        }




        protected bool nextLocation()
        {
            if (Tutorial.mWalkList.Count == 0)
                return false;
            return true;
        }

        bool FrameStarted(FrameEvent evt)
        {
            float move = controller.Speed * (evt.timeSinceLastFrame);
            mDistance -= move;

            if (mDistance <= 0.0f)
            {
                if (!TurnNextLocation())
                {
                    return true;
                }
            }
            else
            {
                model.GameNode.Translate(mDirection * move);
            }

            return true;
        }

        bool TurnNextLocation()
        {
            if (nextLocation())
            {
                LinkedListNode<Vector3> tmp;
                mDestination = Tutorial.mWalkList.First.Value;
                tmp = Tutorial.mWalkList.First;
                Tutorial.mWalkList.RemoveFirst();
                Tutorial.mWalkList.AddLast(tmp);

                mDirection = mDestination - Position;
                mDistance = mDirection.Normalise();

                Vector3 src = model.GameNode.Orientation * Vector3.UNIT_X;

                if ((1.0f + src.DotProduct(mDirection)) < 0.0001f)
                {
                    model.GameNode.Yaw(new Angle(180.0f));
                }
                else
                {
                    Quaternion quat = src.GetRotationTo(mDirection);
                    model.GameNode.Rotate(quat);
                }
                return true;
            }
            return false;
        }
    }
}
