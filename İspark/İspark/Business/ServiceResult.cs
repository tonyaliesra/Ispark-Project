using İspark.Model;

namespace İspark.Business
{
    public class ServiceResult<T>
    {
        public bool Success { get; private set; }
        public T Data { get; private set; }
        public ErrorDetails Error { get; private set; }

        private ServiceResult() { }

        public static ServiceResult<T> Succeed(T data)
        {
            return new ServiceResult<T> { Success = true, Data = data };
        }

        public static ServiceResult<T> Fail(int statusCode, int errorCode)
        {
            return new ServiceResult<T>
            {
                Success = false,
                Error = new ErrorDetails(statusCode, errorCode)
            };
        }
    }
}