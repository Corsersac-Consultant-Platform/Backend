using System.Net.Mime;
using Application.Filters;
using AutoMapper;
using Domain.Interfaces;
using Interface.DTO.Request;
using Interface.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using Support.Models;

namespace Application.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
public class UsageController(IUsageService usageService, IMapper mapper) : ControllerBase
{
    [HttpPost]
    [CustomAuthorize("ADMIN", "TESTER")]
    public async Task<IActionResult> CreateUsage([FromBody] UsageRequestDTO usageRequest)
    {
        var usage = mapper.Map<UsageRequestDTO, Usage>(usageRequest);
        var result = await usageService.CreateUsage(usage);
        return Ok(result);
    }
    
    [HttpGet("range")]
    [CustomAuthorize("ADMIN", "TESTER", "DEFAULT")]
    public async Task<IActionResult> GetUsageByRange([FromQuery] DateTime start, [FromQuery] DateTime end)
    {
        var usages = await usageService.GetUsagesByRange(start, end);
        var usagesResponse = mapper.Map<IEnumerable<Usage>, IEnumerable<UsageResponseDTO>>(usages);
        return Ok(usagesResponse);
    }
    
    [HttpGet("date")]
    [CustomAuthorize("ADMIN", "TESTER", "DEFAULT")]
    public async Task<IActionResult> GetUsageByDate([FromQuery] DateTime date)
    {
        var usages = await usageService.GetUsagesByDate(date);
        var usagesResponse = mapper.Map<IEnumerable<Usage>, IEnumerable<UsageResponseDTO>>(usages);
        return Ok(usagesResponse);
    }
    
    [HttpGet("year")]
    [CustomAuthorize("ADMIN", "TESTER", "DEFAULT")]
    public async Task<IActionResult> CountTotalUsagesByCenterAndYear([FromQuery] int year)
    {
        var usages = await usageService.CountTotalUsagesByYear( year);
        return Ok(usages);
    }
    
    [HttpGet("vehicle")]
    [CustomAuthorize("ADMIN", "TESTER", "DEFAULT")]
    public async Task<IActionResult> GetUsageByVehicleIdentifier([FromQuery] string vehicleIdentifier)
    {
        var usages = await usageService.GetUsagesByVehicleIdentifier(vehicleIdentifier);
        var usagesResponse = mapper.Map<IEnumerable<Usage>, IEnumerable<UsageResponseDTO>>(usages);
        return Ok(usagesResponse);
    }
}