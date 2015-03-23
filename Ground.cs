using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;

namespace RaceGame
{
    class Ground
    {
        Plane plane;

        public Plane Plane
        {
            get { return plane; }
        }

        SceneManager mSceneMgr;

        Entity groundEntity;
        SceneNode groundNode;

        int groundWidth = 10;
        int groundHeight = 10;
        int uTiles = 10;
        int vTiles = 10;

        public Ground(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            groundWidth = 1000;
            groundHeight = 1000;
            CreateGround();
        }

        private void CreateGround()
        {
            plane = new Plane(Vector3.UNIT_Y, 0);
            MeshPtr groundMeshPtr = MeshManager.Singleton.CreatePlane("ground", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME, plane, groundWidth, groundHeight, 10, 10, true, 1, uTiles, vTiles, Vector3.UNIT_Z);
            groundEntity = mSceneMgr.CreateEntity("ground");

            groundNode = mSceneMgr.CreateSceneNode();
            groundNode.AttachObject(groundEntity);
            mSceneMgr.RootSceneNode.AddChild(groundNode);

            groundEntity.SetMaterialName("Ground");
        }

        public void Dispose()
        {
            groundNode.DetachAllObjects();
            groundNode.Parent.RemoveChild(groundNode);
            groundNode.Dispose();
            groundEntity.Dispose();
        }
    }
}
