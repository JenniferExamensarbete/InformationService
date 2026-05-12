using System.ComponentModel.DataAnnotations;

namespace InformationService.Business.Models;

public class CreateInformationRequest
{
    [Required]
    public string Title { get; set; } = null!;

    [Required]
    public string Text { get; set; } = null!;

    public string? CreatedBy { get; set; }

    public string? CreatedByUserId { get; set; }
}