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
        
        SceneNode wheelsGroupNode;
        SceneNode hullGroupNode;
        SceneNode modelNode; //??

        SceneNode gunGroupNode; //

        SceneNode controlNode;

        public PlayerModel(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            
            LoadModelElements();
            
            AssembleModel();
            //this.gameNode = playerMainNode.GameNode;//controlNode;
            //physObj.SceneNode = gameNode;
            //this.SetPosition(new Vector3(-50, 100, 0));
            //this.gameNode = playerMainNode.GameNode;
        }

        protected override void LoadModelElements()
        {
            // Load model
            wheelsGroupNode = mSceneMgr.CreateSceneNode();
            
            hullGroupNode = mSceneMgr.CreateSceneNode();
            
            modelNode = mSceneMgr.CreateSceneNode();
            
            gunGroupNode = mSceneMgr.CreateSceneNode();

            playerMainNode = new ModelElement(mSceneMgr, "Main.mesh");
            playerCellsNode = new ModelElement(mSceneMgr, "PowerCells.mesh");
            playerSphereNode = new ModelElement(mSceneMgr, "Sphere.mesh");

            //base.LoadModelElements();
        }

        protected override void AssembleModel()
        {
            // Attach and assemble model
            wheelsGroupNode.AddChild(playerSphereNode.GameNode);
            
            hullGroupNode.AddChild(playerCellsNode.GameNode);
            hullGroupNode.AddChild(playerMainNode.GameNode);
            hullGroupNode.AddChild(wheelsGroupNode);
            hullGroupNode.AddChild(gunGroupNode);

            modelNode.AddChild(hullGroupNode);

            controlNode = mSceneMgr.CreateSceneNode();
            controlNode.AddChild(modelNode);
            mSceneMgr.RootSceneNode.AddChild(controlNode);
            
            float radius = 50;
            controlNode.Position += radius * Vector3.UNIT_Y;
            modelNode.Position -= radius * Vector3.UNIT_Y;

            physObj = new PhysObj(radius, "Player", 0.1f, 0.7f, 0.3f);
            physObj.SceneNode = controlNode;
            physObj.Position = controlNode.Position;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));
            //physObj.AddForceToList(new FrictionForce(physObj));
            Physics.AddPhysObj(physObj);
            System.Console.WriteLine(physObj.CollisionList);
            this.gameNode = modelNode;
            
            //base.AssembleModel();
        }

        public override void DisposeModel()
        {
            // Call Dispose method from each of the ModelElements
            playerSphereNode.GameNode.RemoveAllChildren();
            playerSphereNode.GameNode.Parent.RemoveChild(playerSphereNode.GameNode);
            playerSphereNode.GameNode.DetachAllObjects();
            playerSphereNode.Dispose();

            wheelsGroupNode.RemoveAllChildren();
            wheelsGroupNode.Parent.RemoveChild(wheelsGroupNode);
            wheelsGroupNode.DetachAllObjects();
            wheelsGroupNode.Dispose();

            playerCellsNode.GameNode.RemoveAllChildren();
            playerCellsNode.GameNode.Parent.RemoveChild(playerCellsNode.GameNode);
            playerCellsNode.GameNode.DetachAllObjects();
            playerCellsNode.Dispose();

            playerMainNode.GameNode.RemoveAllChildren();
            playerMainNode.GameNode.Parent.RemoveChild(playerMainNode.GameNode);
            playerMainNode.GameNode.DetachAllObjects();
            playerMainNode.Dispose();

            gunGroupNode.RemoveAllChildren();
            gunGroupNode.Parent.RemoveChild(gunGroupNode);
            gunGroupNode.DetachAllObjects();
            gunGroupNode.Dispose();

            hullGroupNode.RemoveAllChildren();
            hullGroupNode.Parent.RemoveChild(hullGroupNode);
            hullGroupNode.DetachAllObjects();
            hullGroupNode.Dispose();

            modelNode.RemoveAllChildren();
            modelNode.Parent.RemoveChild(modelNode);
            modelNode.DetachAllObjects();
            modelNode.Dispose();

            

            //playerSphereNode.Dispose();
            //playerMainNode.Dispose(); // Main gets deleted last as that is the parent node

            Physics.RemovePhysObj(physObj);
            physObj = null;
            
            //base.DisposeModel();
        }

        public void AttachGun(Gun gun)
        {
            // Checks whether the gunGroupNode has any child(gunGroupNode.GameNode.NumChildren()!=0) 
            // and if it has children call the RemoveAllChildren() methods from its GameNode

            // outside of the if statment add the GameNode of the gun passed as parameter as child of the
            // GameNode of the gunGorupNode

            if (gunGroupNode.NumChildren() != 0)
            {
                gunGroupNode.RemoveAllChildren();
            }

            gunGroupNode.AddChild(gun.GameNode);
        }
    }
}
