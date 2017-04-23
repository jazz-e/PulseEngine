using Microsoft.Xna.Framework;
using PulseEngine.Objects.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PulseEngine.Action
{
    public class JumpTo
    {
        static JumpTo instance = null;
        Entity _AppliesTo;

        public static bool Relative { get; set; }
        public Vector2 Peak;
        public bool hasJumped;

        public static JumpTo GetInstance(Entity AppliesTo, Vector2 position)
        {
            if (instance == null)
                instance = new JumpTo(AppliesTo, position);

            return instance;
        }

        public JumpTo(Entity AppliesTo, Vector2 position)
        {
            _AppliesTo = AppliesTo;

            if (Relative)
               Peak = _AppliesTo.Position + position; 

            if (!Relative)
               _AppliesTo.Position = position; 
            
            hasJumped = true; 
        }

        public void Update(GameTime gameTime)
        {
            if (_AppliesTo.Position.Y > Peak.Y && hasJumped)
            {
                _AppliesTo.Y -= 1f * gameTime.ElapsedGameTime.Seconds;
            }
            else
                hasJumped = false;
        }
    }
}
