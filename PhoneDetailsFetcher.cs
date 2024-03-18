using MySqlX.XDevAPI.Common;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using OptiCompare.Models;
using System.Net;

namespace OptiCompare;

public static class PhoneDetailsFetcher
{
    public static async Task<Phone> CreateFromSearch(string searchedPhone)
    {
        var result = GetPhoneDetailsById(GetPhoneId(searchedPhone).Result).Result;
        
        var jsonData = JObject.Parse(result);
        var productData = jsonData["product"];
        var keyAspects = jsonData["key_aspects"];
        var inside = jsonData["inside"];
        var design = jsonData["design"];
        var display = jsonData["display"];
        var camera = jsonData["camera"];
        Phone phone = new Phone
            {
                brandName = productData?["brand"]?.ToString() ?? "undefined",
                modelName = productData?["model"]?.ToString() ?? "undefined",
                hasNetwork5GBands = keyAspects?["wireless_&_cellular"]?.ToString().Contains("5G") ?? false,
                bodyWidth = design?["body"]?["width"]?.ToString() ?? "undefined",
                bodyHeight = design?["body"]?["height"]?.ToString(),
                bodyThickness = design?["body"]?["thickness"]?.ToString() ?? "undefined",
                bodyWeight = design?["body"]?["weight"]?.ToString() ?? "undefined",
                displayType = display?["type"]?.ToString() ?? "undefined",
                displaySize = display?["diagonal"]?.ToString() ?? "undefined",
                displayResolution = display?["resolution_(h_x_w)"]?.ToString() ?? "undefined",
                Cpu = inside?["processor"]?["cpu"]?.ToString() ?? "undefined",
                Gpu = inside?["processor"]?["gpu"]?.ToString() ?? "undefined",
                Os = inside?["software"]?["os_version"]?.ToString() ?? "undefined",
                RAM = inside?["ram"]?["capacity"]?.ToString() ?? "undefined",
                storage = inside?["storage"]?["capacity"]?.ToString() ?? "undefined",
                mainCameraDetails = $"{camera?["back_camera"]?["resolution"]}, {camera?["back_camera"]?["resolution_(h_x_w)"]} {camera?["back_camera"]?["aperture_(w)"]}" ?? "undefined",
                frontCameraDetails = $"{camera?[$"front_camera"]?["resolution"]}, {camera?["front_camera"]?["resolution_(h_x_w)"]} {camera?["front_camera"]?["aperture_(w)"]}" ?? "undefined",
                batteryCapacity = inside?["battery"]?["capacity"]?.ToString() ?? "undefined",
                chargingSpeed = $"{inside?["battery"]?["charging_power"]} wired, {inside?["battery"]?["wireless_charging_power"]} wireless" ?? "undefined",
                price = jsonData["price"]?["msrp"]?.ToString() ?? "undefined", 
                batteryLifeTest = "not tested yet"
            };
            return phone;
    }
static async Task<string> GetPhoneId(string phone)
{
    var client = new HttpClient();
    var request = new HttpRequestMessage
    {
        Method = HttpMethod.Post,
        RequestUri = new Uri($"https://api.techspecs.io/v4/product/search?query={phone}"),
        Headers =
            {
                { "accept", "application/json" },
                { "Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6ImN1c19QNTlheUpqWXdRb3BZRSIsIm1vZXNpZlByaWNpbmdJZCI6InByaWNlXzFNUXF5dkJESWxQbVVQcE1NNWc2RmVvbyIsImlhdCI6MTcwMTA3MDI0N30.GyunOW3py18gsmdS29tlCuI4MkE4XiIogl_Cg5pAY5I" },
            },
    };
    using (var response = await client.SendAsync(request))
    {
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();
        JObject json = JObject.Parse(body);
        JArray items = (JArray)json["data"]["items"];
        string firstId = (string)items[0]["product"]["id"];
        return firstId;
    }
}

static async Task<string> GetPhoneDetailsById(string phoneId)
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
                { "Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6ImN1c19QNTlheUpqWXdRb3BZRSIsIm1vZXNpZlByaWNpbmdJZCI6InByaWNlXzFNUXF5dkJESWxQbVVQcE1NNWc2RmVvbyIsImlhdCI6MTcwMTA3MDI0N30.GyunOW3py18gsmdS29tlCuI4MkE4XiIogl_Cg5pAY5I" },
            },
    };
    using (var response = await client.SendAsync(request))
    {
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();
        JObject json = JObject.Parse(body);
        JObject firstProduct = (JObject)json["data"]["items"][0];
        return firstProduct.ToString();
    }
}
}
