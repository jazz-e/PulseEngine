﻿using System;
using Microsoft.Xna.Framework;
using PulseEngine.Component.Collision;
using PulseEngine.Objects.Sprite;

namespace PulseDemoGame.Classes
{
    public class Ball_Class : Entity 
    {

        public ScreenCollision screenCollision;
        BoundingRectangle boundingBox 
            = new BoundingRectangle();
        float dx, dy, speed;

        public override void Initialise()
        {
            screenCollision.LeavingScreen += ScreenCollision_LeavingScreen;
            speed = 600.0f; dx = 1; dy = 1;

            //--- Attach all Components ---
            screenCollision.AttachTo(this);
            boundingBox.AttachTo(this);
            AddComponet(boundingBox);
            AddComponet(screenCollision);
            

            base.Initialise();
        }

        private void ScreenCollision_LeavingScreen(object sender, ScreenAreaArgs e)
        {
            if (e.LeftScreen == LeftBy.Right ||
                e.LeftScreen == LeftBy.Left)
                dx *= -1;

            if (e.LeftScreen == LeftBy.Top ||
                e.LeftScreen == LeftBy.Bottom)
                dy *= -1;
        }

        public override void Update(GameTime gameTime)
        {
            this.X+=dx * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.Y +=dy * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            base.Update(gameTime);
        }
    }
}