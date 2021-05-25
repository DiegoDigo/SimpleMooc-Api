namespace SimpleMooc.Shared.Entities
{
    public class BaseResponse
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public object Content { get; private set; }

        public BaseResponse(bool success, string message, object content)
        {
            Success = success;
            Message = message;
            Content = content;
        }
    }
}