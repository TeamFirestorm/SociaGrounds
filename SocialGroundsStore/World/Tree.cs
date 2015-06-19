using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SocialGroundsStore.World
{
    public class Tree : Asset
    {
        private readonly int _amountOfLogs;
        private readonly Texture2D _bottom;
        private readonly Texture2D _mid;
        private readonly Texture2D _top;

        public Tree(Vector2 position, int amountOfLogs, ContentManager content) : base(position)
        {
            // Initialization
            this.position = position;
            _amountOfLogs = amountOfLogs;

            // Loading textures
            _bottom = content.Load<Texture2D>("World/Tree/TreeBottom");
            _mid = content.Load<Texture2D>("World/Tree/TreeMid");
            _top = content.Load<Texture2D>("World/Tree/TreeTop");

            // Creating the rectangle
            rect = new Rectangle((int)(position.X + (_bottom.Width / 3.1f)), (int)position.Y + 10, (_bottom.Width / 3), (int)(_bottom.Height /2.5));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw the bottom and determine the rectangle height
            spriteBatch.Draw(_bottom, position, Color.White);

            // Draw the amount of logs
            for (int i = 0; i < _amountOfLogs; i++)
            {
                spriteBatch.Draw(_mid, new Vector2(position.X + 24, position.Y - (_bottom.Height + (_mid.Height * i) - 10)), Color.White);
            }

            // Draw the top
            spriteBatch.Draw(_top, new Vector2(position.X - 15, position.Y - (float)((_bottom.Height * 1.85) + (_mid.Height * _amountOfLogs))), Color.White);
        }
    }
}
