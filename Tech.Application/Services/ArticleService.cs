using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Tech.Domain.DTO;
using Tech.Domain.DTO.ReportDTO;
using Tech.Domain.Entity;
using Tech.Domain.Enum;
using Tech.Domain.Interfaces.Repostories;
using Tech.Domain.Interfaces.Services;
using Tech.Domain.Interfaces.Validations;
using Tech.Domain.Result;

namespace Tech.Application.Services;

public class ArticleService : IReportService
{
    private readonly IBaseRepository<Article> _reportRepository;
    private readonly IBaseRepository<User> _userRepository;
    private readonly UserManager<User> _userManager;
    public ArticleService(IBaseRepository<Article> reportRepository, IBaseRepository<User> userRepository,
         UserManager<User> userManager)
    {
        _reportRepository = reportRepository;
        _userRepository = userRepository;
        _userManager = userManager;
    }

    public async Task<CollectionResult<ReportDto>> GetReportsAsync(string email)
    {
        var identityUser = await _userManager.FindByEmailAsync(email);
        
        if (identityUser is null)
        {
            throw new UnauthorizedAccessException();
        } 
        
        var articles = await _reportRepository.GetAll()
            .Where(x => x.UserId == identityUser.Id)
            .Select(x => new ReportDto(x.Id, x.Name))
            .ToArrayAsync();
        
        return new CollectionResult<ReportDto>()
        {
            Data = articles,
            Count = articles.Length
        };
    }
    public async Task<BaseResult<ReportDto>> CreateReportAsync(CreateReportDto createReportDto, string email)
    {
        var identityUser = await _userManager.FindByEmailAsync(email);
        
        if (identityUser is null)
        {
            throw new UnauthorizedAccessException();
        } 
        //var report = await _reportRepository.GetAll().FirstOrDefaultAsync(x => x.Name == createReportDto.Name);
        
        /*var result = _reportValidator.CreateValidator(report, user);
        if (!result.IsSuccess)
        {
            return new BaseResult<ReportDto>()
            {
                ErrorCode = result.ErrorCode,
                ErrorMessage = result.ErrorMessage
            };
        }
        */

        var report = new Article()
        {
            Name = createReportDto.Name,
            UserId = identityUser.Id
        };

        await _reportRepository.CreateAsync(report);

        return new BaseResult<ReportDto>()
        {
            Data = null
        };

    }

    /*public Task<BaseResult<ReportDto>> GetReportByIdAsync(int id)
    {
        ReportDto? report;
        try
        {
            report = _reportRepository.GetAll()
                .AsEnumerable()
                .Select(x => new ReportDto(x.Id, x.Name, x.CreatedAt.ToLongDateString()))
                .FirstOrDefault(x => x.Id == id);

        }
        catch (Exception e)
        {
            _logger.Error(e, e.Message);
            return Task.FromResult(new BaseResult<ReportDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCode.InternalServerError,
            });
        }

        if (report == null)
        {
            _logger.Warning($"Отчет с {id} не найден", id);
            return Task.FromResult(new BaseResult<ReportDto>()
            {
                ErrorMessage = ErrorMessage.ReportNotFound,
                ErrorCode = (int)ErrorCode.InternalServerError,
            });
        }
        return Task.FromResult(new BaseResult<ReportDto>()
        {
            Data = report
        });

    }



    public async Task<BaseResult<ReportDto>> DeleteReportAsync(long id)
    {
        try
        {
            var report = await _reportRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            var result = _reportValidator.ValidateOnNull(report);

            if (!result.IsSuccess)
            {
                return new BaseResult<ReportDto>()
                {
                    ErrorCode = result.ErrorCode,
                    ErrorMessage = result.ErrorMessage
                };
            }

            await _reportRepository.RemoveAsync(report);

            return new BaseResult<ReportDto>()
            {
                Data = _mapper.Map<ReportDto>(report)
            };
        }
        catch (Exception e)
        {
            _logger.Error(e, e.Message);
            return new BaseResult<ReportDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCode.InternalServerError,
            };
        }
    }

    public async Task<BaseResult<ReportDto>> UpdateReportAsync(UpdateReportDto dto)
    {
        try
        {
            var article = await _reportRepository.GetAll().FirstOrDefaultAsync(x => x.Id == dto.Id);

            var result = _reportValidator.ValidateOnNull(article);
            if (!result.IsSuccess)
            {
                return new BaseResult<ReportDto>()
                {
                    ErrorCode = result.ErrorCode,
                    ErrorMessage = result.ErrorMessage
                };
            }

            article.Name = dto.Name;

            await _reportRepository.UpdateAsync(article);

            return new BaseResult<ReportDto>()
            {
                Data = _mapper.Map<ReportDto>(article)
            };

        }

        catch (Exception e)
        {
            _logger.Error(e, e.Message);
            return new BaseResult<ReportDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCode.InternalServerError,
            };
        }
    }*/
}