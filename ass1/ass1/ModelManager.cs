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
    public class ModelManager : DrawableGameComponent {

        SelectionCube selectionCube;
        Ground ground;

        List<BasicModel> models = new List<BasicModel>();

        public ModelManager(Game game) : base(game) {

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
            float? distance = pickRay.Intersects(ground.groundPlane);

            if (distance != null) {
                Vector3 pickedPosition = nearPoint + direction * (float)distance;
                Debug.WriteLine("Picked Position = X: " + pickedPosition.X + " Y: " + pickedPosition.Y + " Z: " + pickedPosition.Z);
                selectionCube.ChangeSelectionPosition(new Vector3(pickedPosition.X, -pickedPosition.Z, pickedPosition.Y - 15));
            }

            //Loop through all the models and call update
            for (int i = 0; i < models.Count; ++i) {
                models[i].Update();
            }

            base.Update(gameTime);
        }

        protected override void LoadContent() {
            ground = new Ground(Game.Content.Load<Model>(@"GroundModels\grassSquare"), new Vector3(0, -15, 0));
            models.Add(ground);
            selectionCube = new SelectionCube(Game.Content.Load<Model>(@"Models\selectionCube"), new Vector3(0, -15, 0));
            models.Add(selectionCube);
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime) {

            foreach(BasicModel model in models) {
                model.Draw(GetCamera());
            }

            base.Draw(gameTime);
        }

        /// <summary>
        /// Helper method to retrieve the camera from the game class
        /// </summary>
        /// <returns></returns>
        private Camera GetCamera() {
            return ((Game1)Game).camera;
        }

    }
}
