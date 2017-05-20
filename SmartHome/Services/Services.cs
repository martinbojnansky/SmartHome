using SmartHome.Services.Alarms;
using SmartHome.Services.RemoteControl;
using SmartHome.Services.SpeechRecognition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPToolkit.IoC;

namespace SmartHome.Services
{
    public class Services : ISingleResolvable
    {
        public RemoteControlServer RemoteControlServer { get; set; }
        public SpeechRecognitionService SpeechRecognitionService { get; set; }
        public AlarmService AlarmService { get; set; }

        public async Task InitAsync()
        {
            await RemoteControlServer.StartAsync();
            await SpeechRecognitionService.StartAsync();
            await AlarmService.StartAsync();
        }
    }
}
