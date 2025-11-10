using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CashFlow.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    [HttpGet("excel")]

    public async Task<IActionResult> GetExcel()
    {
        byte[] file = new byte[1];


        //Octet tipo generico de arquivo
        return File(file, MediaTypeNames.Application.Octet, "report.xlsx");
    }
}
