using System.Text.Json;

namespace Base;

public class ErrorDetail
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string? TraceId { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
