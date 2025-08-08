using System.Text.Json;

namespace İspark.Model
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public int ErrorCode { get; set; }
        public string Message { get; set; }

        public ErrorDetails(int statusCode, int errorCode)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
            
            Message = ErrorMessages.Messages.GetValueOrDefault(errorCode, "An unknown error occurred.");
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
    }
}