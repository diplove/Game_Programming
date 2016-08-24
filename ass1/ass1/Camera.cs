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
    class Camera : Microsoft.Xna.Framework.GameComponent {

        private static float SCROLL_SPEED = 10.0f;
        private static float MAX_ZOOM = 300.0f;
        private static float MIN_ZOOM = 150.0f;

        //Camera Matrices
        public Matrix view { get; protected set; }
        public Matrix projection { get; protected set; }

        //Camera Vectors:
        public Vector3 cameraPosition { get; protected set; }
        Vector3 cameraDirection;
        Vector3 cameraUp;

        //Camera movement properties
        float speed;
        float zoomInterval;


        private Ground ground;

        private int nearPlane;
        private int farPlane;

        //Input Variables
        MouseState prevMouseState;

        /// <summary>
        /// Constructor class for the camera
        /// </summary>
        /// <param name="game"></param>
        /// <param name="pos">The position of the camera</param>
        /// <param name="target">The target that the camera will face</param>
        /// <param name="up">The cameras up vector</param>
        /// <param name="ground">A reference to the ground plane of the game</param>
        public Camera(Game game, Vector3 pos, Vector3 target, Vector3 up, Ground ground) : base(game) {
            //Build the camera view matrix
            cameraPosition = pos;
            cameraDirection = target - pos;

            this.ground = ground;
            //Normalize method takes any vector and converts it to a vector with a magnitude of 1
            //Makes it easier to apply speed values when moving the camera in the direction it is facing
            cameraDirection.Normalize();
            cameraUp = up;
            CreateLookAt();

            speed = SCROLL_SPEED;
            zoomInterval = 100.0f;
            //45 degree rotation limit

            nearPlane = 1;
            farPlane = 3000;

            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)Game.Window.ClientBounds.Width /
                (float)Game.Window.ClientBounds.Height, nearPlane, farPlane);
        }

        public override void Initialize() {

            //Set mouse position and get the initial state
            Mouse.SetPosition(Game.Window.ClientBounds.Width / 2, Game.Window.ClientBounds.Height / 2);
            prevMouseState = Mouse.GetState();

            base.Initialize();
        }

        //Will reconstruct the view matrix according the cameraPosition, cameraDirection and the cameraUp attributes
        public void CreateLookAt() {
            view = Matrix.CreateLookAt(cameraPosition, cameraPosition + cameraDirection, cameraUp);
        }

        
        /// <summary>
        /// Allows for the controlling of the camera
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime) {

            //Move forward and back
            if (Keyboard.GetState().IsKeyDown(Keys.W)) {
                cameraPosition += (cameraDirection * speed) * (new Vector3(1, 0, 1));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S)) {
                cameraPosition -= (cameraDirection * speed) * (new Vector3(1, 0, 1));
            }

            //Strafe
            if (Keyboard.GetState().IsKeyDown(Keys.A)) {
                cameraPosition += Vector3.Cross(cameraUp, cameraDirection) * speed * (new Vector3(1, 0, 1));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D)) {
                cameraPosition -= Vector3.Cross(cameraUp, cameraDirection) * speed * (new Vector3(1, 0, 1));
            }


            //ZOOMING IN AND OUT
            if (Mouse.GetState().ScrollWheelValue < prevMouseState.ScrollWheelValue) {
                if (cameraPosition.Y <= MAX_ZOOM) {
                    cameraPosition = new Vector3(cameraPosition.X, cameraPosition.Y + zoomInterval, cameraPosition.Z);
                }
            } else if (Mouse.GetState().ScrollWheelValue > prevMouseState.ScrollWheelValue) {
                if (cameraPosition.Y >= MIN_ZOOM) {
                    cameraPosition = new Vector3(cameraPosition.X, cameraPosition.Y - zoomInterval, cameraPosition.Z);
                }
            }
            /*
            //Yaw Rotation i.e. changing cameraDirectionVector
            cameraDirection = Vector3.Transform(cameraDirection, Matrix.CreateFromAxisAngle(cameraUp, (-MathHelper.PiOver4 / 360) *
                (Mouse.GetState().X - prevMouseState.X)));
            */
            //Reset the mouse back to the center so that it does not eventually end up off the game screen
            if (Game.IsActive) {
                Mouse.SetPosition(Game.Window.ClientBounds.Width / 2, Game.Window.ClientBounds.Height / 2);
            }

            prevMouseState = Mouse.GetState();

            CreateLookAt();

            base.Update(gameTime);
        }


    }
}
