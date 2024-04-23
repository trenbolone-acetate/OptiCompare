using System.Text.RegularExpressions;
using OptiCompare.DTOs;
using OptiCompare.Models;
using OptiCompare.PhoneSpecs;

namespace OptiCompare.Extensions;

public static class Extensions
{
    public static string GetConnectionString(this WebApplicationBuilder builder)
    {
        return builder.Configuration["phoneDB:ConStr"];
    }
    public static IEnumerable<int> GetNumbers(this string str)
    {
        var numbers = Regex
            .Matches(str, @"-?\d+")
            .Select(m => Int32.Parse(m.Value));
        return numbers;
    }
    public static void CopyDtoToPhone(this Phone target, PhoneDto phoneDto) {
        target.Id = phoneDto.Id;
        target.brandName = phoneDto.brandName;
        target.modelName = phoneDto.modelName;
        target.hasNetwork5GBands = phoneDto.hasNetwork5GBands;
        target.bodyDimensions = new BodyDimensions()
        {
            bodyWidth = phoneDto.bodyDimensions.bodyWidth,
            bodyHeight = phoneDto.bodyDimensions.bodyHeight,
            bodyThickness = phoneDto.bodyDimensions.bodyThickness,
            bodyWeight = phoneDto.bodyDimensions.bodyWeight
        };
        target.batteryDetails = new BatteryDetails()
        {
            batteryCapacity = phoneDto.batteryDetails.batteryCapacity,
            chargingSpeed = phoneDto.batteryDetails.chargingSpeed,
            batteryLifeTest = phoneDto.batteryDetails.batteryLifeTest
        };
        target.cameraDetails = new CameraDetails()
        {
            mainCameraDetails = phoneDto.cameraDetails.mainCameraDetails,
            frontCameraDetails = phoneDto.cameraDetails.frontCameraDetails
        };
        target.displayDetails = new DisplayDetails()
        {
            displayType = phoneDto.displayDetails.displayType,
            displaySize = phoneDto.displayDetails.displaySize,
            displayResolution = phoneDto.displayDetails.displayResolution,
            displayProtection = phoneDto.displayDetails.displayProtection
        };
        target.platformDetails = new PlatformDetails()
        {
            cpu = phoneDto.platformDetails.cpu,
            gpu = phoneDto.platformDetails.gpu,
            os = phoneDto.platformDetails.os,
            ram = phoneDto.platformDetails.ram
        };
        target.storage = phoneDto.storage;
        target.price = phoneDto.price;
        target.image = phoneDto.image;
    }
}