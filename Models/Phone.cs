using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Query;

namespace OptiCompare.Models;

public class Phone
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string brandName { get; set; }
    [Required]
    public string modelName { get; set; }
    public bool hasNetwork5GBands { get; set; }
    public string bodyWidth { get; set; }
    public string? bodyHeight{ get; set; }
    public string bodyThickness{ get; set; }
    public string bodyWeight{ get; set; }
    public string displayType { get; set; }
    public string displaySize { get; set; }
    public string displayResolution { get; set; }
    public string Cpu { get; set; }
    public string Gpu { get; set; }
    public string Os { get; set; }
    public string RAM { get; set; }
    public string storage { get; set; }
    public string mainCameraDetails{ get; set; }
    public string frontCameraDetails{ get; set; }
    public string batteryCapacity{ get; set; }
    public string chargingSpeed{ get; set; }
    public string batteryLifeTest{ get; set; }
    public string price{ get; set; }
    public string? image{ get; set; }
}
