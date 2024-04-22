using Newtonsoft.Json.Linq;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using OptiCompare.Models;

namespace OptiCompare
{
    public abstract class PhoneDetailsFetcher
    {
        private static string? _apiToken;

        public static void SetApiToken(string? token) => _apiToken = token;

        public static async Task<Phone?> CreateFromSearch(string searchedPhone)
        {
            var phoneId = await GetPhoneId(searchedPhone);
            if (phoneId == null)
            {
                return null;
            }

            var result = await GetPhoneDetailsById(phoneId);
            if (result == null)
            {
                return null;
            }

            var phone = Mappers.PhoneMapper.JsonToPhone(result);
            return phone;
        }

        private static async Task<string?> GetPhoneId(string phone)
        {
            using var client = new HttpClient();
            var requestUri = $"https://api.techspecs.io/v4/product/search?query={phone}";
            try
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("accept", "application/json");
                if (!string.IsNullOrEmpty(_apiToken))
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiToken}");
                }

                var response = await client.PostAsync(requestUri, null);
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(body);
                var items = (JArray?)json["data"]?["items"];
                var firstId = items?[0]?["product"]?["id"]?.ToString();
                return firstId;
            }
            catch (HttpRequestException)
            {
                Console.WriteLine("Failed to find searched phone's id through the API");
                return null;
            }
        }

        private static async Task<string?> GetPhoneDetailsById(string? phoneId)
        {
            using var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate });
            var requestUri = $"https://api.techspecs.io/v4/product/detail?productId={phoneId}";
            try
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("accept", "application/json");
                if (!string.IsNullOrEmpty(_apiToken))
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiToken}");
                }

                var response = await client.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(body);
                var firstProduct = (JObject?)json["data"]?["items"]?[0];
                return firstProduct?.ToString();
            }
            catch (HttpRequestException)
            {
                Console.WriteLine("Failed to find searched phone's details through the API");
                return null;
            }
        }
    }
}