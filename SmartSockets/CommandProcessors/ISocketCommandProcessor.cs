using SmartSockets.Commands;
using SmartSockets.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSockets.CommandProcessors
{
    public interface ISocketCommandProcessor
    {
        Task<SocketResponse> ProcessCommandAsync(SocketCommand command);
    }
}
