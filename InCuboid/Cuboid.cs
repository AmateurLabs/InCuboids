using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InCuboid {
    public class Cuboid {

        public readonly Guid GUID;

        public int LastScan;

        public Port[] Ports;
    }

    public enum CuboidType {
        Null=0,

    }
}
