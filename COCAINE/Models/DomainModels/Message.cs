namespace COCAINE.Models.DomainModels
{
    public class Message
    {
        public int      Id { get; set; }
        public string?  Author { get; set; }
        public string?  MessageBody { get; set; }
        public int?     LikesCount { get; set; }
    }
}
