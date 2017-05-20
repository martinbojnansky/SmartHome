using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSockets.Commands
{
    public enum SocketCommandType
    {
        PIN_OFF, PIN_ON,
        PLAY, STOP, VOLUME_DOWN, VOLUME_UP, GET_RADIO_STATIONS,
        GET_ALARM, SET_ALARM
    }
}
