using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ass1 {
    /// <summary>
    /// The basic class for a model
    /// Contains methods to draw and load in a model
    /// </summary>
    public class BasicModel {

        public Model model { get; protected set; }
        protected Matrix world = Matrix.Identity;
        protected Vector3 position;

        /// <summary>
        /// Constructor method for the basic model class that takes a model
        /// </summary>
        /// <param name="m"></param>
        public BasicModel(Model m, Vector3 position) {
            model = m;
            this.position = position;
        }

        /// <summary>
        /// Update method to be overriden by classes that derive BasicModel
        /// </summary>
        public virtual void Update(GameTime gameTime) {

        }

        /// <summary>
        /// Basic draw method for a generic model object
        /// </summary>
        /// <param name="camera"></param>
        public virtual void Draw(Camera camera) {
            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);
            Matrix worldMatrix = GetWorldMatrix();

            foreach (ModelMesh mesh in model.Meshes) {
                foreach (BasicEffect effect in mesh.Effects) {
                    effect.EnableDefaultLighting();
                    effect.Projection = camera.projection;
                    effect.View = camera.view;
                    effect.World = worldMatrix * mesh.ParentBone.Transform;
                }
                mesh.Draw();
            }
        }

        /// <summary>
        /// Method to retrieve the world matrix for this object
        /// Method can be overriden by classes that derive the Basic Model Class
        /// </summary>
        /// <returns>worldMatrix</returns>
        public virtual Matrix GetWorldMatrix() {
            Matrix world;
            world = Matrix.CreateTranslation(position);
            return world;
        }

        public bool CollidesWith(Model otherModel, Matrix otherWorld) {
            //Loop through each model meash in both objects and compare all the bounding spheres
            //for collisions
            foreach (ModelMesh thisModelMeshes in model.Meshes) {
                foreach(ModelMesh otherModelMeshes in otherModel.Meshes) {
                    if (thisModelMeshes.BoundingSphere.Transform(GetWorldMatrix()).Intersects(otherModelMeshes.BoundingSphere.Transform(otherWorld))) {
                        return true;
                    }
                }
            }
            return false;
        }

        public Vector3 GetPosition() {
            return this.position;
        }

    }
}
