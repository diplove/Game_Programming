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
        protected int health;
        protected String name;
        protected String description;

        Game1 game;

        /// <summary>
        /// Constructor method that passes the tower model and the position to the
        /// parent BasicModel class.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="position"></param>

        public Tower(Model m, Vector3 position, Game1 game) : base(m, position) {
            //Debug.WriteLine("Turret created at X: " + position.X + " Y: " + position.Y + " Z: " + position.Z);
            this.game = game;
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
            health = 100;
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
        public int GetTowerHealth()
        {
            return health;
        }

        /// <summary>
        /// Will increase the amount of the tower
        /// </summary>
        public void DamageTower(int damage)
        {
            if (health - damage <= 0 ) {
                health = 0;
                TowerDestroyed();
            } else {
                health -= damage;
            }
        }

        public Vector3 GetPosition() {
            return this.position;
        }

        public void DrawText(SpriteBatch spriteBatch, SpriteFont font) {
            spriteBatch.DrawString(font, "Tower Health: " + this.health, new Vector2(game.SCREEN_WIDTH/2 - 60, 20), Color.Black);
        }

        public void TowerDestroyed() {
            game.GameOver();
        }
    }
}
