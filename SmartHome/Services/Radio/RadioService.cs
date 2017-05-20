using Shared.Models.Radio;
using SmartSockets.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPToolkit.IoC;
using UWPToolkit.Storage;

namespace SmartHome.Services.Radio
{
    public class RadioService : BackgroundMediaPlayerService, ISingleResolvable
    {
        public Json Json { get; set; }
        public LocalSettings LocalSettings { get; set; }

        public List<RadioStation> Stations { get; set; }

        public RadioService() : base()
        {
            LoadStations();
        }

        public SocketResponse GetStations()
        {
            return new SocketResponse(SocketResponseType.OK, Json.ToJson(Stations));
        }

        private void LoadStations()
        {
            try
            {
                Stations = Json.FromJson<List<RadioStation>>((string)LocalSettings.GetValue(nameof(Stations)));
            }
            catch (KeyNotFoundException)
            {
                Stations = Assets.Stations.Default;
                SaveStations();
            }
        }

        private void SaveStations()
        {
            LocalSettings.SetValue(nameof(Stations), Json.ToJson(Stations));
        }
    }
}