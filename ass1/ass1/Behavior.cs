using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ass1 {
    /// <summary>
    /// A variety of static behaviors to be used by non controllable characters
    /// </summary>
    class Behavior {

        /// <summary>
        /// Static method that takes a current position and returns its new position based on 
        /// a simple chase mechanic. A speed of the agent and a reference to the gametime ensures
        /// smooth movement from frame to frame
        /// </summary>
        /// <param name="currentPosition">The current position of the agent</param>
        /// <param name="targetPosition">The position that the agent is chasing</param>
        /// <param name="gameTime">A reference to the current game time</param>
        /// <param name="speed">The speed that the agent is moving</param>
        /// <returns>newPosition</returns>
        public static Vector3 ChaseLocation(Vector3 currentPosition, Vector3 targetPosition, GameTime gameTime, float speed) {

            Vector3 newPosition = new Vector3(currentPosition.X, currentPosition.Y, currentPosition.Z);

            //Y position remains unchanged
            newPosition.Y = currentPosition.Y;

            if (targetPosition.X < currentPosition.X) {
                newPosition.X = currentPosition.X - speed * gameTime.ElapsedGameTime.Milliseconds/1000;
            } else if (targetPosition.X > currentPosition.X) {
                newPosition.X = currentPosition.X + speed * gameTime.ElapsedGameTime.Milliseconds/1000;
            } else {
                //Is in the correct X position therefore DO NOTHING
            }

            if (targetPosition.Y < currentPosition.Y) {
                newPosition.Y = currentPosition.Y - speed * gameTime.ElapsedGameTime.Milliseconds/1000;
            } else if (targetPosition.Y > currentPosition.Y) {
                newPosition.Y = currentPosition.Y + speed * gameTime.ElapsedGameTime.Milliseconds/1000;
            } else {
                //Is in the correct Z position therefore DO NOTHING
            }

            return newPosition;
        }

    }
}
