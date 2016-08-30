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

        Ground ground;
        SelectionCube selectionCube;

        MouseState prevMouseState;

        public WorldModelManager(Game game) : base(game) {
            prevMouseState = Mouse.GetState();
        }

        protected override void LoadContent() {

            ground = new Ground(Game.Content.Load<Model>(@"GroundModels\grassSquare"), new Vector3(0, 0, 0));
            models.Add(ground);
            selectionCube = new SelectionCube(Game.Content.Load<Model>(@"Models\selectionCube"), new Vector3(0, 0, 0));
            models.Add(selectionCube);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime) {
            //THE LOGIC FOR DETERMINING THE POSITION OF THE MOUSE RELATIVE TO GROUND PLANE
            MouseState mouseState = Mouse.GetState();

            Vector3 nearsource = new Vector3((float)mouseState.Position.X, (float)mouseState.Position.Y, 0f);
            Vector3 farsource = new Vector3((float)mouseState.Position.X, (float)mouseState.Position.Y, 1f);

            Matrix world = Matrix.CreateTranslation(0, 0, 0);

            Vector3 nearPoint = GraphicsDevice.Viewport.Unproject(nearsource, GetCamera().projection, GetCamera().view, world);
            Vector3 farPoint = GraphicsDevice.Viewport.Unproject(farsource, GetCamera().projection, GetCamera().view, world);

            // Create a ray from the near clip plane to the far clip plane.
            Vector3 direction = farPoint - nearPoint;
            direction.Normalize();
            Ray pickRay = new Ray(nearPoint, direction);

            // calcuate distance of plane intersection point from ray origin
            float? distance = pickRay.Intersects(Ground.groundPlane);

            if (distance != null) {
                Vector3 pickedPosition = nearPoint + direction * (float)distance;

                selectionCube.ChangeSelectionPosition(new Vector3(pickedPosition.X, -pickedPosition.Z, pickedPosition.Y));
                Debug.WriteLine("Cube position is now: X: " + pickedPosition.X + " Y: " + -pickedPosition.Z + " Z: " + pickedPosition.Y);
                //CREATION OF THE TURRET ON CLICK
                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released) {
                    models.Add(new Turret(Game.Content.Load<Model>(@"Models\Turrets\turretStock"),
                        new Vector3(pickedPosition.X, -pickedPosition.Z, pickedPosition.Y), null));
                }

            }

            prevMouseState = mouseState;
            base.Update(gameTime);
        }

        
    }
}
