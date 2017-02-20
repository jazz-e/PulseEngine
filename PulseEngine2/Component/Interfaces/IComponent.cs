using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PulseEngine2.Component
{
    public interface IComponent<T>
    {
       T GetOwner();

        void Initialise(T sender);
    }
}
