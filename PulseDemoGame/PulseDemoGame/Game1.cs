using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PulseEngine;
using PulseEngine.Component.Collision;
using PulseEngine.Display.World;

namespace PulseDemoGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont spriteFont;
        //--- Node  First --- 
        EntityNode eNode =
            new EntityNode();

        //--- Sprite --- 
        Classes.Ball_Class ball = 
            new Classes.Ball_Class();

        Background background;

        TileMap tm = new TileMap();
        MapCollision mc;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for ay required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            //Initialise Sprite 
            
            ball.AssetName = "sprite1";
            ball.screenCollision =
                new ScreenCollision(0, 0, this.Window.ClientBounds.Width,
                this.Window.ClientBounds.Height);


            eNode.AttachNode(ball);
            background = new Background();
         

            eNode.Initialise();
            base.Initialize();
        }
        

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here  

            spriteFont = this.Content.Load<SpriteFont>("SpriteFont1");
            eNode.Load(this.Content);
            background.assetName = "image1";
            background.LoadContent(this.Content);
            background.screenWidth = this.Window.ClientBounds.Width;
            background.screenHeight = this.Window.ClientBounds.Height;

            tm.TileName.Add("grassLeftBlock");
            tm.TileName.Add("grassCenterBlock");
            tm.TileName.Add("grassRightBlock");

            int[,] map =
                new int[,] {
                    { 0, 0, 0, 0}, { 0, 0, 0, 0 },
                    { 1, 2, 2, 3}, { 0, 0, 0, 0 }
                };

            tm.Load(this.Content, map, 60, 60, 1.0f);
            mc = new MapCollision();

            mc.AttachTo(ball);
            ball.AddComponet(mc);
            mc.objectList = tm.Tiles;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            background.Update(gameTime);
            // TODO: Add your update logic here
           background.TileHorizontal = true;
            background.TileVertical = true;
            background.VerticalSpeed = 1.5f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            background.HorizontalSpeed = 1.5f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            eNode.Update(gameTime);

           tm.Update(gameTime);
           

            mc.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            float frameRate = 1 / (float)gameTime.ElapsedGameTime.TotalSeconds;

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            //-----------------------------------------------
            background.Draw(spriteBatch);

            tm.Draw(spriteBatch);

            eNode.Draw(spriteBatch);
            // ----------------------------------------------
            spriteBatch.DrawString(spriteFont, "FPS: " 
                + frameRate.ToString(), new Vector2(0, 0), Color.White);


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
