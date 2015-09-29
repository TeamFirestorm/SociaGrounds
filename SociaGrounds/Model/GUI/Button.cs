﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SociaGrounds.Model.GUI
{
    /// <summary>
    /// This class is used to manage all the buttons of the application.
    /// </summary>
    public class Button
    {
        // Position of the button
        public Vector2 Position { get; set; }

        // Scaling properties
        private readonly float _scale;

        public float Width { get; }

        //Textures        
        private readonly Texture2D[][] _tetxures;

        // Text
        private readonly SpriteFont _font;
        private readonly string _text;

        // Rectangle for detection
        private readonly Rectangle _hitBox;
        private int state;

        private readonly float _textPosition;
        
        public Button(ContentManager content, Vector2 position, string text, float scale) 
            : this(content, position, text, scale, text.Length)
        {
        }

        public Button(ContentManager content, Vector2 position, string text, float scale, float fixedWidth)
        {
            _tetxures = new[]
            {
                new []
                {
                    content.Load<Texture2D>("SociaGrounds/GUI/Button/StandardButtonLeft"),
                    content.Load<Texture2D>("SociaGrounds/GUI/Button/StandardButtonMiddle"),
                    content.Load<Texture2D>("SociaGrounds/GUI/Button/StandardButtonRight")
                },
                new []
                {
                    content.Load<Texture2D>("SociaGrounds/GUI/Button/PressedButtonLeft"),
                    content.Load<Texture2D>("SociaGrounds/GUI/Button/PressedButtonMiddle"),
                    content.Load<Texture2D>("SociaGrounds/GUI/Button/PressedButtonRight")
                },
            };

            // Text
            _text = text;
            _font = content.Load<SpriteFont>("SociaGrounds/SociaGroundsFont");

            // The position, width and scale
            Position = position;
            Width = fixedWidth;
            _scale = scale;

            _textPosition = (((Width * _tetxures[0][1].Width) /2) - ((text.Length * _tetxures[0][1].Width) /3f));

            _hitBox = new Rectangle((int)(position.X * scale), (int)(position.Y * scale), (int)((_tetxures[0][0].Width * scale) + ((_tetxures[0][1].Width * Width) * scale) + (_tetxures[0][2].Width * scale)), (int)(_tetxures[0][1].Height * scale));

            state = 0;
        }

        /// <summary>
        /// Checks if the mousecursor hovers over this buttons hitbox
        /// </summary>
        /// <param name="mouseState">the current mousestate</param>
        /// <returns></returns>
        public bool IsHover(MouseState mouseState)
        {
            // Check if the current mouse position is over hitbox
            if (new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(_hitBox))
            {
                state = 1;
                return true;
            }
            state = 0;
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {       
                // Drawing the left part
                spriteBatch.Draw(_tetxures[state][0], Position, null, Color.White, 0f, new Vector2(0, 0), new Vector2(_scale, _scale), SpriteEffects.None, 0f);

                // Drawing the mid part
                for (int i = 0; i < Width; i++)
                {
                    spriteBatch.Draw(_tetxures[state][1], new Vector2(Position.X + (_tetxures[0][0].Width * _scale) + ((_tetxures[0][1].Width * _scale) * i), Position.Y), null, Color.White, 0f, new Vector2(0, 0), new Vector2(_scale, _scale), SpriteEffects.None, 0f);
                }

                // Drawing the right part
                spriteBatch.Draw(_tetxures[state][2], new Vector2(Position.X + (_tetxures[0][0].Width * _scale) + ((_tetxures[0][1].Width * _scale) * Width), Position.Y), null, Color.White, 0f, new Vector2(0, 0), new Vector2(_scale, _scale), SpriteEffects.None, 0f);

                // Drawing the text
                spriteBatch.DrawString(_font, _text, new Vector2(Position.X + (_textPosition * _scale), Position.Y + (40 * _scale)), Color.Black, 0f, new Vector2(0, 0), new Vector2(_scale / 0.5f, _scale / 0.5f), SpriteEffects.None, 0f);
            }
        }
}