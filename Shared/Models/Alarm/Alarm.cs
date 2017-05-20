using Shared.Models.Radio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Alarm
{
    [DataContract]
    [KnownType(typeof(Alarm))]
    [KnownType(typeof(RadioStation))]
    public class Alarm
    {
        [DataMember]
        public DateTime? Time { get; set; }
        [DataMember]
        public double Volume { get; set; } = 10;
        [DataMember]
        public bool IsActive { get; set; } = false;
        [DataMember]
        public RadioStation RadioStation { get; set; }
    }
}
