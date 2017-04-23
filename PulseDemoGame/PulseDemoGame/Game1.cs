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

        // ---- Surface -----
        TileMap tm 
            = new TileMap();

        SpriteFont spriteFont;
  
        //--- Node  First --- 
        EntityNode eNode =
            new EntityNode();
        
        //--- PULSE COMPONENT & ENTITY --- 
        Entity entity = new Entity();

        Gravity gravity = new Gravity();
        BoundingRectangle bounding = new BoundingRectangle();
        PlayerController control = new PlayerController();
        JumpTo jumpTo;
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
            entity.X = entity.Y = 0;

            control.AddKeyBinding(Keys.A, MovementState.Left);
            control.AddKeyBinding(Keys.D, MovementState.Right);
            control.AddKeyBinding(Keys.Space, MovementState.Up);

            control.Pressed += Control_Pressed;
            control.Released += Control_Released;

            entity.AddComponent(bounding);
            entity.AddComponent(control);
            
            eNode.Initialise();

            base.Initialize();
        }

        private void Control_Released(object sender, PressedArgs e)
        {
            e.Attached.Velocity
                = new Vector2(0, e.Attached.Velocity.Y);
        }

        private void Control_Pressed(object sender, PressedArgs e)
        {
            JumpTo.Relative = true;

            if (e.Action == MovementState.Left)
                e.Attached.X += -2;

            if (e.Action == MovementState.Right)
                e.Attached.X += +2;

            if (e.Action == MovementState.Up && jumpTo != null)
                if(!jumpTo.hasJumped)
                jumpTo = new JumpTo(entity, new Vector2(0, -entity.Height * 2));
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
            int[,] map = new int[,] { 
                { 0, 0, 0 },
                { 0, 0, 0 },
                { 0, 0, 0 },
                { 1, 1, 0 },
                { 0, 0, 0 },
                { 1, 1, 1 }
            };

            tm.TileName.Add("ball");

            tm.Load(this.Content, map,
                32,32, 1.0f);
            
            gravity.GravityForce = 0.001f;
            gravity.tileMap = tm;
            
            entity.AddComponent(gravity);
            
            spriteFont = this.Content.Load<SpriteFont>("SpriteFont1");

            entity.AssetName ="ball";
            eNode.Load(this.Content);

            JumpTo.Relative = true;
         
            entity.X = 32;
            entity.Y = 0;
            gravity.Initialise();
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

            tm.Update(gameTime);

            if (jumpTo != null)
                jumpTo.Update(gameTime);
             
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