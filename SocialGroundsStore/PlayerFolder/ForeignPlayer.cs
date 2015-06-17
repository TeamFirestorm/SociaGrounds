using System.Collections.Generic;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SocialGroundsStore.GUI;
using SocialGroundsStore.World;

namespace SocialGroundsStore.PlayerFolder
{
    public class ForeignPlayer : CPlayer
    {
        private Vector2 _newPosition;

        public ForeignPlayer(Vector2 startPosition, NetConnection connection, int id)
        {
            newQPosition = new Queue<Vector2>();

            animation = new CAnimation(Game1.texture, startPosition, 64, 64, 10, 25, true);
            position = startPosition;
            speed = 3;
            this.connection = connection;

            rect = new Rectangle((int)position.X, (int)position.Y, 64, 64);

            font = Game1.font;
            chatMessage = "";

            Id = id;

            _newPosition = default(Vector2);
        }

        public ForeignPlayer(Vector2 startPosition, int id)
        {
            newQPosition = new Queue<Vector2>();

            animation = new CAnimation(Game1.texture, startPosition, 64, 64, 10, 25, true);
            position = startPosition;
            speed = 3;

            rect = new Rectangle((int)position.X, (int)position.Y, 64, 64);

            font = Game1.font;
            chatMessage = "";

            Id = id;

            _newPosition = default(Vector2);
        }

        public void AddNewPosition(Vector2 pos)
        {
            newQPosition.Enqueue(pos);
        }

        public override void Update(GameTime gameTime, Ui ui, Viewport viewPort, Map map, KeyboardState keyState)
        {
            if (newQPosition.Count <= 0) return;

            NewPosition(gameTime);

            animation.Position = position;

            rect = new Rectangle((int)position.X, (int)position.Y, 64, 64);
        }

        private void NewPosition(GameTime gameTime)
        {
            if (_newPosition == default(Vector2))
            {
                if (newQPosition.Count > 0)
                {
                    _newPosition = newQPosition.Dequeue();
                }
                else
                {
                    return;
                }
            }

            if (_newPosition.Y > position.Y)
            {
                animation.Play(8, 9, gameTime);

                position.Y += (_newPosition.Y - position.Y);
            }
            else if (_newPosition.Y < position.Y)
            {
                animation.Play(10, 9, gameTime);

                position.Y -= (position.Y - _newPosition.Y);
            } 
            else if (_newPosition.X < position.X)
            {
                animation.Play(9, 9, gameTime);

                position.X -= (position.X - _newPosition.X);
            }
            else if (_newPosition.X > position.X)
            {
                animation.Play(11, 9, gameTime);

                position.X += (_newPosition.X - position.X);
            }

            if (position == _newPosition)
            {
                _newPosition = default(Vector2);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            animation.Draw(spriteBatch);
        }
    }
}
