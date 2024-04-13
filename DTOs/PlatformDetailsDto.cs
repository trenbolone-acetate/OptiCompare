namespace OptiCompare.DTOs;

public class PlatformDetailsDto
{
    public PlatformDetailsDto()
    {
    }

    public PlatformDetailsDto(string? cpu, string? gpu,  string? os, string? ram)
    {
        this.cpu = cpu;
        this.gpu = gpu;
        this.os = os;
        this.ram = ram;
    }
    public string? cpu { get; init; }
    public string? gpu { get; init; }
    public string? os { get; init; }
    public string? ram { get; init; }
}