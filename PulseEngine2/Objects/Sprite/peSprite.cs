using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PulseEngine2.Objects.Sprite
{
    public class peSprite
    {
        //Private Members - Attributes.

        Texture2D _image; //Reference to Image Asset
        ContentManager _content; //Local copy of Game Content Manager

        //Public Members - Properties 
        public Vector2 Position { get; set; }
        public float RotationAngle { get; set; }
        public float Scale { get; set; }
        public bool Visible { get; set; }
        public Vector2 Origin { get; set; }
        public float ZPlane { get; set; }


        //Public Members - Methods 
        public peSprite(ContentManager content)
        {
            _content = content;
            Visible = true; 
        }
        public virtual bool Load(string assetName)
        {
            _image = 
                _content.Load<Texture2D>(assetName);

            if (_image != null)
                return true;

            return false; 
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
                spriteBatch.Draw(_image, Position, null, Color.White, RotationAngle, Origin, Scale, SpriteEffects.None, ZPlane);
        }
    }
}
