using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SocialGroundsStore
{
    public class CAnimation
    {
        // The spritesheet to be animated
        private readonly Texture2D _texture;
        //public Texture2D Texture
        //{
        //    get { return texture; }
        //}

        // The source rectangle
        private Rectangle _sourceRect;
        //public Rectangle SourceRect
        //{
        //    get { return sourceRect; }
        //}

        // Width of a single frame
        private readonly int _frameWidth;
        //public int FrameWidth
        //{
        //    get { return frameWidth; }
        //}

        // Height of a single frame
        private readonly int _frameHeight;
        //public int FrameHeight
        //{
        //    get { return frameHeight; }
        //}

        // The amount of frames per second
        private readonly double _fps;
        //public double Fps
        //{
        //    get { return fps; }
        //}

        // Boolean to check if the animation is looping or not
        private readonly bool _isLooping;
        //public bool IsLooping
        //{
        //    get { return isLooping; }
        //}

        // Integer to check what frame is currently at position
        private int _currentFrame;
        //public int CurrentFrame
        //{
        //    get { return currentFrame; }
        //}

        // Boolean to check if the animation should be playing or not
        private bool _isPlaying;
        //public bool IsPlaying
        //{
        //    get { return isPlaying; }
        //}

        // The amount of time that has been elapsed
        private float _timeElapsed;
        //public float TimeElapsed
        //{
        //    get { return timeElapsed; }
        //}

        // The position to draw the animation at
        private Vector2 _position;
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        // The pixel data of the texture
        private readonly Color[] _textureData;
        //public Color[] TextureData
        //{
        //    get { return textureData; }
        //}

        // Constructor
        public CAnimation(Texture2D texture, Vector2 position, int frameWidth, int frameHeight, int startRow, int fps, bool isLooping)
        {
            this._texture = texture;
            this._position = position;
            this._isLooping = isLooping;
            this._frameWidth = frameWidth;
            this._frameHeight = frameHeight;
            _isPlaying = true;
            _timeElapsed = 0;

            // FPS is entered in the parameter, but transformed in this class
            this._fps = ((double)1 / (double)fps) * 1000;

            // Initializing the source rectangle
            _sourceRect = new Rectangle(0, frameHeight * startRow, frameWidth, frameHeight);

            // Initializing the texture data
            _textureData = new Color[texture.Width * texture.Height];
            texture.GetData(_textureData);
        }

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
