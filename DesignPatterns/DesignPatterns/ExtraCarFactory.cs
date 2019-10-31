using System;
using System.Collections.Generic;
using System.Text;
using DesignPatterns.AbstractFactory.CarParts;
using DesignPatterns.AbstractFactory.EquipmentTypes.EngineTypes;
using DesignPatterns.AbstractFactory.EquipmentTypes.WheelTypes;
using DesignPatterns.AbstractFactory.EquipmentTypes.SuspensionTypes;

namespace DesignPatterns.AbstractFactory
{
    public class ExtraCarFactory : ICarFactory
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
