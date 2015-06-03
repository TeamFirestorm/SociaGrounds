using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SociaGroundsEngine
{
    public class CAnimation
    {
        // The spritesheet to be animated
        Texture2D texture;
        public Texture2D Texture
        {
            get { return texture; }
        }

        // The source rectangle
        Rectangle sourceRect;
        public Rectangle SourceRect
        {
            get { return sourceRect; }
        }

        // Width of a single frame
        int frameWidth;
        public int FrameWidth
        {
            get { return frameWidth; }
        }

        // Height of a single frame
        int frameHeight;
        public int FrameHeight
        {
            get { return frameHeight; }
        }

        // The amount of frames per second
        double fps;
        public double Fps
        {
            get { return fps; }
        }

        // Boolean to check if the animation is looping or not
        bool isLooping;
        public bool IsLooping
        {
            get { return isLooping; }
        }

        // Integer to check what frame is currently at position
        int currentFrame;
        public int CurrentFrame
        {
            get { return currentFrame; }
        }

        // Boolean to check if the animation should be playing or not
        bool isPlaying;
        public bool IsPlaying
        {
            get { return isPlaying; }
        }

        // The amount of time that has been elapsed
        float timeElapsed;
        public float TimeElapsed
        {
            get { return timeElapsed; }
        }

        // The position to draw the animation at
        Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        // The pixel data of the texture
        Color[] textureData;
        public Color[] TextureData
        {
            get { return textureData; }
        }



        // Constructor
        public CAnimation(Texture2D texture, Vector2 position, int frameWidth, int frameHeight, int startRow, int fps, bool isLooping)
        {
            this.texture = texture;
            this.position = position;
            this.isLooping = isLooping;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            isPlaying = true;
            timeElapsed = 0;

            // FPS is entered in the parameter, but transformed in this class
            this.fps = ((double)1 / (double)fps) * 1000;

            // Initializing the source rectangle
            sourceRect = new Rectangle(0, frameHeight * startRow, frameWidth, frameHeight);

            // Initializing the texture data
            textureData = new Color[texture.Width * texture.Height];
            texture.GetData(textureData);
        }


        // Method to be called to update the source rectangle
        public void Play(int row, int maxFrames, GameTime gameTime)
        {
            // Check if the time has been elapsed to update the next frame
            // If not, do nothing
            if (timeElapsed >= fps)
            {
                // Check if the max amount of frames has been reached
                // If the animation is set on looping, reset animation and play it again
                if (currentFrame == maxFrames && isLooping)
                {
                    currentFrame = 0;
                }
                // If the animation is not set on looping, stop the animation
                else if (currentFrame == maxFrames && !isLooping)
                {
                    isPlaying = false;
                }

                // If the animation should be playing, update the source rectangle
                if (isPlaying)
                {
                    sourceRect = new Rectangle(frameWidth * currentFrame, frameHeight * row, frameWidth, frameHeight);
                    currentFrame++;
                }

                // Reset the amount of times elapsed
                timeElapsed = 0;
            }

            // Update the amount of time elapsed
            timeElapsed += gameTime.ElapsedGameTime.Milliseconds;
        }


        // Method to draw the current frame
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, sourceRect, Color.White);
        }
    }
}
