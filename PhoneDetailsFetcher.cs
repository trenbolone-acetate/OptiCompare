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

        var phone = MapJsonToPhone(result);
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

    private static Phone MapJsonToPhone(string result)
    {
        try
        {
            var jsonData = JObject.Parse(result);
            var productData = jsonData["product"];
            var keyAspects = jsonData["key_aspects"];
            var inside = jsonData["inside"];
            var design = jsonData["design"];
            var display = jsonData["display"];
            var camera = jsonData["camera"];
            var thumbnail = jsonData["image"]?["large"];
            Phone phone = new Phone()
            {
                brandName = productData?["brand"]?.ToString() ?? "undefined",
                modelName = productData?["model"]?.ToString() ?? "undefined",
                hasNetwork5GBands = keyAspects?["wireless_&_cellular"]?.ToString().Contains("5G") ?? false,
                bodyDimensions = new BodyDimensions(design?["body"]?["width"]?.ToString() ?? "undefined",
                    design?["body"]?["height"]?.ToString(),
                    design?["body"]?["thickness"]?.ToString() ?? "undefined",
                    design?["body"]?["weight"]?.ToString() ?? "undefined"),
                displayDetails = new DisplayDetails(display?["type"]?.ToString() ?? "undefined",
                    display?["diagonal"]?.ToString() ?? "undefined",
                    display?["resolution_(h_x_w)"]?.ToString() ?? "undefined",
                    display?["glass"]?.ToString() ?? "undefined"),
                platformDetails = new PlatformDetails(inside?["processor"]?["cpu"]?.ToString() ?? "undefined",
                    inside?["processor"]?["gpu"]?.ToString() ?? "undefined",
                    inside?["software"]?["os_version"]?.ToString() ?? "undefined",
                    inside?["ram"]?["capacity"]?.ToString() ?? "undefined"),
                storage = inside?["storage"]?["capacity"]?.ToString() ?? "undefined",
                cameraDetails = new CameraDetails(
                    $"{camera?["back_camera"]?["resolution"]}, {camera?["back_camera"]?["resolution_(h_x_w)"]} {camera?["back_camera"]?["aperture_(w)"]}",
                    $"{camera?[$"front_camera"]?["resolution"]}, {camera?["front_camera"]?["resolution_(h_x_w)"]} {camera?["front_camera"]?["aperture_(w)"]}"),
                batteryDetails = new BatteryDetails(inside?["battery"]?["capacity"]?.ToString() ?? "undefined",
                    $"{inside?["battery"]?["charging_power"]} wired, {inside?["battery"]?["wireless_charging_power"]} wireless",
                    "not tested yet"),
                price = jsonData["price"]?["msrp"]?.ToString() ?? "undefined",
                image = thumbnail?.ToString() ?? "https://cdn-icons-png.flaticon.com/512/244/244210.png"
            };
            return phone;
        }
        catch (JsonException e)
        {
            throw new ApplicationException("Error mapping JSON to Phone.", e);
        }
    }
}