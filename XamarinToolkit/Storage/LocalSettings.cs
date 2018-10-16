using System;
using System.Collections.Generic;
using XamarinToolkit.IoC;
using Xamarin.Forms;

namespace XamarinToolkit.Storage
{
    public class LocalSettings : IResolvable
    {
        private IDictionary<string, object> properties = Application.Current.Properties;

        public void SetValue(string key, object value)
        {
            if (properties.ContainsKey(key))
            {
                properties[key] = value;
            }
            else
            {
                properties.Add(key, value);
            }
        }

        public object GetValue(string key)
        {
            if (properties.ContainsKey(key))
            {
                return properties[key];
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public object TryGetValue(string key)
        {
            if (properties.ContainsKey(key))
            {
                return properties[key];
            }
            else
            {
                return null;
            }
        }

        public void RemoveKey(string key)
        {
            if (properties.ContainsKey(key))
            {
                properties.Remove(key);
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }
    }
}
