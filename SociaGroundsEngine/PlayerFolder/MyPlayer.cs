using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SociaGroundsEngine.Player
{
    class MyPlayer : CPlayer
    {
        public MyPlayer(ContentManager Content, Vector2 startPosition, Texture2D texture)
        {
            animation = new CAnimation(texture, startPosition, 64, 64, 10, 25, true);
            position = startPosition;
            speed = 3;

            rect = new Rectangle((int)position.X, (int)position.Y, 64, 64);
        }

        public override void update(GameTime gameTime)
        {
            
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            animation.draw(spriteBatch);
        }
    }
}
