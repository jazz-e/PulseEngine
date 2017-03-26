using System;
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

        public SurfaceCollision surfaceCollision
            = new SurfaceCollision();

        public float dx, dy, speed;

        public override void Initialise()
        {
            screenCollision.LeavingScreen += ScreenCollision_LeavingScreen;
            surfaceCollision.Collision += SurfaceCollision_Collision;

            speed = 150.0f; dx = 1; dy = 1;

            //--- Attach all Components ---
            screenCollision.AttachTo(this);
            boundingBox.AttachTo(this);

            surfaceCollision.AttachTo(this);
            

            AddComponet(boundingBox);
            AddComponet(screenCollision);
            AddComponet(surfaceCollision);
            

            base.Initialise();
        }

        

        private void ScreenCollision_LeavingScreen(object sender, ScreenAreaArgs e)
        {
            if (e.LeftScreen == LeftBy.Right ||
                e.LeftScreen == LeftBy.Left)
                dx *= -1;

            if (e.LeftScreen == LeftBy.Top && dy == -1  ||
                e.LeftScreen == LeftBy.Bottom && dy ==1)
                dy *= -1;
            
        }

        public override void Update(GameTime gameTime)
        {
            surfaceCollision.Update(gameTime);

            if (top && dy == 1)
            {
                dy *= -1;
                top = false;
            }

            this.X+=dx * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.Y +=dy * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            base.Update(gameTime);
        }
        bool top = false;
        private void SurfaceCollision_Collision(object sender, SideCollisionArgs e)
        {
            if (e.ContactSide == Side.Top || e.ContactSide == Side.Bottom)
                top = true;
        }
    }
}
