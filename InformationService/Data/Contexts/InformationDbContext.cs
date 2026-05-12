using InformationService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace InformationService.Data.Contexts;

public class InformationDbContext(DbContextOptions<InformationDbContext> options)
    : DbContext(options)
{
    public DbSet<InformationEntity> InformationItems { get; set; } = null!;
}