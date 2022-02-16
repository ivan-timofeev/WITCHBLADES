using COCAINE.Models;
using COCAINE.Models.DomainModels;

namespace COCAINE.Data
{
    public class DataContext
    {
        private static DataContext? _instance;
        public List<Message> Messages { get; set; }

        public static DataContext GetContext()
        {
            if (_instance == null)
                _instance = new DataContext();

            return _instance;
        }

        private DataContext()
        {
            Messages = new List<Message>
            {
                new Message()
                {
                    Id = 1,
                    Author = "Ivan Kotov",
                    LikesCount = 0,
                    MessageBody = $"Всем привет. Hello world!"
                },
                new Message()
                {
                    Id = 2,
                    Author = "Ivan Kotov",
                    LikesCount = 0,
                    MessageBody = $"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum."
                }
            };
        }
    }
}
