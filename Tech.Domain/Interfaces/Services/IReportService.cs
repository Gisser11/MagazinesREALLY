using Tech.Domain.DTO;
using Tech.Domain.DTO.ReportDTO;
using Tech.Domain.Result;

namespace Tech.Domain.Interfaces.Services;

/// <summary>
/// Сервис, отвечающий за работу доменной части отчета (Article)
/// </summary>
public interface IReportService
{
    /// <summary>
    /// Получение всех отчетов пользователя
    /// </summary>
    /// <param name="userId">Id Пользователя</param>
    /// <returns></returns>
    Task<CollectionResult<ReportDto>> GetReportsAsync(long userId);
    
    /// <summary>
    /// Получение отчетов по ID 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BaseResult<ReportDto>> GetReportByIdAsync(int id);

    /// <summary>
    /// Создание отчета с базовыми параметрами
    /// </summary>
    /// <param name="createReportDto"></param>
    /// <returns></returns>
    Task<BaseResult<ReportDto>> CreateReportAsync(CreateReportDto createReportDto);

    Task<BaseResult<ReportDto>> DeleteReportAsync(long id);

    Task<BaseResult<ReportDto>> UpdateReportAsync(UpdateReportDto dto);
}