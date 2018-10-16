using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPToolkit.IoC;
using Windows.ApplicationModel.Resources;

namespace UWPToolkit.Localization
{
    public class LocalizedResources : IResolvable
    {
        private ResourceLoader _resourceLoader = new ResourceLoader();

        public string GetString(string name) => _resourceLoader.GetString(name);
    }
}
