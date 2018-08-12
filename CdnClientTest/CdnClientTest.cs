using CdnClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CdnApiClientTest
{
    public class CdnClientTest
    {
        private readonly CDNsunCdnApiClient _client;
        private readonly string _serviceid;

        public CdnClientTest(string username, string password, string serviceid)
        {
            _client = new CDNsunCdnApiClient(username, password);
            _serviceid = serviceid;
        }

        public async Task<string> GetEmptyTest()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            var relativeUrl = "cdns";

            return await _client.GetAsync(data, relativeUrl);
        }

        public async Task<string> GetWithQueryStringTest()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("type", "GB");
            data.Add("period", "4h");
            var relativeUrl = $"cdns/{_serviceid}/reports";

            return await _client.GetAsync(data, relativeUrl);
        }

        public async Task<string> PostDataTest()
        {
            var data = "{	\"purge_paths\" :  [ \"/path1.img\", \"/path2.img\"]}";
            var relativeUrl = $"cdns/{_serviceid}/purge";

            return await _client.PostAsync(data, relativeUrl);
        }
    }
}