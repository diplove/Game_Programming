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

        Game1 game;

        MouseState prevMouseState;

        public List<Enemy> enemies = new List<Enemy>();
        Random rand = new Random();

        public WorldModelManager(Game1 game) : base(game) {
            prevMouseState = Mouse.GetState();
            this.game = game;
        }

        protected override void LoadContent() {

            ground = new Ground(Game.Content.Load<Model>(@"GroundModels\ground"), new Vector3(0, 0, 0));
            models.Add(ground);
            selectionCube = new SelectionCube(Game.Content.Load<Model>(@"Models\selectionCube"), new Vector3(0, 0, 0));
            models.Add(selectionCube);
            tower = new Tower(Game.Content.Load<Model>(@"Models\selectionCube"), new Vector3(0, 300, 0), game);
            models.Add(tower);
            CreateEnemy();
            base.LoadContent();
        }

        public override void Update(GameTime gameTime) {
            List<Enemy> toBeKilled = new List<Enemy>();
            foreach(Enemy enemy in enemies) {
                enemy.Update(gameTime);
                //If an enemy collides with the tower, the tower takes damage and the enemy is destroyed
                if (enemy.CollidesWith(tower.model, tower.GetWorldMatrix())) {
                    tower.DamageTower(enemy.GetDamage());
                    toBeKilled.Add(enemy);
                }
            }
            //Kill all enemies that need to be killed
            foreach (Enemy enemy in toBeKilled) {
                enemies.Remove(enemy);
                models.Remove(enemy);
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
