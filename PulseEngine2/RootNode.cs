using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PulseEngine2
{
    public class RootNode<T>
    {
        private T _parent;

        //Events 
        public delegate void UpdateComponent(GameTime gameTime); //Calls the attached Component
        public delegate void InitialiseComponent();
        public delegate void DrawComponent(SpriteBatch spriteBatch);
        public event InitialiseComponent Begin;
        public event UpdateComponent Tick;
        public event DrawComponent Render;

        public void Initialise()
        {
            if (Begin != null) Begin();
        }
        public void Update(GameTime gameTime)
        {
            if (Tick != null)
                Tick(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Render != null) Render(spriteBatch);
        }

        public virtual void AttachTo(T obj)
        { _parent = obj; }
        public virtual T GetOwner()
        { return _parent; }
    }
}
