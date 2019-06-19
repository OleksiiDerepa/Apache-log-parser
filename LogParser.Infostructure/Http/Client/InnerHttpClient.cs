using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace LogParser.Infrastructure.Http.Client
{
    public class InnerHttpClient : IInnerHttpClient
    {       
        private readonly HttpClient _client =  new HttpClient();

        public async Task<string> Execute(string request)
        {
            using (HttpResponseMessage response = await _client.GetAsync(request))
            {
                string responseString = await response.Content.ReadAsStringAsync();

                return responseString;
            }

        }

        public async Task<TOut> Execute<TOut>(string request)
        {
            string responseString = await Execute(request);

            return JsonConvert.DeserializeObject<TOut>(responseString);
        }
    }
}