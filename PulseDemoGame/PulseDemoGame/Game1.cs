using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using PulseEngine;
using PulseEngine.Component.Movement;
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

        EntityNode en = new EntityNode();

        Entity sprite;

        SpriteFont spriteFont;
        PlayerController pc = new PlayerController();

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
            sprite = new Entity();
           

            en.AttachNode(sprite);
            pc.AttachTo(sprite);
            


            pc.AddKeyBinding(Keys.A, MovementState.Left);
            pc.AddKeyBinding(Keys.D, MovementState.Right);
            pc.Pressed += Pc_Pressed;

            pc.Initialise();
            pc.Speed = 10.0f;
            sprite.AddComponet(pc);

            base.Initialize();
        }

        private void Pc_Pressed(object sender, PressedArgs e)
        {
           if(e.KState == MovementState.Right)
            {
                e.Attached.X+= e.Velocity;
            }

            if (e.KState == MovementState.Left)
                e.Attached.X-= e.Velocity;
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
            sprite.AssetName = "sprite1";
            en.Load(this.Content);           
            

            spriteFont = this.Content.Load<SpriteFont>("SpriteFont1");
           
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

            // TODO: Add your update logic here
            pc.Update(gameTime);

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

            en.Draw(spriteBatch);

            // ----------------------------------------------
            spriteBatch.DrawString(spriteFont, "FPS: " 
                + frameRate.ToString(), new Vector2(0, 0), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
