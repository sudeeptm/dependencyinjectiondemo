namespace DependencyInjectionDemo.Services
{
    public interface ITransientService
    {
        string GetOperationId();
    }

    public interface IScopedService
    {
        string GetOperationId();
    }

    public interface ISingletonService
    {
        string GetOperationId();
    }

    public class TransientService : ITransientService
    {
        private readonly string _operationId;

        public TransientService()
        {
            _operationId = Guid.NewGuid().ToString();
        }

        public string GetOperationId() => _operationId;
    }

    public class ScopedService : IScopedService
    {
        private readonly string _operationId;

        public ScopedService()
        {
            _operationId = Guid.NewGuid().ToString();
        }

        public string GetOperationId() => _operationId;
    }

    public class SingletonService : ISingletonService
    {
        private readonly string _operationId;

        public SingletonService()
        {
            _operationId = Guid.NewGuid().ToString();
        }

        public string GetOperationId() => _operationId;
    }
}
