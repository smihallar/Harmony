using Harmony.Services.Base;

namespace Harmony.DTOs
{
    public class ReturnResponse<T> : ReturnResponse
    {
        public T? Data { get; set; }
    }
}
