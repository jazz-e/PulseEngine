using PulseEngine.Component.Interfaces;
using PulseEngine.Objects.Sprite;
using Microsoft.Xna.Framework;

namespace PulseEngine.Action
{
    public class MoveTowards
    {
        static MoveTowards instance = null;

        bool _moving;
        float distance = 0f;
        Vector2 direction = Vector2.Zero;
        Vector2 start = Vector2.Zero, end = Vector2.Zero;
        Entity _AppliesTo;
        float _speed;

        public  MoveTowards (Entity AppliesTo, Vector2 destination, float speed, bool relative)
        {
            if(instance == null)
            { 
        
            _AppliesTo = AppliesTo;
            _speed = speed;

            start = AppliesTo.Position;

            if (relative)
                end = destination + start;
            else
                end = destination;

            direction = Vector2.Normalize(end - start);
            distance = Vector2.Distance(start, end);

            _moving = true;
           }

        }

        public static MoveTowards GetInstance(Entity AppliesTo, Vector2 destination, float speed, bool relative)
        {
            if(instance == null)
            {
                instance = new Action.MoveTowards(AppliesTo, destination, speed, relative);
            }

            return instance;
        }

        public void Update(GameTime gameTime)
        {
            if(distance != 0)
            _AppliesTo.Velocity= direction * _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

        }
    }
}
