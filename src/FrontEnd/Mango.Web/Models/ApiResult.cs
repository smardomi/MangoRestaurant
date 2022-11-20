using Microsoft.AspNetCore.Mvc;

namespace Mango.Web.Models
{
    public class ApiResult
    {
        public bool IsSuccess { get; set; }
        public ApiResultStatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }

    public class ApiResult<TData> : ApiResult
    {
        public TData Data { get; set; }
    }



    public enum ApiResultStatusCode
    {
        Success = 0,
        ServerError = 1,
        BadRequest = 2,
        NotFound = 3,
        ListEmpty = 4,
        LogicError = 5,
        UnAuthorized = 6,
        Conflict = 7
    }
}
