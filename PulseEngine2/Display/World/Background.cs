using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PulseEngine.Display.World
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
        
        public void Update(GameTime gameTime)
        {
            X +=  HorizontalSpeed * gameTime.ElapsedGameTime.Milliseconds;
            y +=  VerticalSpeed * gameTime.ElapsedGameTime.Milliseconds; 
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            int repeatColumn =0, repeatRow =0;

            if(TileHorizontal)
            {
                repeatColumn = screenWidth / this.width;
            }

            if(TileVertical)
            {
                repeatRow = screenHeight / this.height;
            }

            int rows = repeatRow * this.height;
            int columns = repeatColumn * this.width;

            int startx = (int)x % width;
            int starty = (int)y % height;

            int offsetx=0, offsety=0;

            if (HorizontalSpeed != 0 && TileHorizontal) offsetx = this.width;
            if (VerticalSpeed != 0 && TileVertical) offsety = this.height; 
            
            for (int cy = starty-offsety; cy <= rows+offsety ; cy += this.height )
                for (int cx = startx-offsetx; cx <= columns + offsetx; cx += this.width )
                    spriteBatch.Draw(_image, new Vector2(cx, cy), Color.White);

        }
    }
}
