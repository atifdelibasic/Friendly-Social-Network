using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Friendly.Model.Requests.Hobby;

namespace Friendly.Service
{
    public interface IHobbyService : ICRUDService<Model.Hobby, SearchHobbyRequest, CreateHobbyRequest, UpdateHobbyRequest>
    {

    }
}
