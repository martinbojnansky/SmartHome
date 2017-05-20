using Shared.Models.Alarm;
using Shared.Models.Radio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SmartSockets.Responses
{
    [DataContract]
    [KnownType(typeof(Alarm))]
    [KnownType(typeof(RadioStation))]
    public class SocketResponse : ISocketResponse
    {
        public SocketResponse(SocketResponseType type, string response = "")
        {
            Type = type;
            Response = response;
        }

        [DataMember]
        public SocketResponseType Type { get; set; }
        [DataMember]
        public string Response { get; set; }
    }
}
