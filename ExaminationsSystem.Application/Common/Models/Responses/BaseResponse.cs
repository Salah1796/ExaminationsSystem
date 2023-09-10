
using ExaminationsSystem.Application.Common.Enums;
using System.Collections.Generic;

namespace ExaminationsSystem.Application.Common.Responses
{
    public class BaseResponse<T>
    {
        public BaseResponse()
        {
            Success = true;
        }
        public BaseResponse(string message = null)
        {
            Success = true;
            Message = message;
        }

        public BaseResponse(string message, bool success)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; set; } = true;
        public string Message { get; set; } = "";
        public List<string> ValidationErrors { get; set; }
        public StatusCode Code { get; set; } = StatusCode.Ok;
        public T? Data { get; set; }
    }
}
