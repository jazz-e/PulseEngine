using PulseEngine.Component.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PulseEngine.Objects.Sprite;
using Microsoft.Xna.Framework;

namespace PulseEngine.Component.Movement
{
    public enum Direction {None, Up, UpRight, Right, DownRight, Down, DownLeft, Left, UpLeft};

    public class MoveFixedDirection : IEntityComponent, IEntityInitialiseComponent
    {
        Entity _parent;
        Random _rnd;
        int _choice;

        public int XSpeed
        { get; set; }

        public int YSpeed
        { get; set; }

        public Vector2 DirectionVector { get; set; }

        List<Direction> SelectDirection
            = new List<Direction>(9); 

        public void AttachTo(Entity entity)
        {
            _parent = entity;
        }

        public Entity GetOwner()
        {
            return _parent;
        }
        
        public MoveFixedDirection()
        {
            _choice = 0;
            _rnd = new Random(DateTime.Now.Second);
        }

        public void AddDirection(Direction direction)
        {
            SelectDirection.Add(direction);
            _choice = _rnd.Next(0, 9);
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Initialise()
        {
            if (SelectDirection.Count <= 0) return;

            if (_choice >= SelectDirection.Count)
            {
                _choice = _rnd.Next(0, SelectDirection.Count-1);
            }

            Direction d = SelectDirection[_choice];

            switch(d)
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

            
         _parent.Velocity = DirectionVector * new Vector2(XSpeed, YSpeed);
        }
    }
}
