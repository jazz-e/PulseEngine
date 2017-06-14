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
        public List<LeftBy> LeftScreen { get; set; }
        public Entity Attached { get; set; }
    }

    public delegate void InScreenArea(object sender, ScreenAreaArgs e);
    public delegate void OutScreenArea(object sender, ScreenAreaArgs e);
    public delegate void LeaveScreenArea(object sender, ScreenAreaArgs e);

    public class ScreenCollision : IEntityComponent, IEntityUpdateComponent
    {
        public event InScreenArea OnScreen;
        public event OutScreenArea OffScreen;
        public event LeaveScreenArea LeavingScreen;

        protected Entity _parent;

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
        
        public void Update(GameTime gameTime)
        {
            ScreenAreaArgs _args = new Collision.ScreenAreaArgs()
            {
                LeftScreen = new List<LeftBy>(),

                Attached = this._parent
            };
            if (_parent !=null)
                foreach (IEntityUpdateComponent entity in _parent.UpdateComponents)
                    if(entity is BoundingRectangle)
                    {
                        if (((BoundingRectangle)entity).Box.Left < this.Boundary.Left)
                            _args.LeftScreen.Add( LeftBy.Left);
                        if (((BoundingRectangle)entity).Box.Right > this.Boundary.Right)
                            _args.LeftScreen.Add(LeftBy.Right);
                        if (((BoundingRectangle)entity).Box.Top < this.Boundary.Top)
                            _args.LeftScreen.Add(LeftBy.Top);
                        if (((BoundingRectangle)entity).Box.Bottom > this.Boundary.Bottom)
                            _args.LeftScreen.Add(LeftBy.Bottom);

                        if (_args.LeftScreen.Count > 0)
                        if (((BoundingRectangle)entity).Box.Intersects(this.Boundary))
                            {
                                _args.LeftScreen.Add(LeftBy.None);

                                OnScreen?.Invoke(this, _args);
                            }

                        if (_args.LeftScreen.Count > 0)
                            if (((BoundingRectangle)entity).Box.Intersects(this.Boundary) &&
                            _args.LeftScreen[0] != LeftBy.None)
                            if (LeavingScreen != null)
                                LeavingScreen(this, _args);

                        if (!((BoundingRectangle)entity).Box.Intersects(this.Boundary))
                            if (OffScreen != null)
                                OffScreen(this, _args);
                    }
        }
    }
}
