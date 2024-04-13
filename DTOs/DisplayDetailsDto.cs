namespace OptiCompare.DTOs;

public class DisplayDetailsDto
{
    public DisplayDetailsDto()
    {
    }

    public DisplayDetailsDto(string? displayType, string? displaySize, string? displayResolution, string? displayProtection)
    {
        this.displayType = displayType;
        this.displaySize = displaySize;
        this.displayResolution = displayResolution;
        this.displayProtection = displayProtection;
    }
    public string? displayType { get; init; }
    public string? displaySize { get; init; }
    public string? displayResolution { get; init; }
    public string? displayProtection { get; init; }
}