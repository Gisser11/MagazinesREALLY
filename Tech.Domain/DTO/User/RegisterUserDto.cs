namespace Tech.Domain.DTO.User;

public record RegisterUserDto(
    string Email,
    string Login, 
    string Password
    //string PasswordConfirm
    );