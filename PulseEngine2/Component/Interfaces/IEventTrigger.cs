using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PulseEngine.Objects.Sprite;

namespace PulseEngine.Component.Interfaces
{
    public interface ITriggerComponent
    {
        void AttachTo(Entity entity);
        Entity GetOwner();
        void Initialise();
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
