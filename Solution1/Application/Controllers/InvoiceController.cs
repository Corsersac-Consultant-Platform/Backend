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
public class InvoiceController(IMapper mapper, IInvoiceService invoiceService) : ControllerBase
{
    [HttpPost]
    [CustomAuthorize("ADMIN, TESTER")]
    public async Task<IActionResult> CreateInvoice([FromBody] InvoiceRequestDTO invoiceRequest)
    {
        var invoice = mapper.Map<InvoiceRequestDTO, Invoice>(invoiceRequest);
        await invoiceService.CreateInvoice(invoice);
        return Ok("Invoice created successfully");
    }
    
    [HttpPatch("update-status/{id}")]
    [CustomAuthorize("ADMIN, TESTER")]
    public async Task<IActionResult> UpdateInvoiceStatus(int id, [FromBody] int statusId)
    {
        await invoiceService.UpdateInvoiceStatus(statusId, id);
        return Ok("Invoice status updated successfully");
    }
    
    [HttpGet("range")]
    [CustomAuthorize("ADMIN, TESTER", "DEFAULT")]
    public async Task<IActionResult> GetInvoicesByRange([FromQuery] DateTime start, [FromQuery] DateTime end)
    {
        var invoices = await invoiceService.GetInvoicesByRange(start, end);
        var invoicesResponse = mapper.Map<IEnumerable<Invoice>, IEnumerable<InvoiceResponseDTO>>(invoices);
        return Ok(invoicesResponse);
    }
    
    [HttpGet("date")]
    [CustomAuthorize("ADMIN, TESTER", "DEFAULT")]
    public async Task<IActionResult> GetInvoicesByDate([FromQuery] DateTime date)
    {
        var invoices = await invoiceService.GetInvoicesByDate(date);
        var invoicesResponse = mapper.Map<IEnumerable<Invoice>, IEnumerable<InvoiceResponseDTO>>(invoices);
        return Ok(invoicesResponse);
    }
    
    [HttpGet("serie")]
    [CustomAuthorize("ADMIN, TESTER", "DEFAULT")]
    public async Task<IActionResult> GetInvoicesBySerie([FromQuery] string serie)
    {
        var invoices = await invoiceService.GetInvoicesBySerie(serie);
        var invoicesResponse = mapper.Map<IEnumerable<Invoice>, IEnumerable<InvoiceResponseDTO>>(invoices);
        return Ok(invoicesResponse);
    }
    
    
    
}