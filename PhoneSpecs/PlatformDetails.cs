using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OptiCompare.Models;

namespace OptiCompare.PhoneSpecs;

public class PlatformDetails
{
    public PlatformDetails()
    {
    }

    public PlatformDetails(string? cpu, string? gpu,  string? os, string? ram)
    {
        this.cpu = cpu;
        this.gpu = gpu;
        this.os = os;
        this.ram = ram;
    }
    [Key]
    public int Id { get; init; }
    [ForeignKey("PhoneId")]
    public int PhoneId { get; init; }
    public Phone Phone { get; set; } = null!;

    [MaxLength(200)]
    public string? cpu { get; init; }
    [MaxLength(200)]
    public string? gpu { get; init; }
    [MaxLength(200)]
    public string? os { get; init; }
    [MaxLength(200)]
    public string? ram { get; init; }
}