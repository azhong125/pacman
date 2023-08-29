using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace pacman
{
    public class Ghost : Obj
    {
        private int updateNum = 0;
        private TileMap map;
        public PathTiles currTile;

        public Rectangle rectangle;
        public Rectangle nextRectangle;

        #region Constructor
        public Ghost(string TextureName, TileMap map, Rectangle startPos)
        {
            textureName = TextureName;
            this.map = map;
            isAlive = true;
            totalFrames = 8;
            rectangle = startPos;
            rotation = (int)rotationEnum.East;

            speed = 1;
        }

        public Ghost()
        {
         
        }
        #endregion

        public override void Update(GameTime gameTime)
        {
            
            #region Rotation
            if (rotation == (int)rotationEnum.West)
            {
                if (updateNum % 5 == 0)
                {
                    if (currentFrame != 0)
                    {
                        currentFrame = 0;
                    }
                    else
                    {
                        currentFrame++;
                    }
                }
                nextRectangle = new Rectangle(rectangle.X - 1, rectangle.Y, 8, 8);
            }

            if (rotation == (int)rotationEnum.South)
            {
                if (updateNum % 5 == 0)
                {
                    if (currentFrame != 2)
                    {
                        currentFrame = 2;
                    }
                    else
                    {
                        currentFrame++;
                    }
                }
                nextRectangle = new Rectangle(rectangle.X, rectangle.Y + 1, 8, 8);
            }

            if (rotation == (int)rotationEnum.East)
            {
                if (updateNum % 5 == 0)
                {
                    if (currentFrame != 4)
                    {
                        currentFrame = 4;
                    }
                    else
                    {
                        currentFrame++;
                    }
                }
                nextRectangle = new Rectangle(rectangle.X + 1, rectangle.Y, 8, 8);
            }

            if (rotation == (int)rotationEnum.North)
            {
                if (updateNum % 5 == 0)
                {
                    if (currentFrame != 6)
                    {
                        currentFrame = 6;
                    }
                    else
                    {
                        currentFrame++;
                    }
                }
                nextRectangle = new Rectangle(rectangle.X, rectangle.Y - 1, 8, 8);
            }
            if (inPath(nextRectangle))
            {
                rectangle = nextRectangle;
                //if (currTile.numDirections >= 3) changeDirection();
            }
            else
            {
                changeDirection();
            }

            updateNum++;


            #endregion

        }

        public virtual void changeDirection()
        {
            double small = 99999;
            int rotationVal = 1;

            if(currTile.East)
            {
                double distance = distanceTo(currTile.x + 8, currTile.y, map.pacmanPos.x, map.pacmanPos.y);
                if (distance < small)
                {
                    small = distance;
                    rotationVal = (int)rotationEnum.East;
                }

            }
            if (currTile.North)
            {
                double distance = distanceTo(currTile.x, currTile.y - 8, map.pacmanPos.x, map.pacmanPos.y);
                if (distance < small)
                {
                    small = distance;
                    rotationVal = (int)rotationEnum.North;
                }
            }
            if (currTile.South)
            {
                double distance = distanceTo(currTile.x, currTile.y + 8, map.pacmanPos.x, map.pacmanPos.y);
                if (distance < small)
                {
                    small = distance;
                    rotationVal = (int)rotationEnum.South;
                }
            }
            if (currTile.West)
            {
                double distance = distanceTo(currTile.x - 8, currTile.y, map.pacmanPos.x, map.pacmanPos.y);
                if (distance < small)
                {
                    small = distance;
                    rotationVal = (int)rotationEnum.West;
                }
            }

            rotation = rotationVal;
        }

        public virtual double distanceTo(int x1, int y1, int x2, int y2)
        {
            double xDist = x1 - x2;
            double yDist = y1 - y2;
            return Math.Sqrt(xDist * xDist + yDist * yDist);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle(16 * currentFrame, 0, 16, 16);
            Rectangle destinationRectangle = new Rectangle(rectangle.X - 4, rectangle.Y - 3, 16, 16);

            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);

        }

        public bool inPath(Rectangle next)
        {
            foreach (PathTiles pathTile in map.path)
            {
                if (rotation == (int)rotationEnum.North)
                {
                    if (next.X == pathTile.x && next.Y >= pathTile.rectangle.Y && next.Y <= pathTile.rectangle.Y + 8)
                    {
                        currTile = pathTile;
                        return true;
                    }
                }
                if (rotation == (int)rotationEnum.West)
                {
                    if (next.Y == pathTile.rectangle.Y && next.X >= pathTile.rectangle.X && next.X <= pathTile.rectangle.X + 8)
                    {
                        currTile = pathTile;
                        return true;
                    }
                }
                if (rotation == (int)rotationEnum.South)
                {
                    if (next.X == pathTile.rectangle.X && next.Y >= pathTile.rectangle.Y - 8 && next.Y <= pathTile.rectangle.Y)
                    {
                        currTile = pathTile;
                        return true;
                    }
                }
                if (rotation == (int)rotationEnum.East)
                {
                    if (next.Y == pathTile.rectangle.Y && next.X <= pathTile.rectangle.X && next.X >= pathTile.rectangle.X - 8)
                    {
                        currTile = pathTile;
                        return true;
                    }
                }

            }
            return false;
        }
    }

    /**public class Inky : Ghost
    {
        public Inky(string textureName)
        {
            this.textureName = textureName;
        }
            

    }
    public class Pinky : Ghost
    {
        public Pinky(string textureName)
        {
            this.textureName = textureName;

        }
    }
    public class Blinky : Ghost
    {
        public Blinky(string textureName)
        {
            this.textureName = textureName;

        }
    }
    public class Clyde : Ghost
    {
        public Clyde(string textureName)
        {
            this.textureName = textureName;

        }
    }**/
}
