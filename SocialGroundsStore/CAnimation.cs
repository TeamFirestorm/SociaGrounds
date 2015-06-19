using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SocialGroundsStore
{
    public class CAnimation
    {
        // The spritesheet to be animated
        private readonly Texture2D _texture;

        // The source rectangle
        private Rectangle _sourceRect;

        // Width of a single frame
        private readonly int _frameWidth;

        // Height of a single frame
        private readonly int _frameHeight;

        // The amount of frames per second
        private readonly double _fps;

        // Boolean to check if the animation is looping or not
        private readonly bool _isLooping;

        // Integer to check what frame is currently at position
        private int _currentFrame;

        // Boolean to check if the animation should be playing or not
        private bool _isPlaying;

        // The amount of time that has been elapsed
        private float _timeElapsed;

        // The position to draw the animation at
        private Vector2 _position;
        public Vector2 Position
        {
            set { _position = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="texture">The spritesheet to be parsed</param>
        /// <param name="position">The position of the animation to draw</param>
        /// <param name="frameWidth">The width of a single frame</param>
        /// <param name="frameHeight">The height of a single frame</param>
        /// <param name="startRow">Which row do you want to be animated?</param>
        /// <param name="fps">The amount of frames per second to play</param>
        /// <param name="isLooping">Should the animation loop or not?</param>
        public CAnimation(Texture2D texture, Vector2 position, int frameWidth, int frameHeight, int startRow, int fps, bool isLooping)
        {
            _texture = texture;
            _position = position;
            _isLooping = isLooping;
            _frameWidth = frameWidth;
            _frameHeight = frameHeight;
            _isPlaying = true;
            _timeElapsed = 0;

            // FPS is entered in the parameter, but transformed in this class
            _fps = (1d / fps) * 1000;

            // Initializing the source rectangle
            _sourceRect = new Rectangle(0, frameHeight * startRow, frameWidth, frameHeight);

            // Initializing the texture data
            var textureData = new Color[texture.Width * texture.Height];
            texture.GetData(textureData);
        }

        /// <summary>
        /// Reset the current animation frame to the default frame
        /// </summary>
        /// <param name="row">Which row are you targetting?</param>
        /// <param name="gameTime">The amount of time that has been elapsed</param>
        public void ResetAnimation(int row, GameTime gameTime)
        {
            // Check if the time has been elapsed to update the next frame
            // If not, do nothing
            if (_timeElapsed >= _fps)
            {
                // If the animation should be playing, update the source rectangle
                if (_isPlaying)
                {
                    _currentFrame = 0;
                    _sourceRect = new Rectangle(_frameWidth * _currentFrame, _frameHeight * row, _frameWidth, _frameHeight);
                }

                // Reset the amount of times elapsed
                _timeElapsed = 0;
            }

            // Update the amount of time elapsed
            _timeElapsed += gameTime.ElapsedGameTime.Milliseconds;
        }

        // Method to be called to update the source rectangle
        public void Play(int row, int maxFrames, GameTime gameTime)
        {
            // Check if the time has been elapsed to update the next frame
            // If not, do nothing
            if (_timeElapsed >= _fps)
            {
                // Check if the max amount of frames has been reached
                // If the animation is set on looping, reset animation and play it again
                if (_currentFrame == maxFrames && _isLooping)
                {
                    _currentFrame = 0;
                }
                // If the animation is not set on looping, stop the animation
                else if (_currentFrame == maxFrames && !_isLooping)
                {
                    _isPlaying = false;
                }

                // If the animation should be playing, update the source rectangle
                if (_isPlaying)
                {
                    _sourceRect = new Rectangle(_frameWidth * _currentFrame, _frameHeight * row, _frameWidth, _frameHeight);
                    _currentFrame++;
                }

                // Reset the amount of times elapsed
                _timeElapsed = 0;
            }

            // Update the amount of time elapsed
            _timeElapsed += gameTime.ElapsedGameTime.Milliseconds;
        }


        // Method to draw the current frame
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, _sourceRect, Color.White);
        }
    }
}
