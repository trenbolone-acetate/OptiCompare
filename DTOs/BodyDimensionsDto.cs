namespace OptiCompare.DTOs;

public class BodyDimensionsDto
{
    public BodyDimensionsDto()
    {
        
    }
    public BodyDimensionsDto(string? bodyWidth, string? bodyHeight, string? bodyThickness, string? bodyWeight)
    {
        this.bodyWidth = bodyWidth;
        this.bodyHeight = bodyHeight;
        this.bodyThickness = bodyThickness;
        this.bodyWeight = bodyWeight;
    }
    public string? bodyWidth { get; init; }
    public string? bodyHeight { get; init; }
    public string? bodyWeight { get; init; }
    public string? bodyThickness { get; init; }
}