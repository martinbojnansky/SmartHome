using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSockets.Responses
{
    public interface ISocketResponse
    {
        SocketResponseType Type { get; set; }
        string Response { get; set; }
    }
}
