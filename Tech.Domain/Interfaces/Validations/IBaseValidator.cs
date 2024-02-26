using Tech.Domain.Result;

namespace Tech.Domain.Interfaces.Validations;

public interface IBaseValidator<in T> where T : class
{
    BaseResult ValidateOnNull(T model);
    
    
}