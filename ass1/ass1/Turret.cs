using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ass1 {
    /// <summary>
    /// A base class for a turret
    /// A turret inherits from a basic model class
    /// </summary>
    class Turret : BasicModel {

        public static int COST = 100;

        public ModelManager bullets;

        public int health { get; protected set; }

        //Number of shots per second
        protected float fireRate;
        protected Model bullet;
        protected String name;
        protected String description;

        private WorldModelManager worldModelManager;

        //How many miliseconds since last fire
        private int lastFired;

        /// <summary>
        /// Constructor method that passes the turret model and the position to the
        /// parent BasicModel class. Also takes in a bullet model that this turret
        /// will fire.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="position"></param>
        /// <param name="bullet"></param>
        public Turret(Model m, Vector3 position, Model bullet, WorldModelManager worldModelManager) : base(m, position) {
            this.bullet = bullet;
            this.worldModelManager = worldModelManager;
            bullets = new ModelManager(worldModelManager.Game);
            lastFired = 0;
            //Debug.WriteLine("Turret created at X: " + position.X + " Y: " + position.Y + " Z: " + position.Z);
            Initiate();
        }

        /// <summary>
        /// A method that must be implemented that initiates the stats of the turret
        /// and also the strings for the description and the name
        /// </summary>
        protected virtual void Initiate() {
            health = 10;
            fireRate = 1.0f;
            name = "Basic Turret";
            description = "The default turret - USED FOR TESTING";
        }

        public override void Update(GameTime gameTime) {
            
            foreach(Bullet bullet in bullets.models) {
                bullet.Update(gameTime);
            }

            if (lastFired > fireRate * 1000.0f) {
                if (worldModelManager.enemies.models.Count <= 0) {

                } else {
                    bullets.models.Add(new Bullet(bullet, this.position, (Enemy)worldModelManager.enemies.models.ElementAt(0)));
                    Debug.WriteLine("CANNON IS FIRING");
                    lastFired = 0;
                }
                
            } else {
                Debug.WriteLine("Last Fired = " + lastFired);
                lastFired += gameTime.ElapsedGameTime.Milliseconds;
            }

            base.Update(gameTime);
        }

        public void DamageTurret(int damage) {
            health -= damage;
        }

        public override Matrix GetWorldMatrix() {
            world = base.GetWorldMatrix() ;
            return world;
        }

    }
}
