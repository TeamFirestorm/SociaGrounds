namespace SociaGroundsEngine.Multiplayer
{
    public enum PacketTypes
    {
        Connect,
        Disconnect,
        Move,
        WorldState
    }

    public enum MoveDirection
    {
        Up,
        Down,
        Left,
        Right,
        None
    }
}
