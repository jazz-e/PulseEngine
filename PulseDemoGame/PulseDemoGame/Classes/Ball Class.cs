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
            

            AddComponent(boundingBox);
            AddComponent(screenCollision);
            AddComponent(surfaceCollision);
            

            base.Initialise();
        }

        

        private void ScreenCollision_LeavingScreen(object sender, ScreenAreaArgs e)
        {
            foreach(LeftBy lb in e.LeftScreen)
            if (lb == LeftBy.Right ||
                lb == LeftBy.Left)
                dx *= -1;
            foreach(LeftBy lb in e.LeftScreen)
            if (lb == LeftBy.Top && dy == -1  ||
                lb == LeftBy.Bottom && dy ==1)
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
