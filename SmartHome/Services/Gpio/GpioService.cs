using SmartSockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPToolkit.Gpio;
using UWPToolkit.IoC;
using Windows.Devices.Gpio;

namespace SmartHome.Services.Gpio
{
    public class GpioService : UWPToolkit.Gpio.GpioService, ISingleResolvable
    {
        public override List<Pin> Pins { get; set; } =
            new List<Pin>()
            {
                new Pin(Constants.PIN18, GpioPinDriveMode.Output)
            };
    }
}
