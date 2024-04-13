namespace OptiCompare.DTOs;

public class BatteryDetailsDto
{
    public BatteryDetailsDto()
    {
    }

    public BatteryDetailsDto( string? batteryCapacity, string? chargingSpeed, string? batteryLifeTest)
    {
        this.batteryCapacity = batteryCapacity;
        this.chargingSpeed = chargingSpeed;
        this.batteryLifeTest = batteryLifeTest;
    }
    public string? batteryCapacity { get; init; }
    public string? chargingSpeed { get; init; }
    public string? batteryLifeTest{ get; init; }
}