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

        public async Task<BaseResponse<object>> CreatePlayer(string playerName)
        {
            var path = _configurator.CreatePlayer(playerName);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, path);

            return await BasePost<object>(requestMessage);
        }

        public async Task<BaseResponse<PlayerModel>> Login(string playerName)
        {
            var path = _configurator.Login(playerName);
            return await BaseGet<BaseResponse<PlayerModel>>(path);
        }

        public async Task<BaseResponse<object>> PostMatchResult(MatchResult matchResult)
        {
            var path = _configurator.PostMatchResult();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, path);

            request.Headers.Add("Accept", "application/json");
            request.Properties.Add("Content-Type", "application/json");

            var serializedContent = JsonConvert.SerializeObject(matchResult);
            request.Content = new StringContent(serializedContent, Encoding.UTF8, "application/json");

            return await BasePost<object>(request);
        }

        // consider reflection here
        private async Task<BaseResponse<T>> BasePost<T>(HttpRequestMessage request) where T : class
        {
            HttpResponseMessage result = await _client.SendAsync(request);
            return await GetDeserializedContent<BaseResponse<T>>(result);
        }

        private async Task<T> BaseGet<T>(string path) where T : class
        {
            HttpResponseMessage result = await _client.GetAsync(path);
            return await GetDeserializedContent<T>(result);
        }

        private async Task<T> GetDeserializedContent<T>(HttpResponseMessage response) where T : class
        {
            var contentString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(contentString);
        }
    }
}