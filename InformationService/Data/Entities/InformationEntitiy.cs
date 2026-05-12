using System.ComponentModel.DataAnnotations;

namespace InformationService.Data.Entities;

public class InformationEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public string Title { get; set; } = null!;

    [Required]
    public string Text { get; set; } = null!;

    public string? CreatedBy { get; set; }

    public string? CreatedByUserId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}