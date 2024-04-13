using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OptiCompare.DTOs;
using OptiCompare.Models;
using OptiCompare.PhoneSpecs;

namespace OptiCompare.Mappers;

public static class PhoneMapper
{
    public static PhoneDto ToPhoneDto(this Phone phone)
    {
        var phoneDto = new PhoneDto
        {
            Id = phone.Id,
            brandName = phone.brandName,
            modelName = phone.modelName,
            hasNetwork5GBands = phone.hasNetwork5GBands,
            bodyDimensions = new BodyDimensionsDto
            {
                bodyWidth = phone.bodyDimensions.bodyWidth,
                bodyHeight = phone.bodyDimensions.bodyHeight,
                bodyWeight = phone.bodyDimensions.bodyWeight,
                bodyThickness = phone.bodyDimensions.bodyThickness,
            },
            displayDetails = new DisplayDetailsDto
            {
                displayType = phone.displayDetails.displayType,
                displaySize = phone.displayDetails.displaySize,
                displayResolution = phone.displayDetails.displayResolution,
                displayProtection = phone.displayDetails.displayProtection,
            },
            platformDetails = new PlatformDetailsDto
            {
                cpu = phone.platformDetails.cpu,
                gpu = phone.platformDetails.gpu,
                os = phone.platformDetails.os,
                ram = phone.platformDetails.ram,
            },
            storage = phone.storage,
            cameraDetails = new CameraDetailsDto
            {
                mainCameraDetails = phone.cameraDetails.mainCameraDetails,
                frontCameraDetails = phone.cameraDetails.frontCameraDetails
            },
            batteryDetails = new BatteryDetailsDto
            {
                batteryCapacity = phone.batteryDetails.batteryCapacity,
                chargingSpeed = phone.batteryDetails.chargingSpeed,
                batteryLifeTest = phone.batteryDetails.batteryLifeTest
            },
            price = phone.price,
            image = phone.image
        };
        return phoneDto;
    }
    public static Phone ToPhone(this PhoneDto phoneDto)
        {
            return new Phone
            {
                Id = phoneDto.Id,
                brandName = phoneDto.brandName,
                modelName = phoneDto.modelName,
                hasNetwork5GBands = phoneDto.hasNetwork5GBands,
                bodyDimensions = new BodyDimensions
                {
                    Id = phoneDto.Id,
                    PhoneId = phoneDto.Id,
                    bodyWidth = phoneDto.bodyDimensions.bodyWidth,
                    bodyHeight = phoneDto.bodyDimensions.bodyHeight,
                    bodyWeight = phoneDto.bodyDimensions.bodyWeight,
                    bodyThickness = phoneDto.bodyDimensions.bodyThickness,
                },
                displayDetails = new DisplayDetails
                {
                    Id = phoneDto.Id,
                    PhoneId = phoneDto.Id,
                    displayType = phoneDto.displayDetails.displayType,
                    displaySize = phoneDto.displayDetails.displaySize,
                    displayResolution = phoneDto.displayDetails.displayResolution,
                    displayProtection = phoneDto.displayDetails.displayProtection,
                },
                platformDetails = new PlatformDetails
                {
                    Id = phoneDto.Id,
                    PhoneId = phoneDto.Id,
                    cpu = phoneDto.platformDetails.cpu,
                    gpu = phoneDto.platformDetails.gpu,
                    os = phoneDto.platformDetails.os,
                    ram = phoneDto.platformDetails.ram,
                },
                storage = phoneDto.storage,
                cameraDetails = new CameraDetails
                {
                    Id = phoneDto.Id,
                    PhoneId = phoneDto.Id,
                    mainCameraDetails = phoneDto.cameraDetails.mainCameraDetails,
                    frontCameraDetails = phoneDto.cameraDetails.frontCameraDetails
                },
                batteryDetails = new BatteryDetails
                {
                    Id = phoneDto.Id,
                    PhoneId = phoneDto.Id,
                    batteryCapacity = phoneDto.batteryDetails.batteryCapacity,
                    chargingSpeed = phoneDto.batteryDetails.chargingSpeed,
                    batteryLifeTest = phoneDto.batteryDetails.batteryLifeTest
                },
                price = phoneDto.price,
                image = phoneDto.image
            };
        }
    public  static Phone JsonToPhone(string result)
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