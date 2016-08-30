using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ass1 {
    class Ground : BasicModel {

        private static float SCALE = 100.0f;

        public Ground(Model m, Vector3 position) : base(m, position){

        }

        public override Matrix GetWorldMatrix() {
            Matrix world;
            world = base.GetWorldMatrix();
            world = Matrix.CreateScale(SCALE);
            return world;
        }
    }
}
