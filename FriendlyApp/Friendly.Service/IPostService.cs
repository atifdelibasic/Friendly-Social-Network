using Friendly.Model;
using Friendly.Model.Requests.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friendly.Service
{
    public interface IPostService : ICRUDService<Post, object, CreatePostRequest, object>
    {

    }
}
