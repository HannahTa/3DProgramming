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

        PhysObj physObj;
        SceneNode controlNode;

        public PlayerModel(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            
            LoadModelElements();
            this.gameNode = playerMainNode.GameNode;
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
            

            

            //base.LoadModelElements();
        }

        protected override void AssembleModel()
        {
            // Attach and assemble model
            //this.gameNode = playerMainNode.GameNode;

            //this.gameNode.AddChild(playerCellsNode.GameNode);
            
            //mSceneMgr.RootSceneNode.AddChild(playerMainNode.GameNode);
           // this.gameNode.AddChild(playerCellsNode.GameNode);
            //this.gameNode.AddChild(playerSphereNode.GameNode);
            controlNode = mSceneMgr.CreateSceneNode();

            controlNode.AddChild(playerMainNode.GameNode);
            //controlNode.AddChild(playerCellsNode.GameNode);
            //controlNode.AddChild(playerSphereNode.GameNode);
            mSceneMgr.RootSceneNode.AddChild(controlNode);
            
            float radius = 10;
            controlNode.Position += radius * Vector3.UNIT_Y;
            playerMainNode.GameNode.Position -= radius * Vector3.UNIT_Y;
            //playerCellsNode.GameNode.Position -= radius * Vector3.UNIT_Y;
            //playerSphereNode.GameNode.Position -= radius * Vector3.UNIT_Y;

            physObj = new PhysObj(radius, "Main", 0.1f, 0.7f, 0.3f);
            physObj.SceneNode = controlNode;
            physObj.Position = controlNode.Position;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));
            Physics.AddPhysObj(physObj);

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

            Physics.RemovePhysObj(physObj);
            physObj = null;
            
            //base.DisposeModel();
        }
    }
}
