using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PulseEngine;
using PulseEngine.Action;
using PulseEngine.Component.Collision;
using PulseEngine.Component.Instance;
using PulseEngine.Component.Movement;
using PulseEngine.Display.World;
using PulseEngine.Objects.Sprite;

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

        //--- PULSE COMPONENT & ENTITY --- 
        Entity entity = new Entity();
        Spawn<Entity> spawn =
                new Spawn<Entity>();


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

            eNode = new EntityNode(entity);
            entity.X = entity.Y = 200;

            
           


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

            entity.AssetName ="ball";
            eNode.Load(this.Content);

            entity.Velocity = new Vector2(1, 1);
            spawn.SpawnType = entity;
            spawn.Relative = true;
            spawn.Initialise();

            entity.Velocity = new Vector2(5, 5);
            spawn.Initialise();
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

            spawn.Update(gameTime);
            eNode.Update(gameTime);

          
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            float frameRate = 1 / (float)gameTime.ElapsedGameTime.TotalSeconds;

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            //-----------------------------------------------

            spawn.Draw(spriteBatch);
            eNode.Draw(spriteBatch);

            // ----------------------------------------------
            spriteBatch.DrawString(spriteFont, "FPS: " 
                + frameRate.ToString(), new Vector2(0, 0), Color.White);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}