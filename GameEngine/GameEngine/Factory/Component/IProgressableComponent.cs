using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Factory.Component
{
    public interface IProgressableComponent
    {
        event EventHandler OnProgressMade;
        int ProgressPercent { get; }
    }
}
