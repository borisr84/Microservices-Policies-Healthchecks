using Newtonsoft.Json;
using Polly;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PicturesConsumerClient
{
    class Program
    {
        static HttpClient _httpClient = new HttpClient();
        static async Task Main(string[] args)
        {
            var dataSourceUrls = new List<string>
            {
                "https://localhost:44309", //Local pictures
                "https://localhost:44332" //Remote pictures
            }.GetEnumerator();

            var fallbackPolicy = Policy<IList<byte[]>>
                .Handle<HttpRequestException>()
                .FallbackAsync(fallbackAction: async (ct) => {

                    if (dataSourceUrls.MoveNext())
                        return await GetData(dataSourceUrls.Current);

                    return null;
                });


            dataSourceUrls.MoveNext();
            var data = await fallbackPolicy.ExecuteAsync(async () => await GetData(dataSourceUrls.Current));
        }

        private async static Task<IList<byte[]>> GetData(string url)
        {
            var response = await _httpClient.GetAsync($"{url}/api/Pictures");
            var payload = JsonConvert.DeserializeObject<IList<byte[]>>(await response.Content.ReadAsStringAsync());
            return payload;
        }
    }
}
