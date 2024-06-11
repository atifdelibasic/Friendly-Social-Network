using AutoMapper;

namespace Friendly.WebAPI.Mapping
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<Database.Notification, Model.Notification>();


        }
    }
}
