﻿using PulseEngine.Component.Interfaces;
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
        public Entity CollidedWith { get; set; }
    }

    public delegate void OnBoxOverlap(object sender, CollisionEventArgs e);

    public class BBCollision : IEntityComponent, IEntityUpdateComponent
    {
        public event OnBoxOverlap Overlap;
          
        protected Entity _parent;

        public BoundingRectangle OtherBox {get; set;}
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
            if(_parent !=null)
            if(_parent.UpdateComponents.Count>0)
            foreach(IEntityUpdateComponent entity in _parent.UpdateComponents)
               if(entity is BoundingRectangle)
                    {
                        if(((BoundingRectangle)entity).Box.Intersects(OtherBox.Box))
                        {
                            CollisionEventArgs _args = new CollisionEventArgs();
                            _args.CollidedWith = OtherBox.GetOwner();
                        if (Overlap != null)
                            Overlap(this, _args);
                        }
                    }
        }
    }
}
