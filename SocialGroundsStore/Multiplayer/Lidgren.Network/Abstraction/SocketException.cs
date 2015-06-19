using System;

namespace SocialGroundsStore.Multiplayer.Lidgren.Network.Abstraction
{
    public class SocketException : Exception
    {
        public SocketError SocketErrorCode { get; set; }
    }
}
