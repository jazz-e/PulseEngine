using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PulseEngine.Display.World
{
    public class StaticBackground : IBackground
    {
        protected float x, y;

        protected string _assetName;
        public string assetName
        {
            get;set;
        }

        protected Texture2D _image;
       

        protected int width, height;
        public int screenWidth, screenHeight;

        public void LoadContent(ContentManager content)
        {
            _image = content.Load<Texture2D>(assetName);
            if(_image != null)
            { width = _image.Width; height = _image.Height; }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_image,
                new Rectangle((int)x, (int)y, width, height ),
                Color.White);
        }
    }
}
