namespace OptiCompare.Models;

public class PlatformDetails
{
    public PlatformDetails()
    {
    }

    public PlatformDetails(string cpu, string gpu,  string os, string ram)
    {
        Cpu = cpu;
        Gpu = gpu;
        Os = os;
        RAM = ram;
    }

    public string Cpu { get; set; }
    public string Gpu { get; set; }
    public string Os { get; set; }
    public string RAM { get; set; }
}