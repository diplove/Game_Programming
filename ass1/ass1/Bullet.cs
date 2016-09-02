using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ass1 {
    /// <summary>
    /// Bullet class to maintain the position and the characteristics of a bullet
    /// </summary>
    class Bullet : BasicModel {

        public int damage { get; private set; }

        private Vector3 directionOfTravel;

        float speed;
        Enemy targetEnemy;

        public Bullet(Model m, Vector3 position, Enemy targetEnemy) : base(m, position) {
            this.targetEnemy = targetEnemy;
            this.speed = 80.0f;
            damage = 100;
            CreateDirectionOfTravel();
        }

        private void CreateDirectionOfTravel() {
            directionOfTravel = Vector3.Normalize(targetEnemy.GetPosition() - position);
        }

        public override void Update(GameTime gameTime) {
            this.position += directionOfTravel * speed * gameTime.ElapsedGameTime.Milliseconds / 1000;
            base.Update(gameTime);
        }

    }
}
