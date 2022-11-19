namespace Mange.Services.ProductAPI.Filters
{
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
