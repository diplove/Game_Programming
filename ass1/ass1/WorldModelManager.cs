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

        public ModelManager enemies;
        public ModelManager turrets;
        Random rand = new Random();

        public WorldModelManager(Game1 game) : base(game) {
            prevMouseState = Mouse.GetState();
            enemies = new ModelManager(game);
            turrets = new ModelManager(game);
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
           
            foreach(Enemy enemy in enemies.models) {
                enemy.Update(gameTime);
                //If an enemy collides with the tower, the tower takes damage and the enemy is destroyed
                if (enemy.CollidesWith(tower.model, tower.GetWorldMatrix())) {
                    tower.DamageTower(enemy.GetDamage());
                    toBeKilled.Add(enemy);
                }
            }

            foreach (Turret turret in turrets.models) {
                List<Bullet> bulletsToBeDestroyed = new List<Bullet>();
                turret.Update(gameTime);
                foreach(Bullet bullet in turret.bullets.models) {
                    foreach(Enemy enemy in enemies.models) {
                        if (bullet.CollidesWith(enemy.model, enemy.GetWorldMatrix())) {
                            enemy.DamageEnemy(bullet.damage);
                            bulletsToBeDestroyed.Add(bullet);
                            if (enemy.health < 0) {
                                toBeKilled.Add(enemy);
                            }
                        }
                    }
                }
                //Remove all bullets that collided this frame
                foreach (Bullet bullet in bulletsToBeDestroyed) {
                    turret.bullets.models.Remove(bullet);
                }
            }


            //Kill all enemies that need to be killed
            foreach (Enemy enemy in toBeKilled) {
                enemies.models.Remove(enemy);
            }


            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            base.Draw(gameTime);
            enemies.Draw(gameTime);
            turrets.Draw(gameTime);
            foreach (Turret turret in turrets.models) {
                turret.bullets.Draw(gameTime);
            }
        }

        public void CreateEnemy() {
            
            Enemy enemy = new Enemy(Game.Content.Load<Model>(@"Models\selectionCube"), new Vector3(rand.Next(-500, 500), -500 , 0), tower);
            enemies.models.Add(enemy);
        }

        /// <summary>
        /// Creates a turret at a given position
        /// </summary>
        /// <param name="position"></param>
        public void CreateTurret(Vector3 position) {
            Turret turret = new Turret(game.Content.Load<Model>(@"Models\Turrets\cannon"), position, game.Content.Load<Model>(@"Models\Turrets\Bullets\cannonBall"), this);
            turrets.models.Add(turret);
        }
        
    }
}
