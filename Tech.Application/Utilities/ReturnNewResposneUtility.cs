using Tech.Domain.Enum;
using Tech.Domain.Result;

namespace Tech.Application.Utilities;

public static class ReturnNewResposneUtility<T>
{
    public static BaseResult<T> CreateFailedResponse(string errorMessage, int errorCode)
    {
        return new BaseResult<T>
        {
            ErrorMessage = errorMessage,
            ErrorCode = errorCode
        };
    }
    
    public static BaseResult<T> CreateSuccessResponse(T data = default!)
    {
        return new BaseResult<T>
        {
            Data = data
        };
    }
}