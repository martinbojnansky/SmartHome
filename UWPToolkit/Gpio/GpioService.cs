using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPToolkit.IoC;
using Windows.Devices.Gpio;

namespace UWPToolkit.Gpio
{
    public abstract class GpioService : IResolvable
    {
        private GpioController _gpio;
        public abstract List<Pin> Pins { get; set; }

        public GpioService()
        {
            _gpio = GpioController.GetDefault();

            foreach (var pin in Pins)
            {
                pin.GpioPin = _gpio.OpenPin(pin.PinNumber);
                pin.GpioPin.SetDriveMode(pin.GpioPinDriveMode);
            }
        }

        public void Write(int pinNumber, GpioPinValue value)
        {
            Pins.FirstOrDefault(p => p.PinNumber == 18).GpioPin.Write(value);
        }

        public GpioPinValue Read(int pinNumber)
        {
            return Pins.FirstOrDefault(p => p.PinNumber == 18).GpioPin.Read();
        }
    }
}
