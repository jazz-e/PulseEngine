using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

            int startx = (int)x;
            int starty = (int)y; 

            for (int cy = (int)y; cy <= rows ; cy += this.height )
                for (int cx = (int)x; cx <= columns; cx += this.width )
                    spriteBatch.Draw(_image, new Vector2(cx, cy), Color.White);

        }
    }
}
