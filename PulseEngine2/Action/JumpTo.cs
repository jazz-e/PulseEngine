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
        public bool HasJumped { get; set; }

        float _peak;

        public Jump()
        {
            HasJumped = false; 
        }

        public void Start()
        {
            if (!HasJumped)
            { 
                HasJumped = true;
                _peak = _parent.Position.Y - Force;

                if (_peak < 0) _peak = 0;
            }
        }

        public void Update(GameTime gameTime)
        {
            if (!HasJumped) return;

            if (HasJumped)
            {
                _parent.Velocity.Y -= 0.050f * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            if(_parent.Position.Y <= _peak )
            {
                HasJumped = false;
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
