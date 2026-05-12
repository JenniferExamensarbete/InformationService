using InformationService.Business.Interfaces;
using InformationService.Business.Models;
using InformationService.Data.Entities;
using InformationService.Data.Repositories;

namespace InformationService.Business.Services;

public class InformationService(IInformationRepository informationRepository) : IInformationService
{
    private readonly IInformationRepository _informationRepository = informationRepository;

    public async Task<InformationResult> CreateInformationAsync(CreateInformationRequest request)
    {
        var entity = new InformationEntity
        {
            Title = request.Title,
            Text = request.Text,
            CreatedBy = request.CreatedBy,
            CreatedByUserId = request.CreatedByUserId
        };

        var result = await _informationRepository.AddAsync(entity);

        return result.Success
            ? new InformationResult { Success = true }
            : new InformationResult { Success = false, Error = result.Error };
    }

    public async Task<InformationResult<IEnumerable<Information>>> GetAllInformationAsync()
    {
        var result = await _informationRepository.GetAllAsync();

        if (!result.Success || result.Result == null)
        {
            return new InformationResult<IEnumerable<Information>>
            {
                Success = false,
                Error = result.Error
            };
        }

        var items = result.Result
            .OrderByDescending(x => x.CreatedAt)
            .Select(MapToInformation);

        return new InformationResult<IEnumerable<Information>>
        {
            Success = true,
            Result = items
        };
    }

    public async Task<InformationResult<Information?>> GetInformationAsync(string id)
    {
        var result = await _informationRepository.GetAsync(x => x.Id == id);

        if (!result.Success || result.Result == null)
        {
            return new InformationResult<Information?>
            {
                Success = false,
                Error = result.Error ?? "Information not found."
            };
        }

        return new InformationResult<Information?>
        {
            Success = true,
            Result = MapToInformation(result.Result)
        };
    }

    public async Task<InformationResult> UpdateInformationAsync(string id, UpdateInformationRequest request)
    {
        var result = await _informationRepository.GetAsync(x => x.Id == id);

        if (!result.Success || result.Result == null)
        {
            return new InformationResult
            {
                Success = false,
                Error = result.Error ?? "Information not found."
            };
        }

        var entity = result.Result;

        entity.Title = request.Title ?? entity.Title;
        entity.Text = request.Text ?? entity.Text;
        entity.UpdatedAt = DateTime.UtcNow;

        var updateResult = await _informationRepository.UpdateAsync(entity);

        return updateResult.Success
            ? new InformationResult { Success = true }
            : new InformationResult { Success = false, Error = updateResult.Error };
    }

    public async Task<InformationResult> DeleteInformationAsync(string id)
    {
        var result = await _informationRepository.GetAsync(x => x.Id == id);

        if (!result.Success || result.Result == null)
        {
            return new InformationResult
            {
                Success = false,
                Error = result.Error ?? "Information not found."
            };
        }

        var deleteResult = await _informationRepository.DeleteAsync(result.Result);

        return deleteResult.Success
            ? new InformationResult { Success = true }
            : new InformationResult { Success = false, Error = deleteResult.Error };
    }

    private static Information MapToInformation(InformationEntity entity)
    {
        return new Information
        {
            Id = entity.Id,
            Title = entity.Title,
            Text = entity.Text,
            CreatedBy = entity.CreatedBy,
            CreatedByUserId = entity.CreatedByUserId,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}