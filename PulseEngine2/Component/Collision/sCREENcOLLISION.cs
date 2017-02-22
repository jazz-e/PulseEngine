using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PulseEngine.Component.Interfaces;
using PulseEngine.Objects.Sprite;

namespace PulseEngine.Component.Collision
{
    public enum LeftBy {None, Left, Right, Top, Bottom }
    public class ScreenAreaArgs : EventArgs
    {
        public LeftBy LeftScreen { get; set; }
    }

    public delegate void InScreenArea(object sender, ScreenAreaArgs e);
    public delegate void OutScreenArea(object sender, ScreenAreaArgs e);
    public delegate void LeaveScreenArea(object sender, ScreenAreaArgs e);

    public class ScreenCollision : IEntityComponent, IEntityUpdateComponent
    {
        public event InScreenArea OnScreen;
        public event OutScreenArea OffScreen;
        public event LeaveScreenArea LeavingScreen;

        Entity _parent;

        public Rectangle Boundary { get; set; }

        public ScreenCollision(int X, int Y, int Width, int Height)
        {
            Boundary = new Rectangle(X, Y, Width, Height);
        }

        public void AttachTo(Entity entity)
        {
            _parent = entity;
        }

        public Entity GetOwner()
        {
            return _parent;
        }

        public void Initialise()
        {
            //Do nothing 
        }

        public void Update(GameTime gameTime)
        {
            ScreenAreaArgs _args = new Collision.ScreenAreaArgs();
            _args.LeftScreen = LeftBy.None;

            if(_parent !=null)
                foreach (IEntityUpdateComponent entity in _parent.UpdateComponents)
                    if(entity is BoundingBox)
                    {
                        if (((BoundingBox)entity).Box.Left < this.Boundary.Left)
                            _args.LeftScreen = LeftBy.Left;
                        if (((BoundingBox)entity).Box.Right > this.Boundary.Right)
                            _args.LeftScreen = LeftBy.Right;
                        if (((BoundingBox)entity).Box.Top < this.Boundary.Top)
                            _args.LeftScreen = LeftBy.Top;
                        if (((BoundingBox)entity).Box.Bottom > this.Boundary.Bottom)
                            _args.LeftScreen = LeftBy.Bottom;

                        if (((BoundingBox)entity).Box.Intersects(this.Boundary) &&
                            _args.LeftScreen == LeftBy.None)
                        if (OnScreen != null)
                            OnScreen(this, _args);

                        if (((BoundingBox)entity).Box.Intersects(this.Boundary) &&
                            _args.LeftScreen != LeftBy.None)
                            if (LeavingScreen != null)
                                LeavingScreen(this, _args);

                        if (!((BoundingBox)entity).Box.Intersects(this.Boundary))
                            if (OffScreen != null)
                                OffScreen(this, _args);
                    }
        }
    }
}
