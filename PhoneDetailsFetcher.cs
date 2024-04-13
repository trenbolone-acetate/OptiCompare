using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using OptiCompare.Models;
using System.Net;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OptiCompare.PhoneSpecs;

namespace OptiCompare;

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
        return await Task.FromResult(phone);
    }


    private static async Task<string?> GetPhoneId(string phone)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri($"https://api.techspecs.io/v4/product/search?query={phone}"),
            Headers =
            {
                { "accept", "application/json" },
                { "Authorization", $"Bearer {_apiToken}" },
            },
        };
        try
        {
            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(body);
            JArray? items = (JArray)json["data"]?["items"]!;
            string? firstId = (string)items[0]["product"]?["id"]!;
            return firstId;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("Failed to find searched phone's id through the API");
            return null;
        }
    }

    private static async Task<string?> GetPhoneDetailsById(string? phoneId)
    {
        var clientHandler = new HttpClientHandler
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
        };
        var client = new HttpClient(clientHandler);
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://api.techspecs.io/v4/product/detail?productId={phoneId}"),
            Headers =
            {
                { "accept", "application/json" },
                { "Authorization", $"Bearer {_apiToken}" },
            },
        };
        using var response = await client.SendAsync(request);
        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("Failed to find searched phone's details through the API");
            return null;
        }

        var body = await response.Content.ReadAsStringAsync();
        JObject json = JObject.Parse(body);
        JObject? firstProduct = (JObject)json["data"]?["items"]?[0]!;
        return firstProduct.ToString();
    }

    
}