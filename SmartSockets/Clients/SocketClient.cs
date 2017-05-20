using SmartSockets.Commands;
using SmartSockets.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSockets.Clients
{
    public abstract class SocketClient : ISocketClient
    {
        public string Host { get; private set; }
        public int Port { get; private set; }

        public void Init(string host, int port)
        {
            Host = host;
            Port = port;
        }

        public abstract Task ConnectAsync();

        public abstract Task<SocketResponse> SendCommandAsync(SocketCommand command);

        public abstract void Dispose();
    }
}
