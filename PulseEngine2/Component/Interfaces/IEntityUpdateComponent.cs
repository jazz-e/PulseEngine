using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PulseEngine.Component.Interfaces
{
    public interface IEntityUpdateComponent
    {
        void Update(GameTime gameTime);
    }
}
