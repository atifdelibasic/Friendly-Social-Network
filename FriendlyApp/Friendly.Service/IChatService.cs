﻿using Friendly.Model.Requests.Message;
using Friendly.Model.SearchObjects;

namespace Friendly.Service
{
    public interface IChatService
    {
        public Task<Model.Message> StoreMessage(CreateMessageRequest request);
        public Task<List<Model.Message>> Get(SearchMessageRequest request);
    }
}
