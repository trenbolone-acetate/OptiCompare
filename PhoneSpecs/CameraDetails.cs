using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OptiCompare.Models;

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
    [Key]
    public int Id { get; init; }
    [ForeignKey("PhoneId")]
    public int PhoneId { get; init; }
    public Phone Phone { get; set; }
    [MaxLength(200)]
    public string? mainCameraDetails { get; init; }
    [MaxLength(200)]
    public string? frontCameraDetails { get; init; }
}