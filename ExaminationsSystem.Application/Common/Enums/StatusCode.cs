using System.Net;

namespace ExaminationsSystem.Application.Common.Enums
{
    public enum StatusCode
    {
        Ok = 200,
        ValidationError = 400,
        Unauthorized = 401,
        NotFound = 404
    }
}
