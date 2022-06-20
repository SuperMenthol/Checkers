using Assets.Scripts.Infrastructure.Models.Response;
using CheckerScoreAPI.Model;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Infrastructure
{
    public class APICommunication
    {
        private HttpClient _client;
        private APIConfigurator _configurator;

        public APICommunication()
        {
            _client = new HttpClient();
            _configurator = new APIConfigurator();
        }

        public async Task<BaseResponse> CreatePlayer(string playerName)
        {
            var path = _configurator.CreatePlayer(playerName);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, path);

            return await BasePost(requestMessage, path);
        }

        public async Task<BaseResponse> Login(string playerName)
        {
            var path = _configurator.Login(playerName);
            return await BaseGet(path);
        }

        public async Task<BaseResponse> PostMatchResult(MatchResult matchResult)
        {
            var path = _configurator.PostMatchResult();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, path);

            request.Headers.Add("Accept", "application/json");
            request.Properties.Add("Content-Type", "application/json");

            var serializedContent = JsonConvert.SerializeObject(matchResult);
            request.Content = new StringContent(serializedContent, Encoding.UTF8, "application/json");

            return await BasePost(request, path);
        }

        private async Task<BaseResponse> BasePost(HttpRequestMessage request, string path)
        {
            HttpResponseMessage result = await _client.SendAsync(request);
            return await GetBaseResponseMessage(result);
        }

        private async Task<BaseResponse> BaseGet(string path)
        {
            HttpResponseMessage result = await _client.GetAsync(path);
            return await GetBaseResponseMessage(result);
        }

        private async Task<BaseResponse> GetBaseResponseMessage(HttpResponseMessage response)
        {
            var contentString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BaseResponse>(contentString);
        }
    }
}