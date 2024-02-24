using Tech.Domain.Entity;
using Tech.Domain.Enum;
using Tech.Domain.Interfaces.Validations;
using Tech.Domain.Result;

namespace Tech.Application.Validations;

public class ReportValidator : IReportValidator
{
    public BaseResult ValidateOnNull(Article model)
    {
        if (model == null)
        {
            return new BaseResult()
            {
                ErrorMessage = ErrorMessage.ReportNotFound,
                ErrorCode = (int)ErrorCode.ReportNotFound
            };
        }

        return new BaseResult();
    }

    public BaseResult CreateValidator(Article article, User user)
    {
        if (article != null)
        {
            return new BaseResult()
            {
                ErrorMessage = ErrorMessage.ReportAlreadyExists,
                ErrorCode = (int)ErrorCode.ReportAlreadyExists
            };
        }

        if (user == null)
        {
            return new BaseResult()
            {
                ErrorMessage = ErrorMessage.UserNotFound,
                ErrorCode = (int) ErrorCode.UserNotFound
            };
        }

        return new BaseResult();
    }
}