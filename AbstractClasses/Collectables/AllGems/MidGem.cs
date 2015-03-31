using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;

using PhysicsEng;

namespace RaceGame
{
    class MidGem : Gem
    {
        // Initialize the physiscs engine and add gravity and friction forces to it
        // At the end of the loadModel of the children classes you should pass the gameNode
        // to the physObj through the propert SceneNode of the physics object

        // Write in the Update method the cde for collision detection with the player as
        // explained in the video, just use the remove fild from Collectable setting it to
        // true, use the method increase from the score object to change the score and add
        // a Dispose() call before the break statement

        PhysObj physObj;
        SceneNode controlNode;

        //ModelElement midGemNode1;
        Entity midGemEntity;
        SceneNode midGemNode;

        public MidGem(SceneManager mSceneMgr, Stat score):base(mSceneMgr, score)
        {
            this.mSceneMgr = mSceneMgr;
            increase = 100;
            LoadModel();
            this.gameNode = midGemNode;
        }

        protected override void LoadModel()
        {
            base.LoadModel();
            // Load the geometry for the power up (gem) and the scene graph 
            // nodes for it using as usual the gameNode and gameEntity
            midGemEntity = mSceneMgr.CreateEntity("Gem.mesh");
            midGemNode = mSceneMgr.CreateSceneNode();
            midGemNode.AttachObject(midGemEntity);

            controlNode = mSceneMgr.CreateSceneNode();
            controlNode.AddChild(midGemNode);
            mSceneMgr.RootSceneNode.AddChild(controlNode);
            
            // Physics
            float radius = 10;
            controlNode.Position += radius * Vector3.UNIT_Y;
            midGemNode.Position += radius * Vector3.UNIT_Y;

            physObj = new PhysObj(radius, "MidGem", 0.1f, 0.7f, 0.3f);
            physObj.SceneNode = controlNode;
            physObj.Position = controlNode.Position;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));
            Physics.AddPhysObj(physObj);
        }

        protected void Update()
        {
            // Code for collision detection
            //remove = true
            //method increase score object
            //add a dispose call before the break statement
        }
    }
}
