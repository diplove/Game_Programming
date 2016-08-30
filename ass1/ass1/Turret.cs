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

        protected int health;
        protected int fireRate;
        protected int damage;
        protected Model bullet;
        protected String name;
        protected String description;

        /// <summary>
        /// Constructor method that passes the turret model and the position to the
        /// parent BasicModel class. Also takes in a bullet model that this turret
        /// will fire.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="position"></param>
        /// <param name="bullet"></param>
        public Turret(Model m, Vector3 position, Model bullet) : base(m, position) {
            this.bullet = bullet;
            Debug.WriteLine("Turret created at X: " + position.X + " Y: " + position.Y + " Z: " + position.Z);
        }

        /// <summary>
        /// A method that must be implemented that initiates the stats of the turret
        /// and also the strings for the description and the name
        /// </summary>
        protected virtual void Initiate() {
            health = 100;
            fireRate = 1;
            damage = 10;
            name = "Basic Turret";
            description = "The default turret - USED FOR TESTING";
        }

        public override Matrix GetWorldMatrix() {
            world = base.GetWorldMatrix() ;
            return world;
        }

    }
}
