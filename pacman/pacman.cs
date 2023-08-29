using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace pacman
{
    public class pacman : Obj
    {
        private KeyboardState currentState;

        public Rectangle rectangle;
        public Rectangle nextRect;

        private int updateNum = 0;
        private TileMap map;

        public int score;
        public ContentManager content;

        public pacman(string textName, TileMap map) 
        {
            textureName = textName;
            this.map = map;

            rectangle = map.path[164].rectangle;
            map.pacmanPos = map.path[160];
           
            isAlive = true;
            speed = 1;

            totalFrames = 3;
            rotation = 1;
        }

        public pacman()
        {

        }

        public override void Update(GameTime gameTime)
        {
            updateNum++;

            if (updateNum % 5 == 0)
            {
                currentFrame++;

                if (currentFrame == totalFrames)
                {
                    currentFrame = 0;
                }
            }
            
            currentState = Keyboard.GetState();

            if (!isAlive) return;

            #region Input

            if (currentState.IsKeyDown(Keys.W))
            {
                rotation = (int)rotationEnum.North;
            }
            if (currentState.IsKeyDown(Keys.A))
            {
                rotation = (int)rotationEnum.West;
            }

            if (currentState.IsKeyDown(Keys.S))
            {
                rotation = (int)rotationEnum.South;
            }

            if (currentState.IsKeyDown(Keys.D))
            {
                rotation = (int)rotationEnum.East;
            }

            if (rotation == (int) rotationEnum.North)
            {
                nextRect = new Rectangle(rectangle.X, rectangle.Y - speed, 8, 8);
            }
            if (rotation == (int) rotationEnum.West)
            {
                nextRect = new Rectangle(rectangle.X - speed, rectangle.Y, 8, 8);
            }
            if (rotation == (int) rotationEnum.South)
            {
                nextRect = new Rectangle(rectangle.X, rectangle.Y + speed, 8, 8);
            }
            if (rotation == (int) rotationEnum.East)
            {
                nextRect = new Rectangle(rectangle.X + speed, rectangle.Y, 8, 8);
            }

            if (inPath(nextRect))
            {
                rectangle = nextRect;
                
            }

            #endregion

        }

        public bool inPath(Rectangle next)
        {
            foreach(PathTiles pathTile in map.path)
            {
                if (rotation == (int)rotationEnum.North)
                {
                    if (next.X <= pathTile.rectangle.X + 2 && next.X >= pathTile.rectangle.X - 2 &&
                        next.Y >= pathTile.rectangle.Y && next.Y <= pathTile.rectangle.Y + 8)
                    {
                        map.pacmanPos = pathTile;
                        foodEaten(pathTile);
                        return true;
                    }
                }
                if (rotation == (int)rotationEnum.West)
                {
                    if (next.Y <= pathTile.rectangle.Y + 2 && next.Y >= pathTile.rectangle.Y - 2 &&
                        next.X >= pathTile.rectangle.X && next.X <= pathTile.rectangle.X + 8)
                    {
                        map.pacmanPos = pathTile;
                        foodEaten(pathTile);
                        return true;
                    }
                }
                if (rotation == (int)rotationEnum.South)
                {
                    if (next.X <= pathTile.rectangle.X + 2 && next.X >= pathTile.rectangle.X - 2 &&
                        next.Y >= pathTile.rectangle.Y - 8 && next.Y <= pathTile.rectangle.Y)
                    {
                        map.pacmanPos = pathTile;
                        foodEaten(pathTile);
                        return true;
                    }
                }
                if (rotation == (int)rotationEnum.East)
                {
                    if (next.Y <= pathTile.rectangle.Y + 2 && next.Y >= pathTile.rectangle.Y - 2 &&
                        next.X <= pathTile.rectangle.X && next.X >= pathTile.rectangle.X - 8)
                    {
                        map.pacmanPos = pathTile;
                        foodEaten(pathTile);
                        return true;
                    }
                }

            }
            return false;
        }

        public void foodEaten(PathTiles pathTile)
        {
            if (map.map[pathTile.y, pathTile.x] == -2)
            {
                pathTile.texture = content.Load<Texture2D>("tile" + -1);
                map.map[pathTile.y, pathTile.x] = -1;
                score += 10;
            }
            else if (map.map[pathTile.y, pathTile.x] == -3)
            {
                pathTile.texture = content.Load<Texture2D>("tile" + -1);
                map.map[pathTile.y, pathTile.x] = -1;
                score += 50;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle(16 * currentFrame, 0, 16, 16);
            Rectangle destinationRectangle = new Rectangle(rectangle.X + 4, rectangle.Y + 4, 16, 16);

            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White,
                 MathHelper.ToRadians(rotation), new Vector2( sourceRectangle.Width / 2, sourceRectangle.Height / 2),
                 SpriteEffects.None, 0);

            }
        }
    }

