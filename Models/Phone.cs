using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace OptiCompare.Models;

public class Phone
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string brandName { get; set; }
    [Required]
    public string modelName { get; set; }
    public bool hasNetwork5GBands { get; set; }
    public BodyDimensions BodyDimensions { get; set; }
    public DisplayDetails DisplayDetails { get; set; }
    public PlatformDetails PlatformDetails { get; set; }
    public string? storage { get; set; }
    public CameraDetails CameraDetails { get; set; }
    public BatteryDetails BatteryDetails { get; set; }
    public string? price{ get; set; }
    public string? image{ get; set; }
    public Phone()
    {
        BodyDimensions = new BodyDimensions();
        DisplayDetails = new DisplayDetails();
        PlatformDetails = new PlatformDetails();
        CameraDetails = new CameraDetails();
        BatteryDetails = new BatteryDetails();
    }
}
