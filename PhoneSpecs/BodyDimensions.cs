using System.ComponentModel.DataAnnotations;

namespace OptiCompare.PhoneSpecs;

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
    [MaxLength(200)]
    public string? bodyWidth { get; init; }
    [MaxLength(200)]
    public string? bodyHeight { get; init; }
    [MaxLength(200)]
    public string? bodyWeight { get; init; }
    [MaxLength(200)]
    public string? bodyThickness { get; init; }
}