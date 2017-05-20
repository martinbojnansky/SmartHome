using SmartSockets.Commands;
using SmartSockets.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSockets.Clients
{
    public interface ISocketClient
    {
        void Init(string host, int port);

        Task ConnectAsync();

        Task<SocketResponse> SendCommandAsync(SocketCommand command);

        void Dispose();
    }
}
