using SmartSockets.Clients;
using SmartSockets.Commands;
using SmartSockets.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinToolkit.IoC;
using XamarinToolkit.Storage;

namespace SmartMote.Services.RemoteControl
{
    public class RemoteControlService : ISingleResolvable
    {
        public LocalSettings LocalSettings { get; set; }
        private ISocketClient _client;

        private string _ipAddress
        {
            get
            {
                return (string)LocalSettings.TryGetValue("IPAddress");
            }
            set
            {
                LocalSettings.SetValue("IPAddress", value);
            }
        }

        public RemoteControlService()
        {
            _client = DependencyService.Get<SocketClient>();
            _client.Init(_ipAddress, SmartSockets.Constants.PORT);
        }

        public async Task<SocketResponse> SendCommandSafeAsync(SocketCommand command, bool showAlert = true)
        {
            try { return await _client?.SendCommandAsync(command); }
            catch { return new SocketResponse(SocketResponseType.ERROR); }
        }
    }
}