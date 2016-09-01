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

        public Ground ground;
        public SelectionCube selectionCube;
        public Tower tower;

        MouseState prevMouseState;

        public List<Enemy> enemies = new List<Enemy>();
        Random rand = new Random();

        public WorldModelManager(Game game) : base(game) {
            prevMouseState = Mouse.GetState();
        }

        protected override void LoadContent() {

            ground = new Ground(Game.Content.Load<Model>(@"GroundModels\ground"), new Vector3(0, 0, 0));
            models.Add(ground);
            selectionCube = new SelectionCube(Game.Content.Load<Model>(@"Models\selectionCube"), new Vector3(0, 0, 0));
            models.Add(selectionCube);
            tower = new Tower(Game.Content.Load<Model>(@"Models\selectionCube"), new Vector3(0, 300, 0));
            models.Add(tower);
            CreateEnemy();
            base.LoadContent();
        }

        public override void Update(GameTime gameTime) {
            foreach(Enemy enemy in enemies) {
                enemy.Update(gameTime);
            }
            base.Update(gameTime);
        }

        public void CreateTurret(Turret turret) {
            models.Add(turret);
        }

        public void CreateEnemy() {
            
            Enemy enemy = new Enemy(Game.Content.Load<Model>(@"Models\selectionCube"), new Vector3(rand.Next(-500, 500), -500 , 0), tower);
            models.Add(enemy);
            enemies.Add(enemy);
        }
        
    }
}
