using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PulseEngine.Objects.Sprite;
using PulseEngine.Component.Interfaces;

namespace PulseEngine.Component.Collision
{
    public class BoundingBox : IEntity, IEntityUpdateComponent
    {
        public int OffsetLeft { get; set; }
        public int OffsetRight { get; set; }
        public int OffsetTop { get; set; }
        public int OffsetBottom { get; set; }

        public Rectangle Box;

        Entity _parent;

        public Entity GetOwner()
        {
            return _parent;
        } 
        public void AttachTo(Entity actor)
        {
            _parent = actor;
        }
        public void Update (GameTime gameTime)
        {
            Box =
                new Rectangle((int)_parent.Position.X + OffsetLeft,
               (int)_parent.Position.Y + OffsetTop,
                _parent.Width + OffsetRight,
                _parent.Height + OffsetBottom); 
        }

    }
}
