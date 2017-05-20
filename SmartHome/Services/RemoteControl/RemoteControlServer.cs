using SmartSockets.Commands;
using SmartSockets.Responses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UWPToolkit.IoC;
using UWPToolkit.Storage;

namespace SmartHome.Services.RemoteControl
{
    public sealed class RemoteControlServer : ISingleResolvable
    {
        public Json Json { get; set; }
        private int _port = SmartSockets.Constants.PORT;
        private TcpListener _server { get; set; }
        private RemoteControlCommandProcessor _commandProcessor;

        public RemoteControlServer(RemoteControlCommandProcessor commandProcessor)
        {
            _commandProcessor = commandProcessor;
        }

        public async Task StartAsync()
        {
            try
            {
                _server = new TcpListener(await GetHostPAddressAsync(), _port);
                _server.Start();

                await Task.Factory.StartNew(AcceptClientsAsync);
            }
            catch
            {
                Stop();
            }
        }

        private async Task AcceptClientsAsync()
        {
            while (true)
            {
                var client = await _server.AcceptTcpClientAsync();
                OnConnectionRecevied(client);
            }
        }

        private async void OnConnectionRecevied(TcpClient client)
        {
            var stream = client.GetStream();

            if (!stream.CanRead) return;

            while (true)
            {
                try
                {
                    var command = await ReadCommandAsync(stream);
                    var response = await _commandProcessor.ProcessCommandAsync(command);
                    await WriteResponseAsync(stream, response);
                }
                catch (Exception ex)
                {
#if DEBUG
                    Debug.WriteLine($"TcpSocketServerException:\n {ex.ToString()}");
#endif
                    return;
                }
            }
        }

        public void Stop()
        {
            _server?.Stop();
        }

        private async Task<SocketCommand> ReadCommandAsync(NetworkStream stream)
        {
            var commandLengthBuffer = new byte[sizeof(uint)];
            if (await stream.ReadAsync(commandLengthBuffer, 0, commandLengthBuffer.Length) != commandLengthBuffer.Length) throw new Exception();

            var commandBuffer = new byte[BitConverter.ToUInt32(commandLengthBuffer, 0)];
            if (await stream.ReadAsync(commandBuffer, 0, commandBuffer.Length) != commandBuffer.Length) throw new Exception();

            var commandString = Encoding.UTF8.GetString(commandBuffer);
            var command = Json.FromJson<SocketCommand>(commandString);

#if DEBUG
            Debug.WriteLine($"TcpSocketServer recieved:\n {commandString}");
#endif

            return command;
        }

        private async Task WriteResponseAsync(NetworkStream stream, SocketResponse response)
        {
            var responseString = Json.ToJson(response);
            var responseBuffer = Encoding.UTF8.GetBytes(responseString);
            var responseLengthBuffer = BitConverter.GetBytes((uint)responseBuffer.Length);

            await stream.WriteAsync(responseLengthBuffer, 0, responseLengthBuffer.Length);
            await stream.WriteAsync(responseBuffer, 0, responseBuffer.Length);

#if DEBUG
            Debug.WriteLine($"TcpSocketServer responded:\n {responseString}");
#endif
        }

        private async Task<IPAddress> GetHostPAddressAsync()
        {
            var hostName = Dns.GetHostName();
            var adresses = await Dns.GetHostAddressesAsync(hostName);

            return adresses.First(a => a.AddressFamily == AddressFamily.InterNetwork);
        }
    }
}