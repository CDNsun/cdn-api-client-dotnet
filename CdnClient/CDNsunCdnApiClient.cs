using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CdnClient
{
    public class CDNsunCdnApiClient
    {
        private readonly string _baseUrl = @"https://cdnsun.com/api/";
        private readonly HttpClient _httpClient;

        public CDNsunCdnApiClient(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException(nameof(username));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException(nameof(password));
            }

            _httpClient = new HttpClient();
            InitializeHttpClient(_httpClient, username, password);
        }

        private void InitializeHttpClient(HttpClient httpClient,
            string username,
            string password)
        {
            httpClient.BaseAddress = new Uri(_baseUrl);
            var authBytes = ASCIIEncoding.ASCII.GetBytes($"{username}:{password}");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic",
                Convert.ToBase64String(authBytes));
        }

        public async Task<string> GetAsync(Dictionary<string, string> data, string relativeUrl)
        {
            var queryString = "";
            if (data != null && data.Count > 0)
            {
                queryString = "?" + string.Join("&", data.Select(item => $"{item.Key}={item.Value}"));
            }

            var request = new HttpRequestMessage(HttpMethod.Get, relativeUrl + queryString);

            return await RequestAsync(request);
        }

        public async Task<string> PostAsync(string data, string relativeUrl)
        {
            return await SendRequestOfType(HttpMethod.Post, data, relativeUrl);
        }

        public async Task<string> PutAsync(string data, string relativeUrl)
        {
            return await SendRequestOfType(HttpMethod.Put, data, relativeUrl);
        }

        public async Task<string> DeleteAsync(string data, string relativeUrl)
        {
            return await SendRequestOfType(HttpMethod.Delete, data, relativeUrl);
        }

        private async Task<string> SendRequestOfType(HttpMethod method, string data, string relativeUrl)
        {
            var request = new HttpRequestMessage(method, relativeUrl.TrimStart('/'));
            MakeUrlRelative(request);

            var content = new StringContent(data);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Content = content;
            return await RequestAsync(request);
        }

        private async Task<string> RequestAsync(HttpRequestMessage request)
        {
            var responseStering = await _httpClient.SendAsync(request);
            var res = await responseStering.Content.ReadAsStringAsync();
            return res;
        }

        private void MakeUrlRelative(HttpRequestMessage request)
        {
            if (request.RequestUri.IsAbsoluteUri)
            {
                request.RequestUri.MakeRelativeUri(_httpClient.BaseAddress);
            }
        }
    }
}
