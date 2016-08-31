using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ass1 {
    class WorldModelManager : ModelManager {

        public Ground ground;
        public SelectionCube selectionCube;

        MouseState prevMouseState;

        public WorldModelManager(Game game) : base(game) {
            prevMouseState = Mouse.GetState();
        }

        protected override void LoadContent() {

            ground = new Ground(Game.Content.Load<Model>(@"GroundModels\ground"), new Vector3(0, 0, 0));
            models.Add(ground);
            selectionCube = new SelectionCube(Game.Content.Load<Model>(@"Models\selectionCube"), new Vector3(0, 0, 0));
            models.Add(selectionCube);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime) {
            
        }

        public void CreateTurret(Turret turret) {
            models.Add(turret);
        }

        
    }
}
