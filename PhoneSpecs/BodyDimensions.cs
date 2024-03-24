namespace OptiCompare.Models;

public class BodyDimensions
{
    public BodyDimensions()
    {
    }

    public BodyDimensions(string? bodyWidth, string? bodyHeight, string? bodyThickness, string? bodyWeight)
    {
        this.bodyWidth = bodyWidth;
        this.bodyHeight = bodyHeight;
        this.bodyThickness = bodyThickness;
        this.bodyWeight = bodyWeight;
    }

    public string? bodyWidth { get; set; }
    public string? bodyHeight { get; set; }
    public string? bodyWeight { get; set; }
    public string? bodyThickness { get; set; }
}