using Microsoft.AspNetCore.Mvc;
using Service;

namespace ECommerceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly IRabbitMQService _rabbitMQService;

        public NotificationController(IRabbitMQService rabbitMQService)
        {
            _rabbitMQService = rabbitMQService;
        }

        [HttpPost("SendMessage")]
        public IActionResult SendMessage(string message)
        {
            try
            {
                _rabbitMQService.PublishMessage("test-queue", message);
                return Ok($"Message '{message}' sent to the queue.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to send message: {ex.Message}");
            }
        }
    }
}
