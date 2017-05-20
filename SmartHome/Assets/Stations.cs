using Shared.Models.Radio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Assets
{
    public class Stations
    {
        public static List<RadioStation> Default
        {
            get
            {
                var stations = new List<RadioStation>()
                {
                    new RadioStation("Funrádio", "http://stream.funradio.sk:8000/fun128.mp3"),
                    new RadioStation("Európa 2", "http://pool.cdn.lagardere.cz/fm-europa2sk-128"),
                    new RadioStation("Rádio Expres", "http://85.248.7.162:8000/96.mp3"),
                    new RadioStation("Rádio FM", "http://live.slovakradio.sk:8000/FM_128.mp3"),
                    new RadioStation("Rádio Jemné", "http://stream.jemne.sk/jemne-hi.mp3"),
                    new RadioStation("Rádio Slovensko", "http://live.slovakradio.sk:8000/Slovensko_128.mp3"),
                    new RadioStation("Clubradio", "http://icecast2.play.cz:8000/Clubradio.mp3"),
                    new RadioStation("Rádio Impuls", "http://icecast5.play.cz/impuls128.mp3"),
                    new RadioStation("OE3 Hitradio", "http://194.232.200.156:8000"),
                    new RadioStation("Frekvence 1", "http://pool.cdn.lagardere.cz/fm-frekvence1-128"),
                    new RadioStation("Fajn Rádio", "http://ice.abradio.cz:8000/fajn128.mp3"),
                    new RadioStation("Kiss Publikum", "http://icecast8.play.cz/kisspublikum128.mp3"),
                    new RadioStation("Best FM", "http://stream.bestfm.sk/128.mp3"),
                    new RadioStation("BBC Radio 1", "http://bbcmedia.ic.llnwd.net/stream/bbcmedia_radio1_mf_q?s=1453540823&e=1453555223&h=2f79a47224002936f000e59235ca26dc"),
                    new RadioStation("Capital XTRA London", "http://media-ice.musicradio.com/CapitalXTRALondonMP3"),
                    new RadioStation("Evropa 2", "http://pool.cdn.lagardere.cz/fm-evropa2-128"),
                    new RadioStation("Funrádio SK/CZ", "http://stream.funradio.sk:8000/cs128.mp3"),
                    new RadioStation("Class FM", "http://icast.connectmedia.hu/4784/live.mp3")
                };

                return stations.OrderBy(s => s.Title).ToList();
            }
        }
    }
}
