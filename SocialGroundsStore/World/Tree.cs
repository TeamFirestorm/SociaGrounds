using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SocialGroundsStore.World
{
    public class Tree : Asset
    {
        int amountOfLogs;
        Texture2D bottom;
        Texture2D mid;
        Texture2D top;

        public Tree(Vector2 position, int amountOfLogs, ContentManager content) : base(position)
        {
            // Initialization
            this.position = position;
            this.amountOfLogs = amountOfLogs;

            // Loading textures
            bottom = content.Load<Texture2D>("World/Tree/TreeBottom");
            mid = content.Load<Texture2D>("World/Tree/TreeMid");
            top = content.Load<Texture2D>("World/Tree/TreeTop");

            //64 x 41

            // Creating the rectangle
            rect = new Rectangle((int)(position.X + (bottom.Width / 3.1f)), (int)position.Y + 10, (int)(bottom.Width / 3), (int)(bottom.Height /2.5));

            isSolid = true;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw the bottom and determine the rectangle height
            spriteBatch.Draw(bottom, position, Color.White);

            // Draw the amount of logs
            for (int i = 0; i < amountOfLogs; i++)
            {
                spriteBatch.Draw(mid, new Vector2(position.X + 24, position.Y - (bottom.Height + (mid.Height * i) - 10)), Color.White);
            }

            // Draw the top
            spriteBatch.Draw(top, new Vector2(position.X - 15, position.Y - (float)((bottom.Height * 1.85) + (mid.Height * amountOfLogs))), Color.White);
        }
    }
}
