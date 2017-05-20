using Shared.Models.Alarm;
using Shared.Models.Radio;
using SmartHome.Services.Alarms;
using SmartHome.Services.Gpio;
using SmartHome.Services.Radio;
using SmartSockets.CommandProcessors;
using SmartSockets.Commands;
using SmartSockets.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UWPToolkit.IoC;

namespace SmartHome.Services.RemoteControl
{
    public class RemoteControlCommandProcessor : ISocketCommandProcessor, ISingleResolvable
    {
        private RadioService _radioService;
        private AlarmService _alarmService;
        private GpioService _gpioService;

        private SemaphoreSlim _semaphore = new SemaphoreSlim(1);

        public RemoteControlCommandProcessor(RadioService radioService, AlarmService alarmService, GpioService gpioService)
        {
            _radioService = radioService;
            _alarmService = alarmService;
            _gpioService = gpioService;
        }

        public async Task<SocketResponse> ProcessCommandAsync(SocketCommand command)
        {
            await _semaphore.WaitAsync();

            try
            {
                switch (command.Type)
                {
                    case SocketCommandType.PIN_OFF:
                        _gpioService.Write((int)command.Parameters[0], Windows.Devices.Gpio.GpioPinValue.Low);
                        return new SocketResponse(SocketResponseType.OK);

                    case SocketCommandType.PIN_ON:
                        _gpioService.Write((int)command.Parameters[0], Windows.Devices.Gpio.GpioPinValue.High);
                        return new SocketResponse(SocketResponseType.OK);

                    case SocketCommandType.PLAY:
                        return _radioService.Play((RadioStation)command.Parameters[0]);

                    case SocketCommandType.STOP:
                        return _radioService.Stop();

                    case SocketCommandType.VOLUME_UP:
                        return _radioService.VolumeUp();

                    case SocketCommandType.VOLUME_DOWN:
                        return _radioService.VolumeDown();

                    case SocketCommandType.GET_RADIO_STATIONS:
                        return _radioService.GetStations();

                    case SocketCommandType.GET_ALARM:
                        return _alarmService.GetAlarm();

                    case SocketCommandType.SET_ALARM:
                        return _alarmService.SetAlarm((Alarm)command.Parameters[0]);

                    default:
                        return new SocketResponse(SocketResponseType.BAD_REQUEST);
                }
            }
            catch (Exception ex)
            {
                return new SocketResponse(SocketResponseType.ERROR, ex.ToString());
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
