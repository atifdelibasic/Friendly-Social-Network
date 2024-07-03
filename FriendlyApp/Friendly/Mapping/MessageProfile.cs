using AutoMapper;
using Friendly.Model.Requests.Message;

namespace Friendly.WebAPI.Mapping
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<CreateMessageRequest, Database.Message>();
            CreateMap<Database.Message, Model.Message>();
        }
    }
}
