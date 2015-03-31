using System;
using Mogre;

namespace RaceGame
{
    /// <summary>
    /// This class implements the gound
    /// </summary>
    class Ground
    {
        Plane plane;

        /// <summary>
        /// Read only. This propery returns the ground plane
        /// </summary>
        public Plane Plane
        {
            get { return plane; }
        }

        #region As in Demo 11
        SceneManager mSceneMgr;

        Entity groundEntity;
        SceneNode groundNode;

        int groundWidth = 10;
        int groundHeight = 10;
        int uTiles = 10;
        int vTiles = 10;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mSceneMgr">A reference to the Scene Manager</param>
        public Ground(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            groundWidth = 1000;
            groundHeight = 1000;
            CreateGround();
        }
        #endregion

        /// <summary>
        /// This method set up the mesh for the ground and atthach it to the scenegraph
        /// </summary>
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

        #region As in Demo 11
        /// <summary>
        /// This method disposes of the scene node and enitity
        /// </summary>
        public void Dispose()
        {
            groundNode.DetachAllObjects();
            groundNode.Parent.RemoveChild(groundNode);
            groundNode.Dispose();
            groundEntity.Dispose();
        }
        #endregion
    }
}
