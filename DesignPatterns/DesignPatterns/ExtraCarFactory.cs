using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactory
{
    class ExtraCarFactory: CarFactory
    {
        public IEngine GetEngine()
        {
            return new ExtraEngine();
        }

        public IWheel GetWheel()
        {
            return new ExtraWheel();
        }

        public ISuspension GetSuspension()
        {
            return new ExtraSuspension();
        }
    }
}
