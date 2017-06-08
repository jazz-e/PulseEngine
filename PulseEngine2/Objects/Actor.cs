using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Content;

using PulseEngine.Objects.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PulseEngine.Objects
{
    public class Actor 
    {
        private EntityNode actorNode;
        public Entity actor;

        public Actor()
        {
            actor =
                new Entity();

            actorNode = new EntityNode(actor);

        }

        public void Load(ContentManager Content, string assetName)
        {
            this.actor.AssetName = assetName;
            this.actorNode.Load(Content);
        }

        public void Update(GameTime gameTime)
        {
            this.actorNode.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.actorNode.Draw(spriteBatch);
        }

    }
}
