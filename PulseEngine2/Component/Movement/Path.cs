using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PulseEngine.Component.Interfaces;
using PulseEngine.Objects.Sprite;

namespace PulseEngine.Component.Movement
{
    public enum PathStatus { Forward, Reverse, Stop}
    
    public class FollowPath : IEntityComponent, IEntityInitialiseComponent, IEntityUpdateComponent
    {
        Entity _parent;
        int _index = 0;

        public bool Loop
        { get; set; }

        public float Speed
        { get; set; }

        public Vector2 Target
        { get; set; }
    
        public PathStatus Status
        { get; set;}
       
        public Vector2[] Path;

        void NextStep(GameTime gameTime)
        {
            //If looping through array 
            if (Loop)
            {
                if (Status == PathStatus.Forward && _index == this.Path.Length - 1)
                { _index = 0; Target = this.Path[_index]; return; }

                if (Status == PathStatus.Reverse && _index == 0)
                { _index = this.Path.Length - 1; Target = this.Path[_index]; return; }
            }

            //If moving forward through array
            if (_index < this.Path.Length - 1 && Status == PathStatus.Forward) _index++;
            //It move backward through array 
            if (_index > 0 && Status == PathStatus.Reverse) _index--;

            Target = this.Path[_index];
        }

        public void Step(GameTime gameTime)
        {
            float _delta = Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            Vector2 step =
                StepTowards.Step(_delta,
                _parent.Position, Target);
            
             _parent.Position +=
                new Vector2((int)step.X, (int)step.Y);

            //If Entity reach Target then look for Next Target 

            if (Math.Ceiling(_parent.X + (int)_delta) >= Target.X && 
                Math.Floor(_parent.X - (int)_delta) <= Target.X)
            {
                if (Math.Ceiling(_parent.Y +_delta) >= Target.Y && 
                    Math.Floor(_parent.Y  - _delta) <= Target.Y)
                {
                    NextStep(gameTime);
                }
            }
                // --------------------------------------------------    
        }

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
            Step(gameTime);       
        }

        public void Initialise()
        {
            Target = Path[_index];
        }
    }

    public class FindPath : IEntityComponent, IEntityUpdateComponent
    {
        Entity _parent;

        public float Speed
        { get; set; }

        public Vector2 Target
        { get; set; }

        public PathStatus Status
        { get; set; }

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
            
        }
    }
    
    public static class StepTowards
    {
        public static Vector2 Step(float delta, Vector2 current, Vector2 target)
        {
            float x = target.X - current.X; if (x > -1 && x < 0) x = 0;
            float y = target.Y - current.Y; if (y > -1 && y < 0) y = 0;

            if (x == 0 && y == 0) return Vector2.Zero; //Reached Target

            //Move Towards the Target in vector Direction
            if (x < 0) x = -1;
            if (x > 0) x = 1;
            if (y < 0) y = -1;
            if (y > 0) y = 1;

            x = x * delta;
            y = y * delta;

            return new Vector2(x, y);
        }
    }
}
