using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace UWPToolkit.Gpio
{
    public sealed class Pin
    {
        public int PinNumber { get; }
        public GpioPinDriveMode GpioPinDriveMode { get; }
        public GpioPin GpioPin { get; set; }

        public Pin(int pinNumber, GpioPinDriveMode gpioPinDriveMode)
        {
            PinNumber = pinNumber;
            GpioPinDriveMode = gpioPinDriveMode;
        }
    }
}
