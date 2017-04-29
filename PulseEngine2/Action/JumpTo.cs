using Microsoft.Xna.Framework;
using PulseEngine.Component.Interfaces;
using PulseEngine.Objects.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PulseEngine.Action
{
    public class Jump : IEntityComponent, IEntityUpdateComponent
     {
        Entity _parent;

        public float Force { get; set; }
        public bool hasJumped { get; set; }

        float _peak;

        public Jump()
        {
            hasJumped = false; 
        }

        public void Start()
        {
            if (!hasJumped)
            { 
                hasJumped = true;
                _peak = _parent.Position.Y - Force;

                if (_peak < 0) _peak = 0;
            }
        }

        public void Update(GameTime gameTime)
        {
            if (!hasJumped) return;

            if (hasJumped)
            {
                _parent.Velocity.Y -= 0.050f * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            if(_parent.Position.Y <= _peak )
            {
                hasJumped = false;
            }
        }

        public void AttachTo(Entity entity)
        {
            _parent = entity;
        }

        public Entity GetOwner()
        {
            return _parent;
        }
    }
}
