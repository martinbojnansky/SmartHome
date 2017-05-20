using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using XamarinToolkit.Storage;
using SmartSockets.Clients;
using SmartSockets.Commands;
using SmartSockets.Responses;
using SmartMote.UWP.Services.RemoteControl;

[assembly: Xamarin.Forms.Dependency(typeof(RemoteControlClient))]

namespace SmartMote.UWP.Services.RemoteControl
{
    public class RemoteControlClient : SocketClient
    {
        public Json Json { get; set; }
        private const string UNDERLYING_SOCKET_CLOSED_BEFORE_READ = "The underlying socket was closed before read.";

        private TcpClient _client;
        private NetworkStream _stream;
        private SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);

        public override async Task ConnectAsync()
        {
            _client = new TcpClient();

            try
            {
                await _client.ConnectAsync(Host, Port);
            }
            catch
            {
                Dispose();
            }
        }

        public override async Task<SocketResponse> SendCommandAsync(SocketCommand command)
        {
            // Wait while previous command is proccessed
            await _semaphoreSlim.WaitAsync();

            try
            {
                // Check connection
                if (_client == null)
                {
                    await ConnectAsync();
                }

                _stream = _client.GetStream();

                await WriteCommandAsync(command);
                var response = await ReadResponseAsync();

                return response;
            }
            catch
            {
                Dispose();
                throw;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public override void Dispose()
        {
            _stream?.Dispose();
            _client?.Dispose();
            _client = null;
        }

        private async Task WriteCommandAsync(SocketCommand command)
        {
            var commandString = Json.ToJson(command);
            var commandBuffer = Encoding.UTF8.GetBytes(commandString);
            var commandBufferLength = BitConverter.GetBytes((uint)commandBuffer.Length);

            await _stream.WriteAsync(commandBufferLength, 0, commandBufferLength.Length);
            await _stream.WriteAsync(commandBuffer, 0, commandBuffer.Length);
        }

        private async Task<SocketResponse> ReadResponseAsync()
        {
            var responseLengthBuffer = new byte[sizeof(uint)];
            if (await _stream.ReadAsync(responseLengthBuffer, 0, responseLengthBuffer.Length) != responseLengthBuffer.Length) throw new Exception(UNDERLYING_SOCKET_CLOSED_BEFORE_READ);

            var responseBuffer = new byte[BitConverter.ToUInt32(responseLengthBuffer, 0)];
            if (await _stream.ReadAsync(responseBuffer, 0, responseBuffer.Length) != responseBuffer.Length) throw new Exception(UNDERLYING_SOCKET_CLOSED_BEFORE_READ);

            var responseString = Encoding.UTF8.GetString(responseBuffer);
            var response = Json.FromJson<SocketResponse>(responseString);

            return response;
        }
    }
}