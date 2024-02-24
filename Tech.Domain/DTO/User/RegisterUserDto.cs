namespace Tech.Domain.DTO.User;

public record RegisterUserDto(string Login, string Password, string PasswordConfirm);