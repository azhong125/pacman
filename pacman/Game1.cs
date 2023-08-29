using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project2
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private pacman Pacman;

        //private Ghost inky;
        private Ghost blinky;
       // private Ghost pinky;
        //private Ghost clyde;

        public RenderTarget2D scene;

        private SpriteFont font;

        public TileMap map;
        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            #region Map
            //left top corner = 1;
            //top edge = 2
            //top right corner = 3;
            //left edge = 4;
            //right edge =5;
            // bottom left corner = 6;
            // bottom edge = 7
            //bottom right corner = 8;
            // inner tiles:
            //top left = 9
            //top edge = 10
            //top right corner = 11
            //left edge = 12
            //right edge = 13
            //bottom left corner = 14
            //bottom edge = 15
            //bottom right corner = 16
            //wall curvy things
            //top left corner = 17
            //top right corner = 18
            //left side top  = 19
            //left side bottom = 20
            //right side top = 21
            //right side bottom = 22
            //ghost cage
            //top left = 23
            //top edge = 24
            //top right = 25
            //left edge = 26
            //right edge = 27
            // bottom left = 28
            //bottom edge = 29
            //bottom right = 30
            //doors = 31

            int[,] mapTiles = new int[,] { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                      { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                      { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                      { 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2,17,18, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3 },
                                      { 4,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,12,13,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2, 5 },
                                      { 4,-2, 9,10,10,11,-2, 9,10,10,10,11,-2,12,13,-2, 9,10,10,10,11,-2, 9,10,10,11,-2, 5 },
                                      { 4,-3,12, 0, 0,13,-2,12, 0, 0, 0,13,-2,12,13,-2,12, 0, 0, 0,13,-2,12, 0, 0,13,-3, 5 },
                                      { 4,-2,14,15,15,16,-2,14,15,15,15,16,-2,14,16,-2,14,15,15,15,16,-2,14,15,15,16,-2, 5 },
                                      { 4,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2, 5 },
                                      { 4,-2, 9,10,10,11,-2, 9,11,-2, 9,10,10,10,10,10,10,11,-2, 9,11,-2, 9,10,10,11,-2, 5 },
                                      { 4,-2,14,15,15,16,-2,12,13,-2,14,15,15,11, 9,15,15,16,-2,12,13,-2,14,15,15,16,-2, 5 },
                                      { 4,-2,-2,-2,-2,-2,-2,12,13,-2,-2,-2,-2,12,13,-2,-2,-2,-2,12,13,-2,-2,-2,-2,-2,-2, 5 },
                                      { 6, 7, 7, 7, 7,11,-2,12,14,15,15,11,-1,12,13,-1, 9,10,10,16,13,-2, 9, 7, 7, 7, 7, 8 },
                                      { 0, 0, 0, 0, 0, 4,-2,12, 9,10,10,16,-1,14,16,-1,14,15,15,11,13,-2, 5, 0, 0, 0, 0, 0 },
                                      { 0, 0, 0, 0, 0, 4,-2,12,13,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,12,13,-2, 5, 0, 0, 0, 0, 0 },
                                      { 0, 0, 0, 0, 0, 4,-2,12,13,-1,23,24,24,24,24,24,24,25,-1,12,13,-2, 5, 0, 0, 0, 0, 0 },
                                      { 2, 2, 2, 2, 2,16,-2,14,16,-1,26,-1,-1,-1,-1,-1,-1,27,-1,14,16,-2,14, 2, 2, 2, 2, 2 },
                                      {-1,-1,-1,-1,-1,-1,-2,-1,-1,-1,26,-1,-1,-1,-1,-1,-1,27,-1,-1,-1,-2,-1,-1,-1,-1,-1,-1 },
                                      { 7, 7, 7, 7, 7,11,-2, 9,11,-1,26,-1,-1,-1,-1,-1,-1,27,-1, 9,11,-2, 9, 7, 7, 7, 7, 7 },
                                      { 0, 0, 0, 0, 0, 4,-2,12,13,-1,28,29,29,29,29,29,29,30,-1,12,13,-2, 5, 0, 0, 0, 0, 0 },
                                      { 0, 0, 0, 0, 0, 4,-2,12,13,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,12,13,-2, 5, 0, 0, 0, 0, 0 },
                                      { 0, 0, 0, 0, 0, 4,-2,12,13,-1, 9,10,10,10,10,10,10,11,-1,12,13,-2, 5, 0, 0, 0, 0, 0 },
                                      { 1, 2, 2, 2, 2,16,-2,14,16,-1,14,15,15,11, 9,15,15,16,-1,14,16,-2,14, 2, 2, 2, 2, 3 },
                                      { 4,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,12,13,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2, 5 },
                                      { 4,-2, 9,10,10,11,-2, 9,10,10,10,11,-2,12,13,-2, 9,10,10,10,11,-2, 9,10,10,11,-2, 5 },
                                      { 4,-2,14,15,11,13,-2,14,15,15,15,16,-2,14,16,-2,14,15,15,15,16,-2,12, 9,15,16,-2, 5 },
                                      { 4,-3,-2,-2,12,13,-2,-2,-2,-2,-2,-2,-2,-1,-1,-2,-2,-2,-2,-2,-2,-2,12,13,-2,-2,-3, 5 },
                                      {19,15,11,-2,12,13,-2, 9,11,-2, 9,10,10,10,10,10,10,11,-2, 9,11,-2,12,13,-2, 9,10,21 },
                                      {20,10,16,-2,14,16,-2,12,13,-2,14,15,15,11, 9,15,15,16,-2,12,13,-2,14,16,-2,14,15,22 },
                                      { 4,-2,-2,-2,-2,-2,-2,12,13,-2,-2,-2,-2,12,13,-2,-2,-2,-2,12,13,-2,-2,-2,-2,-2,-2, 5 },
                                      { 4,-2, 9,10,10,10,10,16,14,15,15,11,-2,12,13,-2, 9,10,10,16,14,10,10,10,10,11,-2, 5 },
                                      { 4,-2,14,10,10,10,10,10,10,10,10,16,-2,14,16,-2,14,15,15,15,15,15,15,15,15,16,-2, 5 },
                                      { 4,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2, 5 },
                                      { 6, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 8 },
                                      { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                      { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}}; ;
            map = new TileMap(mapTiles);
            map.Generate();
            #endregion

            Pacman = new pacman( "pacmanRight", map);
            //clyde = new Ghost("clyde", map, map.path[139].rectangle);
            //inky = new Ghost("inky", map, map.path[176].rectangle);
           // pinky = new Ghost("pinky", map, map.path[162].rectangle);
            blinky = new Ghost("blinky", map, map.path[152].rectangle);

        }

        protected override void Initialize()
        {
            //224 by 288
            _graphics.PreferredBackBufferWidth = 224;
            _graphics.PreferredBackBufferHeight = 288;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();
            //scene = new RenderTarget2D(_graphics.GraphicsDevice, 448, 576);

            base.Initialize();

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Pacman.content = Content;
           
            //clyde.texture = Content.Load<Texture2D>(clyde.textureName);
            //inky.texture = Content.Load<Texture2D>(inky.textureName);
           // pinky.texture = Content.Load<Texture2D>(pinky.textureName);
            blinky.texture = Content.Load<Texture2D>(blinky.textureName);
            font = Content.Load<SpriteFont>("font");

            foreach(CollisionTiles colTile in map.collisionTile)
            {
                colTile.texture = Content.Load<Texture2D>("tile" + colTile.i);
            }

            foreach (PathTiles paths in map.path)
            {
                paths.texture = Content.Load<Texture2D>("tile" + paths.i);
            }

            Pacman.texture = Content.Load<Texture2D>(Pacman.textureName);

        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            Pacman.Update(gameTime);
            //clyde.Update(gameTime);
            //inky.Update(gameTime);
           // pinky.Update(gameTime);
            blinky.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);
            //GraphicsDevice.SetRenderTarget(scene);
            //GraphicsDevice.SetRenderTarget(null);

            _spriteBatch.Begin();

            _spriteBatch.DrawString(font, "hello", new Vector2(10, 10), Color.White);
            map.Draw(_spriteBatch);

            Pacman.Draw(_spriteBatch);
            //clyde.Draw(_spriteBatch);
            //inky.Draw(_spriteBatch);
           // pinky.Draw(_spriteBatch);
            blinky.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
