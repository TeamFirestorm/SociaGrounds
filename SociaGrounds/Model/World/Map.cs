using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SociaGrounds.Model.World
{
    public class Map
    {
        private readonly List<Asset> _nonSolidAssets;

        private static readonly List<SolidAsset> SOLID_ASSETS;

        public static List<SolidAsset> SolidAssets => SOLID_ASSETS;

        public Vector2 StartPosition { get; }

        private readonly int _mapWidth, _mapHeight;

        public int MapWidth => _mapWidth;

        public int MapHeight => _mapHeight;

        private int[,] _createdMap;

        static Map()
        {
            SOLID_ASSETS = new List<SolidAsset>();
        }

        // Generate the map by saving certain objects in the list
        public Map(int[,] map, Vector2 startPosition, Vector2 margin, ContentManager content)
        {
            _createdMap = map;

            _nonSolidAssets = new List<Asset>();
            StartPosition = startPosition;
            Vector2 currentPosition = startPosition;

            Texture2D grass0 = content.Load<Texture2D>("SociaGrounds/World/Grass/Grass_0");
            Texture2D grass1 = content.Load<Texture2D>("SociaGrounds/World/Grass/Grass_1");
            Texture2D grass2 = content.Load<Texture2D>("SociaGrounds/World/Grass/Grass_2");
            Texture2D grass3 = content.Load<Texture2D>("SociaGrounds/World/Grass/Grass_3");
            Texture2D grass4 = content.Load<Texture2D>("SociaGrounds/World/Grass/Grass_4");

            Texture2D grassFlower0 = content.Load<Texture2D>("SociaGrounds/World/Grass/Grass_F_0");
            Texture2D grassFlower1 = content.Load<Texture2D>("SociaGrounds/World/Grass/Grass_F_1");
            Texture2D grassFlower2 = content.Load<Texture2D>("SociaGrounds/World/Grass/Grass_F_2");

            _mapWidth = grass0.Width* (int)margin.X;
            _mapHeight = grass0.Height * (int)margin.Y;

            // Looping through the multidimensional array that has been given
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    switch (map[x, y])
                    {
                        case 0:
                            _nonSolidAssets.Add(new GrassTile(grass0, currentPosition));
                            break;
                        case 1:
                            _nonSolidAssets.Add(new GrassTile(grass1, currentPosition));
                            break;
                        case 2:
                            _nonSolidAssets.Add(new GrassTile(grass2, currentPosition));
                            break;
                        case 3:
                            _nonSolidAssets.Add(new GrassTile(grass3, currentPosition));
                            break;
                        case 4:
                            _nonSolidAssets.Add(new GrassTile(grass4, currentPosition));
                            break;
                        case 5:
                            _nonSolidAssets.Add(new GrassTile(grassFlower0, currentPosition));
                            break;
                        case 6:
                            _nonSolidAssets.Add(new GrassTile(grassFlower1, currentPosition));
                            break;
                        case 7:
                            _nonSolidAssets.Add(new GrassTile(grassFlower2, currentPosition));
                            break;
                        default:
                            _nonSolidAssets.Add(new GrassTile(grass0, currentPosition));
                            break;
                    }

                    //update the current position
                    currentPosition.X += grass0.Width;
                }
                // Update the y position of the currentPosition
                // And reset the x position
                currentPosition.Y += grass0.Height;
                currentPosition.X = startPosition.X;
            }
        }

        public void AddSolidAsset(SolidAsset asset)
        {
            SOLID_ASSETS.Add(asset);
        }

        public void DrawNonSolid(SpriteBatch spriteBatch)
        {
            foreach(Asset asset in _nonSolidAssets)
            {
                asset.Draw(spriteBatch);
            }
        }

        public void DrawSolid(SpriteBatch spriteBatch)
        {
            foreach (SolidAsset asset in SOLID_ASSETS)
            {
                asset.Draw(spriteBatch);
            }
        }

        public void DrawShade(SpriteBatch spriteBatch)
        {
            foreach (SolidAsset asset in SOLID_ASSETS)
            {
                asset.DrawShade(spriteBatch);
            }
        }
    }
}
