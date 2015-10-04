using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SociaGrounds.Model.World
{
    public class Tree : SolidAsset
    {
        private readonly int _amountOfLogs;
        private static readonly Texture2D[] TREE;
        private float _scale;
        public string Name { get; set; }

        static Tree()
        {
            // Loading textures
            TREE = new[]
            {
                Game1.StaticContent.Load<Texture2D>("SociaGrounds/World/Tree/TreeBottom"),
                Game1.StaticContent.Load<Texture2D>("SociaGrounds/World/Tree/TreeMid"),
                Game1.StaticContent.Load<Texture2D>("SociaGrounds/World/Tree/TreeTop"),
                Game1.StaticContent.Load<Texture2D>("SociaGrounds/World/Tree/TreeShade"),
            };
        }

        public Tree(string name, Vector2 position, int amountOfLogs, float scale) : base(position)
        {
            _amountOfLogs = amountOfLogs;
            _scale = scale;
            Name = name;

            // Creating the rectangle
            rect = new Rectangle((int)position.X, (int)position.Y, TREE[0].Width, TREE[0].Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawBottom(spriteBatch);
            DrawMiddle(spriteBatch);
            DrawTop(spriteBatch);
        }

        public override void DrawShade(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TREE[3], new Vector2(position.X - 19,position.Y + 11), Color.White);
        }

        private void DrawBottom(SpriteBatch spriteBatch)
        {
            // Draw the bottom and determine the rectangle height
            spriteBatch.Draw(TREE[0], position, Color.White);
        }

        private void DrawMiddle(SpriteBatch spriteBatch)
        {
            // Draw the amount of logs
            for (int i = 0; i < _amountOfLogs; i++)
            {
                spriteBatch.Draw(TREE[1], new Vector2(position.X + 5, position.Y - (TREE[0].Height + (TREE[1].Height * i))), Color.White);
            }
        }

        private void DrawTop(SpriteBatch spriteBatch)
        {
            // Draw the top
            spriteBatch.Draw(TREE[2], new Vector2(position.X - 33, position.Y - (float)((TREE[0].Height * 1.85) + (TREE[1].Height * _amountOfLogs))), Color.White);
        }

        public override string ToString()
        {
            return string.Format("Name:{0} - Rectangle {1}", Name, rect);
        }
    }
}
