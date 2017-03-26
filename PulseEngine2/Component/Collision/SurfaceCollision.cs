using PulseEngine.Component.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PulseEngine.Objects.Sprite;
using Microsoft.Xna.Framework;

namespace PulseEngine.Component.Collision
{
    public enum Side { None, Left, Right, Top, Bottom }
    public class SideCollisionArgs : EventArgs
    {
        public Side ContactSide { get; set; }
        public Entity ContactEntity;
    }

    public delegate void CollisionHandler(object sender, SideCollisionArgs e);

    public class SurfaceCollision : IEntityComponent, IEntityUpdateComponent
    {
        Entity _parent;

        public List<Entity> Entities =
            new List<Entity>();

        public event CollisionHandler Collision;
        public event CollisionHandler CollisionFree;

        public void AttachTo(Entity entity)
        {
            _parent = entity;
        }

        public Entity GetOwner()
        {
            return _parent;
        }

        public int Penetration
        { get; set; }

        public void Initialise()
        {
            //throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            int _x = 0, _y = 0, _width = 0, _height = 0;
            int this_x = 0, this_y = 0, this_width = 0, this_height = 0;

            SideCollisionArgs _args = new SideCollisionArgs();
            _args.ContactSide = Side.None;

            if (_parent != null)
                foreach (Entity ListEntity in Entities)
                {
                    foreach (IEntityUpdateComponent _listEntity in ListEntity.UpdateComponents)
                        if (_listEntity is BoundingRectangle && !ListEntity.AssetName.Contains("_Blank_"))
                        {
                            _x = ((BoundingRectangle)_listEntity).Box.X;
                            _y = ((BoundingRectangle)_listEntity).Box.Y;
                            _width = ((BoundingRectangle)_listEntity).Box.Width;
                            _height = ((BoundingRectangle)_listEntity).Box.Height;
                        }

                   // if (_width <= 0 && _height <= 0) return;

                    Rectangle BBOtherEntity =
                        new Rectangle(_x, _y, _width, _height);

                    foreach (IEntityUpdateComponent entity in _parent.UpdateComponents)
                        if (entity is BoundingRectangle)
                        {
                            this_x = ((BoundingRectangle)entity).Box.X;
                            this_y = ((BoundingRectangle)entity).Box.Y;
                            this_width = ((BoundingRectangle)entity).Box.Width;
                            this_height = ((BoundingRectangle)entity).Box.Height;
                        }

                    Rectangle BBEntity =
                        new Rectangle(this_x, this_y, this_width, this_height);

                    Rectangle temp;

                    // Is there a collision?
                    if(BBEntity.Intersects(BBOtherEntity))
                    {
                        // Left Side Collision 
                        temp =
                             new Rectangle(BBOtherEntity.Left - Penetration, 
                             BBOtherEntity.Top + Penetration,
                             Penetration, BBOtherEntity.Bottom - Penetration);

                        if(BBEntity.Intersects(temp))
                        {
                            _args.ContactSide = Side.Left;
                            _args.ContactEntity = ListEntity;
                            Collision(this, _args);
                        }

                        //Right Side Collision 
                        temp =
                             new Rectangle(BBOtherEntity.Right + Penetration, 
                             BBOtherEntity.Top + Penetration,
                             Penetration, BBOtherEntity.Bottom - Penetration);

                        if (BBEntity.Intersects(temp))
                        {
                            _args.ContactSide = Side.Right;
                            _args.ContactEntity = ListEntity;
                            Collision(this, _args);
                        }

                        //Top Side Collision
                        temp =
                             new Rectangle(BBOtherEntity.Left, 
                             BBOtherEntity.Top - Penetration,
                             BBOtherEntity.Width, Penetration);

                        if (BBEntity.Intersects(temp))
                        {
                            _args.ContactSide = Side.Top;
                            _args.ContactEntity = ListEntity;
                            Collision(this, _args);
                        }

                        //Bottom Side Collision
                        temp =
                             new Rectangle(BBOtherEntity.Left,
                             BBOtherEntity.Bottom - Penetration,
                             BBOtherEntity.Width, Penetration);

                        if (BBEntity.Intersects(temp))
                        {
                            _args.ContactSide = Side.Bottom;
                            _args.ContactEntity = ListEntity;
                            Collision(this, _args);
                        }
                    }

                    
                }
        }
    }
}
