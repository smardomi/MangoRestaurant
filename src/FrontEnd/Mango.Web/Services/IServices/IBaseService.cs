using Mango.Web.Models;

namespace Mango.Web.Services.IServices
{
    public interface IBaseService : IDisposable
    {
        ApiResult resultModel { get; set; }
        Task<T> SendAsync<T>(ApiRequest request);
    }
}
