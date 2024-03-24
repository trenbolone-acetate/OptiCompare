namespace OptiCompare.Models;

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

    public string? batteryCapacity { get; set; }
    public string? chargingSpeed { get; set; }
    public string? batteryLifeTest{ get; set; }
}