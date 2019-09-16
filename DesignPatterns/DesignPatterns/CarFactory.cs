using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactory
{
    interface CarFactory
    {
        IEngine GetEngine();

        IWheel GetWheel();

        ISuspension GetSuspension();
    }
}
