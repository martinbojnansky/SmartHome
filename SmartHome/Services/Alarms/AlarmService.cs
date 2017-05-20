using Shared.Models.Alarm;
using Shared.Models.Radio;
using SmartHome.Services.Radio;
using SmartSockets.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPToolkit.IoC;
using UWPToolkit.Storage;

namespace SmartHome.Services.Alarms
{
    public class AlarmService : ISingleResolvable
    {
        public Json Json { get; set; }
        public LocalSettings LocalSettings { get; set; }

        private Alarm _alarm
        {
            get
            {
                try { return Json.FromJson<Alarm>((string)LocalSettings.GetValue(typeof(Alarm).Name)); }
                catch { return _alarm = new Alarm(); }
            }
            set
            {
                LocalSettings.SetValue(typeof(Alarm).Name, Json.ToJson(value));
            }
        }

        private RadioService _radioService { get; set; }

        public AlarmService(RadioService radioService)
        {
            _radioService = radioService;
        }

        public SocketResponse GetAlarm()
        {
            try
            {
                return new SocketResponse(SocketResponseType.OK, Json.ToJson(_alarm));
            }
            catch (Exception ex)
            {
                return new SocketResponse(SocketResponseType.ERROR, ex.Message);
            }
        }

        public SocketResponse SetAlarm(Alarm alarm)
        {
            try
            {
                _alarm = alarm;
                return new SocketResponse(SocketResponseType.OK);
            }
            catch (Exception ex)
            {
                return new SocketResponse(SocketResponseType.ERROR, ex.Message);
            }
        }

        public async Task StartAsync()
        {
            await Task.Factory.StartNew(WaitAlarmAsync);
        }

        private async Task WaitAlarmAsync()
        {
            while (true)
            {
                try
                {
                    if (_alarm.IsActive && DateTime.UtcNow >= _alarm.Time)
                    {
                        StartAlarm(_alarm.RadioStation);
                    }

                    await Task.Delay((60 - DateTime.UtcNow.Second) * 1000);
                }
                catch
                {
                    await StartAsync();
                }
            }
        }

        private void StartAlarm(RadioStation station)
        {
            _radioService.PlayAlarm(station, _alarm.Volume / 20);
            ObsoleteAlarm();
        }

        private void ObsoleteAlarm()
        {
            _alarm = new Alarm()
            {
                IsActive = false,
                Time = _alarm.Time,
                Volume = _alarm.Volume,
                RadioStation = _alarm.RadioStation
            };
        }
    }
}