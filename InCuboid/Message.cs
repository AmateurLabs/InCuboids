using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InCuboid {
    public class Message {

        public MessageType Type;
        public byte[] Data;

        public Message(MessageType type, byte[] data) {
            Type = type;
            Data = data;
        }
    }

    public enum MessageType {
        Null=0,

    }
}
