namespace OptiCompare.DTOs;

public class CameraDetailsDto
{
    public CameraDetailsDto()
    {
    }

    public CameraDetailsDto( string? mainCameraDetails, string? frontCameraDetails)
    {
        this.mainCameraDetails = mainCameraDetails;
        this.frontCameraDetails = frontCameraDetails;
    }
    public string? mainCameraDetails { get; init; }
    public string? frontCameraDetails { get; init; }
}