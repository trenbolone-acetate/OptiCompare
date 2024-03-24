namespace OptiCompare.Models;

public class DisplayDetails
{
    public DisplayDetails()
    {
    }

    public DisplayDetails(string displayType, string displaySize, string displayResolution, string displayProtection)
    {
        this.displayType = displayType;
        this.displaySize = displaySize;
        this.displayResolution = displayResolution;
        this.displayProtection = displayProtection;
    }

    public string displayType { get; set; }
    public string displaySize { get; set; }
    public string displayResolution { get; set; }
    public string displayProtection { get; set; }
}