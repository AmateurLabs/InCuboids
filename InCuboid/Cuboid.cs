using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InCuboid {
    public class Cuboid {

        public readonly Guid GUID;
        public readonly CuboidType Type;

        public int LastScan;

        public Port[] Ports;

        public Cuboid(CuboidType type) {
            GUID = new Guid();
            Type = type;
            if (type == CuboidType.Cube) {
                Ports = new Port[6];
                for (byte i = 0; i < 6; i++) Ports[i] = new Port(i);
            }
        }

        public void Update() {
            for (int i = 0; i < Ports.Length; i++) {
                Message msg = Ports[i].Recieve();
                if (msg == null) continue;
                if (msg.Type == MessageType.Scan) {
                    int scan = BitConverter.ToInt32(msg.Data, 0);
                    if (scan == LastScan) continue;
                    LastScan = scan;
                    Broadcast(new Message(MessageType.Identify, GUID.ToByteArray()));
                }
                else if (msg.Type == MessageType.Identify) {
                    Broadcast(new Message(MessageType.Identify, Concat(Concat(msg.Data, new byte[] { msg.PortId }), GUID.ToByteArray())));
                }
            }
        }

        public void Broadcast(Message msg) {
            for (int i = 0; i < Ports.Length; i++) {
                Ports[i].Send(msg);
            }
        }

        public static byte[] Concat(byte[] a, byte[] b) {
            byte[] arr = new byte[a.Length + b.Length];
            Buffer.BlockCopy(a, 0, arr, 0, a.Length);
            Buffer.BlockCopy(b, 0, arr, a.Length, b.Length);
            return arr;
        }
    }

    public enum CuboidType {
        Null=0,
        Cube=1
    }
}
