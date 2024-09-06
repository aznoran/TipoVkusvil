using System.ComponentModel.DataAnnotations;

namespace TipoVkusvil.Contracts.Users;

public record LoginUserRequest(
    [Required] string Email,
    [Required] string Password);