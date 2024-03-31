using System.ComponentModel.DataAnnotations;

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
    [MaxLength(200)]
    public string? cpu { get; init; }
    [MaxLength(200)]
    public string? gpu { get; init; }
    [MaxLength(200)]
    public string? os { get; init; }
    [MaxLength(200)]
    public string? ram { get; init; }
}