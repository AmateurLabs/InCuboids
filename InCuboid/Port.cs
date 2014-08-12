using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace InCuboid {
    public class Port {

        public readonly byte ID;

        public BinaryReader Input = new BinaryReader(new MemoryStream());
        public BinaryWriter Output = new BinaryWriter(new MemoryStream());

        public Port(byte id) {
            ID = id;
        }

        public void Send(Message msg) {
            msg.PortId = ID;
            byte[] buffer = new byte[3+msg.Data.Length];
            buffer[0] = (byte)msg.Type;
            buffer[1] = (byte)msg.PortId;
            buffer[2] = (byte)msg.Data.Length;
            Buffer.BlockCopy(msg.Data, 0, buffer, 3, msg.Data.Length);
            Output.Write(buffer, 0, buffer.Length);
            Output.Flush();
        }

        //Convert to buffered version and return null if no message has arrived
        public Message Recieve() {
            if (Input.BaseStream.Position == Input.BaseStream.Length) return null;
            MessageType type = (MessageType)Input.ReadByte();
            byte portId = (byte)Input.ReadByte();
            byte length = (byte)Input.ReadByte();
            byte[] buffer = new byte[length];
            Input.Read(buffer, 0, length);
            Send(new Message(MessageType.Acknowledge, new byte[] { 255, 255 }));
            return new Message(type, portId, buffer);
        }

        public static void Connect(Port a, Port b) {
            a.Input = new BinaryReader(b.Output.BaseStream);
            b.Input = new BinaryReader(a.Output.BaseStream);
        }
    }
}
