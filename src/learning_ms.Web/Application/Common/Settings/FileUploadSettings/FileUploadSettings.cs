namespace learning_ms.Web.Application.Common.Settings.FileUploadSettings;
public class FileUploadSettings
{
  public long MaxFileSizeBytes { get; set; } = 52_428_800; // 50MB
  public Dictionary<string, string[]> AllowedTypes { get; set; } = new()
  {
    ["application/pdf"] = [".pdf"],
    ["application/vnd.ms-excel"] = [".xls"],
    ["application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"] = [".xlsx"],
    ["application/msword"] = [".doc"],
    ["application/vnd.openxmlformats-officedocument.wordprocessingml.document"] = [".docx"],
    ["text/csv"] = [".csv"],
    ["image/png"] = [".png"],
    ["image/jpeg"] = [".jpg", ".jpeg"],
    ["video/mp4"] = [".mp4"],
    ["video/x-msvideo"] = [".avi"],
    ["video/x-matroska"] = [".mkv"],
  };
}
