using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SociaGrounds.Model.Controllers;

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
            _HitBox = new Rectangle((int)position.X, (int)position.Y + (TREE[0].Height /2), TREE[0].Width, TREE[0].Height /2);
            
            Debug.WriteLine("TopLeft: {0} | RightDown: {2} || name: {1}", _HitBox.Location, name, new Vector2(_HitBox.Location.X + _HitBox.Width, _HitBox.Location.Y + _HitBox.Height));
            Debug.WriteLine("Left: {0} | Top: {1} | Right: {2} | Bottom: {3} || Name: {4}", _HitBox.Left, _HitBox.Top, _HitBox.Right, _HitBox.Bottom, name);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawBottom(spriteBatch);
            DrawMiddle(spriteBatch);
            DrawTop(spriteBatch);
        }

        public override void DrawShade(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TREE[3], new Vector2(_Position.X - 19,_Position.Y + 11), Color.White);

            //spriteBatch.Draw(Static.DummyTexture, _HitBox, Color.Chocolate);
        }

        private void DrawBottom(SpriteBatch spriteBatch)
        {
            // Draw the bottom and determine the rectangle height
            spriteBatch.Draw(TREE[0], _Position, Color.White);
        }

        private void DrawMiddle(SpriteBatch spriteBatch)
        {
            // Draw the amount of logs
            for (int i = 0; i < _amountOfLogs; i++)
            {
                spriteBatch.Draw(TREE[1], new Vector2(_Position.X + 5, _Position.Y - (TREE[0].Height + (TREE[1].Height * i))), Color.White);
            }
        }

        private void DrawTop(SpriteBatch spriteBatch)
        {
            // Draw the top
            spriteBatch.Draw(TREE[2], new Vector2(_Position.X - 33, _Position.Y - (float)((TREE[0].Height * 1.85) + (TREE[1].Height * _amountOfLogs))), Color.White);
        }

        public override string ToString()
        {
            return string.Format("Name:{0} - Rectangle {1}", Name, _HitBox);
        }
    }
}
