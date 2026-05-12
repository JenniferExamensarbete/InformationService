using InformationService.Business.Interfaces;
using InformationService.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace InformationService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InformationController(IInformationService informationService) : ControllerBase
{
    private readonly IInformationService _informationService = informationService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _informationService.GetAllInformationAsync();

        return result.Success
            ? Ok(result)
            : StatusCode(500, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var result = await _informationService.GetInformationAsync(id);

        return result.Success
            ? Ok(result)
            : NotFound(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateInformationRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _informationService.CreateInformationAsync(request);

        return result.Success
            ? Ok(result)
            : BadRequest(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, UpdateInformationRequest request)
    {
        var result = await _informationService.UpdateInformationAsync(id, request);

        return result.Success
            ? Ok(result)
            : NotFound(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _informationService.DeleteInformationAsync(id);

        return result.Success
            ? Ok(result)
            : NotFound(result);
    }
}
