using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OptiCompare.Models;

namespace OptiCompare.PhoneSpecs;

public class BatteryDetails
{
    public BatteryDetails()
    {
    }

    public BatteryDetails(string? batteryCapacity, string? chargingSpeed, string? batteryLifeTest)
    {
        this.batteryCapacity = batteryCapacity;
        this.chargingSpeed = chargingSpeed;
        this.batteryLifeTest = batteryLifeTest;
    }
    [Key]
    public int Id { get; init; }
    [ForeignKey("PhoneId")]
    public int PhoneId { get; init; }
    public Phone Phone { get; set; }
    public string? batteryCapacity { get; init; }
    public string? chargingSpeed { get; init; }
    public string? batteryLifeTest{ get; init; }
}