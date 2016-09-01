using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ass1
{
    public class Tower : BasicModel 
    {
        protected int damage;
        protected String name;
        protected String description;

        /// <summary>
        /// Constructor method that passes the tower model and the position to the
        /// parent BasicModel class.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="position"></param>

        public Tower(Model m, Vector3 position) : base(m, position) {
            //Debug.WriteLine("Turret created at X: " + position.X + " Y: " + position.Y + " Z: " + position.Z);
            Initiate();
        }

        /// <summary>
        /// A method that must be implemented that initiates the stats of the tower
        /// and also the strings for the description and the name
        /// </summary>
        protected virtual void Initiate()
        {
            name = "Main Tower";
            description = "The Main Tower - USED FOR TESTING";
            damage = 0;
        }

        /// <summary>
        /// Will return the world matrix of the model
        /// </summary>
        /// <returns>worldMatrix</returns>
        public override Matrix GetWorldMatrix()
        {
            Matrix world;
            world = base.GetWorldMatrix();
            return world;
        }

        /// <summary>
        /// Will return the amount of damage of the tower
        /// </summary>
        public int GetTowerDamage()
        {
            return damage;
        }

        /// <summary>
        /// Will increase the amount of the tower
        /// </summary>
        public void IncreaseTowerDamage()
        {
            damage = damage + 1;
        }

        public Vector3 GetPosition() {
            return this.position;
        }
    }
}
