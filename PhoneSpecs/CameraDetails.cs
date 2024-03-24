namespace OptiCompare.Models;

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

    public string? mainCameraDetails { get; set; }
    public string? frontCameraDetails { get; set; }
}