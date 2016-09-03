using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ass1 {
    public class Enemy : BasicModel {

        public int health { get; protected set; }
        public int rewardForKilling { get; protected set; }
        Tower tower;
        float speed;
        int damage;

        public Enemy(Model m, Vector3 position, Tower tower) : base(m, position) {
            this.tower = tower;
            this.health = 100;
            this.speed = 50.0f;
            this.rewardForKilling = 10;
            this.damage = 10;
        }

        public virtual void Initiate() {
            
        }

        /// <summary>
        /// Enemy position will be updated based on enemies chasing the position of the castle
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime) {
            position = Behavior.StraightLineChase(this.position, tower.GetPosition(), gameTime, this.speed);
            rotation = BasicModel.RotateToFace(position, tower.GetPosition(), new Vector3(0, 0, 1));
            base.Update(gameTime);
        }

        public int GetDamage() {
            return this.damage;
        }

        public void DamageEnemy(int damage) {
            health -= damage;
        }

    }
}
