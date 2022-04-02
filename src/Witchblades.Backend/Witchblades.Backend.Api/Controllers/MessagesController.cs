//using Witchblades.Backend.Data;
//using Witchblades.Backend.Models;
//using Witchblades.Backend.Models.DomainModels;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.Linq;
//
//namespace Witchblades.Backend.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class MessagesController : ControllerBase
//    {
//        DataContext context = DataContext.GetContext();
//
//        [HttpGet]
//        public ActionResult<IEnumerable<Message>> GetMessages()
//        {
//            return context.Messages;
//        }
//
//        [HttpPost]
//        public ActionResult<Message> CreateMessage([FromBody] Message message)
//        {
//            if (message is null)
//            {
//                return BadRequest();
//            }
//
//            message.Id = context.Messages.Max(t => t.Id) + 1;
//            context.Messages.Add(message);
//
//            return message;
//        }
//
//        [HttpGet("{id}")]
//        public ActionResult<Message> GetMessage(int id)
//        {
//            var message = context.Messages.FirstOrDefault(t => t.Id == id);
//
//            if (message is null)
//            {
//                return NotFound();
//            }
//
//            return message;
//        }
//        
//        [Route("IncreaseLikesToMessage/{id}")]
//        [HttpPost]
//        public ActionResult<Message> IncreaseLikesToMessage(int id)
//        {
//            var message = context.Messages.FirstOrDefault(t => t.Id == id);
//
//            if (message is null)
//            {
//                return NotFound();
//            }
//
//            message.LikesCount++;
//
//            return message;
//        }
//
//        [Route("ReduceLikesToMessage/{id}")]
//        [HttpPost]
//        public ActionResult<Message> ReduceLikesToMessage(int id)
//        {
//            var message = context.Messages.FirstOrDefault(t => t.Id == id);
//
//            if (message is null)
//            {
//                return NotFound();
//            }
//
//            message.LikesCount--;
//
//            return message;
//        }
//    }
//}
