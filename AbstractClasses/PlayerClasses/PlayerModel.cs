using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;

using PhysicsEng;

namespace RaceGame
{
    class PlayerModel : CharacterModel
    {
        // ModelElement object for each node in the player scenegraph
        ModelElement playerMainNode; // 
        ModelElement playerCellsNode; //
        ModelElement playerSphereNode; //

        //PhysObj physObj;
        SceneNode controlNode;

        public PlayerModel(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            this.gameNode = playerMainNode.GameNode;
            LoadModelElements();
            AssembleModel();

            //this.gameNode = playerMainNode.GameNode;
        }

        protected override void LoadModelElements()
        {
            // Load model
            System.Console.WriteLine("LoadModelElements");
            playerMainNode = new ModelElement(mSceneMgr, "Main.mesh");
            playerCellsNode = new ModelElement(mSceneMgr, "PowerCells.mesh");
            playerSphereNode = new ModelElement(mSceneMgr, "Sphere.mesh");

            // Physics
            controlNode = mSceneMgr.CreateSceneNode();
            controlNode.AddChild(this.gameNode);
            mSceneMgr.RootSceneNode.AddChild(controlNode);

            float radius = 10;
            controlNode.Position += radius * Vector3.UNIT_Y;
            this.gameNode.Position -= radius * Vector3.UNIT_Y;

            physObj = new PhysObj(radius, "Player", 0.1f, 0.7f, 0.3f);
            physObj.SceneNode = controlNode;
            physObj.Position = controlNode.Position;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));
            Physics.AddPhysObj(physObj);

            //base.LoadModelElements();
        }

        protected override void AssembleModel()
        {
            // Attach and assemble model
            //this.gameNode = playerMainNode.GameNode;

            //this.gameNode.AddChild(playerCellsNode.GameNode);
            
            mSceneMgr.RootSceneNode.AddChild(playerMainNode.GameNode);
            this.gameNode.AddChild(playerCellsNode.GameNode);
            this.gameNode.AddChild(playerSphereNode.GameNode);

            
            
            //mSceneMgr.RootSceneNode.AddChild(playerCellsNode.GameNode);
            //mSceneMgr.RootSceneNode.AddChild(playerSphereNode.GameNode);
            
            //base.AssembleModel();
        }

        public override void DisposeModel()
        {
            // Call Dispose method from each of the ModelElements
            playerCellsNode.Dispose();
            playerSphereNode.Dispose();
            playerMainNode.Dispose(); // Main gets deleted last as that is the parent node
            //base.DisposeModel();
        }
    }
}
