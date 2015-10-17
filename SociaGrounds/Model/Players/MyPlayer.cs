using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using SociaGrounds.Model.Controllers;
using SociaGrounds.Model.GUI;
using SociaGrounds.Model.GUI.Input;

namespace SociaGrounds.Model.Players
{
    public class MyPlayer : Player
    {
        private Direction _lastDirection = Direction.Down;

        /// <summary>
        /// Constructor of the player that can be controlled with direct input
        /// This version is used if the player is a client
        /// </summary>
        /// <param name="startPosition">The startPosition of the player when entering the room</param>
        /// <param name="texture">The spritesheet of the player</param>
        /// <param name="id">The ID of the player relevant for the server</param>
        public MyPlayer(Vector2 startPosition, Texture2D texture, int id) : this(startPosition, texture)
        {
            Id = id;
        }

        /// <summary>
        /// Constructor of the player that can be controlled with direct input
        /// This version is used for the host
        /// </summary>
        /// <param name="startPosition">The startPosition of the player when entering the room</param>
        /// <param name="texture">The spritesheet of the player</param>
        public MyPlayer(Vector2 startPosition, Texture2D texture)
        {
            // General initialize
            _Animation = new Animation(texture, startPosition, 64, 64, 10, 25, true);
            Position = startPosition;
            _Speed = 4;

            // Chat message initialize
            _ChatMessage = "";

            // Rectangle initialize
            _Rect = new Rectangle((int)Position.X, (int)Position.Y,1,1);
        }

        /// <summary>
        /// The update method for the player
        /// </summary>
        /// <param name="gameTime">Gametime object</param>
        /// <param name="state">The keyboard state</param>
        public override void Update(GameTime gameTime, KeyboardState state = default(KeyboardState))
        {
            base.Update(gameTime, state);

            _Rect = new Rectangle((int)Position.X + 17, (int)Position.Y + 12, 30, 50);

            if (Static.ThisDevice != "Windows.Desktop")
            {
                Input(gameTime);
            }
            else
            {
                Input(gameTime, state);
            }
        }

        /// <summary>
        /// Draw method for 
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Static.DummyTexture, _Rect, Color.Blue);
            _Animation.Draw(spriteBatch);
        }

        public void Input(GameTime gameTime)
        {
            if (InputLocation.IsTouchDown(1))
            {
                _lastDirection = Direction.Up;

                if (CollisionDetection.IsCollidingTop(_Rect)) return;

                _Animation.Play(8, 9, gameTime);
                _Position.Y -= _Speed;
            }
            else if (InputLocation.IsTouchDown(3))
            {
                _lastDirection = Direction.Down;

                if (CollisionDetection.IsCollidingBottom(_Rect)) return;

                _Animation.Play(10, 9, gameTime);
                _Position.Y += _Speed;
            }
            else if (InputLocation.IsTouchDown(0))
            {
                _lastDirection = Direction.Left;

                if (CollisionDetection.IsCollidingLeft(_Rect)) return;

                _Animation.Play(9, 9, gameTime);
                _Position.X -= _Speed;
            }
             else if (InputLocation.IsTouchDown(2))
            {
                _lastDirection = Direction.Right;

                if (CollisionDetection.IsCollidingRight(_Rect)) return;

                _Animation.Play(11, 9, gameTime);
                _Position.X += _Speed;
            }
            else
            {
                if (_lastDirection == Direction.Up) _Animation.ResetAnimation(8, gameTime);
                if (_lastDirection == Direction.Left) _Animation.ResetAnimation(9, gameTime);
                if (_lastDirection == Direction.Down) _Animation.ResetAnimation(10, gameTime);
                if (_lastDirection == Direction.Right) _Animation.ResetAnimation(11, gameTime);
            }
        }

        /// <summary>
        /// Method for all input for the palyer
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="keyState"></param>
        public void Input(GameTime gameTime, KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.Up))
            {
                _lastDirection = Direction.Up;

                if (CollisionDetection.IsCollidingTop(_Rect)) return;

                _Animation.Play(8, 9, gameTime);
                _Position.Y -= _Speed;
            }
            else if (keyState.IsKeyDown(Keys.Down))
            {
                _lastDirection = Direction.Down;

                if (CollisionDetection.IsCollidingBottom(_Rect)) return;

                _Animation.Play(10, 9, gameTime);
                _Position.Y += _Speed;
            }
            else if (keyState.IsKeyDown(Keys.Left))
            {
                _lastDirection = Direction.Left;

                if (CollisionDetection.IsCollidingLeft(_Rect)) return;

                _Animation.Play(9, 9, gameTime);
                _Position.X -= _Speed;
            }
            else if (keyState.IsKeyDown(Keys.Right))
            {
                _lastDirection = Direction.Right;

                if (CollisionDetection.IsCollidingRight(_Rect)) return;

                _Animation.Play(11, 9, gameTime);
                _Position.X += _Speed;
            }
            else
            {
                if (_lastDirection == Direction.Up) _Animation.ResetAnimation(8,gameTime);
                if (_lastDirection == Direction.Left) _Animation.ResetAnimation(9, gameTime);
                if (_lastDirection == Direction.Down) _Animation.ResetAnimation(10, gameTime);
                if (_lastDirection == Direction.Right) _Animation.ResetAnimation(11, gameTime);
            }
        }
    }
}
