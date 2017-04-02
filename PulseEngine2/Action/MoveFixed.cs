using System;
using PulseEngine.Objects.Sprite;
using Microsoft.Xna.Framework;

namespace PulseEngine.Action
{
    public enum Direction { None, Up, UpRight, Right, DownRight, Down, DownLeft, Left, UpLeft };

    public class MoveFixed
    {
        static MoveFixed instance = null;

        Entity _AppliesTo;
        float _speed;

        Vector2 DirectionVector { get; set; }
        int _choice;

        public static MoveFixed GetInstance(Entity AppliesTo, float speed, params Direction[] SelectDirection)
        {
            if (instance == null)
                instance = new MoveFixed(AppliesTo, speed, SelectDirection);

            return instance;
        }

        public MoveFixed(Entity AppliesTo, float speed, params Direction[] SelectDirection)
        {
            _AppliesTo = AppliesTo;
            _speed = speed;

            Random _rnd = new Random(DateTime.Now.Millisecond);

            if (SelectDirection.GetLength(0) <= 0) return;

            _choice = _rnd.Next(0, SelectDirection.GetLength(0));

            Direction d = SelectDirection[_choice];

            switch (d)
            {
                case Direction.Down:
                    DirectionVector = new Vector2(0, 1);
                    break;
                case Direction.DownLeft:
                    DirectionVector = new Vector2(-1, 1);
                    break;
                case Direction.DownRight:
                    DirectionVector = new Vector2(1, 1);
                    break;
                case Direction.Left:
                    DirectionVector = new Vector2(-1, 0);
                    break;
                case Direction.None:
                    DirectionVector = new Vector2(0, 0);
                    break;
                case Direction.Right:
                    DirectionVector = new Vector2(1, 0);
                    break;
                case Direction.Up:
                    DirectionVector = new Vector2(0, -1);
                    break;
                case Direction.UpLeft:
                    DirectionVector = new Vector2(-1, -1);
                    break;
                case Direction.UpRight:
                    DirectionVector = new Vector2(1, -1);
                    break;
            }
        }
        public void Update(GameTime gameTime)
        {
            _AppliesTo.Velocity = DirectionVector * _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }


    }
}
