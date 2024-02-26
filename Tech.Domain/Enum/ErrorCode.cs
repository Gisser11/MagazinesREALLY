namespace Tech.Domain.Enum;

public enum ErrorCode
{
    ReportsNotFound = 0,
    ReportNotFound = 1,
    ReportAlreadyExists = 2,
    UserNotFound = 11,
    InternalServerError = 10,
    UserAlreadyExists = 12,
    PasswordIsWrong = 13,
    
    InvalidClientRequest = 20,
    PasswordNotEquals = 21
    
}