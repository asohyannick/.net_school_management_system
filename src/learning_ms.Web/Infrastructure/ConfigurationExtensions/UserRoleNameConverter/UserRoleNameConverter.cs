using learning_ms.Web.Domain.Enums.UserRole;
namespace learning_ms.Web.Infrastructure.ConfigurationExtensions.UserRoleNameConverter;
using System.Text.RegularExpressions;
public static class UserRoleNameConverter
{
  public static string ToUpperSnakeCase(UserRole role) =>
    ToUpperSnakeCase(role.ToString());

  public static string ToUpperSnakeCase(string pascalCase) =>
    Regex.Replace(pascalCase, "(?<!^)([A-Z])", "_$1").ToUpperInvariant();

  public static UserRole FromUpperSnakeCase(string upperSnakeCase)
  {
    var pascalCase = string.Concat(
      upperSnakeCase
        .Split('_', StringSplitOptions.RemoveEmptyEntries)
        .Select(part => char.ToUpperInvariant(part[0]) + part[1..].ToLowerInvariant()));

    return Enum.Parse<UserRole>(pascalCase);
  }
}
