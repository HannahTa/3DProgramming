using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;

namespace RaceGame
{
    class Wall
    {
        Plane plane1;

        public Plane Plane1
        {
            get { return plane1; }
        }

        Plane plane2;

        public Plane Plane2
        {
            get { return plane2; }
        }

        Plane plane3;

        public Plane Plane3
        {
            get { return plane3; }
        }

        Plane plane4;

        public Plane Plane4
        {
            get { return plane4; }
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

            wallMainNode = mSceneMgr.CreateSceneNode();
            //wallMainNode.AttachObject(wallEntity);
            mSceneMgr.RootSceneNode.AddChild(wallMainNode);

            CreateWall1();
            CreateWall2();
            CreateWall3();
            CreateWall4();
        }

        private void CreateWall1()
        {
            plane1 = new Plane(Vector3.UNIT_X, -500);
            //plane = new Plane(Vector3.UNIT_Z, 0);
            MeshPtr wallMeshPtr = MeshManager.Singleton.CreatePlane("wall", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME, plane1, wallWidth, wallHeight, 10, 10, true, 1, uTiles, vTiles, Vector3.UNIT_Z);
            //MeshPtr wallMeshPtrZ = MeshManager.Singleton.CreatePlane("wall", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME, planeZ, wallWidth, wallHeight, 10, 10, true, 1, uTiles, vTiles, Vector3.UNIT_Z);

            wallEntity1 = mSceneMgr.CreateEntity("wall");
            
            wallNode1 = mSceneMgr.CreateSceneNode();
            wallNode1.AttachObject(wallEntity1);
            wallEntity1.SetMaterialName("Ground");
            wallNode1.Rotate(new Quaternion(Mogre.Math.Sqrt(0.5f), -Mogre.Math.Sqrt(0.5f), 0, 0)); // Rotate -90 degrees around X axis
            
            wallMainNode.AddChild(wallNode1);
        }

        public void CreateWall2()
        {
            plane2 = new Plane(Vector3.NEGATIVE_UNIT_X, -500);
            MeshPtr wallMeshPtr = MeshManager.Singleton.CreatePlane("wall", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME, plane2, wallWidth, wallHeight, 10, 10, true, 1, uTiles, vTiles, Vector3.UNIT_Z);

            wallEntity2 = mSceneMgr.CreateEntity("wall");

            wallNode2 = mSceneMgr.CreateSceneNode();
            wallNode2.AttachObject(wallEntity2);
            
            wallEntity2.SetMaterialName("Ground");
            
            wallNode2.SetPosition(0, 0, 0);
            wallNode2.Rotate(new Quaternion(0, 0, 0, Mogre.Math.Sqrt(0.5f)));
            //wallNode2.Rotate(new Quaternion(0, 0, Mogre.Math.Sqrt(0.5f), 0)); // Rotate -90 degrees around X axis
            wallNode2.Rotate(new Quaternion(Mogre.Math.Sqrt(0.5f), Mogre.Math.Sqrt(0.5f), 0, 0));
            //wallNode2.Rotate(new Quaternion(Mogre.Math.Sqrt(0.5f), 0, Mogre.Math.Sqrt(0.5f), 0));//wallNode2.Rotate(new Quaternion(1, -Mogre.Math.Sqrt(0.5f), 0, 0)); // Rotate -90 degrees around X axis
    
            wallMainNode.AddChild(wallNode2);
        }

        public void CreateWall3()
        {
            plane3 = new Plane(Vector3.UNIT_Z, -500);
            MeshPtr wallMeshPtr = MeshManager.Singleton.CreatePlane("wall", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME, plane3, wallWidth, wallHeight, 10, 10, true, 1, uTiles, vTiles, Vector3.UNIT_X);

            wallEntity3 = mSceneMgr.CreateEntity("wall");

            wallNode3 = mSceneMgr.CreateSceneNode();
            wallNode3.AttachObject(wallEntity3);

            wallEntity3.SetMaterialName("Ground");

            wallNode3.SetPosition(0, 0, 0);
            wallNode3.Rotate(new Quaternion(Mogre.Math.Sqrt(0.5f), 0, Mogre.Math.Sqrt(0.5f), 0));
            wallNode3.Rotate(new Quaternion(Mogre.Math.Sqrt(0.5f), Mogre.Math.Sqrt(0.5f), 0, 0));
            //wallNode3.SetPosition(500, 0, 0);

            wallMainNode.AddChild(wallNode3);
        }

        public void CreateWall4()
        {
            plane4 = new Plane(Vector3.NEGATIVE_UNIT_Z, -500);
            MeshPtr wallMeshPtr = MeshManager.Singleton.CreatePlane("wall", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME, plane4, wallWidth, wallHeight, 10, 10, true, 1, uTiles, vTiles, Vector3.UNIT_X);

            wallEntity4 = mSceneMgr.CreateEntity("wall");
            
            wallNode4 = mSceneMgr.CreateSceneNode();
            wallNode4.AttachObject(wallEntity4);
            
            wallEntity4.SetMaterialName("Ground");

            //wallNode4.SetPosition(0, 0, 0);
            wallNode4.Rotate(new Quaternion(Mogre.Math.Sqrt(0.5f), 0, -Mogre.Math.Sqrt(0.5f), 0)); // Rotate -90 degrees around X axis
            wallNode4.Rotate(new Quaternion(Mogre.Math.Sqrt(0.5f), -Mogre.Math.Sqrt(0.5f), 0, 0)); // Rotate 180 degrees around X axis

            //wallNode4.SetPosition(0, 0, -500);

            wallMainNode.AddChild(wallNode4);
        }

        public void Dispose()
        {
            wallNode1.DetachAllObjects();
            wallNode1.Parent.RemoveChild(wallNode1);
            wallNode1.Dispose();

            wallNode2.DetachAllObjects();
            wallNode2.Parent.RemoveChild(wallNode2);
            wallNode2.Dispose();

            //wallNode3.DetachAllObjects();
            //wallNode3.Parent.RemoveChild(wallNode3);
            //wallNode3.Dispose();

            //wallNode4.DetachAllObjects();
            //wallNode4.Parent.RemoveChild(wallNode4);
            //wallNode4.Dispose();

            wallMainNode.DetachAllObjects();
            wallMainNode.Parent.RemoveChild(wallMainNode);
            wallMainNode.Dispose();

            wallEntity1.Dispose();
            wallEntity2.Dispose();
            //wallEntity3.Dispose();
            //wallEntity4.Dispose();
        }
    }
}
