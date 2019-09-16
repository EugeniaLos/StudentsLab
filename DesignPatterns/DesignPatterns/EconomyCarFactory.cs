using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactory
{
    class EconomyCarFactory: CarFactory
    {
        public IEngine GetEngine()
        {
            return new EconomyEngine();
        }

        public IWheel GetWheel()
        {
            return new EconomyWheel();
        }

        public ISuspension GetSuspension()
        {
            return new EconomySuspension();
        }
    }
}
