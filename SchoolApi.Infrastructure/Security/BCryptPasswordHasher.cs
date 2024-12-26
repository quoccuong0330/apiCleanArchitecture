using SchoolApi.Application.Services;
using BCrypt.Net;
namespace SchoolApi.Infrastructure.Security;

public class BCryptPasswordHasher : IPasswordHasher {
    public string HashPassword(string password) {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string hashedPassword) {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}