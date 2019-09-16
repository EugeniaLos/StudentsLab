using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactory
{
    class StandardCarFactory
    {
        public IEngine GetEngine()
        {
            return new StandardEngine();
        }

        public IWheel GetWheel()
        {
            return new StandardWheel();
        }

        public ISuspension GetSuspension()
        {
            return new StandardSuspension();
        }
    }
}
