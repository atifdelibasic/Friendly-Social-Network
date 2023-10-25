using Friendly.Model.Requests.Like;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friendly.Service
{
    public interface ILikeService
    {
        public Task<List<Model.Like>> GetLikes(SearchLikesRequest search);
    }
}
