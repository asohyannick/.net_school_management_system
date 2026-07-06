namespace learning_ms.Web.Infrastructure.ConfigurationExtensions;
using System.Text.RegularExpressions;
public static class ConfigurationExtensions
{
  private static readonly Regex PlaceholderRegex = new(
    @"\$\{(?<name>[A-Za-z0-9_]+)\}",
    RegexOptions.Compiled
  );

  public static IConfiguration ResolveEnvironmentVariables(this IConfiguration configuration)
  {
    foreach (var item in configuration.AsEnumerable().ToList())
    {
      if (string.IsNullOrWhiteSpace(item.Value))
        continue;

      var resolved = PlaceholderRegex.Replace(
        item.Value,
        match =>
        {
          var variableName = match.Groups["name"].Value;

          return Environment.GetEnvironmentVariable(variableName) ?? match.Value;
        }
      );

      configuration[item.Key] = resolved;
    }

    return configuration;
  }
}
