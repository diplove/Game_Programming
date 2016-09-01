using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ass1 {
    public class Enemy : BasicModel {

        int health;
        Tower tower;
        float speed;

        public Enemy(Model m, Vector3 position, Tower tower) : base(m, position) {
            this.tower = tower;
            this.health = 100;
            this.speed = 10.0f;
        }

        public virtual void Initiate() {
            
        }

        /// <summary>
        /// Enemy position will be updated based on enemies chasing the position of the castle
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime) {
            position = Behavior.ChaseLocation(this.position, tower.GetPosition(), gameTime, this.speed);
            base.Update(gameTime);
        }

    }
}
