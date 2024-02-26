using AutoMapper;
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
    private readonly IBaseRepository<Author> _authorRepository;
    private readonly IReportValidator _reportValidator;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    public ArticleService(IBaseRepository<Article> reportRepository, ILogger logger, IBaseRepository<User> userRepository, 
        IReportValidator reportValidator, IMapper mapper, IBaseRepository<Author> authorRepository)
    {
        _reportRepository = reportRepository;
        _logger = logger;
        _userRepository = userRepository;
        _reportValidator = reportValidator;
        _mapper = mapper;
        _authorRepository = authorRepository;
    }

    public async Task<CollectionResult<ReportDto>> GetReportsAsync(long userId)
    {
        ReportDto[] articles;
        
        try
        {
            articles = await _reportRepository.GetAll()
                .Select(x => new ReportDto(x.Id, x.Name, x.CreatedAt.ToLongDateString()))
                .ToArrayAsync();
        }
        catch (Exception e)
        {
            _logger.Error(e, e.Message);
            return new CollectionResult<ReportDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCode.InternalServerError,
            };
        }

        // if (!reports.Any())
        // {
        //     _logger.Warning(ErrorMessage.ReportsNotFound, reports.Length);
        //     return new CollectionResult<ReportDto>()
        //     {
        //         ErrorMessage = ErrorMessage.ReportsNotFound,
        //         ErrorCode = (int)ErrorCode.ReportsNotFound,
        //     };
        // }

        return new CollectionResult<ReportDto>()
        {
            Data = articles,
            Count = articles.Length
        };
    }

    public Task<BaseResult<ReportDto>> GetReportByIdAsync(int id)
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

    public async Task<BaseResult<ReportDto>> CreateReportAsync(CreateReportDto createReportDto)
    {
        try
        {
            var user = await _authorRepository.GetAll().FirstOrDefaultAsync(x => x.Id == createReportDto.AuthorId);
            var report = await _reportRepository.GetAll().FirstOrDefaultAsync(x => x.Name == createReportDto.Name);
            var result = _reportValidator.CreateValidator(report, user);
            
            if (!result.IsSuccess)
            {
                return new BaseResult<ReportDto>()
                {
                    ErrorCode = result.ErrorCode,
                    ErrorMessage = result.ErrorMessage
                };
            }

            report = new Article()
            {
                Name = createReportDto.Name,
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = 1,
                AuthorId = 1,
            };

            await _reportRepository.CreateAsync(report);
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
    }
}