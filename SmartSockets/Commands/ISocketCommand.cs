using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSockets.Commands
{
    public interface ISocketCommand
    {
        SocketCommandType Type { get; set; }
        object[] Parameters { get; set; }
    }
}
