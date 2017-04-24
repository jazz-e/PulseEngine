using PulseEngine.Component.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PulseEngine.Objects.Sprite;
using Microsoft.Xna.Framework;
using PulseEngine.Display.World;
using PulseEngine.Component.Collision;

namespace PulseEngine.Component.Movement
{
    public class Gravity : IEntityComponent, IEntityInitialiseComponent, IEntityUpdateComponent
    {
        Entity _parent;

        public float GravityForce { get; set; }

        Vector2 _velocity;

        Side _collisionSide;

        public TileMap tileMap { get; set; }

        SurfaceCollision surfaceCollision;
        
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
            surfaceCollision = new SurfaceCollision();
            
            if (this.tileMap != null)
            {
                surfaceCollision.Entities = this.tileMap.Tiles;
                surfaceCollision.Collision += TileCollision;
               
                _parent.AddComponent(surfaceCollision);
            }
        }

        private void TileCollision(object sender, SideCollisionArgs e)
        {
            if (e.ContactSide == Side.Top)
            {
                _collisionSide = Side.Top;
                _parent.Y = 
                ((BoundingRectangle)e.ContactEntity.UpdateComponents[0]).Box.Top 
                - _parent.Height;
            }
        }

        private void Fall(GameTime gameTime)
        {
            if (_collisionSide != Side.Top)
            {
                _velocity.Y += this.GravityForce * gameTime.ElapsedGameTime.Milliseconds; //0.015f;
                _parent.Velocity +=
                    _velocity;
            }
        }
        
        private bool AboveGround()
        {
            if (_collisionSide == Side.None)
                return true;

            GravityForce = 0;
            return false;
        }

        public void Update(GameTime gameTime)
        {
            surfaceCollision.Penetration = 8;

            surfaceCollision.Update(gameTime);

            //Move Entity Down
            if (this.AboveGround())
                Fall(gameTime);
            else
                _parent.Velocity = Vector2.Zero;

            if (_collisionSide == Side.Top)
            { 
                _collisionSide = Side.None;
            }
        }
    }
}
