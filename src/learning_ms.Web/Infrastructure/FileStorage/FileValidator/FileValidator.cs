using learning_ms.Web.Application.Common.Settings.FileUploadSettings;
using learning_ms.Web.Domain.Exceptions.FileValidationException;
namespace learning_ms.Web.Infrastructure.FileStorage.FileValidator;
internal sealed class FileValidator
{
  private static readonly (string Mime, int Offset, byte[] Signature)[] Signatures =
  [
      ("application/pdf",  0, [0x25, 0x50, 0x44, 0x46]),          
        ("image/png",        0, [0x89, 0x50, 0x4E, 0x47]),          
        ("image/jpeg",       0, [0xFF, 0xD8, 0xFF]),                 
        ("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",           0, [0x50, 0x4B, 0x03, 0x04]),
        ("application/vnd.openxmlformats-officedocument.wordprocessingml.document",     0, [0x50, 0x4B, 0x03, 0x04]),
        ("application/vnd.ms-excel",  0, [0xD0, 0xCF, 0x11, 0xE0]), 
        ("application/msword",        0, [0xD0, 0xCF, 0x11, 0xE0]), 
        ("text/csv",         0, []),                                 
        ("video/mp4",        4, [0x66, 0x74, 0x79, 0x70]),          
        ("video/x-msvideo",  0, [0x52, 0x49, 0x46, 0x46]),          
        ("video/x-matroska", 0, [0x1A, 0x45, 0xDF, 0xA3]),          
    ];

  private readonly FileUploadSettings _settings;

  public FileValidator(FileUploadSettings settings)
  {
    _settings = settings;
  }

  public async Task ValidateAsync(IFormFile file, CancellationToken ct = default)
  {
    if (file is null || file.Length == 0)
      throw new FileValidationException("No file was provided or the file is empty.");

    ValidateSize(file);
    ValidateExtensionAndMime(file);
    await ValidateMagicBytesAsync(file, ct);
  }
  private void ValidateSize(IFormFile file)
  {
    if (file.Length > _settings.MaxFileSizeBytes)
    {
      var maxMb = _settings.MaxFileSizeBytes / 1_048_576.0;
      var fileMb = file.Length / 1_048_576.0;
      throw new FileValidationException(
          $"File size {fileMb:F2} MB exceeds the maximum allowed size of {maxMb:F2} MB.");
    }
  }

  private void ValidateExtensionAndMime(IFormFile file)
  {
    var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
    if (string.IsNullOrWhiteSpace(extension))
      throw new FileValidationException("The file has no extension.");

    var declaredMime = file.ContentType.ToLowerInvariant();

    if (!_settings.AllowedTypes.TryGetValue(declaredMime, out var allowedExtensions))
      throw new FileValidationException(
          $"Content type '{declaredMime}' is not permitted.");

    if (!allowedExtensions.Contains(extension))
      throw new FileValidationException(
          $"Extension '{extension}' does not match the declared content type '{declaredMime}'.");
  }

  private static async Task ValidateMagicBytesAsync(IFormFile file, CancellationToken ct)
  {
    var declaredMime = file.ContentType.ToLowerInvariant();

    var match = Signatures.FirstOrDefault(s => s.Mime == declaredMime);
    if (match == default || match.Signature.Length == 0)
      return;

    var bytesToRead = match.Offset + match.Signature.Length;
    var buffer = new byte[bytesToRead];

    await using var stream = file.OpenReadStream();
    var bytesRead = await stream.ReadAsync(buffer.AsMemory(0, bytesToRead), ct);

    if (bytesRead < bytesToRead)
      throw new FileValidationException("File is too small to be a valid document.");

    var actualBytes = buffer.Skip(match.Offset).Take(match.Signature.Length).ToArray();
    if (!actualBytes.SequenceEqual(match.Signature))
      throw new FileValidationException(
          $"File content does not match the declared type '{declaredMime}'. " +
          "The file may be corrupt or its extension has been spoofed.");
  }
}