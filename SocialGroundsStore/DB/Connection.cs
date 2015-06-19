namespace SocialGroundsStore.DataBase
{
    // ReSharper disable once ClassNeverInstantiated.Global
    /// <summary>
    /// This class is used to deserialize the IpAddress and DNSSuffix for the hosting of the room
    /// </summary>
    public class Connection
    {
        public string IpAddress { get; set; }
        public string DnsSuffix { get; set; }
    }
}
