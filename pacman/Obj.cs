using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace pacman
{
    public class Obj
    {
        #region Variables
        public String textureName = String.Empty;
        public Texture2D texture = null;
       
        public Vector2 center = Vector2.Zero;
        public Vector2 position = Vector2.Zero;

        public float rotation = 0.0f;
        public float scale = 1.0f;
        public int speed = 0;

        public int currentFrame;
        public int totalFrames;

        public bool isAlive = true;

        #endregion

        public Obj(Vector2 pos, string TextureName)
        {
            position = pos;
            textureName = TextureName;

        }

        public Obj()
        {

        }

        public virtual void LoadContent(ContentManager content)
        {
        }

        public virtual void Update(GameTime gameTime)
        {
            
        }

    }
}
