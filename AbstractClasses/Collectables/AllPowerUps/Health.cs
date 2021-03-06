﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    class Health : PowerUp
    {
        Entity healthEntity;
        SceneNode healthNode;

        public Health(SceneManager mSceneMgr, Stat health) : base(mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            this.stat = health;
            increase = 5;
            LoadModel();
        }

        protected override void LoadModel()
        {
            healthEntity = mSceneMgr.CreateEntity("Heart.mesh");

            healthNode = mSceneMgr.CreateSceneNode();
            healthNode.AttachObject(healthEntity);
            healthNode.Scale(6f, 6f, 6f);
            mSceneMgr.RootSceneNode.AddChild(healthNode);

            remove = false;
            
            physObj = new PhysObj(7, "Heart", 0.3f, 0.5f);
            physObj.AddForceToList(new WeightForce(physObj.InvMass));
            physObj.SceneNode = healthNode;
            physObj.Position = healthNode.Position;

            Physics.AddPhysObj(physObj);

            this.gameNode = healthNode;
            //base.LoadModel();
        }

        public override void Dispose()
        {
            Physics.RemovePhysObj(physObj);
            physObj = null;

            base.Dispose();
        }
    }
}
