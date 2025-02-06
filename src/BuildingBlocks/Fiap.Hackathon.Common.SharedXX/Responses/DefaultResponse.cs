namespace Fiap.Hackathon.Common.Shared.Responses
{
    public class DefaultResponse
    {
        public DefaultResponse()
        {
        }
        public DefaultResponse(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public DefaultResponse(bool isSuccess, string message, object data)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }

        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
    }
}
