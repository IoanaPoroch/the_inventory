namespace Services.Models
{
    public enum ServiceError { NotFound, DependencyNotFound }

    public class ServiceResult
    {
        public bool IsSuccess { get; protected set; }
        public ServiceError? Error { get; protected set; }
        public string? ErrorMessage { get; protected set; }

        public static ServiceResult Ok() => new() { IsSuccess = true };
        public static ServiceResult Fail(ServiceError error, string message) => new() { IsSuccess = false, Error = error, ErrorMessage = message };
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T? Value { get; private set; }

        public static ServiceResult<T> Ok(T value) => new() { IsSuccess = true, Value = value };
        public static new ServiceResult<T> Fail(ServiceError error, string message) => new() { IsSuccess = false, Error = error, ErrorMessage = message };
    }
}
