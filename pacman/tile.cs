using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace pacman
{
    public class tile
    {

        public Texture2D texture;

        public int i;

        public Rectangle rectangle { get; set; }

        public int type;
        public int x;
        public int y;

        public tile()
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }

    }

    public class CollisionTiles : tile
    {
        public CollisionTiles(Rectangle rect, int i)
        {
            this.i = i;
            rectangle = rect;
        }

        public CollisionTiles()
        {

        }
    }

    public class PathTiles : tile
    {
        public bool North;
        public bool East;
        public bool South;
        public bool West;

        public int numDirections;

        public PathTiles(Rectangle rect, int i)
        {
            this.i = i;
            rectangle = rect;
            North = false;
            East = false;
            South = false;
            West = false;
            numDirections = 0;
        }
    }

    }
