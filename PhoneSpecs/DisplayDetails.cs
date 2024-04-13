using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OptiCompare.Models;

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
    [Key]
    public int Id { get; init; }
    [ForeignKey("PhoneId")]
    public int PhoneId { get; init; }
    public Phone Phone { get; set; }
    [MaxLength(200)]
    public string? displayType { get; init; }
    [MaxLength(200)]
    public string? displaySize { get; init; }
    [MaxLength(50)]
    public string? displayResolution { get; init; }
    [MaxLength(100)]
    public string? displayProtection { get; init; }
}