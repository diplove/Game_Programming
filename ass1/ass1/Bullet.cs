using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ass1 {
    class Bullet : BasicModel {

        public int damage { get; private set; }
        float speed;
        Enemy targetEnemy;

        public Bullet(Model m, Vector3 position, Enemy targetEnemy) : base(m, position) {
            this.targetEnemy = targetEnemy;
            this.speed = 80.0f;
            damage = 100;
        }

        public override void Update(GameTime gameTime) {
            this.position = Behavior.ChaseLocation(this.position, targetEnemy.GetPosition(), gameTime, speed);
            base.Update(gameTime);
        }

    }
}
