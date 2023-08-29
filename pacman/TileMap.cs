using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace pacman
{
    public class TileMap
    {
        public int[,] tileList;

        public List<CollisionTiles> collisionTile = new List<CollisionTiles>();
        public List<PathTiles> path = new List<PathTiles>();

        public PathTiles pacmanPos;
        public int[,] map;

        public TileMap(int[,] map)
        {
            this.map = map;
        }


        public virtual void Draw(SpriteBatch spriteBatch)
        {

            foreach(CollisionTiles tile1 in collisionTile)
            {
                tile1.Draw(spriteBatch);
            }

            foreach(PathTiles paths in path)
            {
                paths.Draw(spriteBatch);
            }
        }

        public virtual void Generate()
        {
            for (int i = 0; i < map.GetLength(1); i++)
                for (int j = 0; j < map.GetLength(0); j++)
                {
                    int num = map[j, i];

                    if(num > 0)
                    {
                        collisionTile.Add(new CollisionTiles(new Rectangle(i * 8, j * 8, 8, 8), num));
                    }

                    if (num < 0)
                    {
                        PathTiles pathTile = new PathTiles(new Rectangle(i * 8, j * 8, 8, 8), num);
                        pathTile.x = i;
                        pathTile.y = j;
                        path.Add(pathTile);

                        if(j > 0 )
                        {
                            if (map[j - 1, i] < 0)
                            {
                                pathTile.North = true;
                                pathTile.numDirections++;
                            }
                        }

                        if (j < 36)
                        {
                            if (map[j + 1, i] < 0)
                            {
                                pathTile.South = true;
                                pathTile.numDirections++;
                            }
                        }

                        if (i > 0)
                        {
                            if (map[j, i - 1] < 0)
                            {
                                pathTile.West = true;
                                pathTile.numDirections++;
                            }
                        }

                        if (i < 27)
                        {
                            if (map[j, i + 1] < 0)
                            {
                                pathTile.East = true;
                                pathTile.numDirections++;
                            }
                        }
                    }
                }
        }
    }
}
