using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ass1 {
    public class ModelManager : DrawableGameComponent {

        List<BasicModel> models = new List<BasicModel>();

        public ModelManager(Game game) : base(game) {

        }

        public override void Update(GameTime gameTime) {
            //Loop through all the models and call update
            for (int i = 0; i < models.Count; ++i) {
                models[i].Update();
            }

            base.Update(gameTime);
        }

        protected override void LoadContent() {
            models.Add(new Ground(Game.Content.Load<Model>(@"GroundModels\grassSquare"), new Vector3(0,-15,0)));
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
