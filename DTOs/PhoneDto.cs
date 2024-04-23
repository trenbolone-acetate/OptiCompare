using OptiCompare.Models;

namespace OptiCompare.DTOs;

public class PhoneDto
{
    public int Id { get; init; }
    public string? brandName { get; init; }
    public string? modelName { get; init; }
    public bool hasNetwork5GBands { get; init; }
    public BodyDimensionsDto bodyDimensions { get; init; } = new();
    public DisplayDetailsDto displayDetails { get; init; } = new();    
    public PlatformDetailsDto platformDetails { get; init; } = new();
    public string? storage { get; init; }
    public CameraDetailsDto cameraDetails { get; init; } = new();
    public BatteryDetailsDto batteryDetails { get; init; } = new();
    public string? price{ get; init; }
    public string? image{ get; init; }
}