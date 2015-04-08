using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;

namespace RaceGame
{
    class Wall
    {
        Plane plane;

        public Plane Plane
        {
            get { return plane; }
        }

        SceneManager mSceneMgr;

        Entity wallEntity1;
        Entity wallEntity2;
        Entity wallEntity3;
        Entity wallEntity4;
        SceneNode wallMainNode;
        SceneNode wallNode1;
        SceneNode wallNode2;
        SceneNode wallNode3;
        SceneNode wallNode4;

        int wallWidth = 10;
        int wallHeight = 10;
        int uTiles = 10;
        int vTiles = 10;

        public Wall(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            wallWidth = 1000;
            wallHeight = 150;
            CreateWall();
        }

        private void CreateWall()
        {
            plane = new Plane(Vector3.UNIT_Y, 0);
            MeshPtr wallMeshPtr = MeshManager.Singleton.CreatePlane("wall", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME, plane, wallWidth, wallHeight, 10, 10, true, 1, uTiles, vTiles, Vector3.UNIT_Z);
            wallEntity1 = mSceneMgr.CreateEntity("wall");
            wallEntity2 = mSceneMgr.CreateEntity("wall");
            wallEntity3 = mSceneMgr.CreateEntity("wall");
            wallEntity4 = mSceneMgr.CreateEntity("wall");

            wallMainNode = mSceneMgr.CreateSceneNode();
            //wallMainNode.AttachObject(wallEntity);
            mSceneMgr.RootSceneNode.AddChild(wallMainNode);

            wallNode1 = mSceneMgr.CreateSceneNode();
            wallNode1.AttachObject(wallEntity1);
            wallMainNode.AddChild(wallNode1);

            wallNode2 = mSceneMgr.CreateSceneNode();
            wallNode2.AttachObject(wallEntity2);
            wallMainNode.AddChild(wallNode2);

            wallNode3 = mSceneMgr.CreateSceneNode();
            wallNode3.AttachObject(wallEntity3);
            wallMainNode.AddChild(wallNode3);

            wallNode4 = mSceneMgr.CreateSceneNode();
            wallNode4.AttachObject(wallEntity4);
            wallMainNode.AddChild(wallNode4);

            wallEntity1.SetMaterialName("Ground");
            wallEntity2.SetMaterialName("Ground");
            wallEntity3.SetMaterialName("Ground");
            wallEntity4.SetMaterialName("Ground");

            wallNode1.Rotate(new Quaternion(Mogre.Math.Sqrt(0.5f), -Mogre.Math.Sqrt(0.5f), 0, 0)); // Rotate -90 degrees around X axis
            wallNode1.SetPosition(0, 0, 500);
            
            wallNode2.Rotate(new Quaternion(Mogre.Math.Sqrt(0.5f), 0, -Mogre.Math.Sqrt(0.5f), 0));
            wallNode2.Rotate(new Quaternion(Mogre.Math.Sqrt(0.5f), -Mogre.Math.Sqrt(0.5f), 0, 0));
            wallNode2.SetPosition(-500, 0, 0);

            wallNode3.Rotate(new Quaternion(Mogre.Math.Sqrt(0.5f), 0, Mogre.Math.Sqrt(0.5f), 0));
            wallNode3.Rotate(new Quaternion(Mogre.Math.Sqrt(0.5f), -Mogre.Math.Sqrt(0.5f), 0, 0));
            wallNode3.SetPosition(500, 0, 0);
            
            wallNode4.Rotate(new Quaternion(Mogre.Math.Sqrt(0.5f), Mogre.Math.Sqrt(0.5f), 0, 0)); // Rotate -90 degrees around X axis
            //wallNode4.Rotate(new Quaternion(Mogre.Math.Sqrt(0.5f), 0, 1, 0)); // Rotate 180 degrees around X axis
            
            wallNode4.SetPosition(0, 0, -500);
        }

        public void Dispose()
        {
            wallNode1.DetachAllObjects();
            wallNode1.Parent.RemoveChild(wallNode1);
            wallNode1.Dispose();

            wallNode2.DetachAllObjects();
            wallNode2.Parent.RemoveChild(wallNode2);
            wallNode2.Dispose();

            wallNode3.DetachAllObjects();
            wallNode3.Parent.RemoveChild(wallNode3);
            wallNode3.Dispose();

            wallNode4.DetachAllObjects();
            wallNode4.Parent.RemoveChild(wallNode4);
            wallNode4.Dispose();

            wallMainNode.DetachAllObjects();
            wallMainNode.Parent.RemoveChild(wallMainNode);
            wallMainNode.Dispose();

            wallEntity1.Dispose();
            wallEntity2.Dispose();
            wallEntity3.Dispose();
            wallEntity4.Dispose();
        }
    }
}
