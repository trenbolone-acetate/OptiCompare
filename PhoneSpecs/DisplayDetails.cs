using System.ComponentModel.DataAnnotations;

namespace OptiCompare.PhoneSpecs;

public class DisplayDetails
{
    public DisplayDetails()
    {
    }

    public DisplayDetails(string? displayType, string? displaySize, string? displayResolution, string? displayProtection)
    {
        this.displayType = displayType;
        this.displaySize = displaySize;
        this.displayResolution = displayResolution;
        this.displayProtection = displayProtection;
    }
    [MaxLength(200)]
    public string? displayType { get; init; }
    [MaxLength(200)]
    public string? displaySize { get; init; }
    [MaxLength(50)]
    public string? displayResolution { get; init; }
    [MaxLength(100)]
    public string? displayProtection { get; init; }
}