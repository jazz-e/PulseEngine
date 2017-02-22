﻿using PulseEngine.Objects.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PulseEngine.Component.Interfaces
{
    interface IEntity
    {
        void AttachTo(Entity entity);
        Entity GetOwner();
    }
}