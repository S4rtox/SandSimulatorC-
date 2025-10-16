using System;
using System.Collections.Generic;

namespace SandSimulator2.GridManagers
{
    [Serializable]
    public class GridState
    {
        public List<ElementInfo> Elements { get; set; }
    }

    [Serializable]
    public class ElementInfo
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Type ElementType { get; set; }
    }
}

