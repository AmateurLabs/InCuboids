using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace InCuboid {
    public class Port {

        public readonly byte ID;

        public Stream Input = new MemoryStream();
        public Stream Output = new MemoryStream();

        public void Send(Message msg) {
            byte[] buffer = new byte[2+msg.Data.Length];
            buffer[0] = (byte)msg.Type;
            buffer[1] = (byte)msg.Data.Length;
            Buffer.BlockCopy(msg.Data, 0, buffer, 2, msg.Data.Length);
            Output.Write(buffer, 0, buffer.Length);
        }

        public Message Recieve() {
            byte[] buffer = new byte[0];
            MessageType type = (MessageType)Input.ReadByte();
            byte length = (byte)Input.ReadByte();
            Input.Read(buffer, 0, length);
            return new Message(type, buffer);
        }
    }
}
