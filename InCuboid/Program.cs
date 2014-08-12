using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InCuboid {
    class Program {

        static void Main(string[] args) {

            Port entry = new Port(0);

            Cuboid cube0 = new Cuboid(CuboidType.Cube);
            Cuboid cube1 = new Cuboid(CuboidType.Cube);
            Cuboid cube2 = new Cuboid(CuboidType.Cube);
            Port.Connect(entry, cube0.Ports[0]);
            Port.Connect(cube0.Ports[2], cube1.Ports[0]);
            Port.Connect(cube1.Ports[1], cube2.Ports[3]);

            entry.Send(new Message(MessageType.Scan, BitConverter.GetBytes(47)));
            for (int i = 0; i < 256; i++) {
                cube0.Update();
                cube1.Update();
                cube2.Update();
            }
            Message msg;
            while ((msg = entry.Recieve()) != null) {
                Console.WriteLine("[" + msg.Type + "]:" + msg.PortId + "{" + BitConverter.ToString(msg.Data) + "}");
                Console.ReadLine();
            }
            Console.ReadLine();
        }
    }
}
