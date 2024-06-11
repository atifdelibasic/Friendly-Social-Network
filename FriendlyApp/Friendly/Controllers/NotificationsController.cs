using Friendly.Model.Requests.Notification;
using Friendly.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Friendly.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationsController(INotificationService service)
        {
            _notificationService = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetNotifications([FromQuery] GetNotificationsRequest request)
        {
            List<Model.Notification> notifications = await _notificationService.GetNotifications(request);
            return Ok(notifications);
        }

    }
}
