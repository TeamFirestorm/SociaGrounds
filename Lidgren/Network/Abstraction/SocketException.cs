using System;

namespace Lidgren.Network.Abstraction
{
    public class SocketException : Exception
    {
        public SocketError SocketErrorCode { get; set; }
    }
}
