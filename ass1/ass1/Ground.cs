using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ass1 {
    class Ground : BasicModel {

        public static float SCALE = 40.0f;

        public Plane groundPlane;

        public Ground(Model m, Vector3 position) : base(m, position){
            groundPlane = new Plane(Vector3.UnitY, position.Y);
        }

        public override Matrix GetWorldMatrix() {
            Matrix world;
            world = base.GetWorldMatrix();
            world = Matrix.CreateScale(SCALE * Game1.GLOBAL_SCALE);
            return world;
        }
    }
}
