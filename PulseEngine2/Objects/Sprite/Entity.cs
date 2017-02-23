﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PulseEngine.Component;
using PulseEngine.Component.Interfaces;

namespace PulseEngine.Objects.Sprite
{
    public class Entity 
    {
        //Sprite Components 
        protected List<IEntityComponent> EntityComponents
            = new List<IEntityComponent>();
        protected List<IEntityUpdateComponent> EntityUpdateComponents
            = new List<IEntityUpdateComponent>();
        protected List<IEntityDrawComponent> EntityDrawComponents
            = new List<IEntityDrawComponent>();

        //List Properties 
        public List<IEntityComponent> Components { get { return EntityComponents; } }
        public List<IEntityUpdateComponent> UpdateComponents { get { return EntityUpdateComponents; } }
        public List<IEntityDrawComponent> DrawComponents { get { return EntityDrawComponents; } }

        //Private Members - Attributes.

        protected Texture2D _image; //Reference to Image Asset
        protected Entity _parent;

        //Public Members - Properties 
        public Vector2 Position { get; set; }

        public float RotationAngle { get; set; }
        protected float _scale;
        public float Scale { get {return _scale; } set {_scale=value; SetScale(); } }
        public bool Visible { get; set; }
        public Vector2 Origin { get; set; }
        public float ZPlane { get; set; }
        public string AssetName { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        protected float _x, _y;
        public float X { get { return _x; } set { _x = value; SetPosition(); } }
        public float Y { get { return _y; } set { _y = value; SetPosition(); } }

        protected void SetPosition ()
        {
            Position =
                new Vector2(this._x, this._y);
        }
        protected void SetScale()
        {
            if (_image != null)
            {
                Width = (int)((float)_image.Width * _scale);
                Height = (int)((float)_image.Height * _scale);
            }
        }

        //Public Members - Methods 
        public Entity()
        {
            Visible = true;
            Scale = 1.0f;
        }
        public virtual void Initialise()
        {  }
        public virtual bool Load(ContentManager Content)
        {
            if(AssetName != null)
            _image = 
                Content.Load<Texture2D>(AssetName);

            if (_image != null)
            {
                Width =(int)((float) _image.Width * Scale);
                Height =(int)((float) _image.Height * Scale);
               
                return true;
            }

            return false; 
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
                if(_image!= null)
                spriteBatch.Draw(_image, 
                    Position, null, Color.White, 
                    RotationAngle, Origin, Scale, 
                    SpriteEffects.None, ZPlane);
        }
        public virtual void Update(GameTime gameTime)
        {
            //Must be Updated Every Iteration
        }

        //Parenting 
        public void AttachTo(Entity entity)
        {
            _parent = entity;
        }
        public Entity GetOwner()
        {
            return _parent;
        }


        //Component Methods 
       public void AddComponet(object entityComponent)
        {
            if (entityComponent is IEntityComponent)
            {
                EntityComponents.
                    Add((IEntityComponent)entityComponent);
            }

            if (entityComponent is IEntityUpdateComponent)
            {
                EntityUpdateComponents.
                    Add((IEntityUpdateComponent)entityComponent);
            }

            if (entityComponent is IEntityDrawComponent)
            {
                EntityDrawComponents.
                    Add((IEntityDrawComponent)entityComponent);
            }
        }

    }
}
