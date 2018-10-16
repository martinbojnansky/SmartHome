using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using XamarinToolkit.Storage;
using XamarinToolkit.IoC;

namespace XamarinToolkit.Http
{
    public abstract class RestClient : HttpClient, IResolvable
    {
        private Json _json = new Json();

        public void Init(string baseAddress, string authorizationKey = null)
        {
            BaseAddress = new Uri($"{baseAddress}/");

            if (authorizationKey != null)
            {
                DefaultRequestHeaders.Add("Authorization", authorizationKey);
            }
        }

        public async Task<O> GetAsync<O>(string requestUrl)
        {
            var response = await base.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            O obj = _json.FromJson<O>(json);

            return obj;
        }

        public async Task PostAsync<I>(string requestUrl, I content)
        {
            var response = await base.PostAsync(requestUrl, GetJsonContent(content));
            response.EnsureSuccessStatusCode();
        }

        public async Task<O> PostAsync<I, O>(string requestUrl, I content)
        {
            var response = await base.PostAsync(requestUrl, GetJsonContent(content));
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            O obj = _json.FromJson<O>(json);

            return obj;
        }

        public async Task PutAsync<I>(string requestUrl, I content)
        {
            var response = await base.PutAsync(requestUrl, GetJsonContent(content));
            response.EnsureSuccessStatusCode();
        }

        public async Task<O> PutAsync<I, O>(string requestUrl, I content)
        {
            var response = await base.PutAsync(requestUrl, GetJsonContent(content));
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            O obj = _json.FromJson<O>(json);

            return obj;
        }

        public async Task<O> DeleteAsync<O>(string requestUrl)
        {
            var response = await base.DeleteAsync(requestUrl);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            O obj = _json.FromJson<O>(json);

            return obj;
        }

        private StringContent GetJsonContent<I>(I content)
        {
            string json = string.Empty;

            if (content != null)
            {
                json = _json.ToJson(content);
            }

            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}