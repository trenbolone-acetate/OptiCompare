using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using OptiCompare.PhoneSpecs;

namespace OptiCompare.Models;

public class Phone
{
    [Key]
    public int Id { get; init; }
    [Required]
    [MaxLength(100)]
    public string? brandName { get; init; }
    [Required]
    [MaxLength(100)]
    public string? modelName { get; init; }
    public bool hasNetwork5GBands { get; init; }
    public BodyDimensions bodyDimensions { get; init; } = new();
    public DisplayDetails displayDetails { get; init; } = new();
    public PlatformDetails platformDetails { get; init; } = new();

    [MaxLength(200)]
    public string? storage { get; init; }
    public CameraDetails cameraDetails { get; init; } = new();
    public BatteryDetails batteryDetails { get; init; } = new();

    [MaxLength(50)]
    public string? price{ get; init; }
    [MaxLength(200)]
    public string? image{ get; init; }
}
