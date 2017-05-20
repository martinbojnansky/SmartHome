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

namespace SmartMote.ViewModels.Alarm
{
    public class AlarmViewModel : ViewModelBase
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

        private Shared.Models.Alarm.Alarm _alarm;

        public Shared.Models.Alarm.Alarm Alarm
        {
            get
            {
                return _alarm;
            }
            set
            {
                _alarm = value;
                RaisePropertyChanged(nameof(Alarm));
            }
        }

        private TimeSpan _time;

        public TimeSpan Time
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
                RaisePropertyChanged(nameof(Time));
            }
        }

        private ObservableCollection<KeyValuePair<string, object>> _stations = new ObservableCollection<KeyValuePair<string, object>>();

        public ObservableCollection<KeyValuePair<string, object>> Stations
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

        private object _selectedStation;

        public object SelectedStation
        {
            get
            {
                return _selectedStation;
            }
            set
            {
                _selectedStation = value;
                RaisePropertyChanged(nameof(SelectedStation));
            }
        }

        private Command _saveCommand;
        public Command SaveCommand => _saveCommand ?? (_saveCommand = new Command(Save));

        private Command _refresCommand;
        public Command RefreshCommand => _refresCommand ?? (_refresCommand = new Command(Refresh));

        public async void Refresh() => await RefreshAsync();

        public async Task RefreshAsync()
        {
            IsBusy = true;

            await LoadAlarmAsync();
            await LoadStationsAsync();

            IsBusy = false;
        }

        public async Task LoadAlarmAsync()
        {
            var response = await RemoteControlService.SendCommandSafeAsync(new SocketCommand(SocketCommandType.GET_ALARM), false);
            if (response != null && !string.IsNullOrEmpty(response.Response))
            {
                Alarm = Json.FromJson<Shared.Models.Alarm.Alarm>(response.Response);

                if (Alarm.Time.HasValue && Alarm.Time.Value != null)
                {
                    Time = Alarm.Time.Value.ToLocalTime().TimeOfDay;
                }
            }
        }

        public async Task LoadStationsAsync()
        {
            var response = await RemoteControlService.SendCommandSafeAsync(new SocketCommand(SocketCommandType.GET_RADIO_STATIONS), false);
            if (response != null && !string.IsNullOrEmpty(response.Response))
            {
                var stations = Json.FromJson<List<RadioStation>>(response.Response);
                Stations = new ObservableCollection<KeyValuePair<string, object>>(stations.Select(s => new KeyValuePair<string, object>(s.Title, s)));

                if (Alarm.RadioStation != null)
                {
                    SelectedStation = Stations.FirstOrDefault(s => s.Key == Alarm.RadioStation.Title);
                }
                else { SelectedStation = null; }
            }
        }

        private async void Save()
        {
            await SaveAlarmAsync();
        }

        private async Task SaveAlarmAsync()
        {
            var dateTime = DateTime.Now.Date.AddHours(Time.Hours).AddMinutes(Time.Minutes);
            if (DateTime.Now.TimeOfDay >= Time)
            {
                dateTime = dateTime.AddDays(1);
            }

            Alarm.Time = dateTime.ToUniversalTime();
            RaisePropertyChanged(nameof(Alarm));

            try { Alarm.RadioStation = (RadioStation)((KeyValuePair<string, object>)SelectedStation).Value; } catch { }

            var command = new SocketCommand(SocketCommandType.SET_ALARM, new object[] { Alarm });
            await RemoteControlService.SendCommandSafeAsync(command);
        }
    }
}