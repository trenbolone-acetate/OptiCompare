namespace OptiCompare.Models;

public class PlatformDetails
{
    public PlatformDetails()
    {
    }

    public PlatformDetails(string ram, string cpu, string gpu, string os)
    {
        RAM = ram;
        Cpu = cpu;
        Gpu = gpu;
        Os = os;
    }

    public string RAM { get; set; }
    public string Cpu { get; set; }
    public string Gpu { get; set; }
    public string Os { get; set; }
}