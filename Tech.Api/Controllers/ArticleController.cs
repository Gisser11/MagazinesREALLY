using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tech.Domain.DTO;
using Tech.Domain.DTO.ReportDTO;
using Tech.Domain.Interfaces.Services;
using Tech.Domain.Result;

namespace Tech.Api.Controllers;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ArticleController : ControllerBase
{
     private readonly IReportService _reportService;

     public ArticleController(IReportService reportService)
     {
          _reportService = reportService;
     }
     
     [HttpGet("reports")]
     [ProducesResponseType(StatusCodes.Status200OK)]
     [ProducesResponseType(StatusCodes.Status400BadRequest)]
     public async Task<ActionResult<ReportDto>> GetUserReports()
     {
          var userId = new Guid("d4011776-c512-4b75-9877-fe981c05bbbc");
          var response = await _reportService.GetReportsAsync(userId);
          if (response.IsSuccess)
          {
               return Ok(response);
          }

          return BadRequest(response);
     }
    
     /*[HttpGet]
     [ProducesResponseType(StatusCodes.Status200OK)]
     [ProducesResponseType(StatusCodes.Status400BadRequest)]
      public async Task<ActionResult<BaseResult<ReportDto>>> GetReport(int id)
     {
          var response = await _reportService.GetReportByIdAsync(id);
          if (response.IsSuccess)
          {
               return Ok(response);
          }

          return BadRequest(response);
     }
      
     [HttpDelete("{id}")]
     [ProducesResponseType(StatusCodes.Status200OK)]
     [ProducesResponseType(StatusCodes.Status400BadRequest)]
     public async Task<ActionResult<BaseResult<ReportDto>>> Delete(int id)
     {
          var response = await _reportService.DeleteReportAsync(id);
          if (response.IsSuccess)
          {
               return Ok(response);
          }

          return BadRequest(response);
     }
     
     [HttpPost]
     [ProducesResponseType(StatusCodes.Status200OK)]
     [ProducesResponseType(StatusCodes.Status400BadRequest)]
     public async Task<ActionResult<BaseResult<ReportDto>>> Create([FromBody] CreateReportDto dto)
     {
          var response = await _reportService.CreateReportAsync(dto);
          if (response.IsSuccess)
          {
               return Ok(response);
          }

          return BadRequest(response);
     }
     
     [HttpPut]
     [ProducesResponseType(StatusCodes.Status200OK)]
     [ProducesResponseType(StatusCodes.Status400BadRequest)]
     public async Task<ActionResult<BaseResult<ReportDto>>> Update([FromBody] UpdateReportDto dto)
     {
          var response = await _reportService.UpdateReportAsync(dto);
          if (response.IsSuccess)
          {
               return Ok(response);
          }

          return BadRequest(response);
     }*/
}