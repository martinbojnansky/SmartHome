using SmartMote.Services.RemoteControl;
using SmartSockets;
using SmartSockets.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinToolkit.ViewModel;

namespace SmartMote.ViewModels.Devices
{
    public class DevicesViewModel : ViewModelBase
    {
        public RemoteControlService RemoteControlService { get; set; }

        private Command _turnOnCommand;
        public Command TurnOnCommand => _turnOnCommand ?? (_turnOnCommand = new Command(TurnOn));

        private Command _turnOffCommand;
        public Command TurnOffCommand => _turnOffCommand ?? (_turnOffCommand = new Command(TurnOff));

        public async void TurnOn()
        {
            var command = new SocketCommand(SocketCommandType.PIN_ON, new object[] { Constants.PIN18 });
            await RemoteControlService.SendCommandSafeAsync(command);
        }

        public async void TurnOff()
        {
            var command = new SocketCommand(SocketCommandType.PIN_OFF, new object[] { Constants.PIN18 });
            await RemoteControlService.SendCommandSafeAsync(command);
        }
    }
}
