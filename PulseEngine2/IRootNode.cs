using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PulseEngine
{
    interface IRootNode<T>
    {
       T GetOwner();
    }
}
