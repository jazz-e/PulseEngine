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
    public class TileCollisionArgs : EventArgs
    {
        public Side TileSide { get; set; }
    }

    public delegate void CollisionHandler(object sender, TileCollisionArgs e);

    public class TileCollision : IEntityComponent, IEntityUpdateComponent
    {
        Entity _parent;

        public List<Entity> Tiles =
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

        public void Initialise()
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            TileCollisionArgs _args = new TileCollisionArgs();
            _args.TileSide = Side.None;

            if (_parent != null)
                foreach (IEntityUpdateComponent entity in _parent.UpdateComponents)
                    if (entity is BoundingRectangle)
                    {
                       // ((BoundingRectangle)entity).Box.Left
                    }
        }
    }
}
