using learning_ms.Web.Domain.Enums.UserRole;
using learning_ms.Web.Infrastructure.ConfigurationExtensions.UserRoleNameConverter;
namespace learning_ms.Web.Infrastructure.Persistence.configurations.UserRoleUpperSnakeCaseConverter;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
public class UserRoleUpperSnakeCaseConverter : ValueConverter<UserRole, string>
{
  public UserRoleUpperSnakeCaseConverter()
    : base(
      role => ToUpperSnakeCase(role.ToString()),
      value => UserRoleNameConverter.FromUpperSnakeCase(value))
  {
  }

  private static string ToUpperSnakeCase(string pascalCase)
  {
    return Regex.Replace(pascalCase, "(?<!^)([A-Z])", "_$1").ToUpperInvariant();
  }

  private static string ToPascalCase(string upperSnakeCase)
  {
    return string.Concat(
      upperSnakeCase
        .Split('_', StringSplitOptions.RemoveEmptyEntries)
        .Select(part => char.ToUpperInvariant(part[0]) + part[1..].ToLowerInvariant()));
  }
}
