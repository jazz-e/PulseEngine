using Microsoft.Xna.Framework;
using PulseEngine.Component.Interfaces;
using PulseEngine.Objects.Sprite;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace PulseEngine.Component.Instance
{
    public class Spawn<T> : ITriggerComponent where T : Entity
    {
        Entity _parent;
        public T SpawnType { get; set; }
        public bool Relative { get; set; }

        public List<T> spawnList =
            new List<T>();

        public Vector2 Position { get; set; }

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
            T _temp = null;
            if((T)SpawnType != null)
            _temp =(T)SpawnType.Clone();

            if (Relative)
            {
                //Relative
                if (_parent != null)
                {
                    _temp.X = _parent.Position.X + this.Position.X;
                    _temp.Y = _parent.Position.Y + this.Position.Y;
                }
                else
                {
                    _temp.X += this.Position.X;
                    _temp.Y += this.Position.Y;
                }
            }
            else
            {
                //Absolute
                _temp.X = this.Position.X;
                _temp.Y = this.Position.Y;
            }
            
            spawnList.Add(_temp);
        }
        
        public void Update(GameTime gameTime)
        {
            foreach (T t in spawnList)
                t.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (T t in spawnList)
                t.Draw(spriteBatch);
        }
        
    }
}
