using PulseEngine.Component.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PulseEngine.Objects.Sprite;
using Microsoft.Xna.Framework;

namespace PulseEngine.Component.Collision
{
    public class CollisionEventArgs : EventArgs
    {
        
    }
    public class BBCollision : IEntityUpdateComponent
    {
        public EventHandler Overlap;
          
        Entity _parent;

        public BoundingBox OtherBox {get; set;}
        public Entity GetOwner()
        {
            return _parent;
        }
        public void AttachTo(Entity entity)
        {
            _parent = entity;    
        }
        public void Update(GameTime gameTime)
        {
            
        }
        protected virtual void OnOverlap(CollisionEventArgs e)
        {
            EventHandler handler = Overlap;
            if (handler != null)
            {
                handler(this, e);
            }
        }

    }
}
