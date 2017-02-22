using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PulseEngine.Component.Interfaces;
using PulseEngine.Objects.Sprite;

namespace PulseEngine
{
    public class RootNode<T> : IRootNode<T>
    {
        T _parent;

        protected List<T> nodes =
            new List<T>();
        
        #region "RootNode Methods"
        public RootNode()
            {}

        public void AttachNode(T node)
        {
            nodes.Add (node);
        }
        public RootNode(T node)
        {
            this._parent = node;
        }
        public T GetOwner()
        {
            return this._parent;
        }
        #endregion

        #region "Virtual MethodS"
        public virtual void Initialise() { }
        public virtual void Load(ContentManager content) {}
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(SpriteBatch spriteBatch) { }
        #endregion
    }

    public class EntityNode : RootNode<Entity>
    {
         
        #region "MAIN METHODS"
        public EntityNode() :
            base ()
        { }
        public EntityNode(Entity entity) : 
            base (entity)
        { }
        public override void Initialise()
        {
            foreach(Entity entity in nodes)
            {
                entity.Initialise();
               
                if (entity.Components.Count > 0)
                    foreach (IEntityComponent e in entity.Components)
                        e.Initialise();
            }


            base.Initialise();
        }
        public override void Load(ContentManager content)
        {
           foreach( Entity e in nodes)
            {
                e.Load(content);
            }
        }
        public override void Update(GameTime gameTime)
        {
            foreach (Entity entity in nodes)
            {
                entity.Update(gameTime);

                if (entity.UpdateComponents.Count > 0)
                    foreach (IEntityUpdateComponent e in entity.UpdateComponents)
                        e.Update(gameTime);
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach(Entity entity in nodes)
            {
                entity.Draw(spriteBatch);

                if (entity.DrawComponents.Count > 0)
                    foreach (IEntityDrawComponent e in entity.DrawComponents)
                        e.Draw(spriteBatch);
            }
        }
        #endregion


    }
}
