using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ass1 {
    class SelectionCube : BasicModel {

        /// <summary>
        /// Constructor method for the selection cube sub class
        /// </summary>
        /// <param name="m"></param>
        /// <param name="position"></param>
        public SelectionCube(Model m, Vector3 position) : base(m,position) {

        }

        /// <summary>
        /// Updates the position attribute of the selection cube
        /// </summary>
        /// <param name="newPosition"></param>
        public void ChangeSelectionPosition(Vector3 newPosition) {
            position = newPosition;
            Debug.WriteLine("Cube position is now: X: " + position.X + " Y: " + position.Y + " Z: " + position.Z);
        }

        /// <summary>
        /// Will return the world matrix of the model
        /// </summary>
        /// <returns>worldMatrix</returns>
        public override Matrix GetWorldMatrix() {
            Matrix world;
            world = base.GetWorldMatrix();
            
            return world;
        }

    }
}
