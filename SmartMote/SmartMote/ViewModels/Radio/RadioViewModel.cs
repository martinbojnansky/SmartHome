using Shared.Models.Radio;
using SmartMote.Services.RemoteControl;
using SmartSockets.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinToolkit.Storage;
using XamarinToolkit.ViewModel;

namespace SmartMote.ViewModels.Radio
{
    public class RadioViewModel : ViewModelBase
    {
        public Json Json { get; set; }
        public RemoteControlService RemoteControlService { get; set; }

        private bool _isBusy = false;

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                RaisePropertyChanged(nameof(IsBusy));
            }
        }

        private ObservableCollection<RadioStation> _stations;

        public ObservableCollection<RadioStation> Stations
        {
            get
            {
                return _stations;
            }
            set
            {
                _stations = value;
                RaisePropertyChanged(nameof(Stations));
            }
        }

        private Command<RadioStation> _playCommand;
        public Command<RadioStation> PlayCommand => _playCommand ?? (_playCommand = new Command<RadioStation>(Play));

        private Command _stopCommand;
        public Command StopCommand => _stopCommand ?? (_stopCommand = new Command(Stop));

        private Command _volumeDownCommand;
        public Command VolumeDownCommand => _volumeDownCommand ?? (_volumeDownCommand = new Command(VolumeDown));

        private Command _volumeUpCommand;
        public Command VolumeUpCommand => _volumeUpCommand ?? (_volumeUpCommand = new Command(VolumeUp));

        private Command _refresCommand;
        public Command RefreshCommand => _refresCommand ?? (_refresCommand = new Command(Refresh));

        public async void Refresh() => await RefreshAsync();

        public async Task RefreshAsync()
        {
            IsBusy = true;

            await LoadStationsAsync();

            IsBusy = false;
        }

        public async Task LoadStationsAsync()
        {
            var response = await RemoteControlService.SendCommandSafeAsync(new SocketCommand(SocketCommandType.GET_RADIO_STATIONS), false);
            if (response != null && !string.IsNullOrEmpty(response.Response))
            {
                var stations = Json.FromJson<List<RadioStation>>(response.Response);
                Stations = new ObservableCollection<RadioStation>(stations);
            }
        }

        private async void Play(RadioStation station)
        {
            var command = new SocketCommand(SocketCommandType.PLAY, new object[] { station });
            await RemoteControlService.SendCommandSafeAsync(command);
        }

        private async void Stop()
        {
            await RemoteControlService.SendCommandSafeAsync(new SocketCommand(SocketCommandType.STOP));
        }

        private async void VolumeDown()
        {
            await RemoteControlService.SendCommandSafeAsync(new SocketCommand(SocketCommandType.VOLUME_DOWN));
        }

        private async void VolumeUp()
        {
            await RemoteControlService.SendCommandSafeAsync(new SocketCommand(SocketCommandType.VOLUME_UP));
        }
    }
}