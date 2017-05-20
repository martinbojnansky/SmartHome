using Shared.Models.Alarm;
using Shared.Models.Radio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SmartSockets.Commands
{
    [DataContract]
    [KnownType(typeof(Alarm))]
    [KnownType(typeof(RadioStation))]
    public class SocketCommand : ISocketCommand
    {
        public SocketCommand(SocketCommandType type, object[] parameters = null)
        {
            Type = type;
            Parameters = parameters;
        }

        [DataMember]
        public SocketCommandType Type { get; set; }
        [DataMember]
        public object[] Parameters { get; set; }
    }
}
