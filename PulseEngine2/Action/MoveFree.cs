using System;
using PulseEngine.Objects.Sprite;
using Microsoft.Xna.Framework;

namespace PulseEngine.Action
{
    public class MoveFree 
    {
        static MoveFree instance = null;

        Entity _AppliesTo; 

        float _x, _y, _angle = 0;
        public static MoveFree GetInstance(Entity AppliesTo, float angle, float speed)
        {
            if (instance == null)
                instance = new MoveFree(AppliesTo, angle, speed);

            return instance; 
        }

        public MoveFree(Entity AppliesTo, float angle, float speed )
        {
            _AppliesTo = AppliesTo;
                            
            _angle = (angle + 270) * (float)Math.PI /180;

            _x = speed * (float)Math.Cos(_angle);
            _y = speed * (float)Math.Sin(_angle);
        }

        public void Update(GameTime gameTime)
        {
            _AppliesTo.Velocity = new Vector2(_x, _y) * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

    }
}
