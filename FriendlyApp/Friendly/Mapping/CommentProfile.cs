using AutoMapper;
using Friendly.Model.Requests.Comment;

namespace Friendly.WebAPI.Mapping
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<CreateCommentRequest, Database.Comment>();
            CreateMap<UpdateCommentRequest, Database.Comment>();
            CreateMap<Database.Comment, Model.Comment>();
        }
    }
}
