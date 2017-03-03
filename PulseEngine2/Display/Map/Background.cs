using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PulseEngine.Display.Map
{
    public class Background : StaticBackground
    {
        
        public bool TileHorizontal
        {
            get;set;
        } 
        public bool TileVertical
        {
            get;set;
        }
        public float X
        {
            get { return this.x; }
            set { this.x = value; }
        }
        public float Y
        {
            get { return this.y; }
            set { this.y = value; }
        }

        public float HorizontalSpeed, VerticalSpeed;

        public Background(int screenWidth, int screenHeight) : 
            base(screenWidth, screenHeight)
        {
        }

        public void Update(GameTime gameTime)
        {
            X +=  HorizontalSpeed * gameTime.ElapsedGameTime.Milliseconds;
            y +=  VerticalSpeed * gameTime.ElapsedGameTime.Milliseconds; 
        }
    }
}
