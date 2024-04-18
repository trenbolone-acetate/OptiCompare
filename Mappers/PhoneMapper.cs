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

    public static Phone JsonToPhone(string result)
    {
        try
        {
            var jsonData = JObject.Parse(result);
            Phone phone = new Phone()
            {
                brandName = jsonData.SelectToken("product.brand")?.ToString() ?? "undefined",
                modelName = jsonData.SelectToken("product.model")?.ToString() ?? "undefined",
                hasNetwork5GBands =
                    jsonData.SelectToken("key_aspects.wireless_&_cellular")?.ToString().Contains("5G") ?? false,
                bodyDimensions = CreateBodyDimensions(jsonData.SelectToken("design.body")),
                displayDetails = CreateDisplayDetails(jsonData.SelectToken("display")),
                platformDetails = CreatePlatformDetails(jsonData.SelectToken("inside")),
                storage = jsonData.SelectToken("inside.storage.capacity")?.ToString() ?? "undefined",
                cameraDetails = CreateCameraDetails(jsonData.SelectToken("camera")),
                batteryDetails = CreateBatteryDetails(jsonData.SelectToken("inside.battery")),
                price = jsonData.SelectToken("price.msrp")?.ToString() ?? "undefined",
                image = jsonData.SelectToken("image.large")?.ToString() ??
                        "https://cdn-icons-png.flaticon.com/512/244/244210.png"
            };
            return phone;
        }
        catch (JsonException e)
        {
            throw new ApplicationException("Error mapping JSON to Phone.", e);
        }
    }

    private static BodyDimensions CreateBodyDimensions(JToken bodyData)
    {
        return new BodyDimensions(bodyData?["width"]?.ToString() ?? "undefined",
            bodyData?["height"]?.ToString(),
            bodyData?["thickness"]?.ToString() ?? "undefined",
            bodyData?["weight"]?.ToString() ?? "undefined");
    }

    private static DisplayDetails CreateDisplayDetails(JToken displayData)
    {
        return new DisplayDetails(displayData?["type"]?.ToString() ?? "undefined",
            displayData?["diagonal"]?.ToString() ?? "undefined",
            displayData?["resolution_(h_x_w)"]?.ToString() ?? "undefined",
            displayData?["glass"]?.ToString() ?? "undefined");
    }

    private static PlatformDetails CreatePlatformDetails(JToken insideData)
    {
        return new PlatformDetails(insideData?["processor"]["cpu"]?.ToString() ?? "undefined",
            insideData?["processor"]["gpu"]?.ToString() ?? "undefined",
            insideData?["software"]["os_version"]?.ToString() ?? "undefined",
            insideData?["ram"]["capacity"]?.ToString() ?? "undefined");
    }

    private static CameraDetails CreateCameraDetails(JToken cameraData)
    {
        return new CameraDetails(
            $"{cameraData?["back_camera"]["resolution"]}, {cameraData?["back_camera"]["resolution_(h_x_w)"]} {cameraData?["back_camera"]["aperture_(w)"]}",
            $"{cameraData?["front_camera"]["resolution"]}, {cameraData?["front_camera"]["resolution_(h_x_w)"]} {cameraData?["front_camera"]["aperture_(w)"]}");
    }

    private static BatteryDetails CreateBatteryDetails(JToken batteryData)
    {
        return new BatteryDetails(batteryData?["capacity"]?.ToString() ?? "undefined",
            $"{batteryData?["charging_power"]} wired, {batteryData?["wireless_charging_power"]} wireless",
            "not tested yet");
    }
}