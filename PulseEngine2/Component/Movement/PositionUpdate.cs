using PulseEngine.Component.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PulseEngine.Objects.Sprite;
using Microsoft.Xna.Framework;

namespace PulseEngine.Component.Movement
{
    public class PositionUpdate : IEntityComponent, IEntityUpdateComponent
    {
        Entity _parent;

        public Vector2 Velocity { get; set; }

        public void AttachTo(Entity entity)
        {
            _parent = entity;
        }

        public Entity GetOwner()
        {
            return _parent;
        }

        public void Update(GameTime gameTime)
        {
            _parent.Position +=
                Velocity * gameTime.ElapsedGameTime.Seconds;
        }
    }
}
