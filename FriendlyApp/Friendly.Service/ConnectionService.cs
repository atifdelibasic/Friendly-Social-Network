using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friendly.Service
{
    public interface IConnectionService<T>
    {
        void AddConnection(T key, string connectionId);
        IEnumerable<string> GetConnections(T key);
        void RemoveConnection(T key, string connectionId);
    }

    public class ConnectionService<T> : IConnectionService<T>
    {
        private readonly ConnectionMapping<T> _connections = new ConnectionMapping<T>();

        public void AddConnection(T key, string connectionId)
        {
            _connections.Add(key, connectionId);
        }

        public IEnumerable<string> GetConnections(T key)
        {
            return _connections.GetConnections(key);
        }

        public void RemoveConnection(T key, string connectionId)
        {
            _connections.Remove(key, connectionId);
        }
    }

}
