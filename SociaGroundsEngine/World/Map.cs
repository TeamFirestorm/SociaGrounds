using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SociaGroundsEngine.World
{
    public class Map
    {
        List<Asset> nonSolidAssets;
        public List<Asset> NonSolidAssets
        {
            get { return nonSolidAssets; }
        }

        List<Asset> solidAssets;
        public List<Asset> SolidAssets
        {
            get { return solidAssets; }
        }

        int mapWidth, mapHeight;
        public int MapWidth
        {
            get { return mapWidth; }
        }
        public int MapHeight
        {
            get { return mapHeight; }
        }
        
        // Generate the map by saving certain objects in the list
        public Map(int[,] map, Vector2 startPosition, ContentManager content)
        {
            nonSolidAssets = new List<Asset>();
            solidAssets = new List<Asset>();
            Vector2 currentPosition = startPosition;
            Texture2D grassTexture = content.Load<Texture2D>("World/Grass/Grass_0");

            // Looping through the multidimensional array that has been given
            for (int x = 0; x < map.GetLength(0); x++)
            {
                mapWidth = 0;

                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x,y] == 0)
                    {
                        nonSolidAssets.Add(new GrassTile(grassTexture, currentPosition));

                        // Update the x position of the currentPosition
                        currentPosition.X += grassTexture.Width;
                    }

                    if (map[x, y] == 1)
                    {
                        nonSolidAssets.Add(new GrassTile(content.Load<Texture2D>("World/Grass/Grass_2"), currentPosition));

                        // Update the x position of the currentPosition
                        currentPosition.X += grassTexture.Width;
                    }

                    if (map[x, y] == 2)
                    {
                        nonSolidAssets.Add(new GrassTile(content.Load<Texture2D>("World/Grass/Grass_3"), currentPosition));

                        // Update the x position of the currentPosition
                        currentPosition.X += grassTexture.Width;
                    }

                    if (map[x, y] == 3)
                    {
                        nonSolidAssets.Add(new GrassTile(content.Load<Texture2D>("World/Grass/Grass_4"), currentPosition));

                        // Update the x position of the currentPosition
                        currentPosition.X += grassTexture.Width;
                    }

                    if (map[x, y] == 4)
                    {
                        nonSolidAssets.Add(new GrassTile(content.Load<Texture2D>("World/Grass/Grass_F_0"), currentPosition));

                        // Update the x position of the currentPosition
                        currentPosition.X += grassTexture.Width;
                    }

                    if (map[x, y] == 5)
                    {
                        nonSolidAssets.Add(new GrassTile(content.Load<Texture2D>("World/Grass/Grass_F_1"), currentPosition));

                        // Update the x position of the currentPosition
                        currentPosition.X += grassTexture.Width;
                    }

                    if (map[x, y] == 6)
                    {
                        nonSolidAssets.Add(new GrassTile(content.Load<Texture2D>("World/Grass/Grass_F_2"), currentPosition));

                        // Update the x position of the currentPosition
                        currentPosition.X += grassTexture.Width;
                    }

                    // Update the mapWidth
                    mapWidth += grassTexture.Width;
                }
                // Update the y position of the currentPosition
                // And reset the x position
                currentPosition.Y -= grassTexture.Height;
                currentPosition.X = startPosition.X;

                // Update the mapHeight
                mapHeight += grassTexture.Height;
            }
        }

        public void addSolidAsset(Asset asset)
        {
            solidAssets.Add(asset);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            foreach(Asset asset in nonSolidAssets)
            {
                asset.draw(spriteBatch);
            }

            foreach (Asset asset in solidAssets)
            {
                asset.draw(spriteBatch);
            }
        }
    }
}
