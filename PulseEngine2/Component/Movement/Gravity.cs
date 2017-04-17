﻿using PulseEngine.Component.Interfaces;
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

        int _dx;
        bool _ground;

        public float Force { get; set; }

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

            _dx = 0;

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
                _velocity.Y += this.Force * 0.015f;
                _parent.Velocity +=
                    _velocity;
            }
        }

        private void Push(GameTime gameTime)
        {

        }

        private bool Collision()
        {
            return false;
        }

        private bool AboveGround()
        {
            if (_collisionSide != Side.Top)
                return true;

            return false;
        }

        public void Update(GameTime gameTime)
        {
            surfaceCollision.Penetration = 3;

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
