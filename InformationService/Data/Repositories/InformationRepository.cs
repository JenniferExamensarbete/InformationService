using InformationService.Data.Contexts;
using InformationService.Data.Entities;

namespace InformationService.Data.Repositories;

public class InformationRepository(InformationDbContext context)
    : BaseRepository<InformationEntity>(context), IInformationRepository
{
}