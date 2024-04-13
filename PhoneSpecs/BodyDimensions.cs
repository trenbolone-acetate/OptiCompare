using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OptiCompare.Models;

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
    [Key]
    public int Id { get; init; }
    [ForeignKey("PhoneId")]
    public int PhoneId { get; init; }
    public Phone Phone { get; set; }
    [MaxLength(200)]
    public string? bodyWidth { get; init; }
    [MaxLength(200)]
    public string? bodyHeight { get; init; }
    [MaxLength(200)]
    public string? bodyWeight { get; init; }
    [MaxLength(200)]
    public string? bodyThickness { get; init; }
}