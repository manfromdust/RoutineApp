using System;
using System.Collections.Generic;
using System.Text;

namespace Expedition178.Interfaces
{
    public interface IRandomGenerator
    {
        int GetNext(int min, int max);
    }
}
