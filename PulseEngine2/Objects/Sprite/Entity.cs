using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PulseEngine.Component.Interfaces;

namespace PulseEngine.Objects.Sprite
{
    public class Entity 
    {
        //Sprite Components 
        protected List<IEntityInitialiseComponent> EntityComponents
            = new List<IEntityInitialiseComponent>();
        protected List<IEntityUpdateComponent> EntityUpdateComponents
            = new List<IEntityUpdateComponent>();
        protected List<IEntityDrawComponent> EntityDrawComponents
            = new List<IEntityDrawComponent>();

        //List Properties 
        public List<IEntityInitialiseComponent> Components { get { return EntityComponents; } }
        public List<IEntityUpdateComponent> UpdateComponents { get { return EntityUpdateComponents; } }
        public List<IEntityDrawComponent> DrawComponents { get { return EntityDrawComponents; } }

        //Private Members - Attributes.

        protected Texture2D _image; //Reference to Image Asset
        protected Entity _parent;

        //Public Members - Properties
        protected Vector2 _position; 
        public Vector2 Position { get { return this._position; } set { this._position = value; SetXY(); } }

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
        public Vector2 Velocity; // { get; set; }

        protected void SetPosition ()
        {
            _position =
                new Vector2(this._x, this._y);
        }

        protected void SetXY()
        {
            this._x = this._position.X;
            this._y = this._position.Y;
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
            ZPlane = 1.0f;
        }
        public virtual void Initialise()
        {  }
        public virtual bool Load(ContentManager Content)
        {
            if (AssetName != null)
                try
                {
                    _image =
                        Content.Load<Texture2D>(AssetName);
                }
                catch
                {
                    //Log Error
                    return false;
                }

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
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            X += Velocity.X; // * delta;
            Y += Velocity.Y; //* delta;
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
       public void AddComponent(object entityComponent)
        {
            if(entityComponent is IEntityComponent)
            {
                ((IEntityComponent)entityComponent).AttachTo(this);
            }

            if (entityComponent is IEntityInitialiseComponent)
            {
                EntityComponents.
                    Add((IEntityInitialiseComponent)entityComponent);
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

        public virtual Entity Clone()
        {
            return (Entity)this.MemberwiseClone();
        }
    }
}
