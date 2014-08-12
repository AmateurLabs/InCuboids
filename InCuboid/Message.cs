using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InCuboid {
    public class Message {

        public MessageType Type;
        public byte PortId;
        public byte[] Data;

        public Message(MessageType type, byte[] data) {
            Type = type;
            Data = data;
        }

        public Message(MessageType type, byte portId, byte[] data) : this(type, data) {
            PortId = portId;
        }
    }

    public enum MessageType {
        Null=0,
        Scan=1,
        Identify=2,
        Acknowledge=3
    }
}
