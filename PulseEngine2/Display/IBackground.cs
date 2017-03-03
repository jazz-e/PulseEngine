using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PulseEngine.Display
{
    interface IBackground
    {
        string assetName
        {
            get;set;
        }

        Texture2D Image
        {
            get;set;
        }
        
        void LoadContent(ContentManager content);
        void Draw(SpriteBatch spriteBatch);
    }
}
