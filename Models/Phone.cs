using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using OptiCompare.PhoneSpecs;

namespace OptiCompare.Models;

public class Phone
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string? brandName { get; set; }
    [Required]
    [MaxLength(100)]
    public string? modelName { get; set; }
    public bool hasNetwork5GBands { get; set; }
    public BodyDimensions bodyDimensions { get; set; } = new();
    public DisplayDetails displayDetails { get; set; } = new();    
    public PlatformDetails platformDetails { get; set; } = new();
    [Required]
    [MaxLength(200)]
    public string? storage { get; set; }
    public CameraDetails cameraDetails { get; set; } = new();
    public BatteryDetails batteryDetails { get; set; } = new();
    [Required]
    [MaxLength(50)]
    public string? price{ get; set; }
    [MaxLength(200)]
    public string? image{ get; set; }
}
