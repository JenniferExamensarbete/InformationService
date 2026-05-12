using InformationService.Business.Models;

namespace InformationService.Business.Interfaces;

public interface IInformationService
{
    Task<InformationResult> CreateInformationAsync(CreateInformationRequest request);
    Task<InformationResult<Information?>> GetInformationAsync(string id);
    Task<InformationResult<IEnumerable<Information>>> GetAllInformationAsync();
    Task<InformationResult> UpdateInformationAsync(string id, UpdateInformationRequest request);
    Task<InformationResult> DeleteInformationAsync(string id);
}