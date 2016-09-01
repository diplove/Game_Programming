using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ass1 {
    class Player {

        public static int STARTING_MONEY = 1000;

        public int money;
        Game1 game;

        public Player(Game1 game) {
            money = STARTING_MONEY;
            this.game = game;
        }

        /// <summary>
        /// Will return true if the player has more money than the cost
        /// and false if they do not have enough money
        /// </summary>
        /// <param name="cost"></param>
        /// <returns></returns>
        public bool HasSuffucientMoney(int cost) {
            if (money - cost >= 0) {
                return true;
            } else {
                return false;
            }
        }

        public void SpendMoney(int cost) {
            money -= cost;
        }

        private String InsufficientMoneyMessage() {
            return "You have insufficient money for your purchase";
        }

        public void DrawText(SpriteBatch spriteBatch, SpriteFont font) {
            spriteBatch.DrawString(font, "Money : $" + money, new Vector2(20, 20), Color.Black);
        }

    }
}
