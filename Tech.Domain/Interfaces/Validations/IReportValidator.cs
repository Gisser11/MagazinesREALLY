using Tech.Domain.DTO;
using Tech.Domain.Entity;
using Tech.Domain.Result;

namespace Tech.Domain.Interfaces.Validations;

public interface IReportValidator : IBaseValidator<Article>
{
    /// <summary>
    /// Проверка наличия отчета у пользователя
    /// </summary>
    /// <param name="article></param>
    /// <param name="user"></param>
    /// <returns></returns>
    BaseResult CreateValidator(Article article, Author author);
    
}