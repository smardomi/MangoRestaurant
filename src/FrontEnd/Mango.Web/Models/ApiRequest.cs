namespace Mango.Web.Models
{
    public class ApiRequest
    {
        public ApiUtil.ApiType ApiType { get; set; } = ApiUtil.ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public string? AccessToken { get; set; }
    }
}
