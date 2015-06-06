using System.Collections.Generic;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SociaGroundsEngine.GUI;
using SociaGroundsEngine.World;

namespace SociaGroundsEngine.PlayerFolder
{
    public class ForeignPlayer : CPlayer
    {
        private Queue<Vector2> NewQPosition;

        public ForeignPlayer(Vector2 startPosition, NetConnection connection)
        {
            NewQPosition = new Queue<Vector2>();

            animation = new CAnimation(Game1.texture, startPosition, 64, 64, 10, 25, true);
            position = startPosition;
            speed = 3;
            this.connection = connection;

            rect = new Rectangle((int)position.X, (int)position.Y, 64, 64);
        }

        public void AddNewPosition(Vector2 pos)
        {
            NewQPosition.Enqueue(pos);
        }

        public ForeignPlayer()
        {
            NewQPosition = new Queue<Vector2>();
        }

        public override void Update(GameTime gameTime, Ui ui, Viewport viewPort, Map map)
        {
            if (NewQPosition.Count <= 0) return;

            NewPosition(gameTime);

            animation.Position = position;

            rect = new Rectangle((int)position.X, (int)position.Y, 64, 64);
        }

        private void NewPosition(GameTime gameTime)
        {
            Vector2 newPosition = NewQPosition.Dequeue();

            if (newPosition.Y > position.Y)
            {
                animation.Play(8, 9, gameTime);

                position.Y -= (newPosition.Y - position.Y);
            }
            else if (newPosition.Y < position.Y)
            {
                animation.Play(10, 9, gameTime);

                position.Y += (position.Y - newPosition.Y);
            }
            else if (newPosition.X < position.X)
            {
                animation.Play(9, 9, gameTime);

                position.X -= (position.X - newPosition.X);
            }
            else if (newPosition.X > position.X)
            {
                animation.Play(11, 9, gameTime);

                position.X += (newPosition.X - position.X);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            animation.Draw(spriteBatch);
        }
    }
}
