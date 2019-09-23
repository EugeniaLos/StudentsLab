using System;
using System.Collections.Generic;
using System.Text;
using DesignPatterns.AbstractFactory.CarParts;

namespace DesignPatterns.AbstractFactory
{
    public interface ICarFactory
    {
        IEngine GetEngine();

        IWheel GetWheel();

        ISuspension GetSuspension();
    }
}
