using learning_ms.Web.Domain.Enums.UserRole;
using System.Text.Json;
using System.Text.Json.Serialization;
using learning_ms.Web.Infrastructure.ConfigurationExtensions.UserRoleNameConverter;

public class UserRoleJsonConverter : JsonConverter<UserRole>
{
  public override UserRole Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    var value = reader.GetString()
                ?? throw new JsonException("Role value cannot be null.");
    return UserRoleNameConverter.FromUpperSnakeCase(value);
  }

  public override void Write(Utf8JsonWriter writer, UserRole value, JsonSerializerOptions options)
  {
    writer.WriteStringValue(UserRoleNameConverter.ToUpperSnakeCase(value));
  }
}
