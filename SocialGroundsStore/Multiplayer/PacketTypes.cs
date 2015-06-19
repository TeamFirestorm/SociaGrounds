namespace SocialGroundsStore.Multiplayer
{
    /// <summary>
    /// Packettypes that determine what to do
    /// </summary>
    public enum PacketTypes
    {
        Connect,
        Disconnect,
        Move,
        WorldState
    }

    /// <summary>
    /// Move direction that determines which way to move
    /// </summary>
    public enum MoveDirection
    {
        Up,
        Down,
        Left,
        Right,
        None
    }
}
