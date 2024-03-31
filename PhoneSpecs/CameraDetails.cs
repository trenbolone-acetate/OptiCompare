using System.ComponentModel.DataAnnotations;

namespace OptiCompare.PhoneSpecs;

public class CameraDetails
{
    public CameraDetails()
    {
    }

    public CameraDetails(string? mainCameraDetails, string? frontCameraDetails)
    {
        this.mainCameraDetails = mainCameraDetails;
        this.frontCameraDetails = frontCameraDetails;
    }
    [MaxLength(200)]
    public string? mainCameraDetails { get; set; }
    [MaxLength(200)]
    public string? frontCameraDetails { get; set; }
}