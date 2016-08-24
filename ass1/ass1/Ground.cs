using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ass1 {
    class Ground {

        public int NUM_VERTICES = 6;

        public VertexPositionTexture[] verts;
        public VertexBuffer vertBuffer;

        public Texture2D floorTexture;

        public int size;
        public Vector3 position;
        GraphicsDevice graphicsDevice;

        public Ground(int size, Vector3 position, Texture2D floorTexture, GraphicsDevice graphicsDevice) {
            this.size = size;
            this.position = position;
            this.floorTexture = floorTexture;
            this.graphicsDevice = graphicsDevice;

            Vector3 vert1 = new Vector3(position.X - size, position.Y, position.Z - size);
            Vector3 vert2 = new Vector3(position.X + size, position.Y, position.Z - size);
            Vector3 vert3 = new Vector3(position.X + size, position.Y, position.Z + size);
            Vector3 vert4 = new Vector3(position.X - size, position.Y, position.Z + size);

            Vector2 textureTopLeft = new Vector2(0, 0);
            Vector2 textureTopRight = new Vector2(1, 0);
            Vector2 textureBottomLeft = new Vector2(0, 1);
            Vector2 textureBottomRight = new Vector2(1, 1);

            verts = new VertexPositionTexture[NUM_VERTICES];
            verts[0] = new VertexPositionTexture(vert1, textureTopLeft);
            verts[1] = new VertexPositionTexture(vert2, textureTopRight);
            verts[2] = new VertexPositionTexture(vert3, textureBottomRight);

            verts[3] = new VertexPositionTexture(vert1, textureTopLeft);
            verts[4] = new VertexPositionTexture(vert3, textureBottomRight);
            verts[5] = new VertexPositionTexture(vert4, textureBottomLeft);

            vertBuffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionTexture), verts.Length, BufferUsage.None);
        }

        public void Draw() {

        }

    }
}
