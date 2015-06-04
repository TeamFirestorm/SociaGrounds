using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SociaGroundsEngine.GUI;
using SociaGroundsEngine.World;

namespace SociaGroundsEngine.PlayerFolder
{
    public class ForeignPlayer : CPlayer
    {
        public ForeignPlayer(Vector2 startPosition, NetConnection connection)
        {
            animation = new CAnimation(Game1.texture, startPosition, 64, 64, 10, 25, true);
            position = startPosition;
            speed = 3;
            this.connection = connection;

            rect = new Rectangle((int)position.X, (int)position.Y, 64, 64);
        }

        public ForeignPlayer()
        {
            
        }

        public override void Update(GameTime gameTime, Ui ui, Viewport viewPort, Map map)
        {
            animation.Position = position;

            rect = new Rectangle((int)position.X, (int)position.Y, 64, 64);

            if (NewPosition.Equals(new Vector2(-1, -1))) return;

            if (NewPosition.Y > position.Y)
            {
                animation.Play(8, 9, gameTime);

                position.Y -= (NewPosition.Y - position.Y);
                
            }
            else if (NewPosition.Y < position.Y)
            {
                animation.Play(10, 9, gameTime);

                position.Y += (position.Y - NewPosition.Y);
                
            }
            else if (NewPosition.X < position.X)
            {
                animation.Play(9, 9, gameTime);

                position.X -= (position.X - NewPosition.X);
            }
            else if (NewPosition.X > position.X)
            {
                animation.Play(11, 9, gameTime);

                position.X += (NewPosition.X - position.X);
            }
            else// (NewPosition.Equals(position))
            {
                
            }

            NewPosition = new Vector2(-1,-1);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            animation.Draw(spriteBatch);
        }
    }
}
