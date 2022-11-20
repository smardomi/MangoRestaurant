using Mango.Web.Models;

namespace Mango.Web.Services.IServices
{
    public interface IBaseService : IDisposable
    {
        Task<T> SendAsync<T>(ApiRequest request);
    }
}
