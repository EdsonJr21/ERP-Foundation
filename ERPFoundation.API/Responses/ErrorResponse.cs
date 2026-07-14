namespace ERPFoundation.API.Responses;

public class ErrorResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? Detail { get; set; }
    public Dictionary<string, string[]>? Errors { get; set; }
}