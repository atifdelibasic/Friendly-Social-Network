using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friendly.Service
{
    public interface IChatService
    {
        void SendMessage(int recipientId, string message);
    }
}
