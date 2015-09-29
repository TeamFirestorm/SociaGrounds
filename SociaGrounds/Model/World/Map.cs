using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SociaGrounds.Model.World
{
    public class Map
    {
        private readonly List<Asset> _nonSolidAssets;

        private readonly List<Asset> _solidAssets;
        public List<Asset> SolidAssets
        {
            get { return _solidAssets; }
        }

        private readonly Vector2 _startPosition;
        public Vector2 StartPosition
        {
            get { return _startPosition; }
        }

        private readonly int _mapWidth, _mapHeight;

        public int MapWidth
        {
            get { return _mapWidth; }
        }

        public int MapHeight
        {
            get { return _mapHeight; }
        }

        // Generate the map by saving certain objects in the list
        public Map(int[,] map, Vector2 startPosition, ContentManager content)
        {
            _nonSolidAssets = new List<Asset>();
            _solidAssets = new List<Asset>();
            _startPosition = startPosition;
            Vector2 currentPosition = startPosition;
            Texture2D grassTexture = content.Load<Texture2D>("SociaGrounds/World/Grass/Grass_0");

            // Looping through the multidimensional array that has been given
            for (int x = 0; x < map.GetLength(0); x++)
            {
                _mapWidth = 0;

                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x,y] == 0)
                    {
                        _nonSolidAssets.Add(new GrassTile(grassTexture, currentPosition));

                        // Update the x position of the currentPosition
                        currentPosition.X += grassTexture.Width;
                    }

                    if (map[x, y] == 1)
                    {
                        _nonSolidAssets.Add(new GrassTile(content.Load<Texture2D>("SociaGrounds/World/Grass/Grass_2"), currentPosition));

                        // Update the x position of the currentPosition
                        currentPosition.X += grassTexture.Width;
                    }

                    if (map[x, y] == 2)
                    {
                        _nonSolidAssets.Add(new GrassTile(content.Load<Texture2D>("SociaGrounds/World/Grass/Grass_3"), currentPosition));

                        // Update the x position of the currentPosition
                        currentPosition.X += grassTexture.Width;
                    }

                    if (map[x, y] == 3)
                    {
                        _nonSolidAssets.Add(new GrassTile(content.Load<Texture2D>("SociaGrounds/World/Grass/Grass_4"), currentPosition));

                        // Update the x position of the currentPosition
                        currentPosition.X += grassTexture.Width;
                    }

                    if (map[x, y] == 4)
                    {
                        _nonSolidAssets.Add(new GrassTile(content.Load<Texture2D>("SociaGrounds/World/Grass/Grass_F_0"), currentPosition));

                        // Update the x position of the currentPosition
                        currentPosition.X += grassTexture.Width;
                    }

                    if (map[x, y] == 5)
                    {
                        _nonSolidAssets.Add(new GrassTile(content.Load<Texture2D>("SociaGrounds/World/Grass/Grass_F_1"), currentPosition));

                        // Update the x position of the currentPosition
                        currentPosition.X += grassTexture.Width;
                    }

                    if (map[x, y] == 6)
                    {
                        _nonSolidAssets.Add(new GrassTile(content.Load<Texture2D>("SociaGrounds/World/Grass/Grass_F_2"), currentPosition));

                        // Update the x position of the currentPosition
                        currentPosition.X += grassTexture.Width;
                    }

                    // Update the mapWidth
                    _mapWidth += grassTexture.Width;
                }
                // Update the y position of the currentPosition
                // And reset the x position
                currentPosition.Y += grassTexture.Height;
                currentPosition.X = startPosition.X;

                // Update the mapHeight
                _mapHeight += grassTexture.Height;
            }
        }

        public void AddSolidAsset(Asset asset)
        {
            _solidAssets.Add(asset);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Asset asset in _nonSolidAssets)
            {
                asset.Draw(spriteBatch);
            }
        }

        public void DrawSolid(SpriteBatch spriteBatch)
        {
            foreach (Asset asset in _solidAssets)
            {
                asset.Draw(spriteBatch);
            }
        }
    }
}
