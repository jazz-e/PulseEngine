using PulseEngine.Component.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PulseEngine.Objects.Sprite;

namespace PulseEngine.Component.Collision
{
    public class MapCollision : IEntityComponent, IEntityUpdateComponent
    {
        protected Entity _parent;

        List<Entity> _objects
            = new List<Entity>();

        public List<Entity> objectList
        {
            set { this._objects = value; }
            get { return _objects; }
        } 

        public void AttachTo(Entity entity)
        {
            this._parent = entity;
        }

        public Entity GetOwner()
        {
            return this._parent;
        }

        public void Initialise()
        {
            //Do Nothing
        }

        public void Update(GameTime gameTime)
        {
            BoundingRectangle br = 
                new BoundingRectangle();

            foreach(IEntityUpdateComponent entity in this._parent.UpdateComponents)
            {
                if (entity is BoundingRectangle)
                {
                    br = ((BoundingRectangle)entity);
                    break;
                }
            }

            foreach (Entity e in _objects)
            {
                
                
                foreach(IEntityUpdateComponent entity in e.UpdateComponents)
                {
                    if (entity is BoundingRectangle)
                    {
                        ((BoundingRectangle)entity).Update(gameTime);

                       if(((BoundingRectangle)entity).Box.Intersects(br.Box))
                            System.Diagnostics.Debug.Write("Worked!" + e.AssetName + "\n");
                    }
                }
            }
        }
    }
}
